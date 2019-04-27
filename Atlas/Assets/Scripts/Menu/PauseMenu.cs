using System;
using Game;
using Menu.Settings;
using SceneManagement;
using UnityEngine;

namespace Menu
{
	public class PauseMenu : MenuWidget
	{
		[Header("Pause specifics")]
		[SerializeField] private SettingsMenu _settings;

		protected override void InitialiseWidget()
		{
			_settings.OnShow += ExitSetting;
		}

		private void ExitSetting(bool settingsShowing)
		{
			if (!settingsShowing)
				Open();
		}
		
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
				Show(!Displayed);
		}

		public void ShowSettings()
		{
			_settings.Open();
		}
		
		public override void Show(bool display, bool force = false)
		{
			TimeManager.Instance.PauseGame(display);
			base.Show(display, force);
		}

		public void QuitTheGame()
		{
			SceneLoader.Instance.QuitTheGame();
		}
		
	}
}
