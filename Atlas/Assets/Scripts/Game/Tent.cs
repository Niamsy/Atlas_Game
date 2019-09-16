using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Game.ResourcesManagement;

public class Tent : AInteractable
{
    public override void Interact(PlayerController playerController)
    {
        var energy = playerController.GetPlayerStats().Resources[Resource.Energy];
        energy.Quantity = energy.Limit;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
