using Game;
using UnityEngine;

namespace Menu
{
	public class PauseMenu : MenuWidget
	{
		protected override void InitialiseWidget() {}
		protected override void UpdateButtonState() {}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
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
}
