using System;
using Tools;
using UnityEngine;

namespace Game.DayNight
{
    [Serializable]
    public class Date
    {
        [Range(0, 23)]
        public int Hours = 0;
        public static readonly int HourPerDay = 24;
                
        [Range(0, 59)]
        public int Minutes = 0;
        public static readonly int MinutesPerHour = 60;
        
        [Range(0, 59)]
        public float Seconds = 0;
        public int SecondsInt => Mathf.FloorToInt(Seconds);
        public static readonly int SecondsPerMinute = 60;
        public static readonly int SecondsPerHour = SecondsPerMinute * MinutesPerHour;
        
        [Range(1, 31)]
        public int Day = 1;
        public static readonly int DayPerMonth = 30;
        
        [Range(1, 12)]
        public int Month = 1;    
        public static readonly int MonthPerYear = 12;

        public delegate void DayChanged();
        public event DayChanged OnDayChanged;

        public float DayAdvancement
        {
            get
            {
                float ret = Hours;
                ret += (float)Minutes / MinutesPerHour;
                ret += Seconds / (float) SecondsPerHour;
                return (ret);
            }
        }

        public int Year = 0;

        public Date() {}
        
        public Date(int day, int month, int year, int hour, int minutes, int seconds)
        {
            Day = day;
            Month = month;
            Year = year;
            Hours = hour;
            Minutes = minutes;
            Seconds = seconds;
            
        }
        
        public static Date FromRealDate()
        {
            DateTime date = DateTime.Now;
            return new Date(date.Day, date.Month, date.Year, date.Hour, date.Minute, date.Second);
        }

        public int TotalTime
        {
            get { return ((Hours * MinutesPerHour + Minutes) * SecondsPerMinute + SecondsInt); }
            set
            {
                Seconds = value % SecondsPerMinute;
                Minutes = (value / SecondsPerMinute) % MinutesPerHour;
                Hours = (value / SecondsPerHour) % HourPerDay;
            }
        }

        public void NextDay()
        {
            ++Day;
            if (Day > DayPerMonth)
            {
                Day = 1;
                ++Month;
                if (Month > MonthPerYear)
                {
                    Month = 1;
                    ++Year;
                }
            }
            if (OnDayChanged != null)
                OnDayChanged();
        }

        public static Date operator +(Date a, Date b) // Don't use it
        {
            Date value = new Date();
            int ret = MyMath.AddWithRetenueFloat(ref value.Seconds, a.Seconds + b.Seconds, SecondsPerMinute);
            ret = MyMath.AddWithRetenue(ref value.Minutes, a.Minutes + b.Minutes + ret, MinutesPerHour);
            ret = MyMath.AddWithRetenue(ref value.Hours, a.Hours + b.Hours + ret, HourPerDay);
            ret = MyMath.AddWithRetenue(ref value.Day, a.Day + b.Day + ret, DayPerMonth);
            ret = MyMath.AddWithRetenue(ref value.Month, a.Month + b.Month + ret, MonthPerYear);
            
           
            value.Year = ret + a.Year + b.Year;
            return (value);
        }
        
        public void AddSeconds(float addedTime)
        {
            int ret = 0;
            float fRet = 0;

            ret = MyMath.AddWithRetenueFloat(ref Seconds, addedTime + fRet, SecondsPerMinute);
            ret = MyMath.AddWithRetenue(ref Minutes, ret, MinutesPerHour);
            ret = MyMath.AddWithRetenue(ref Hours, ret, HourPerDay);
            if (ret > 0) // If ret >0 then it will be next day
            {
                if (OnDayChanged != null)
                    OnDayChanged();
            }
            ret = MyMath.AddWithRetenue(ref Day, ret, DayPerMonth);
            ret = MyMath.AddWithRetenue(ref Month, ret, MonthPerYear);
            Year += ret;
        }
    }
    
    public class CalendarManager : MonoBehaviour
    {
        public static CalendarManager Instance { private set; get; }
    
        #region Public Variables
        public Date ActualDate;
        public bool IsReal = false;
        
        public int DeltaTimeMultiplier = 1;
        #endregion

        #region Private Variables
        [HideInInspector]
        public float TimeMultiplier = 1f;
        #endregion

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Start ()
        {
            if (IsReal)
                ActualDate = Date.FromRealDate();
        }

        private void Update()
        {
            ActualDate.AddSeconds((Time.deltaTime * DeltaTimeMultiplier) * TimeMultiplier);
        }

    }
}
