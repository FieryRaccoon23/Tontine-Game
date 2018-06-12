using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStartScreen : UITreeNode
{
    [SerializeField]
    private InputField email = null;

    [SerializeField]
    private InputField password = null;

    [SerializeField]
    private Text errorText = null;

    [SerializeField]
    private SignIn signIn = null;

    private bool isInAcceptTransition = false;

    void Start()
    {
        selfID = StartScreenID;
    }

    void Update()
    {
        if (isInAcceptTransition && signIn.ErrorValue > -1)
        {
            EndLoginTransition();
        }
    }

    public void BeginLoginTransition()
    {
        isInAcceptTransition = true;
        ProcessLoginTransition();
    }

    private void ProcessLoginTransition()
    {
        //signIn.SignInUser(email.text, password.text);
        signIn.SignInUser("abs@gmail.com", "abcd12345");
    }

    private void EndLoginTransition()
    {
        isInAcceptTransition = false;
        int value = signIn.ErrorValue;
        signIn.ErrorValue = -1;

        if (value > 0)
        {
            errorText.text = DebugGameManager.debugGameManagerInstance.errorMessages[value];
        }

        else
        {
            if (signIn.InGameState)
            {
                PerformTransition(InGameScreenID);
            }
            else
            {
                PerformTransition(InviteScreenIDScreenID);
            }
        }
    }

    public void CreateAccountTransition()
    {
        PerformTransition(NewPlayerScreenID);
    }
}
