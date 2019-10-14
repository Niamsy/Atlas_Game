using Game.Inventory;
using Plants.Plant;
using Player;
using UnityEngine;

namespace Game.Item
{
    public class ItemPickable : AInteractable
    {
        public ItemStackBehaviour BaseStack;

        private PlantModel _modelPlant;
        private Canvas _guiCanvas;

        protected virtual void Awake()
        {
            anim = InteractAnim.pick;
            if (gameObject)
            {
                _modelPlant = gameObject.GetComponent<PlantModel>();
                _guiCanvas = gameObject.GetComponentInChildren<Canvas>();
                if (_guiCanvas)
                    _guiCanvas.gameObject.SetActive(false);
            }
        }

        public override void Interact(PlayerController playerController)
        {
            if (_modelPlant && _modelPlant && _modelPlant.IsSowed == true)
                return;
            if (!playerController.IsInteracting)
                playerController.InteractValue = anim.ToInt();

            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

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
                if (_guiCanvas && _guiCanvas.gameObject)
                {
                    _guiCanvas.gameObject.SetActive(true);
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if (_guiCanvas)
                    _guiCanvas.gameObject.SetActive(false);
            }
        }
    }
}
