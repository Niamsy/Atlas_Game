using Game.Inventory;
using Plants.Plant;
using Player;
using UnityEngine;

public class ItemPickable : MonoBehaviour
{
    private GameObject _Player;
    private PlantModel _ModelPlant;
    void Start()
    {
        _Player = null;
    }

    private void Awake()
    {
        _ModelPlant = gameObject.GetComponent<PlantModel>();
    }

    void Update()
    {
        if (_Player && _Player.GetComponent<PlayerController>().CheckForPickInput())
        {
            if (_ModelPlant && _ModelPlant.PlantItem && _ModelPlant.PlantItem.IsSowed == true)
                return;
            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
            ItemStack baseStack = gameObject.GetComponent<ItemStackBehaviour>().Slot;
            ItemStack leftStack = inventory.AddItemStack(baseStack);
            if (leftStack == null)
            {
                Destroy(gameObject);
            }
            else
            {
                baseStack = leftStack;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _Player = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _Player = null;
        }
    }
}
