using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UIFillBar : MonoBehaviour
{
    //Public Members
    public Image healthBar;
    public Text healthBarText;
    public float rateOfChange;
    
    //Private Members
    private float sign = 1.0f;
    private float changeAmount = 0;

    //Private Functions
	void Update ()
    {
        if (changeAmount > 0)
        {
            float amountToChangePerFrame = Time.deltaTime * rateOfChange;
            changeAmount -= amountToChangePerFrame;
            changeAmount = Mathf.Min(0.0f, changeAmount);
            healthBar.fillAmount = Mathf.Clamp(healthBar.fillAmount + (amountToChangePerFrame  * sign), 0.0f, 100.0f);
            healthBarText.text = ((int)(Mathf.Ceil(healthBar.fillAmount * 100.0f))).ToString();
        }
	}

    private void ChangeHealth(float change)
    {
        changeAmount += change;
    }

    //Public Functions
    public void IncreaseHealth(float change)
    {
        sign = 1.0f;
        ChangeHealth(change);
    }

    public void DecreaseHealth(float change)
    {
        sign = -1.0f;
        ChangeHealth(change);
    }

}
