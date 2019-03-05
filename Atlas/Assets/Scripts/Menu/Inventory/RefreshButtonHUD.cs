using Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButtonHUD : MonoBehaviour
{
    public Button _button;

    void Start()
    {
        _button.onClick.AddListener(GetScannedPlants);  
    }

    public void GetScannedPlants()
    {
        RequestManager.Instance.GetScannedPlants();
        _button.interactable &= RequestManager.Instance.CanReceiveANewRequest;
    }
}
