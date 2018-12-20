using Game;
using Menu;
using UnityEngine;
using InputManagement;
using AtlasEvents;
using AtlasAudio;

public class InventoryHUD : MenuWidget
{
	protected override void InitialiseWidget() {}
	protected override void UpdateButtonState() {}

    [Header("Audio")]
    public Audio OnToggleGUIAudio;
    public AudioEvent OnToggleGUIEvent;

    [Header("Inventory Keys")]
    public InputKey _Inventory;

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
	}

	public void QuitTheGame()
	{
		Application.Quit();
	}
}
