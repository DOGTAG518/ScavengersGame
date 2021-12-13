using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UIObject;

    public GameObject BuildOption;

    private void Start()
    {
        GameEvent.OnOpenUIPage += OpenUIPage;

        OpenUIPage(false);
    }

    private void OnDestroy()
    {
        GameEvent.OnOpenUIPage -= OpenUIPage;
    }

    public void ReturnMainPage()
    {
        UIObject.SetActive(true);
        BuildOption.SetActive(false);
    }

    #region Event

    public void OpenUIPage(bool isOn)
    {
        UIObject.SetActive(isOn);
        BuildOption.SetActive(false);
    }

    #endregion

    public void OpenBuildPage()
    {
        UIObject.SetActive(false);
        BuildOption.SetActive(true);
    }

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
