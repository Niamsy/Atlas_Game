using Game;
using Menu;
using UnityEngine;

public class InventoryHUD : MenuWidget
{
	protected override void InitialiseWidget() {}
	protected override void UpdateButtonState() {}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
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
