using UnityEngine;

namespace Game
{
    public class TimeManager
    {
        #region Singleton
        private static TimeManager _instance = null;
        public static TimeManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimeManager();
                return (_instance);
            }
        }
        #endregion
    
        private bool _gamePaused = false;
        public bool IsGamePaused    { get { return (_gamePaused); } }
        
        private float _gameSpeed = 1f;
    
        public void PauseGame(bool paused)
        {
            _gamePaused = paused;
            UpdateGameSpeed();
        }

        private void UpdateGameSpeed()
        {
            if (_gamePaused)
                Time.timeScale = 0f;
            else
                Time.timeScale = _gameSpeed;
        }
    }
}
