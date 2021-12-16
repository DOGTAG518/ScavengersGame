using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float health = 0;
    private float nowHealth = 0;

    private float stamina = 0;
    private int staminaLevel = 0;
    [SerializeField]
    private float nowStamina = 0;
    [SerializeField]
    private float restTime = 0;

    private float thirsty = 0;
    private float nowThristy = 0;
    private float hungry = 0;
    private float nowHungry = 0;

    private float mental = 0;
    private int mentalLevel = 0;
    private float nowMental = 0;

    private void Start()
    {
        InitializePlayerStats();
    }

    #region Setting

    void InitializePlayerStats()
    {
        InitializeHealth();
        InitializeStamina();

        thirsty = Define.DEFAULTTHIRSTY;
        nowThristy = thirsty;
        hungry = Define.DEFAULTHUNGRY;
        nowHungry = hungry;

        InitalizeMental();
    }

    void InitializeHealth()
    {
        var healthTemp = Define.DEFAULTHEALTH + WorldManager.Instance.day;

        if (healthTemp < Define.MAXHEALTH)
            health = healthTemp;
        else
            health = Define.MAXHEALTH;

        nowHealth = health;
    }

    void InitializeStamina()
    {
        stamina = Define.DEFAULTSTAMINA + (5 * staminaLevel);

        nowStamina = stamina;
    }

    #endregion

    void InitalizeMental()
    {

    }

    #region Calc

    public bool IsCanRun()
    {
        if (nowStamina > 0)
            return true;
        else
            return false;
    }

    public bool IsCanJump()
    {
        if (nowStamina >= Define.DEFAULTCONSUMESTAMINA)
            return true;
        else
            return false;
    }

    public void NowRun()
    {
        nowStamina -= Define.DEFAULTCONSUMESTAMINA* Time.deltaTime;

        restTime = 0;
    }

    public void Jump()
    {
        nowStamina -= Define.DEFAULTCONSUMESTAMINA;

        restTime = 0;
    }

    public void Rest()
    {
        if (restTime >= Define.DEFAULTRESTTIME)
        {
            nowStamina += (stamina * 0.05f * Time.deltaTime);
            if (nowStamina > stamina)
                nowStamina = stamina;
        }
        else
            restTime += Time.deltaTime;
    }

    #endregion
}
