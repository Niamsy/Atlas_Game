using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : Singleton<Popup>
{
    #region members
    private static Popup popupInstance = null;
    public const float popupFadeDuration = 2; // Millisecond;
    public const float popupDuration = 4; // Millisecond;
    Color currentColor;
    public Image popupBackground;
    public Text popupText;
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
        initpopup();
        CurrentTimer = 0;
        currentMode = popupMode.Appear;
        popupText.text = displayedText;
    }

    private void popupAppear()
    {
        if (CurrentTimer > popupFadeDuration)
        {
            currentColor.a = 196;
            currentMode = popupMode.Still;
        }
        if (currentColor.a < 196)
        {
            currentColor.a += (196 / (popupFadeDuration / 1000)); // 196 = Maximum Transparency value;
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
            currentColor.a -= (196 / (popupFadeDuration / 1000));
            popupText.text = "";
        }
    }
}
