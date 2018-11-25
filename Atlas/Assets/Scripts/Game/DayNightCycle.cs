using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    /**
     *  Public variables
     **/
    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTime = 0;
    [HideInInspector]
    public float timeMultiplayer = 1f;
    
    /**
     *  Private variables
     **/
    private float _sunInitialIntensity;
    private float _sunUp = 0.23f;
    private float _sunDown = 0.73f;
    private float _latitude = 90;
    private float _longitude = 170;

	// Use this for initialization
	void Start () {
        _sunInitialIntensity = sun.intensity;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSun();

        currentTime += (Time.deltaTime / secondsInFullDay) * timeMultiplayer;

        if (currentTime >= 1)
            currentTime = 0;
	}

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTime * 360f) - _latitude, _longitude, 0);

        float intensityMultiplayer = 1f;

        if (currentTime <= 0.23f || currentTime >= 0.75)
            intensityMultiplayer = 0;
        else if (currentTime <= 0.25f)
            intensityMultiplayer = Mathf.Clamp01((currentTime - _sunUp) * (1 / 0.02f));
        else if (currentTime >= 0.73f)
            intensityMultiplayer = Mathf.Clamp01(1 - ((currentTime - _sunDown) * (1 / 0.02f)));
        sun.intensity = _sunInitialIntensity * intensityMultiplayer;
    }

    public void SetSunUp(float value)
    {
        _sunUp = value;
    }

    public void SetSunDowm(float value)
    {
        _sunDown = value;
    }

    private void SetLatitude(float value)
    {
        _latitude = value;
    }

    public void SetLongitude(float value)
    {
        _longitude = value; 
    }
}
