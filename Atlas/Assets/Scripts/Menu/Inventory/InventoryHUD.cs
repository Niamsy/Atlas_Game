using Game;
using Menu;
using UnityEngine;
using InputManagement;

public class InventoryHUD : MenuWidget
{
	protected override void InitialiseWidget() {}
	protected override void UpdateButtonState() {}

    [Header("Inventory Keys")]
    public InputKey _Inventory;

	private void Update()
	{
		if (_Inventory.GetDown())
			Show(!Displayed);
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
