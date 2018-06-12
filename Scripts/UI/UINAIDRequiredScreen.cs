using UnityEngine;
using System.Collections;

public class UINAIDRequiredScreen : UITreeNode
{
    //Private functions
    void Start ()
    {
        selfID = NA_IDRequiredScreenID;
    }

    //Public Functions
    public void BackTransition()
    {
        PerformTransition(StartScreenID);
    }
}
