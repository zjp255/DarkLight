using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : Quest
{
    public static EquipmentUI instance;

    public GameObject headGear;
    public GameObject armor;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject shoe;
    public GameObject accessory;

    public GameObject equipmentItem;

    public GameObject useButton;
    private PlayerStatus playerStatus;
    private GameObject useObject;

    //public int exATK;
    //public int exDef;
    //public int exSpeed;


    new private void Start()
    {
        base.Start();

        instance = this;

        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        gameObject.SetActive(false);
    }

    new private void FixedUpdate()
    {
        base.FixedUpdate();

    }

    public bool DressEquipment(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfo(id);
        if (info.type != ObjectType.Equip)
        {
            return false;
        }
        if (playerStatus.heroType != info.applicationType && info.applicationType != ApplicationType.Common)
        {
            return false;
        }
        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = headGear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.RightHand:
                parent = rightHand;
                break;
            case DressType.Lefthand:
                parent = leftHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
        }
        if (parent.transform.childCount < 1)
        {
            AddEquip(parent, id, info);
        }
        else if (parent.transform.childCount == 1)
        {
            RemoveEquip(parent);

            AddEquip(parent, id, info);
        }
        
        return true;
    }

    public void RemoveEquip(GameObject parent)
    {
        int tempid = parent.transform.Find("EquipItem(Clone)").GetComponent<EquipItem>().id;
        ObjectInfo tempinfo = ObjectsInfo._instance.GetObjectInfo(tempid);

        playerStatus.attack -= tempinfo.atk;
        playerStatus.def -= tempinfo.def;
        playerStatus.speed -= tempinfo.speed;
        Inventory.instance.GetId(tempid);
        GameObject.Destroy(parent.transform.Find("EquipItem(Clone)").gameObject);
    }

    public void AddEquip(GameObject parent,int id,ObjectInfo info)
    {
        GameObject item = GameObject.Instantiate(equipmentItem, parent.transform);
        item.GetComponent<EquipItem>().id = id;
        item.GetComponent<Image>().sprite = ImageManager._instance.GetSpriteOfIcon(info.icon_name);
        playerStatus.attack += info.atk;
        playerStatus.def += info.def;
        playerStatus.speed += info.speed;
        Inventory.instance.DeleteId(id);
        Inventory.instance.CloseUseItemButton();
    }

    public void ShowUseButton(GameObject gameObject)
    {
        useButton.SetActive(true);
        useObject = gameObject;
        Vector3 position = gameObject.GetComponent<RectTransform>().position;
        useButton.GetComponent<RectTransform>().position = new Vector3(position.x + 40, position.y - 20, position.x);
    }

    public void CloseUseButton()
    {
        useButton.SetActive(false);
    }

    public void ButtonRemoveEquip()
    {
        RemoveEquip(useObject.transform.parent.gameObject);
        CloseUseButton();
    }

    //void UpdatePS()
    //{
    //    exATK = GetAtk(headGear) + GetAtk(armor) + GetAtk(rightHand) + GetAtk(leftHand) + GetAtk(shoe) + GetAtk(accessory);
    //    exDef = GetDef(headGear) + GetDef(armor) + GetDef(rightHand) + GetDef(leftHand) + GetDef(shoe) + GetDef(accessory);
    //    exSpeed = GetSpeed(headGear) + GetSpeed(armor) + GetSpeed(rightHand) + GetSpeed(leftHand) + GetSpeed(shoe) + GetSpeed(accessory);
    //}

    //int GetAtk(GameObject gameObject)
    //{
    //    ObjectInfo info;
    //    if (gameObject.transform.Find("EquipItem") == null)
    //    {
    //        return 0;
    //    }
    //    else {
    //        info = ObjectsInfo._instance.GetObjectInfo(gameObject.transform.Find("EquipItem").GetComponent<EquipItem>().id);
    //        return info.atk;       
    //    }
    //}

    //int GetDef(GameObject gameObject)
    //{
    //    ObjectInfo info;
    //    if (gameObject.transform.Find("EquipItem") == null)
    //    {
    //        return 0;
    //    }
    //    else
    //    {
    //        info = ObjectsInfo._instance.GetObjectInfo(gameObject.transform.Find("EquipItem").GetComponent<EquipItem>().id);
    //        return info.def;
    //    }
    //}

    //int GetSpeed(GameObject gameObject)
    //{
    //    ObjectInfo info;
    //    if (gameObject.transform.Find("EquipItem") == null)
    //    {
    //        return 0;
    //    }
    //    else
    //    {
    //        info = ObjectsInfo._instance.GetObjectInfo(gameObject.transform.Find("EquipItem").GetComponent<EquipItem>().id);
    //        return info.speed;
    //    }
    //}
}
