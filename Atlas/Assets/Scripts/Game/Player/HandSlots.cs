using Game.Inventory;
using Game.Item.PlantSeed;
using Player;
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
        private PlayerController _controller;
        private int             _handEquipToggle = 0;
        private Seed            _plant;
        private ItemStack       _itemStack;
        private Transform       _itemTransform;

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
            _controller = FindObjectOfType<PlayerController>();
            LoadData();

            GameControl.BeforeSaving += SaveData;
        }

        private void Update()
        {
            if ((_handEquipToggle = _controller.CheckForEquippedHandUsed()) > 0)
            {
                if ((_handEquipToggle == 1 && RightHandItem.Content is Seed) ||
                    (_handEquipToggle == 2 && LeftHandItem.Content is Seed))
                {
                    _plant = (_handEquipToggle == 1) ? RightHandItem.Content as Seed : LeftHandItem.Content as Seed;
                    _itemStack = (_handEquipToggle == 1) ? RightHandItem : LeftHandItem;
                    _itemTransform = (_handEquipToggle == 1) ? _rightHandTransform : _leftHandTransform;
                    _controller.TrackToSow(_itemTransform);
                    _controller.CheckForSowing(true);
                    if (_plant != null && _itemStack != null && _itemStack.Quantity > 0 && _controller.CheckToSow())
                    {
                        _controller.SowPlant(_plant);
                        _itemStack.ModifyQuantity(_itemStack.Quantity - 1);
                    }
                }
                else
                {
                    _controller.CheckForSowing(false);
                }
            }
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
