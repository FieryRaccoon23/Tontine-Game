using System;
using UnityEngine;
using System.Collections;

public class UITreeNode : MonoBehaviour
{
    //List of IDs
    protected const Int16 StartScreenID              = 0;
    protected const Int16 NA_IDRequiredScreenID      = 1;
    protected const Int16 InviteScreenIDScreenID     = 2;
    protected const Int16 NewPlayerScreenID          = 3;
    protected const Int16 NA_PlayersNotFoundScreenID = 4;
    protected const Int16 Waiting_LeaderScreenID     = 5;
    protected const Int16 Waiting_CompetitorScreenID = 6;
    protected const Int16 InGameScreenID             = 7;
    protected const Int16 Waiting_EndScreenID        = 8;

    protected Int16 selfID;

    //Private Functions
    void Start()
    {
        if (UIGameManager.uiGameManagerInstance == null)
        {
            Debug.LogError("UI Game Manager not found.");
        }
    }

    //Protected Functions
    protected void PerformTransition(Int16 toID)
    {
        if (UIGameManager.uiGameManagerInstance != null)
        {
            UIGameManager.uiGameManagerInstance.Transition(selfID, toID);
        }
        else
        {
            Debug.LogError("UI Game Manager Instance Not Found.");
        }
    }
}
