using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDes : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text desTest;
    public Text priceText;
    private int id;
    private ObjectInfo info;
    public void SetInt(int id)
    {
        this.id = id;
        info = ObjectsInfo._instance.GetObjectInfo(id);
        icon.sprite = ImageManager._instance.GetSpriteOfIcon(info.icon_name);
        nameText.text = info.name;
        string atkdes = "+ATK " + info.atk + " ";
        string defdes = "+DEF " + info.def + " ";
        string speeddes = "+SPEED " + info.speed + " ";
        string des = "";
        if (info.atk != 0)
        {
            des += atkdes;
        }
        if (info.def != 0)
        {
            des += defdes;
        }
        if (info.speed != 0)
        {
            des += speeddes;
        }
        desTest.text = des;
        priceText.text = info.price_buy.ToString();
    }

    public void BuyThis()
    {
        PlayerStatus ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        if ( ps.coin >= info.price_sell)
        {
            ps.coin -= info.price_sell;
            Inventory.instance.GetId(id);
        }

    }
}
