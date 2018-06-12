using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class SignIn : MonoBehaviour
{
    //Private Members
    private int errorValue = -1;

    private bool inGameState = false;

    //Accessors
    public int ErrorValue
    {
        get
        {
            return errorValue;
        }
        set
        {
            errorValue = value;
        }
    }

    public bool InGameState
    {
        get
        {
            return inGameState;
        }
    }

    private void ParseCheckUserGameState(string email)
    {
        //var query = ParseObject.GetQuery("Test")
        //.WhereEqualTo("username", email);
        //query.FindAsync().ContinueWith(t =>
        //{
        //    IEnumerable<ParseObject> results = t.Result;
        //    List<ParseObject> resultsList = results.ToList();
        //    inGameState = resultsList[0].Get<bool>("InGame");
        //    errorValue = (int)DebugGameManager.ErrorMessagesCodes.Success;
        //});

        var query = ParseObject.GetQuery("GameState")
        .WhereEqualTo("Username", email);
        query.FirstAsync().ContinueWith(t =>
        {
            ParseObject result = t.Result;
            inGameState = result.Get<bool>("InGameState");
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.Success;
        });
        
    }

    private void ParseUserSignIn(string email, string password)
    {
        ParseUser.LogInAsync(email, password).ContinueWith(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                errorValue = (int)DebugGameManager.ErrorMessagesCodes.LoginFailed;
            }
            else
            {
                ParseCheckUserGameState(email);
                //errorValue = (int)DebugGameManager.ErrorMessagesCodes.Success;
            }
        });
    }

    public void SignInUser(string email, string password)
    {
        //Check if email field empty
        if (email == null || email == "")
        {
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.EmailEmpty;
            return;
        }

        //Check if password field empty
        if (password == null || password == "")
        {
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.PasswordEmpty;
            return;
        }

        //Login User - Check parse related errors
        ParseUserSignIn(email, password);
    }
}
