using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    static WorldManager instance = null;

    public static WorldManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("World Manager").AddComponent<WorldManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;

        nowWeather = WeatherEnum.Sunny;
    }

    private void Update()
    {
        CalcWorldTime();
        DayAndNight();
    }

    #region Time

    public int day, minute = 0;
    public int hour = 6;
    public float second = 0;

    bool isNight = false;

    void CalcWorldTime()
    {
        second += Time.unscaledDeltaTime * 60;

        if(second >= 60)
        {
            second = 0;
            minute++;

            if(minute >= 60)
            {
                minute = 0;
                hour++;

                CheckWeather();

                if (hour >= 24)
                {
                    hour = 0;
                    day++;
                }
            }
        }
    }

    void DayAndNight()
    {
        // 시간, 분, 초로 태양의 위치 계산
        float totalTimeTemp = 0;

        totalTimeTemp += GetTotalTime();

        var rotationTemp = transform.eulerAngles;
        rotationTemp.x = ((totalTimeTemp / (24 * 60 * 60)) * 360) - 90f;
        rotationTemp.y = -30f;
        rotationTemp.z = 0f;

        transform.eulerAngles = rotationTemp;

        // fog 설정
        if (hour >= 18 || hour < 6)
            isNight = true;
        else
            isNight = false;

        if(isNight)
        {
            if (RenderSettings.fog != true)
            {
                RenderSettings.fogColor = Color.black;
                RenderSettings.fog = true;
                RenderSettings.fogMode = FogMode.Exponential;
            }

            if(hour >= 18)
                totalTimeTemp = ((hour - 18) * 60 * 60) + (minute * 60) + second;
            else
                totalTimeTemp = (6 * 60 * 60) - ((hour * 60 * 60) + (minute * 60) + second);

            float fogDestiny = Mathf.Lerp(0, 0.5f, totalTimeTemp / (6 * 60 * 60));
            RenderSettings.fogDensity = fogDestiny;

        }
        else
        {
            if (RenderSettings.fog != false)
            {
                RenderSettings.fogMode = FogMode.Exponential;
                RenderSettings.fog = false;
            }
        }
    }

    public float GetTotalTime()
    {
        return (hour * 60 * 60) + (minute * 60) + second;
    }

    public bool GetIsNight()
    {
        return isNight;
    }

    #endregion

    #region Weather

    int rainStackhour = 0;
    WeatherEnum nowWeather;

    void CheckWeather()
    {
        rainStackhour++;
        float rainPercent = 0;

        switch(nowWeather)
        {
            case WeatherEnum.Sunny:
                rainPercent = 5 + rainStackhour;
                break;
            case WeatherEnum.Rain:
                rainPercent = 30;
                break;
        }

        var randomvalue = Random.Range(0f, 100f);

        if(randomvalue < rainPercent)
        {
            GameEvent.WeatherChange(WeatherEnum.Rain);
            rainStackhour = 0;
        }
        else
            GameEvent.WeatherChange(WeatherEnum.Sunny);
    }

    #endregion
}
