using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshPopupHandler : MonoBehaviour
{
    [SerializeField] private GameObject SuccessPopup;
    [SerializeField] private GameObject FailurePopup;
    [SerializeField] private Text FailurePopupText;
    [SerializeField] private float DisplayTime;

    private float Timer;
    private bool displaying = false;

    private void Start()
    {
        hidePopups();    
    }

    private void handlePopup(bool state, string Message)
    {
        Debug.LogError("POPUP BEGIN");
        SuccessPopup.SetActive(state);
        FailurePopup.SetActive(!state);
        FailurePopupText.text += Message;
        displaying = true;
        Timer = 0;
    }

    private void hidePopups()
    {
        Debug.LogError("HIDING POPUPS");
        SuccessPopup.SetActive(false);
        FailurePopup.SetActive(false);
        displaying = false;
    }

    public void refreshResult(bool result, string Message) 
    {
        handlePopup(result, Message);
        FailurePopupText.text = "";
    }

    void Update()
    {
       if (displaying)
        {
            Timer += 0.1f; //(Time.deltatime == 0 WTF) ???
            print("Delta : " + Time.deltaTime);
            if (Timer > DisplayTime)
            {
                hidePopups();
            }
            else
            {
                print(Timer + "/" + DisplayTime);
            }
        }
    }
}
