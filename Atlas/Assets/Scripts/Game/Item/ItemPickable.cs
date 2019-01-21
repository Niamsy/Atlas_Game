using Game.Inventory;
using Player;
using UnityEngine;

public class ItemPickable : MonoBehaviour
{
    private GameObject _Player;

    void Start()
    {
        _Player = null;
    }

    void Update()
    {
        if (_Player && _Player.GetComponent<PlayerController>().CheckForPickInput())
        {
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
