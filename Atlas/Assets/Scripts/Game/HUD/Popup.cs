using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class Popup : Singleton<Popup>
{
    #region members
#pragma warning disable 414
    private static Popup popupInstance = null;
#pragma warning restore 414
    public const float popupFadeDuration = 4; // Millisecond;
    public const float popupDuration = 4; // Millisecond;
    Color currentColor;
    public Image popupBackground;
    public Text popupText;
    public float backgroundTransparency = 120;
    private enum popupMode
    {
        None,
        Appear,
        Still,
        Disapear
    };

    popupMode currentMode;

    private float CurrentTimer;
    #endregion

    public void Start()
    {
        initpopup();
    }

    public void Update()
    {  
        if (currentMode != popupMode.None)
        {
            CurrentTimer += Time.deltaTime;
            switch (currentMode)
            {
                case popupMode.Appear:
                    popupAppear();
                    break;
                case popupMode.Still:
                    popupStill();
                    break;
                case popupMode.Disapear:
                    popupDisapear();
                    break;
            }
            popupBackground.color = currentColor;
        }
    }

    private void initpopup()
    {
        currentColor = popupBackground.color;
        currentColor.a = 0;
        popupBackground.color = currentColor;
    }

    public void sendPopup(string displayedText)
    {
        CurrentTimer = 0;
        currentMode = popupMode.Appear;
        popupText.text = displayedText;
    }

    private void popupAppear()
    {
        if (CurrentTimer > popupFadeDuration)
        {
            currentColor.a = backgroundTransparency;
            currentMode = popupMode.Still;
        }
        if (currentColor.a < backgroundTransparency)
        {
            currentColor.a += (backgroundTransparency / (popupFadeDuration / 1000.0f)); 
        }
    }

    private void popupStill()
    {
        if (CurrentTimer > (popupDuration + popupFadeDuration))
        {
            currentMode = popupMode.Disapear;
        }
    }

    private void popupDisapear()
    {
        if (CurrentTimer > (popupFadeDuration * 2 + popupDuration))
        {
            currentMode = popupMode.None;
            popupText.text = "";
        }
        if (currentColor.a > 0)
        {
            currentColor.a -= (backgroundTransparency / (popupFadeDuration / 1000.0f));
            popupText.text = "";
        }
    }
}
