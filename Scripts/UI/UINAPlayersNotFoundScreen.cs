using UnityEngine;
using System.Collections;

public class UINAPlayersNotFoundScreen : UITreeNode
{
    //Private functions
    void Start()
    {
        selfID = NA_PlayersNotFoundScreenID;
    }

    //Public Functions
    public void OKTransition()
    {
        PerformTransition(InviteScreenIDScreenID);
    }
}
