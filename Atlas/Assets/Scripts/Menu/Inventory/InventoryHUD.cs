using Game;
using Menu;
using UnityEngine;
using InputManagement;
using AtlasEvents;
using AtlasAudio;
using Menu.Inventory.ItemDescription;
using SceneManagement;

public class InventoryHUD : MenuWidget
{
	protected override void InitialiseWidget()
	{
	}

	[Header("Audio")] public Audio OnToggleGUIAudio;
	public AudioEvent OnToggleGUIEvent;

	[Header("Inventory Keys")] public InputKey _Inventory;

	[SerializeField] private ItemDescriptionHUD _description;

	private void Update()
	{
		if (_Inventory.GetDown()) {
            Show(!Displayed);
            if (OnToggleGUIAudio && OnToggleGUIEvent)
            {
                OnToggleGUIEvent.Raise(OnToggleGUIAudio, null);
            }
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
