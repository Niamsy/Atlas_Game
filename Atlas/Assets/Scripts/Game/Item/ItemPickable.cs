using Game.Inventory;
using Game.Item.PlantSeed;
using Game.Item.Tools;
using Game.Item.Tools.Bucket;
using Plants.Plant;
using Player;
using UnityEngine;
using Tools;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Game.Item
{
    public class ItemPickable : AInteractable
    {
        public ItemStackBehaviour BaseStack;

        private PlantModel _modelPlant;
        [SerializeField]
        public Text _hidedCanvas;

        protected virtual void Awake()
        {
            anim = InteractAnim.pick;
            if (gameObject)
            {
                _modelPlant = gameObject.GetComponent<PlantModel>();
                hideCanvas();
            }
        }

        private void hideCanvas()
        {
            if (_hidedCanvas != null)
            {
                Color col = _hidedCanvas.color;
                col.a = 255;
                _hidedCanvas.color = col;
            }
        }

        private void showCanvas()
        {
            if (_hidedCanvas != null)
            {
                Color col = _hidedCanvas.color;
                col.a = 0;
                _hidedCanvas.color = col;
            }
        }

        public override void Interact(PlayerController playerController)
        {
            if (_modelPlant && _modelPlant && _modelPlant.IsSowed == true)
                return;
            if (!playerController.IsInteracting)
                playerController.InteractValue = anim.ToInt();

            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
            Singleton<QuestManager>.Instance.UpdateQuestsWith(ObjType.PICKUP, "ITEM", null);
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

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                hideCanvas();
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                showCanvas();
            }
        }
    }
}
