using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    public UnityEvent<bool> OnOpenUIPage;

    public void OpenUIPage(bool isOn)
    {
        if (OnOpenUIPage != null)
            OnOpenUIPage.Invoke(isOn);
    }
}
