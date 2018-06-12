using UnityEngine;
using System.Collections;
using System;

public class UIGameManager : MonoBehaviour
{
    //Public Members
    public static UIGameManager uiGameManagerInstance { get; private set; }
    
    [SerializeField]
    public GameObject[] uiGameObjects = null;

    //Private Members
    [SerializeField]
    private GameObject canvas = null;

    [SerializeField]
    private Int16 numberOfMaximumPlayers = 4;

    //Accessors
    public GameObject Canvas
    {
        get
        {
            return canvas;
        }
    }

    public Int16 NumberOfMaximumPlayers
    {
        get
        {
            return numberOfMaximumPlayers;
        }
    }

    //Public Functions
    public void Transition(Int16 objectID, Int16 childID)
    {
        if (objectID >= uiGameObjects.Length || objectID < 0)
        {
            Debug.LogError("Game object not found.");
        }

        if (childID >= uiGameObjects.Length || childID < 0)
        {
            Debug.LogError("Child object not found.");
        }

        uiGameObjects[objectID].SetActive(false);
        uiGameObjects[childID].SetActive(true);

    }

    //Private Functions
    private void Awake()
    {
        if (uiGameManagerInstance == null)
        {
            uiGameManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}