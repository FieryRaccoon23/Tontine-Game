using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;


public class SignUp : MonoBehaviour
{
    //Private Members
    private int errorValue = -1;

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

    //Private functions
    private bool checkIfValidEmail(string email)
    {
        if (email == null)
        {
            return false;
        }

        int atSymbolPosition  = email.IndexOf("@");
        //checks if the @ symbol is not found, at the start or end of the address.
        //That is, the following are not valid:
        //somewhere.com
        //@somewhere.com
        //someone@
        //someone@somewhere.com@
        if (atSymbolPosition < 1 || email.EndsWith("@"))
        {
            return false;
        }

        var periodSymbolPosition = email.IndexOf(".", atSymbolPosition);

        //checks if the period is not found, and that it's not beside the @ symbol, and it's not at the end.  
        //That is, the following are not valid:
        //someone@somewhere
        //someone@.somewhere.com
        //someone@somewhere.
        //someone@somewhere.co.
        if (periodSymbolPosition > (atSymbolPosition + 1) || !email.EndsWith("."))
        {
            return true;
        }

        return false;
    }

    private void ParseUserSignUp(string email, string password)
    {
        var user = new ParseUser()
        {
            Username = email,
            Password = password,
            Email = email
        };

        try
        {
            user.SignUpAsync().ContinueWith(t => 
            {
                if (t.IsFaulted)
                {
                    // Errors from Parse Cloud and network interactions
                    using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ParseException error = (ParseException)enumerator.Current;
                            Debug.LogError("Error Message:" + error.Message + "----" + error.Code.ToString());

                            if (error.Code.ToString() == "UsernameTaken")
                            {
                                errorValue = (int)DebugGameManager.ErrorMessagesCodes.EmailAlreadyTaken;
                            }
                        }
                    }
                }
                else
                {
                    ParseObject gameState = new ParseObject("GameState");
                    gameState["Username"] = email;
                    gameState["InGameState"] = false;
                    Task saveTask = gameState.SaveAsync();
                    errorValue = (int)DebugGameManager.ErrorMessagesCodes.Success;
                }
            });
        }
        catch (System.InvalidOperationException e)
        {
            Debug.LogError("Exception Message:" + e.Message);
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.UnknownError;
        }
        
    }

    //Public functions
    public void SignUpUser(string email, string password, string retypedPassword)
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

        //Check if retypedPassword field empty

        if (retypedPassword == null || retypedPassword == "")
        {
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.RetypedPasswordEmpty;
            return;
        }

        //Check if email is valid
        if(!checkIfValidEmail(email))
        {
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.InvalidEmail;
            return;
        }

        //Check if password is same in password field and retypedPassword field
        if (password != retypedPassword)
        {
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.PasswordNotEqualRetypedPassword;
            return;
        }

        //Check if passoword length is too small
        if (password.Length <= DebugGameManager.minimumPasswordLength)
        {
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.PasswordLenghtSmall;
            return;
        }

        //Perform signup with Parse and check parse related errors
        ParseUserSignUp(email, password);
    }
}
