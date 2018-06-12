using UnityEngine;
using System.Collections;

public class DebugGameManager : MonoBehaviour
{
    //Public Members
    public enum ErrorMessagesCodes {
                    Success,
                    EmailEmpty,
                    PasswordEmpty,
                    RetypedPasswordEmpty,
                    InvalidEmail,
                    PasswordNotEqualRetypedPassword,
                    PasswordLenghtSmall,
                    EmailAlreadyTaken,
                    NetworkError,
                    UnknownError,
                    LoginFailed,
                    AllPlayersEmpty,
                    DuplicatePlayers
                   };

    public enum InGameStateCodes {
                    Yes,
                    No
                    };

    public const int minimumPasswordLength = 8;

    public static DebugGameManager debugGameManagerInstance { get; private set; }

    [SerializeField]
    public string[] errorMessages = null;

    //Private Functions
    private void Awake()
    {
        if (debugGameManagerInstance == null)
        {
            debugGameManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
