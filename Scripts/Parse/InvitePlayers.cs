using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;

public class InvitePlayers : MonoBehaviour
{
    //Private Members
    private int errorValue = -1;

    private List<int> allPlayersFoundResponse;

    [SerializeField]
    private PlayersList playersList = null;

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
    
    private void InviteOnePlayerToSession(string player)
    {
        var query = ParseObject.GetQuery("GameState")
        .WhereEqualTo("Username", player);
        query.FirstAsync().ContinueWith(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                playersList.listOfPlayersUnavailable.Add(player);
                //This success is only for error messages and not transition
                errorValue = (int)DebugGameManager.ErrorMessagesCodes.Success;
            }
            else
            {
                ParseObject result = t.Result;
                if (result.Get<bool>("InGameState"))
                {
                    playersList.listOfPlayersUnavailable.Add(player);
                }
                else
                {
                    playersList.listOfPlayersInGame.Add(player);
                }
                //This success is only for error messages and not transition
                errorValue = (int)DebugGameManager.ErrorMessagesCodes.Success;
            }

        });
    }

    private void ParseInvitePlayersToSession(List<string> players)
    {
        for (Int16 i = 0; i < players.Count; ++i)
        {
            InviteOnePlayerToSession(players[i]);
        }
    }

    public void InvitePlayersToSession(List<string> players)
    {
        //Check if all fields are empty
        bool areAllPlayersEmpty = true;
        for (Int16 i = 0; i < players.Count; ++i)
        {
            if (players[i] != "")
            {
                areAllPlayersEmpty = false;
            }
        }
        if (areAllPlayersEmpty)
        {
            errorValue = (int)DebugGameManager.ErrorMessagesCodes.AllPlayersEmpty;
            return;
        }

        //remove empty strings
        players.RemoveAll(p => string.IsNullOrEmpty(p));

        //Check if duplicates
        for (Int16 i = 0; i < players.Count; ++i)
        {
            Int16 occur = 0;
            for (Int16 j = 0; j < players.Count; ++j)
            {
                if(players[i] == players[j])
                {
                    if (occur > 0)
                    {
                        errorValue = (int)DebugGameManager.ErrorMessagesCodes.DuplicatePlayers;
                        return;
                    }
                    ++occur;
                }
            }
        }

            //Invite players and check if they are available
            ParseInvitePlayersToSession(players);
    }
}
