using Game.Inventory;
using Game.Item;
using Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Game.SavingSystem;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButtonHUD : MonoBehaviour
{
    public Button _button;
    private DateTime lastGetScannedPlant;

    void Start()
    {
        lastGetScannedPlant = SaveManager.Instance.AccountData.LastGetScannedPlant;

        if (RequestManager.Instance)
            RequestManager.Instance.OnGetScannedPlantsRequestFinished += GetScannedPlantsFinished;
        _button.onClick.AddListener(GetScannedPlants);  
    }

    public void GetScannedPlants()
    {
        RequestManager.Instance.GetScannedPlants();
        _button.interactable &= RequestManager.Instance.CanReceiveANewRequest;
    }

    public void GetScannedPlantsFinished(bool success, string message, List<RequestManager.ScannedPlant> scannedPlants)
    {
        Debug.Log("last get scanned plant = " + lastGetScannedPlant);
        List<ItemStack> seeds = new List<ItemStack>();
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

        scannedPlants.RemoveAll(s => DateTime.Compare(DateTime.Parse(s.scanned_at), lastGetScannedPlant) <= 0);
        if (scannedPlants.Count > 0)
        {
            foreach (var scanned_plant in scannedPlants)
            {
                var stack = new ItemStack();
                var seed = ItemFactory.GetItemForId(scanned_plant.id + 1000);
                if (seed)
                {
                    stack.SetItem(seed, 1);
                    seeds.Add(stack);
                }
            }

            lastGetScannedPlant = scannedPlants.Max(x => DateTime.Parse(x.scanned_at));
            SaveManager.Instance.AccountData.LastGetScannedPlant = lastGetScannedPlant;

            inventory.AddItemStacks(seeds);
        }

        _button.interactable = true;
    }
}
