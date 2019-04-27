﻿using UnityEngine;
using System;
using Game;
using Game.DayNight;

public class DayNightSaver : MonoBehaviour {

    public CalendarManager calendar;

    private float _LastSavedTime = 0f;

    [Serializable]
    public struct SaveDate
    {
        public int seconds;
        public int minutes;
        public int hours;
        public int days;
        public int months;
        public int years;
    }

    [Serializable]
    public struct CalendarData
    {
        private SaveDate date;

        public void SetFromDate(int seconds, int minutes, int hours, int days, int months, int years)
        {
            date.seconds = seconds;
            date.minutes = minutes;
            date.hours = hours;
            date.days = days;
            date.months = months;
            date.years = years;
        }

        public SaveDate GetDate()
        {
            return date;
        }
    }

    public void Save()
    {
        GameControl.Control.GameData.CalendarData.SetFromDate(calendar.Seconds, calendar.Minutes, calendar.Hours, calendar.Day, calendar.Month, calendar.Year);
    }

    private void Awake()
    {
        calendar.Seconds = GameControl.Control.GameData.CalendarData.GetDate().seconds;
        calendar.Minutes = GameControl.Control.GameData.CalendarData.GetDate().minutes;
        calendar.Hours = GameControl.Control.GameData.CalendarData.GetDate().hours;
        calendar.Day = GameControl.Control.GameData.CalendarData.GetDate().days;
        calendar.Month = GameControl.Control.GameData.CalendarData.GetDate().months;
        calendar.Year = GameControl.Control.GameData.CalendarData.GetDate().years;
        _LastSavedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _LastSavedTime > 1.0f)
        {
            Save();
            _LastSavedTime = Time.time;
        }
    }
}
