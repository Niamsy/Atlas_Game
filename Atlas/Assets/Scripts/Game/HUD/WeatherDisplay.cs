using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game.HUD
{
    public enum Weather
    {
        Sunny = 0,
        Rain = 1,
        Clouded = 2,
        Night = 3
    }
    
    public class WeatherDisplay : MonoBehaviour
    {
        [SerializeField] private Weather _displayedValue = 0;

        [SerializeField] private Image[] _icons = null;

        private void Start()
        {
            ChangeWeather((int)_displayedValue);
        }

        public void ChangeWeather(int weather)
        {
            _displayedValue = (Weather)weather;

            int x = 0;
            foreach (var icon in _icons)
            {
                icon.gameObject.SetActive(x == weather);
                x++;
            }
        }
        
    }
}
