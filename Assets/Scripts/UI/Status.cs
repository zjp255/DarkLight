using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : Quest
{
    public static Status instance;
    private PlayerStatus playerStatus;

    public Text sumATK;
    public Text playerATK;
    public Text extraATK;

    public Text sumDEF;
    public Text playerDEF;
    public Text extraDEF;

    public Text sumSPEED;
    public Text playerSPEED;
    public Text extraSPEED;

    public Text point;

    private GameObject addPointButPanel;
    private GameObject applyBut;
    private GameObject resetBut;
  

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        instance = this;
        
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateStatusPanel();
        addPointButPanel = gameObject.transform.Find("+-Buttons").gameObject;
        resetBut = gameObject.transform.Find("ReSetButton").gameObject;
        applyBut = gameObject.transform.Find("ApplyButton").gameObject;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    new void  FixedUpdate()
    {
        base.FixedUpdate();
        UsePoint();
       
    }

    public void UpdateStatusPanel()
    {
        sumATK.text = "" + (playerStatus.attack + playerStatus.attack_plus);
        playerATK.text = "" + playerStatus.attack;
        extraATK.text = "" + playerStatus.attack_plus;

        sumDEF.text = "" + (playerStatus.def + playerStatus.def_plus);
        playerDEF.text = "" + playerStatus.def;
        extraDEF.text = "" + playerStatus.def_plus;

        sumSPEED.text = "" + (playerStatus.speed + playerStatus.speed_plus);
        playerSPEED.text = "" + playerStatus.speed;
        extraSPEED.text = "" + playerStatus.speed_plus;

        point.text = "" + playerStatus.point_remain;
    }

    void UsePoint()
    {
        if (playerStatus.point_remain > 0)
        {
            addPointButPanel.SetActive(true);
            applyBut.SetActive(true);
            resetBut.SetActive(true);
            
        }
    }

    public void AddATK()
    {
        if (playerStatus.point_remain > 0)
        {
            playerStatus.attack_plus++;
            playerStatus.point_remain--;
        }
        UpdateStatusPanel();
    }

    public void AddDEF()
    {
        if (playerStatus.point_remain > 0)
        {
            playerStatus.def_plus++;
            playerStatus.point_remain--;
        }
        UpdateStatusPanel();
    }

    public void AddSPEED()
    {
        if (playerStatus.point_remain > 0)
        {
            playerStatus.speed_plus++;
            playerStatus.point_remain--;
        }
        UpdateStatusPanel();
    }

    public void ApplyButton()
    {
        playerStatus.attack += playerStatus.attack_plus;
        playerStatus.attack_plus = 0;

        playerStatus.def += playerStatus.def_plus;
        playerStatus.def_plus = 0;

        playerStatus.speed += playerStatus.speed_plus;
        playerStatus.speed_plus = 0;

        UpdateStatusPanel();
        if (playerStatus.point_remain <= 0)
        {
            addPointButPanel.SetActive(false);
            applyBut.SetActive(false);
            resetBut.SetActive(false);
        }

    }

    public void ResetButton()
    {
        playerStatus.point_remain = playerStatus.point_remain + playerStatus.speed_plus + playerStatus.def_plus + playerStatus.attack_plus;

        playerStatus.attack_plus = 0;
        playerStatus.def_plus = 0;
        playerStatus.speed_plus = 0;
        UpdateStatusPanel();
    }
}
