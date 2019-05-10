using Game.Inventory;
using Game.Item.PlantSeed;
using Game.Item.Tools;
using Game.Item.Tools.Bucket;
using Plants.Plant;
using Player;
using UnityEngine;

namespace Game.Item
{
    public class ItemPickable : MonoBehaviour
    {
        public ItemStackBehaviour BaseStack;
        
        private GameObject _player;
        private PlantModel _modelPlant;
        private Canvas _guiCanvas;
        void Start()
        {
            _player = null;
        }

        protected virtual void Awake()
        {
            if (gameObject)
            {
                _modelPlant = gameObject.GetComponent<PlantModel>();
                _guiCanvas = gameObject.GetComponentInChildren<Canvas>();
                if (_guiCanvas)
                    _guiCanvas.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            if (_player && _player.GetComponent<PlayerController>().CheckForPickInput())
            {
                if (_modelPlant && _modelPlant && _modelPlant.IsSowed == true)
                    return;
                PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
                if (BaseStack.Slot.Content is BucketItem)
                {
                    AchievementManager.Instance.achieve(AchievementManager.AchievementId.PickupBucket);
                }
                if (BaseStack.Slot.Content is ShovelItem)
                {
                    AchievementManager.Instance.achieve(AchievementManager.AchievementId.PickupShovel);
                }
                if (BaseStack.Slot.Content is Seed)
                {
                    AchievementManager.Instance.achieve(AchievementManager.AchievementId.PickupFirstSeed);
                }
            
                ItemStack leftStack = inventory.AddItemStack(BaseStack.Slot);
                if (leftStack == null)
                    Destroy(gameObject);
                else
                    BaseStack.Slot = leftStack;
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if (_guiCanvas && _guiCanvas.gameObject)
                {
                    _guiCanvas.gameObject.SetActive(true);
                    _player = col.gameObject;
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if (_guiCanvas)
                    _guiCanvas.gameObject.SetActive(false);
                _player = null;
            }
        }
    }
}
