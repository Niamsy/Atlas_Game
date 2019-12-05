using System;
using System.Collections.Generic;
using Game.Map.DayNight;
using UnityEngine.InputSystem;
using Game.SavingSystem;
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
        
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("---- Glossary ----");
            RequestManager.Instance.OnGlossaryRequestFinish += OnRequestFinish;
            RequestManager.Instance.Glossary();
            allPlants = PlantSystem.GetAllPlantStats();
            
            foreach (var plant in allPlants)
            {
                var button = Instantiate(buttonPrefab, grid.transform);
                button.name = plant.name;
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

        // Update is called once per frame    
        void Update()
        {

        }
        
        protected override void InitialiseWidget()
        {
         
        }
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