using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvent
{
    #region UI Event
    public static Action<bool> OnOpenUIPage;
    #endregion

    #region Weather Event

    public static Action<WeatherEnum> OnWeatherChange;

    #endregion


    #region Invoke

    public static void OpenUIPage(bool isOn)
    {
        if (OnOpenUIPage != null)
            OnOpenUIPage.Invoke(isOn);
    }

    public static void WeatherChange(WeatherEnum weather)
    {
        if (OnWeatherChange != null)
            OnWeatherChange.Invoke(weather);
    }

    #endregion
}
