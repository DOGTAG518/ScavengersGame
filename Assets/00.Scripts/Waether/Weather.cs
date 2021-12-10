using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public GameObject rainObj;

    // Start is called before the first frame update
    void Start()
    {
        GameEvent.OnWeatherChange += WeatherChange;

        rainObj.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvent.OnWeatherChange -= WeatherChange;
    }

    void WeatherChange(WeatherEnum weather)
    {
        switch(weather)
        {
            case WeatherEnum.Sunny:
                rainObj.SetActive(false);
                break;
            case WeatherEnum.Rain:
                rainObj.SetActive(true);
                break;
        }
    }
}
