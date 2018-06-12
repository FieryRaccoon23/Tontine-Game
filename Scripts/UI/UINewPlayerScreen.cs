using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UINewPlayerScreen : UITreeNode
{
    [SerializeField]
    private InputField email = null;

    [SerializeField]
    private InputField password = null;

    [SerializeField]
    private InputField retypedPassword = null;

    [SerializeField]
    private Text errorText = null;

    [SerializeField]
    private SignUp signUp = null;

    private bool isInAcceptTransition = false;

    void Start()
    {
        selfID = NewPlayerScreenID;
    }

    void Update()
    {
        if (isInAcceptTransition && signUp.ErrorValue > -1)
        {
            EndAcceptTransition();
        }
    }
    
    public void BackTransition()
    {
        PerformTransition(StartScreenID);
    }

    public void BeginAcceptTransition()
    {
        isInAcceptTransition = true;
        ProcessAcceptTransition();
    }

    private void ProcessAcceptTransition()
    {
        signUp.SignUpUser(email.text, password.text, retypedPassword.text);
    }

    private void EndAcceptTransition()
    {
        isInAcceptTransition = false;
        int value = signUp.ErrorValue;
        signUp.ErrorValue = -1;

        if (value > 0)
        {
            errorText.text = DebugGameManager.debugGameManagerInstance.errorMessages[value];
        }

        else
        {
            PerformTransition(StartScreenID);
        }
    }
}
