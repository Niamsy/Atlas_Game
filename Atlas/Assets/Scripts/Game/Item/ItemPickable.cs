using Game.Inventory;
using Plants.Plant;
using Player;
using UnityEngine;

#region ItemUsing
using Game.Item.Tools.Bucket;
using Game.Item.Tools;
using Game.Item.PlantSeed;
#endregion

public class ItemPickable : MonoBehaviour
{
    private GameObject _Player;
    private PlantModel _ModelPlant;
    private Canvas _GuiCanvas;

    void Start()
    {
        _Player = null;
    }

    private void Awake()
    {
        if (gameObject)
        {
            _ModelPlant = gameObject.GetComponent<PlantModel>();
            _GuiCanvas = gameObject.GetComponentInChildren<Canvas>();
            if (_GuiCanvas)
                _GuiCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_Player && _Player.GetComponent<PlayerController>().CheckForPickInput())
        {
            if (_ModelPlant && _ModelPlant && _ModelPlant.IsSowed == true)
                return;
            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
            ItemStack baseStack = gameObject.GetComponent<ItemStackBehaviour>().Slot;
            if (baseStack.Content is BucketItem)
            {
                AchievementManager.Instance.achieve(AchievementManager.AchievementId.PickupBucket);
            }
            if (baseStack.Content is ShovelItem)
            {
                AchievementManager.Instance.achieve(AchievementManager.AchievementId.PickupShovel);
            }
            if (baseStack.Content is Seed)
            {
                AchievementManager.Instance.achieve(AchievementManager.AchievementId.PickupFirstSeed);
            }
            
            ItemStack leftStack = inventory.AddItemStack(baseStack);
            if (leftStack == null)
                Destroy(gameObject);
            else
                baseStack = leftStack;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (_GuiCanvas && _GuiCanvas.gameObject)
            {
                _GuiCanvas.gameObject.SetActive(true);
                _Player = col.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (_GuiCanvas)
                _GuiCanvas.gameObject.SetActive(false);
            _Player = null;
        }
    }
}
