using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UIInviteScreen : UITreeNode
{
    [SerializeField]
    private InputField[] playerFields = null;
    
    [SerializeField]
    private Text errorText = null;

    [SerializeField]
    private InvitePlayers invitePlayers = null;

    [SerializeField]
    private PlayersList playersList = null;

    private bool isInBeginTransition = false;

    private bool shouldMoveToWaitScreen = false;

    void Start()
    {
        selfID = InviteScreenIDScreenID;
    }

    void Update()
    {
        if (isInBeginTransition && invitePlayers.ErrorValue > -1)
        {
            if(playersList.listOfPlayersUnavailable.Count == 0 &&
                playersList.listOfPlayersInGame.Count > 0)
            {
                shouldMoveToWaitScreen = true;
            }
            EndSendTransition();
        }
    }

    public void BeginSendTransition()
    {
        isInBeginTransition = true;
        playersList.listOfPlayersInGame.Clear();
        playersList.listOfPlayersUnavailable.Clear();
        ProcessSendTransition();
    }

    private void ProcessSendTransition()
    {
        List<string> listOfPlayers = new List<string>();
        for (Int16 i = 0; i < playerFields.Length; ++i)
        {
            listOfPlayers.Add(playerFields[i].text);
        }
        
        invitePlayers.InvitePlayersToSession(listOfPlayers);
    }

    private void EndSendTransition()
    {
        isInBeginTransition = false;
        int value = invitePlayers.ErrorValue;
        invitePlayers.ErrorValue = -1;
        
        if (value > 0)
        {
            errorText.text = DebugGameManager.debugGameManagerInstance.errorMessages[value];
        }

        else
        {
            if (shouldMoveToWaitScreen)
            {
                PerformTransition(Waiting_LeaderScreenID);
            }
            else
            {
                PerformTransition(NA_PlayersNotFoundScreenID);
            }
        }
        shouldMoveToWaitScreen = false;
    }
}
