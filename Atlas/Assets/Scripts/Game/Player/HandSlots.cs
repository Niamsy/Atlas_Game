using Game.Inventory;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(BaseInventory))]
    public class HandSlots : MonoBehaviour
    {
        /// <summary>
        /// TEMPORARY WILL BE DELETED LATER
        /// </summary>
        private BaseInventory _baseInventory;
        
        public ItemStack LeftHandItem;
        public ItemStack RightHandItem;

        private void Awake()
        {
            _baseInventory = GetComponent<BaseInventory>();

            LoadData();
        }

        private void OnDisable()
        {
            SaveData();
        }

        #region Load/Saving Methods
        private void SaveData()
        {
            if (GameControl.control == null)
                return;
            
            GameData gameData = GameControl.control.gameData;
            gameData.LeftHandItem.SetObject(LeftHandItem);
            gameData.RightHandItem.SetObject(RightHandItem);
            
        }

        private void LoadData()
        {
            if (GameControl.control == null)
                return;
            
            GameData gameData = GameControl.control.gameData;
            LeftHandItem.SetFromGameData(gameData.LeftHandItem);
            RightHandItem.SetFromGameData(gameData.RightHandItem);
        }
        #endregion
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                Equip(true, _baseInventory[0]);
            if (Input.GetKeyDown(KeyCode.E))
                Equip(false, _baseInventory[1]);
        }
        
        public void Equip(bool left, ItemStack newItem)
        {
            if (left)
                LeftHandItem.SwapStack(newItem);
            else
                RightHandItem.SwapStack(newItem);
            
            // ToDo: Display model in game in the hand
        }
    }
}
