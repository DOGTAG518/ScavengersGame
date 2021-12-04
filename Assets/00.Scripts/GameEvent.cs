using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvent
{
    public static Action<bool> OnOpenUIPage;

    #region Invoke

    public static void OpenUIPage(bool isOn)
    {
        if (OnOpenUIPage != null)
            OnOpenUIPage.Invoke(isOn);
    }


    #endregion
}
