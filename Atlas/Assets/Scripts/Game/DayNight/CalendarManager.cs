using UnityEngine;

public class CalendarManager : MonoBehaviour {

    #region Public Variables
    [Range(0, 23)]
    public int Hours = 0;
    [Range(0, 59)]
    public int Minutes = 0;
    [Range(0, 59)]
    public int Seconds = 0;
    [Range(1, 31)]
    public int Day = 1;
    [Range(1, 12)]
    public int Month = 1;    
    public int Year = 0;
    public bool IsReal = false;

    #endregion

    #region Private Variables

    #endregion

    // Use this for initialization
    void Start () {
        if (IsReal)
        {
            SetDateToCurrentTime();
        }
	}
	
    private void SetDateToCurrentTime()
    {
        System.DateTime date = System.DateTime.Now;
        Day = date.Day;
        Month = date.Month;
        Year = date.Year;
        Hours = date.Hour;
        Minutes = date.Minute;
        Seconds = date.Second;
    }

    public void NextDay()
    {
        ++Day;
        if (Day > 30)
        {
            Day = 1;
            ++Month;
            if (Month > 12)
            {
                Month = 1;
                ++Year;
            }
        }
    }

    public int GetSeconds()
    {
        return Seconds;
    }

    public int GetMinutes()
    {
        return Minutes;
    }

    public int GetHours()
    {
        return Hours;
    }

    public void SetTime(int newTime)
    {
        Seconds = newTime % 60;
        Minutes = (newTime / 60) % 60;
        Hours = (newTime / 3600) % 24;
    }
}
