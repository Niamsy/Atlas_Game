using System;
using System.Collections.Generic;
using Game.Map.DayNight;
using UnityEngine.InputSystem;
using Game.SavingSystem;
using Menu.Inventory.ItemDescription;
using Networking;
using Plants;
using Plants.Plant;
using UnityEngine;
using UnityEngine.UI;


namespace Menu.Glossary
{
    public class Glossary : MenuWidget
    {

        private List<int> listID;
        private PlantStatistics[] allPlants;
        public GridLayoutGroup grid;
        public GameObject buttonPrefab;

        public PlantStatsDescriptionHUD Description;
        
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("---- Glossary ----");
            RequestManager.Instance.OnGlossaryRequestFinish += OnRequestFinish;
            RequestManager.Instance.Glossary();
            allPlants = PlantSystem.GetAllPlantStats();
            
            foreach (var plant in allPlants)
            {
                if (plant.ScientificName == "grass")
                    continue;
                var button = Instantiate(buttonPrefab, grid.transform);
                var gBut = button.GetComponent<GlossaryButton>();
                if (gBut != null)
                    gBut.SetPlantStat(plant, Description);
            }
        }

        private void OnDestroy()
        {
            RequestManager.Instance.OnGlossaryRequestFinish -= OnRequestFinish;
        }

        private void OnRequestFinish(bool success, string message, List<RequestManager.ScannedPlant> scannedPlants)
        {
            foreach (var tmp in scannedPlants)
            {
                listID.Add(tmp.id);
            }
        }
        
        protected override void InitialiseWidget() {}
        
        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Glossary.performed += OpenCloseGlossary;
            SaveManager.Instance.InputControls.Player.Glossary.Enable();
        }

        private void OnDisable()
        {
            SaveManager.Instance.InputControls.Player.Glossary.performed -= OpenCloseGlossary;
            SaveManager.Instance.InputControls.Player.Glossary.Disable();
        }

        private void OpenCloseGlossary(InputAction.CallbackContext obj)
        {
            Show(!Displayed);
        }

        public override void Show(bool display, bool force = false)
        {
            if (display)
                TimeManager.AskForPause(this);
            else
                TimeManager.StopPause(this);
            base.Show(display, force);
           // _description.Reset();
        }
    }
}