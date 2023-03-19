using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BarNpc : Npc
{
    public static BarNpc instance;
    public GameObject questUI;
    public Text testInfo;
    public GameObject acceptBut;
    public GameObject cancleBut;
    public GameObject okBut;
    public Quest quest;

    private PlayerStatus playerStatus;
    private bool isInTest = false;
    public int killCount = 0;//杀死目标数

    private void Start()
    {
        instance = this;
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            ShowQuest();
            if (!isInTest)
            {
                ShowTestInfo();
            }
            else
            {
                ShowTestProgress();
            }
        }
    }
    void ShowQuest()
    {
        questUI.SetActive(true);
    }

    void ShowTestInfo()
    {
        testInfo.text = "任务：\n讨伐10头小野狼\n\n奖励：\n1000金币";
        acceptBut.SetActive(true);
        cancleBut.SetActive(true);
        okBut.SetActive(false);
    }

    void ShowTestProgress()
    {
        testInfo.text = "任务：\n已讨伐" + killCount +"/10头小野狼\n\n奖励：\n1000金币";
        acceptBut.SetActive(false);
        cancleBut.SetActive(false);
        okBut.SetActive(true);
    }

    public void OnAcceptButDown()
    {
        isInTest = true;
        ShowTestProgress();
    }

    public void OnOkBtnClick()
    {
        if (killCount >= 10)
        {//完成任务 
            playerStatus.GetCoin(1000);
            killCount = 0;
            isInTest = false;
            ShowTestInfo();
        }
        else 
        {
            quest.CancleB();
        }
    }

    public void OnKillWolf()
    {
        if (isInTest == true)
        {
            killCount++;
        }
    }
}
