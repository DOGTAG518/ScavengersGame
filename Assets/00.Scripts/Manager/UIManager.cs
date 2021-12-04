using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIObject;

    private void Start()
    {
        GameEvent.OnOpenUIPage += OpenUIPage;

        OpenUIPage(false);
    }

    private void OnDestroy()
    {
        GameEvent.OnOpenUIPage -= OpenUIPage;
    }

    #region Event

    public void OpenUIPage(bool isOn)
    {
        UIObject.SetActive(isOn);
    }

    #endregion

    public void OnCloseUI()
    {
        GameEvent.OpenUIPage(false);
    }

    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
