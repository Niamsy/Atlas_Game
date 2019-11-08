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
    private bool displaying;

    private void Start()
    {
        hidePopups();    
    }

    private void handlePopup(bool state, string Message)
    {
        SuccessPopup.SetActive(state);
        FailurePopup.SetActive(!state);
        FailurePopupText.text += Message;
        displaying = true;
        Timer = 0;
    }
    private void hidePopups()
    {
        SuccessPopup.SetActive(false);
        FailurePopup.SetActive(false);
    }

    public void refreshResult(bool result, string Message) 
    {
        handlePopup(result, Message);
    }

    void Update()
    {
       if (displaying)
        {
            Timer += Time.deltaTime;
            if (Timer > DisplayTime)
            {
                hidePopups();
            }
        }
    }
}
