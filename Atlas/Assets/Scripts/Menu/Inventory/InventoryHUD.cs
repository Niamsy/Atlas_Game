using Game;
using Menu;
using UnityEngine;
using InputManagement;
using AtlasEvents;
using AtlasAudio;
using Menu.Inventory.ItemDescription;
using SceneManagement;
using System;
using Game.SavingSystem;
using UnityEngine.InputSystem;

public class InventoryHUD : MenuWidget
{
    protected override void InitialiseWidget()
    {
    }

    [Header("Audio")] public Audio OnToggleGUIAudio;
    public AudioEvent OnToggleGUIEvent;

    [Header("Inventory Keys")] public InputKey _Inventory;

    [SerializeField] private ItemDescriptionHUD _description;

    private void OnEnable()
    {
        SaveManager.Instance.InputControls.Player.Inventory.performed += OpenCloseInventory;
        SaveManager.Instance.InputControls.Player.Inventory.Enable();
    }

    private void OnDisable()
    {
        SaveManager.Instance.InputControls.Player.Inventory.performed -= OpenCloseInventory;
        SaveManager.Instance.InputControls.Player.Inventory.Disable();
    }

    private void OpenCloseInventory(InputAction.CallbackContext obj)
    {
        Show(!Displayed);
        if (OnToggleGUIAudio && OnToggleGUIEvent)
        {
            OnToggleGUIEvent.Raise(OnToggleGUIAudio, null);
        }
    }

    public override void Show(bool display, bool force = false)
    {
        TimeManager.Instance.PauseGame(display);
        base.Show(display, force);
        _description.Reset();
    }

    public void QuitTheGame()
    {
        SceneLoader.Instance.QuitTheGame();
    }
}
