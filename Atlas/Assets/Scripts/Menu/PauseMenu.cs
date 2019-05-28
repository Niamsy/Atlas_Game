using System;
using Game;
using Menu.Settings;
using SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private void OnEnable()
        {
            GameControl.Instance.InputControls.Player.Menu.performed += ctx => OpenCloseMenu(ctx);
            GameControl.Instance.InputControls.Player.Menu.Enable();
        }

        private void OnDisable()
        {
            GameControl.Instance.InputControls.Player.Menu.performed -= ctx => OpenCloseMenu(ctx);
            GameControl.Instance.InputControls.Player.Menu.Disable();
        }

        private void OpenCloseMenu(InputAction.CallbackContext ctx)
        {
            Show(!Displayed);
        }

        private void ExitSetting(bool settingsShowing)
		{
            if (!settingsShowing)
				Open();
		}

		public void ShowSettings()
		{
			_settings.gameObject.SetActive(true);
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
