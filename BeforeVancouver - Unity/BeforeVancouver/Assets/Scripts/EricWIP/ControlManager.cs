using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
	#region Singleton
	private static ControlManager _instance = null;
	public static ControlManager instance
	{
		get
		{
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<ControlManager>();
			return _instance;
		}
	}

	void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			_instance = this;
		}
		DontDestroyOnLoad(transform.gameObject);
	}

	void OnApplicationQuit()
	{
		_instance = null;
		DestroyImmediate(gameObject);
	}
	#endregion

	public YearData[] yearData;
	public int currentYearIndex;// { get; private set; }

	public Material daySkyboxMat;
	public Material nightSkyboxMat;

	public Color dayLightColor;
	public Color nightLightColor;

	public Color daySkyColor;
	public Color nightSkyColor;

	public float dayLightIntensity = 0.4f;
	public float nightLightIntensity = 0.1f;

	public Light lightComponent;
	public bool isDay = true;

	private void Start()
	{
		SetYear(1920);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Y))
			SetYear();

		if (Input.GetKeyUp(KeyCode.N))
			ToggleDayNight();
	}

	private void SetYear(bool isIncrement = true)
	{
		currentYearIndex = isIncrement ? (currentYearIndex >= yearData.Length - 1 ? 0 : currentYearIndex + 1) : (currentYearIndex > 0 ? currentYearIndex - 1 : yearData.Length - 1);
		SendYearDataMissive(yearData[currentYearIndex]);
	}

	private void SetYear(int year)
	{
		for (int i = 0; i < yearData.Length; i++)
		{
			if (year == yearData[i].year)
			{
				currentYearIndex = i;
			}
		}
		currentYearIndex = 0;
		SendYearDataMissive(yearData[currentYearIndex]);
	}

	private void SendYearDataMissive(YearData data)
	{
		YearDataMissive missive = new YearDataMissive();
		missive.data = data;
		Missive.Send(missive);
	}

	private void ToggleDayNight()
	{
		isDay = !isDay;
		RenderSettings.skybox = isDay ? daySkyboxMat: nightSkyboxMat;
		RenderSettings.ambientLight = isDay ? daySkyColor : nightSkyColor;
		RenderSettings.ambientIntensity = isDay ? dayLightIntensity : nightLightIntensity;
		lightComponent.color = isDay ? dayLightColor : nightLightColor;
	}
}



[System.Serializable]
public class YearData
{
	public int year;
}

public class YearDataMissive : Missive
{
	public YearData data;
}
