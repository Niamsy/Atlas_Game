using Game.Inventory;
using Plants.Plant;
using Player;
using UnityEngine;

public class ItemPickable : MonoBehaviour
{
    public Popup popupSender;
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
            Debug.Log("[" + baseStack.Content.ToString() + "]");
            if (baseStack.Content.ToString().Contains("Tools"))
            {
                popupSender.sendPopup("Open inventory and drag the tool into an hand slot to use it :)");
            } else if (baseStack.Content.ToString().Contains("Seed"))
            {
                popupSender.sendPopup("You can plant your new seed by drag it into your hand slot to use it ! :)");
            }
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
