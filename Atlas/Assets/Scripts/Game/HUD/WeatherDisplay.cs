using UnityEngine;
using UnityEngine.UI;

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
            SetWeather(_displayedValue);
        }

        private void SetWeather(Weather weather)
        {
            _displayedValue = weather;

            int x = 0;
            foreach (var icon in _icons)
            {
                icon.gameObject.SetActive(x == (int)weather);
                x++;
            }
        }
        
    }
}
