using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.DayNight
{
    public static class TimeManager
    {
        private static List<Object> _pauseAsker = new List<Object>();
        public static bool IsGamePaused => (_pauseAsker.Count > 0);
        private static float _gameSpeed = 1f;

        public static void AskForPause(Object value)
        {
            if (_pauseAsker.Contains(value) || value == null)
                return;
            
            _pauseAsker.Add(value);
            UpdateGameSpeed();
        }

        public static void StopPause(Object value)
        {
            if (!_pauseAsker.Contains(value))
                return;
            
            _pauseAsker.Remove(value);
            UpdateGameSpeed();
        }
    
        private static void UpdateGameSpeed()
        {
            if (IsGamePaused)
                Time.timeScale = 0f;
            else
                Time.timeScale = _gameSpeed;
        }
    }
}
