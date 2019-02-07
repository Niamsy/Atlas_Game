using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapScript : MonoBehaviour
{
    public Image Rain;
    public Image Cloud;
    public Image Sunshine;
    public Image Night;
    public Text DayTime;
    public Text Month;
    public CalendarManager calendar;
    public Image TimeBar;
    public Image DateBar;
    public Image ClimateBar;


    public enum WeatherState
    {
        SUN,
        RAIN,
        CLOUD,
        NIGHT
    }

    private WeatherState currentWeatherState = WeatherState.NIGHT;
    private int CurrentDay = -1;

    private enum Season
    {
        WINTER,
        SPRING,
        SUMMER,
        AUTUMN
    }

    private Color WinterColor = new Color32(64, 89, 128, 255);
    private Color SpringColor = new Color32(100, 125, 95, 255);
    private Color SummerColor = new Color32(167, 82, 99, 255);
    private Color AutumnColor = new Color32(168, 128, 59, 255);

    private string[] monthText =
    {
        "JAN",
        "FEB",
        "MAR",
        "APR",
        "MAY",
        "JUN",
        "JUL",
        "AUGT",
        "SEP",
        "OCT",
        "NOV",
        "DEC"
    };
    
    // Start is called before the first frame update
    void Start()
    {
        setWeather(WeatherState.SUN);
        updateDate();
    }

    // Update is called once per frame
    void Update()
    {
        DayTime.text = calendar.Hours.ToString("D2") + " : " + calendar.Minutes.ToString("D2");
        if (calendar.Hours >= 6 && calendar.Hours <= 19)
        {
            setWeather(WeatherState.SUN);
        }
        else
        {
            setWeather(WeatherState.NIGHT);
        }
        if (calendar.Day != CurrentDay) {
            updateDate();
        }
    }

    private void setWeather(WeatherState state)
    {
        if (currentWeatherState == state)
        {
            return;
        }
        currentWeatherState = state;
        Rain.enabled = false;
        Sunshine.enabled = false;
        Night.enabled = false;
        Cloud.enabled = false;
        switch (state)
        {
            case WeatherState.SUN:
                Sunshine.enabled = true;
                break;
            case WeatherState.RAIN:
                Rain.enabled = false;
                break;
            case WeatherState.NIGHT:
                Night.enabled = true;
                break;
            case WeatherState.CLOUD:
                Cloud.enabled = false;
                break;
        }
    }

    private void updateDate()
    {
        if (CurrentDay == calendar.Day)
        {
            return;
        }
        
        switch (calendar.Month)
        {
            case 1:
            case 2:
            case 12:
                TimeBar.color = WinterColor;
                DateBar.color = WinterColor;
                ClimateBar.color = WinterColor;
                break;
            case 3:
            case 4:
            case 5:
                TimeBar.color = SpringColor;
                DateBar.color = SpringColor;
                ClimateBar.color = SpringColor;
                break;
            case 6:
            case 7:
            case 8:
                TimeBar.color = SummerColor;
                DateBar.color = SummerColor;
                ClimateBar.color = SummerColor;
                break;
            case 9:
            case 10:
            case 11:
                TimeBar.color = AutumnColor;
                DateBar.color = AutumnColor;
                ClimateBar.color = AutumnColor;
                break;
        }
        CurrentDay = calendar.Day;
        Month.text = monthText[calendar.Month - 1] + " - " + calendar.Day.ToString("D2") + " - " + calendar.Year.ToString("D4");
    }
}