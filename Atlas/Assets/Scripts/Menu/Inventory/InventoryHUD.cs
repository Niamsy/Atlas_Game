using Game;
using Menu;
using UnityEngine;
using InputManagement;
using AtlasEvents;
using AtlasAudio;
using Menu.Inventory.ItemDescription;

public class InventoryHUD : MenuWidget
{
	protected override void InitialiseWidget()
	{
	}

	protected override void UpdateButtonState()
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

	public override void Show(bool value)
	{
		TimeManager.Instance.PauseGame(value);
		base.Show(value);
		_description.Reset();
	}

	public void QuitTheGame()
	{
		Application.Quit();
	}
}
