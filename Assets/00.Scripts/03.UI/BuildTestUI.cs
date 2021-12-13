using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTestUI : MonoBehaviour
{
    UIManager uIManager = null;

    private void Start()
    {
        uIManager = GetComponentInParent<UIManager>();
    }

    public void BuildBridge()
    {
        PlayerStateManager.Instance.playerState = PlayerState.Build;
        GameEvent.OpenUIPage(false);
    }

    public void BuildBranch()
    {
        PlayerStateManager.Instance.playerState = PlayerState.Build;
        GameEvent.OpenUIPage(false);
    }

    public void OnBack()
    {
        uIManager.ReturnMainPage();
    }
}
