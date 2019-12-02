using System;
using System.Collections.Generic;
using System.Linq;
using Game.HUD;
using Game.Inventory;
using Game.Item;
using Game.SavingSystem;
using Networking;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Inventory
{
    public class RefreshButtonHUD : MonoBehaviour
    {
        [SerializeField] private Button _button = null;
        [SerializeField] private RefreshPopupHandler _refreshPopup = null;

        private DateTime _lastGetScannedPlant;

        void Start()
        {
            if (SaveManager.Instance.AccountData != null)
                _lastGetScannedPlant = SaveManager.Instance.AccountData.LastGetScannedPlant;
            else
                _lastGetScannedPlant = DateTime.Now;

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
            _refreshPopup.RefreshResult(success, message);
            if (!success)
            {
                return;
            }
            List<ItemStack> seeds = new List<ItemStack>();
            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

            scannedPlants.RemoveAll(s => DateTime.Compare(DateTime.Parse(s.scanned_at), _lastGetScannedPlant) <= 0);
            if (scannedPlants.Count > 0)
            {
                foreach (var scannedPlant in scannedPlants)
                {
                    var stack = new ItemStack();
                    var seed = ItemFactory.GetItemForId(scannedPlant.id + 1000);
                    if (seed)
                    {
                        stack.SetItem(seed, 1);
                        seeds.Add(stack);
                    }
                }

                _lastGetScannedPlant = scannedPlants.Max(x => DateTime.Parse(x.scanned_at));
                SaveManager.Instance.AccountData.LastGetScannedPlant = _lastGetScannedPlant;

                inventory.AddItemStacks(seeds);
            }

            _button.interactable = true;
        }
    }
}
