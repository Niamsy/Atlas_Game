using System.Collections;
using System.Collections.Generic;
using Game.Map.DayNight;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.SavingSystem;
using System.Collections.Generic;
using AtlasAudio;
using AtlasEvents;
using Game.Map.DayNight;
using Game.Player;
using Game.SavingSystem;
using Menu.Inventory.ItemDescription;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace Menu.Glossary
{
    public class Glossary : MenuWidget
    {
        // Start is called before the first frame update
        void Start()
        {
            
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