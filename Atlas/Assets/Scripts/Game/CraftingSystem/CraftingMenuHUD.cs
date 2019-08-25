using AtlasAudio;
using AtlasEvents;
using Game;
using Game.SavingSystem;
using InputManagement;
using Menu.Inventory.ItemDescription;
using SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingMenuHUD : Menu.MenuWidget
{
    protected override void InitialiseWidget()
    {
    }

    [Header("Audio")]
    public Audio OnToggleGUIAudio = null;
    public AudioEvent OnToggleGUIEvent = null;

    [SerializeField] private GameObject _descriptionGameObject = null;
    private ItemDescriptionHUD _description = null;

    private void OnEnable()
    {
        if (_descriptionGameObject)
        {
            _description = _descriptionGameObject.GetComponent<ItemDescriptionHUD>();
        }
    }

    private void OnDisable()
    {
        SaveManager.Instance.InputControls.Player.Interact.performed -= OpenCloseCraftingMenu;
    }

    private void OpenCloseCraftingMenu(InputAction.CallbackContext obj)
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
        if (_description) _description.Reset();
        if (display)
        {
            SaveManager.Instance.InputControls.Player.Interact.performed += OpenCloseCraftingMenu;
        }
        else
        {
            SaveManager.Instance.InputControls.Player.Interact.performed -= OpenCloseCraftingMenu;
        }
    }

    public void QuitTheGame()
    {
        SceneLoader.Instance.QuitTheGame();
    }
}
