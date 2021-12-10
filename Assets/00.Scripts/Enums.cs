using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 현재 플레이어의 상태를 나타내는 enum
/// </summary>

public enum PlayerState
{
    Play,       // 일반적인 Play상태
    UI,         // UI창을 연 상태
    Build,      // 건축중인 상태

    Max,
}

public enum WeatherEnum
{
    Sunny,
    Rain,
}
