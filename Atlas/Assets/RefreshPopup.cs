using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshPopup : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] float TimeDuration;
    [SerializeField] float CurrentTimer;
    [SerializeField] Image exclamationPoint;

    bool Success;

    private void Start()
    {
        CurrentTimer = 0;
    }

    public void setDisplay(bool display)
    {
        enabled = display;
    }

    public void setColor(Color exclamationColor)
    {
        exclamationPoint.color = exclamationColor;
    }

    public void StartPopup(string text)
    {
        CurrentTimer = 0;
        setText(text);
    }

    public void setText(string text)
    {
        _text.text = (Success) ? "Success :\n" : "Failure :\n";
        _text.text += text;
    }


    private void Update()
    {
        CurrentTimer += 0.1f;
        if (CurrentTimer > TimeDuration)
        {
            enabled = false;
        }
    }
}
