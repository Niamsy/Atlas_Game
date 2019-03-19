using Game.Inventory;
using Game.Item;
using Game.Item.PlantSeed;
using Player;
using UnityEngine;
using UnityEngine.UI;

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
        private ItemAbstract        _object;
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

        private void InitHandUsing(ItemAbstract seed, ItemStack item, Transform transform)
        {
            _object = seed;
            _itemStack = item;
            _itemTransform = transform;
        }

        private string HandUseToString()
        {
            return (_handEquipToggle == 1) ? "RightHand" : (_handEquipToggle == 2) ? "LeftHand" : "";
        }

        private void ResetUI()
        {
            GameObject canvasObject = GameObject.FindGameObjectWithTag("RightHand");
            Transform textTr = canvasObject.transform.Find("UIHelp");
            Text text = textTr.GetComponent<Text>();
            text.enabled = false;
            canvasObject = GameObject.FindGameObjectWithTag("LeftHand");
            textTr = canvasObject.transform.Find("UIHelp");
            text = textTr.GetComponent<Text>();
            text.enabled = false;
        }

        private void UsingItem()
        {
            bool canUse = _object.CanUse(_itemTransform);
            if (_object is Seed)
            {
                _controller.CanSow = canUse;
                if (canUse)
                {
                    string canvasName = HandUseToString();
                    Debug.Log(canvasName);
                    if (canvasName != "")
                    {
                        GameObject canvasObject = GameObject.FindGameObjectWithTag(canvasName);
                        Transform textTr = canvasObject.transform.Find("UIHelp");
                        Text text = textTr.GetComponent<Text>();
                        text.enabled = true;
                        text.text = "Click to sow";
                    }
                }
                else
                {
                    ResetUI();
                }
                _controller.IsCheckSowing = true;
            }
            if (_object != null && _itemStack != null && _itemStack.Quantity > 0)
            {
                if (_object is Seed)
                {
                    if (!_controller.CheckToSow())
                        return ;
                }
                _object.Use();
                _itemStack.ModifyQuantity(_itemStack.Quantity - 1);
            }
        }

        private void Update()
        {
            if ((_handEquipToggle = _controller.CheckForEquippedHandUsed()) > 0)
            {
                if (_handEquipToggle == 1 && RightHandItem.Content)
                {
                    InitHandUsing(RightHandItem.Content, RightHandItem, _rightHandTransform);
                    UsingItem();
                }
                else if (_handEquipToggle == 2 && LeftHandItem.Content)
                {
                    InitHandUsing(LeftHandItem.Content, LeftHandItem, _leftHandTransform);
                    UsingItem();
                }
                else
                {
                    _handEquipToggle = 0;
                    _controller.IsCheckSowing = false;
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
