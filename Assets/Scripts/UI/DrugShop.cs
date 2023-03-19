using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugShop : Quest
{
    public GameObject panel;//输入数量和确认购买的panel
    private int id = 0;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void BuyDrug()
    {
        panel.SetActive(true);
    }

    public void Buy1()
    {
        id = 1001;
        BuyDrug();
    }

    public void Buy2()
    {
        id = 1002;
        BuyDrug();
    }

    public void Buy3()
    {
        id = 1003;
        BuyDrug();
    }

    public void Cancel()
    {
        panel.SetActive(false);
    }

    public void Buy()
    {
        int count = int.Parse(panel.transform.Find("BuyImage").Find("InputField").Find("Text").GetComponent<Text>().text);

        int money = count * ObjectsInfo._instance.GetObjectInfo(id).price_buy;

        PlayerStatus playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        GameObject buyImage = panel.transform.Find("BuySatueImage").gameObject;
        if (playerStatus.coin >= money)
        {
            playerStatus.coin -= money;
            buyImage.SetActive(true);
            buyImage.transform.Find("Text").GetComponent<Text>().text = "购买成功";
            for (; count > 0; count--)
                Inventory.instance.GetId(id);
        }
        else 
        {
            buyImage.SetActive(true);
            buyImage.transform.Find("Text").GetComponent<Text>().text = "购买失败";
        }
    }

    public void OkButton()
    {
        panel.transform.Find("BuySatueImage").gameObject.SetActive(false);
        panel.SetActive(false);
    }
}
