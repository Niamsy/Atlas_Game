using Game.ResourcesManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantWateringIcon : MonoBehaviour
{
    private Image _waterDrop = null;
    public Image WaterDrop { get => _waterDrop; set => _waterDrop = value; }


    private Canvas _waterIcon = null;
    public Canvas   WaterIcon { get => _waterIcon;
        set { Debug.Log("Canvas In setter Value + " + value);
            if (value != null)
                _waterIcon = value;
        }
    }

    private Stock _plantStock = null;
    public Stock PlantStock { get => _plantStock; set => _plantStock = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartUpdate()
    {
        WaterIcon.enabled = true;
        InvokeRepeating("UpdateWaterFillAmount", 0.1f, 1f);
    }

    public void StopUpdate()
    {
        WaterIcon.enabled = false;
        Debug.Log("CANVAS GO AWAY");
        WaterDrop = null;
        WaterIcon = null;
        PlantStock = null;
        CancelInvoke("UpdateWaterFillAmount");
    }

    void UpdateWaterFillAmount()
    {
        Debug.Log("IMAGE YOU MUST FILL ENTER : PlantStock Quantity : " + PlantStock.Quantity + " Plant Stock Limit : " + PlantStock.Limit);
         if (PlantStock != null)
            setWaterDropResource((float)PlantStock.Quantity / (float)PlantStock.Limit);
    }

    void setWaterDropResource(float amount)
    {
        Debug.Log("IMAGE IS FILLING " + amount.ToString());
        Debug.Log("Water Drop : " + WaterDrop);
        WaterDrop.fillAmount = amount;
    }
}
