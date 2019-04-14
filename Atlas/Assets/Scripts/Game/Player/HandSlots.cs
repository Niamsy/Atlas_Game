using Game.Inventory;
using Game.Item;
using Game.Item.PlantSeed;
using InputManagement;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    [RequireComponent(typeof(BaseInventory))]
    public class HandSlots : MonoBehaviour
    {
        private PlayerController _controller;

        [SerializeField] private ItemStack _equippedItemStack;
        private ItemAbstract               _equippedItem;
        private GameObject                 _equippedItemInstance;
        private Transform                  _handTransform;
        public ItemStack                   EquippedItemStack { get { return (_equippedItemStack); } }

        private void Awake()
        {
            _equippedItemStack.OnItemStackUpdated += OnEquippedUpdate;

            _handTransform = transform.Find("RightHand");
            _controller = FindObjectOfType<PlayerController>();
            LoadData();

            GameControl.BeforeSaving += SaveData;
        }

        private void ResetUI()
        {
            GameObject canvasObject = GameObject.FindGameObjectWithTag("RightHand");
            Transform textTr = canvasObject.transform.Find("UIHelp");
            Text text = textTr.GetComponent<Text>();
            text.enabled = false;
        }

        private void UsingItem(InputKeyStatus status)
        {
            bool canUse = _equippedItem.CanUse(_handTransform);
            if (_equippedItem is Seed)
            {
                _controller.CanSow = canUse;
                if (canUse)
                {
                    GameObject canvasObject = GameObject.FindGameObjectWithTag("RightHand");
                    Transform textTr = canvasObject.transform.Find("UIHelp");
                    Text text = textTr.GetComponent<Text>();
                    text.enabled = true;
                    text.text = "Click to sow";
                }
                else
                    ResetUI();

                _controller.IsCheckSowing = true;
            }

            if (_equippedItem != null && EquippedItemStack != null && EquippedItemStack.Quantity > 0)
            {
                // TODO: Someone remake that
                // if (_equippedItem is Seed && !_controller.CheckToSow())
                //    return;
                _equippedItem.Use(EquippedItemStack, status);
                if (EquippedItemStack.Quantity == 0)
                    ResetUI();
            }
        }

        private void Update()
        {
            InputKeyStatus status = _controller.GetUseInput();

            if (EquippedItemStack.Content)
                UsingItem(status);
            else
                _controller.IsCheckSowing = false;
        }

        #region Load/Saving Methods
        private void SaveData(GameControl gameControl)
        {
            GameData gameData = gameControl.gameData;
            gameData.RightHandItem.SetObject(_equippedItemStack);
        }

        private void LoadData()
        {
            if (GameControl.control == null)
                return;
            
            GameData gameData = GameControl.control.gameData;
            EquippedItemStack.SetFromGameData(gameData.RightHandItem);
        }
        #endregion
        
        public void Equip(bool left, ItemStack newItem)
        {
            EquippedItemStack.SwapStack(newItem);
        }

        private void OnEquippedUpdate(ItemStack updated)
        {
            UpdateHand(ref _equippedItemInstance, updated, _handTransform);
        }

        private void UpdateHand(ref GameObject itemGo, ItemStack item, Transform position)
        {
            if (_equippedItem != null)
                _equippedItem.UnEquip();

            _equippedItem = null;

            if (item.IsEmpty)
                return;
            
            _equippedItem = item.Content;
            itemGo = _equippedItem.Equip(position);
        }
    }
}
