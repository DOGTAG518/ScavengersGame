using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    static WorldManager instance = null;

    public WorldManager Instance
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

                if(hour >= 24)
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

        totalTimeTemp += (hour * 60 * 60) + (minute * 60) + second;

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
            }

            if(hour >= 18)
                totalTimeTemp = ((hour - 18) * 60 * 60) + (minute * 60) + second;
            else
                totalTimeTemp = (6 * 60 * 60) - ((hour * 60 * 60) + (minute * 60) + second);

            float fogDestiny = Mathf.Lerp(0, 0.3f, totalTimeTemp / (6 * 60 * 60));
            RenderSettings.fogDensity = fogDestiny;

        }
        else
        {
            if (RenderSettings.fog != false)
                RenderSettings.fog = false;
        }
    }

    #endregion
}
