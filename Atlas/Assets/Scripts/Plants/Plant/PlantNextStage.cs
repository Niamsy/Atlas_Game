using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantNextStage : MonoBehaviour
{
    private GameObject _Item = null;

    void Update()
    {
        if (_Item && _Item.GetComponent<PlayerController>().CheckForNextStageInput())
        {
            Plants.Plant.PlantModel plant_model = this.GetComponent<Plants.Plant.PlantModel>();
            if (plant_model)
            {
                Debug.Log("Next Stage");
                plant_model.GoToNextStage();
            }
            _Item.GetComponent<PlayerController>().IsNextStage = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _Item = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _Item = null;
        }
    }
}
