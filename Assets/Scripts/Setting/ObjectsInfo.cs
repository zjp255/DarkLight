using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInfo : MonoBehaviour
{
    public static ObjectsInfo _instance;

    private Dictionary<int, ObjectInfo> objectsInfoDir = new Dictionary<int, ObjectInfo>();

    public TextAsset objectsInfoList;//得到文本
    void Awake()
    {
        _instance = this;
        ReadInfo();
    }

    public ObjectInfo GetObjectInfo(int id)
    {
        ObjectInfo info;
        objectsInfoDir.TryGetValue(id,out info);

        return info;
    }

    void ReadInfo()
    {
        string text = objectsInfoList.text;
        string[] strArray = text.Split('\n');//按照/n将字符串分成几个数组

        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');
            ObjectInfo info = new ObjectInfo();

            info.id = int.Parse(proArray[0]);
            info.name = proArray[1];
            info.icon_name = proArray[2];
            string str_type = proArray[3];
            switch (str_type)
            {
                case "Drug":
                    info.type = ObjectType.Drug;
                    break;
                case "Equip":
                    info.type = ObjectType.Equip;
                    break;
                case "Mat":
                    info.type = ObjectType.Mat;
                    break;
            }
            if (info.type == ObjectType.Drug)
            {
                info.hp = int.Parse(proArray[4]);
                info.mp = int.Parse(proArray[5]);
                info.price_sell = int.Parse(proArray[6]);
                info.price_buy = int.Parse(proArray[7]);
            }
            else if (info.type == ObjectType.Equip)
            {
                info.atk = int.Parse(proArray[4]);
                info.def = int.Parse(proArray[5]);
                info.speed = int.Parse(proArray[6]);
                info.price_sell = int.Parse(proArray[9]);
                info.price_buy = int.Parse(proArray[10]);
                string str_dresstype = proArray[7];
                switch (str_dresstype)
                {
                    case "Headgear":
                        info.dressType = DressType.Headgear;
                        break;
                    case "Armor":
                        info.dressType = DressType.Armor;
                        break;
                    case "LeftHand":
                        info.dressType = DressType.Lefthand;
                        break;
                    case "RightHand":
                        info.dressType = DressType.RightHand;
                        break;
                    case "Shoe":
                        info.dressType = DressType.Shoe;
                        break;
                    case "Accessory":
                        info.dressType = DressType.Accessory;
                        break;
                }
                string str_applytype = proArray[8];
                switch (str_applytype)
                {
                    case "Swordman":
                        info.applicationType = ApplicationType.Swordman;
                        break;
                    case "Magician":
                        info.applicationType = ApplicationType.Magician;
                        break;
                    case "Common":
                        info.applicationType = ApplicationType.Common;
                        break;
                }
            }
            objectsInfoDir.Add(info.id, info);//添加到字典，可以方便的通过id找到物品信息
        }
    }
}

public enum ObjectType 
{ 
Drug,
Equip,
Mat

}

public enum DressType
{ 
Headgear,
Armor,
RightHand,
Lefthand,
Shoe,
Accessory
}

public enum ApplicationType
{ 
Swordman,
Magician,
Common
}


public class ObjectInfo
{
    public int id;
    public string name;
    public string icon_name;
    public ObjectType type;
    public int hp;
    public int mp;
    public int price_sell;
    public int price_buy;

    public int atk;
    public int def;
    public int speed;
    public DressType dressType;
    public ApplicationType applicationType;
}
