using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

	public float currentHour;
	public float currentMinute;
	public float minutesPerSecond;

	public float sunriseHour;
	public float sunsetHour;

	public bool isSunny = false;

	public GameObject daylight;
	public GameObject nightlight;

	// Use this for initialization
	void Start () 
	{
		DoNight();
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentMinute += Time.deltaTime * minutesPerSecond;
		if(currentMinute >= 60f)
		{
			currentHour++;
			currentMinute = 0f;
			if(currentHour >= 24)
			{
				currentHour = 0f;
			}
		}

		if(isSunny)
		{
			if(currentHour < sunriseHour || currentHour >= sunsetHour)
			{
				DoNight();
			}
		}
		else
		{
			if(currentHour >= sunriseHour && currentHour < sunsetHour)
			{
				DoDay();
			}
		}
	}

	void DoDay()
	{
		daylight.SetActive(true);
		nightlight.SetActive(false);
		isSunny = true;
	}

	void DoNight()
	{
		daylight.SetActive(false);
		nightlight.SetActive(true);
		isSunny = false;
	}
}
