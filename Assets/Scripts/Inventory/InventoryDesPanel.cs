using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDesPanel : MonoBehaviour
{
    public GameObject itemDesPanel;
    public Text txt;
    public static InventoryDesPanel instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItemDes(int id,Vector3 position)
    {
        position = new Vector3(position.x + itemDesPanel.GetComponent<RectTransform>().sizeDelta.x / 2, position.y - itemDesPanel.GetComponent<RectTransform>().sizeDelta.y / 4, position.z);
        itemDesPanel.transform.position = position;
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfo(id);
        string des = "";
        switch (info.type)
        {
            case ObjectType.Drug:
                GetDesOfDrug(out des,info);
                break;
            case ObjectType.Equip:
                GetDesOfEquip(out des, info);
                break;
        }
        txt.text = des;
        itemDesPanel.SetActive(true);
    }
    public void CloseItemDes()
    {

        itemDesPanel.SetActive(false);
    }

    void GetDesOfDrug(out string des,ObjectInfo info)
    {
        des = "";
        des += "名称:" + info.name + "\n";
        des += "回复HP:" + info.hp + "\n";
        des += "回复MP:" + info.mp + "\n";
        des += "回收价格:" + info.price_sell + "\n";
    }
    void GetDesOfEquip(out string des, ObjectInfo info)
    {
        des = "";
        des += "名称:" + info.name + "\n";
        des += "ATK:" + info.atk + "\n";
        des += "DEF:" + info.def + "\n";
        des += "SPEED:" + info.speed + "\n";
        des += "职业:" + info.applicationType.ToString() + "\n";
        des += "回收价格:" + info.price_sell + "\n";
    }
}
