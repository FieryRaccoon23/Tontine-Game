using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIInGameScreen : UITreeNode
{
    //Private members
    [Header("General Settings")]
    [SerializeField]
    private float xOffset = 0.0f;

    [SerializeField]
    private float yOffset = 0.0f;

    [Header("Character Image Settings")]
    [SerializeField]
    private RawImage characterImage = null;
    [SerializeField]
    private float characterImageScaleX = 0.08f;
    [SerializeField]
    private float characterImageScaleY = 0.08f;

    [Header("Health Bar Settings")]
    [SerializeField]
    private GameObject healthBar = null;
    [SerializeField]
    private float healthBarWidthHeightRatio = 1.0f;
    private UIFillBar uiFillbar = null;

    [Header("Age Settings")]
    [SerializeField]
    private Text age = null;

    [Header("Money Settings")]
    [SerializeField]
    private Text money = null;

    [Header("Medic Panel Settings")]
    [SerializeField]
    private GameObject medicPanel = null;

    [Header("Defense Panel Settings")]
    [SerializeField]
    private GameObject defensePanel = null;

    void Start ()
    {
        float maxScreenDimension = Mathf.Max(Screen.width, Screen.height);
        //Character Image
        if (characterImage)
        {
            characterImage.rectTransform.sizeDelta = new Vector2(maxScreenDimension * characterImageScaleX,
                                                                 maxScreenDimension * characterImageScaleY);

            characterImage.rectTransform.position = new Vector2(characterImage.rectTransform.rect.width + xOffset,
                                                                characterImage.rectTransform.rect.height + yOffset);
        }
        else
        {
            Debug.LogError("Character Image Not Found.");
        }

        //Health Bar
        if (healthBar)
        {
            uiFillbar = healthBar.GetComponent<UIFillBar>();

            RectTransform rt = (RectTransform)healthBar.transform;
            float scaleFactorX = characterImage.rectTransform.rect.width / rt.rect.width;
            float scaleFactorY = scaleFactorX / healthBarWidthHeightRatio;

            healthBar.transform.localScale = new Vector2(scaleFactorX, scaleFactorY);

            healthBar.transform.position = new Vector2(characterImage.rectTransform.position.x + xOffset,
                                                       1.5f * characterImage.rectTransform.position.y + yOffset);

        }
        else
        {
            Debug.LogError("Healthbar GameObject Not Found.");
        }

        //Medic Panel
        if (medicPanel)
        {
            RectTransform rt = (RectTransform)medicPanel.transform;
            float medicalPanelWidth = rt.rect.width * medicPanel.transform.localScale.x;
            float medicalPanelHeight = rt.rect.height * medicPanel.transform.localScale.y;
            medicPanel.transform.position = new Vector2(Screen.width - medicalPanelWidth - xOffset, 
                                                        medicalPanelHeight + yOffset);
        }
        else
        {
            Debug.LogError("Medic Panel Not Found.");
        }

        //Defense Panel
        if (defensePanel)
        {
            RectTransform rt = (RectTransform)defensePanel.transform;
            float defensePanelWidth = rt.rect.width * defensePanel.transform.localScale.x;
            float defensePanelHeight = rt.rect.height * defensePanel.transform.localScale.y;
            rt = (RectTransform)medicPanel.transform;
            defensePanel.transform.position = new Vector2(Screen.width - defensePanelWidth - rt.rect.width - xOffset, 
                                                          defensePanelHeight + yOffset);
        }
        else
        {
            Debug.LogError("Defense Panel Not Found.");
        }
    }
	
	void Update ()
    {
	}
}
