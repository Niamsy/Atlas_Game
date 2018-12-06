using Game.Inventory;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(BaseInventory))]
    public class HandSlots : MonoBehaviour
    {
        [SerializeField] private ItemStack _leftHandItem;
        private GameObject _leftHandGO;
        private Transform _leftHandTransform;
        public ItemStack LeftHandItem { get { return (_leftHandItem); } }

        [SerializeField] private ItemStack _rightHandItem;
        private GameObject _rightHandGO;
        private Transform _rightHandTransform;
        public ItemStack RightHandItem { get { return (_rightHandItem); } }

        private void Awake()
        {
            _leftHandItem.OnItemStackUpdated += OnLeftHandUpdate;
            _rightHandItem.OnItemStackUpdated += OnRightHandUpdate;

            _leftHandTransform = transform.Find("LeftHand");
            _rightHandTransform = transform.Find("RightHand");
            
            LoadData();

            GameControl.BeforeSaving += SaveData;
        }

        #region Load/Saving Methods
        private void SaveData(GameControl gameControl)
        {
            GameData gameData = gameControl.gameData;
            gameData.LeftHandItem.SetObject(_leftHandItem);
            gameData.RightHandItem.SetObject(_rightHandItem);
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
        
        public void Equip(bool left, ItemStack newItem)
        {
            if (left)
                LeftHandItem.SwapStack(newItem);
            else
                RightHandItem.SwapStack(newItem);
            
            // ToDo: Display model in game in the hand
        }

        private void OnLeftHandUpdate(ItemStack updated)
        {
            UpdateHand(ref _leftHandGO, updated, _leftHandTransform);
        }

        private void OnRightHandUpdate(ItemStack updated)
        {
            UpdateHand(ref _rightHandGO, updated, _rightHandTransform);
        }

        private void UpdateHand(ref GameObject itemGo, ItemStack item, Transform position)
        {
            if (itemGo != null)
                Destroy(itemGo);

            if (item.IsEmpty)
                return;

            itemGo = Instantiate(item.Content.PrefabHoldedGO, position);
        }
    }
}
