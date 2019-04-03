using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public const float popupFadeDuration = 200; // Millisecond;
    public const float popupDuration = 1000; // Millisecond;
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

    public void Start()
    {
        currentColor = popupBackground.color;
        currentColor.a = 0;
        popupBackground.color = currentColor;
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
            currentMode = popupMode.Still;
        }
        if (currentColor.a < 196)
        {
            currentColor.a += (196 / popupFadeDuration); // 196 = Maximum Transparency value;
        }
    }

    private void popupStill()
    {
        if (CurrentTimer > (popupDuration + popupFadeDuration))
        {
            Debug.Log("i change mode");
            currentMode = popupMode.Disapear;
        }
    }

    private void popupDisapear()
    {
        if (CurrentTimer > (popupFadeDuration * 2 + popupDuration))
        {
            currentMode = popupMode.None;
        }
        if (currentColor.a > 0)
        {
            currentColor.a -= (196 / popupFadeDuration);
        }
    }
}
