using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantNextStage : MonoBehaviour
{
    private GameObject _Item = null;

    void Update()
    {
        /* if (_Item && _Item.GetComponent<PlayerController>().CheckForNextStageInput())
        {
            Plants.Plant.PlantModel plant_model = this.GetComponent<Plants.Plant.PlantModel>();
            if (plant_model)
            {
                plant_model.GoToNextStage();
            }
            _Item.GetComponent<PlayerController>().IsNextStage = false;
        }
        if (_Item && _Item.GetComponent<PlayerController>().CheckForGiveInput())
        {
            Plants.Plant.PlantModel plant_model = this.GetComponent<Plants.Plant.PlantModel>();
            if (plant_model)
            {
                plant_model.GiveResource();
            }
            _Item.GetComponent<PlayerController>().IsGive = false;
        }
        if (_Item && _Item.GetComponent<PlayerController>().CheckForConsumeInput())
        {
            Plants.Plant.PlantModel plant_model = this.GetComponent<Plants.Plant.PlantModel>();
            if (plant_model)
            {
                plant_model.ConsumeResource();
            }
            _Item.GetComponent<PlayerController>().IsConsume = false;
        }*/
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
