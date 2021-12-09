using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    #region PlayerStats

    public const float DEFAULTHEALTH = 100;             // 플레이어의 기본 체력
    public const float DEFAULTSTAMINA = 100;            // 플레이어의 기본 스테미너
    public const float DEFAULTTHIRSTY = 100;            // 기본 목마름(수분양)
    public const float DEFAULTHUNGRY = 100;             // 기본 배고픔(포만감)
    public const float DEFAULTMENTAL = 100;             // 기본 정신력

    public const int MAXHEALTH = 200;                   // 최대 체력
    public const int MAXSTAMINA = 150;                  // 최대 스테미너
    public const int MAXMENTAL = 150;                   // 최대 정신력

    #endregion

    #region PlayerStatCalc

    public const float DEFAULTRESTTIME = 1;
    public const float DEFAULTCONSUMESTAMINA = 10;      // 기본 소모 스테미너

    #endregion
}
