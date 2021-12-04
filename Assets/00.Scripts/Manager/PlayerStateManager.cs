using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    static PlayerStateManager instance = null;

    public static PlayerStateManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PlayerStateManager").AddComponent<PlayerStateManager>();
            }

            return instance;
        }
    }

    public PlayerState playerState = PlayerState.Play;

    private void Start()
    {
        GameEvent.OnOpenUIPage += OpenUIPage;

        OpenUIPage(false);
    }

    private void OnDestroy()
    {
        GameEvent.OnOpenUIPage -= OpenUIPage;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOpenUI();
        }
    }

    // 위치 옮겨야함. 임시(이벤트로 관리해야한다)
    void OnOpenUI()
    {
        switch(playerState)
        {
            case PlayerState.Play:
                playerState = PlayerState.UI;
                GameEvent.OpenUIPage(true);
                break;
            case PlayerState.UI:
                playerState = PlayerState.Play;
                GameEvent.OpenUIPage(false);
                break;
        }
    }

    #region Event

    void OpenUIPage(bool isOn)
    {
        Cursor.visible = isOn;

        if (isOn)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion
}
