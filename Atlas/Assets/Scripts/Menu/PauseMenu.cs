using System;
using Game;
using Game.Map.DayNight;
using Game.SavingSystem;
using Menu.Settings;
using SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu
{
	public class PauseMenu : MenuWidget
	{
		[Header("Pause specifics")]
		[SerializeField] private SettingsMenu _settings = null;
        
		protected override void InitialiseWidget()
		{
			_settings.OnShow += ExitSetting;
		}

        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Menu.performed += ctx => OpenCloseMenu(ctx);
            SaveManager.Instance.InputControls.Player.Menu.Enable();
        }

        private void OnDisable()
        {
            SaveManager.Instance.InputControls.Player.Menu.performed -= ctx => OpenCloseMenu(ctx);
            SaveManager.Instance.InputControls.Player.Menu.Disable();
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
			if (display)
				TimeManager.AskForPause(this);
			else
				TimeManager.StopPause(this);
			
			base.Show(display, force);
		}

		public void QuitTheGame()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			SceneLoader.Instance.QuitTheGame();
#endif
		}
		
	}
}
