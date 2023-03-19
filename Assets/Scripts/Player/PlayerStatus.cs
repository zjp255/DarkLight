using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    public ApplicationType heroType;
    [Header("当前状态")]
    public string playerName = "默认";
    public int nowHp = 100;
    public int nowMp = 100;

    public int grade = 1;
    public int hp = 100;
    public int mp = 100;
    public int coin = 200;

    public int attack = 20;
    public int attack_plus = 0;

    public int def = 20;
    public int def_plus = 0;

    public int speed = 20;
    public int speed_plus = 0;

    public int point_remain = 0;//剩余点数

    private int hptemp;
    private int mptemp;
    private int gradetemp;

    public int nowExp;
    public int targetExp;
    private void Start()
    {
        hptemp = nowHp;
        mptemp = nowMp;
        gradetemp = grade;
        targetExp = 100;
        playerName = PlayerPrefs.GetString("name");
    }

    private void FixedUpdate()
    {
        if (nowExp >= targetExp)
            letThatLevelUp();
    }
    public void GetCoin(int count)
    {
        coin += count;
    }

    private void Update()
    {
        if (hptemp != nowHp || mptemp != nowMp || gradetemp != grade)
        {
            PlayerStatusPanel.instance.UpdateShow();
            hptemp = nowHp;
            mptemp = nowMp;
            gradetemp = grade;
        }
    }

    public void letThatLevelUp()
    {
        while (nowExp >= targetExp)
        {
            nowExp -= targetExp;
            grade++;
           
            targetExp = 70 + grade * 30;
        }
    }

    public void GetExp(int exp)
    {
        nowExp += exp;
    }
}
