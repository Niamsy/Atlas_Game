using Game.Inventory;
using Plants.Plant;
using Player;
using UnityEngine;

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
        _ModelPlant = gameObject.GetComponent<PlantModel>();
        _GuiCanvas = gameObject.GetComponentInChildren<Canvas>();
        _GuiCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_Player && _Player.GetComponent<PlayerController>().CheckForPickInput())
        {
            if (_ModelPlant && _ModelPlant && _ModelPlant.IsSowed == true)
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
            _GuiCanvas.gameObject.SetActive(true);
            _Player = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _GuiCanvas.gameObject.SetActive(false);
            _Player = null;
        }
    }
}
