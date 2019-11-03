using Game.Inventory;
using Plants.Plant;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Item
{
    public class ItemPickable : AInteractable
    {
        public ItemStackBehaviour BaseStack;

        private PlantModel _modelPlant;
        [SerializeField]
        public Text _hidedCanvasName;
        [SerializeField]
        public Text _hidedCanvasUsage;

        protected virtual void Awake()
        {
            anim = InteractAnim.pick;
            if (gameObject)
            {
                _modelPlant = gameObject.GetComponent<PlantModel>();
                hideCanvas(_hidedCanvasName);
                hideCanvas(_hidedCanvasUsage);
            }
        }

        private void hideCanvas(Text canvas)
        {
            if (_hidedCanvasName.text.Length <= 0 && BaseStack.Slot != null && BaseStack.Slot.Content != null)
            {
                _hidedCanvasName.text = BaseStack.Slot.Content.Name;
            }

            if (canvas != null)
            {
                Color col = canvas.color;
                col.a = 255;
                canvas.color = col;
            }
        }

        private void showCanvas(Text canvas)
        {
            if (_hidedCanvasName.text.Length <= 0 && BaseStack.Slot != null && BaseStack.Slot.Content != null)
            {
                _hidedCanvasName.text = BaseStack.Slot.Content.Name;
            }

            if (canvas != null && canvas.text != "New Text")
            {
                Color col = canvas.color;
                col.a = 0;
                canvas.color = col;
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
                hideCanvas(_hidedCanvasName);
                hideCanvas(_hidedCanvasUsage);
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                showCanvas(_hidedCanvasName);
                showCanvas(_hidedCanvasUsage);
            }
        }
    }
}
