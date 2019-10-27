using System.Collections.Generic;
using UnityEngine;

namespace Game.Map.DayNight
{
    public class TimeManager
    {
        static private List<Object> _pauseAsker = new List<Object>();
        static public bool IsGamePaused => (_pauseAsker.Count > 0);
        static private float _gameSpeed = 1f;

        static public void AskForPause(Object value)
        {
            _pauseAsker.Add(value);
            UpdateGameSpeed();
        }

        static public void StopPause(Object value)
        {
            _pauseAsker.Remove(value);
            UpdateGameSpeed();
        }
    
        static private void UpdateGameSpeed()
        {
            if (IsGamePaused)
                Time.timeScale = 0f;
            else
                Time.timeScale = _gameSpeed;
        }
    }
}
