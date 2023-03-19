using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Quest
{
    public static Inventory instance;
    public GameObject grid;
    public List<GameObject> grids;
    public List<GameObject> items;
    public Text coinText;
    public GameObject itemPrefab;
    public GameObject useButton;
    private int useId;
   
    
    private int coincount = 1000;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        instance = this;
      
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        if (chuXianCount == 1)
            Chuxian_I();


        if (hideQuest)
        {
            grid.SetActive(false);
            HidQuest();
        }

        CoinGuanli();

       
    }

    public void GetId(int id)//拾取到id的物品，并添加到背包
    {
        
        int count = 1;
        InventoryItemInfo info;
        foreach (GameObject temp in items)
        {
            info = temp.GetComponent<InventoryItemInfo>();
            if (info.GetId() == id)
            {
                info.num++;
                count--;
                break;
            }
        }
        if (count == 1)
        {
            int i = 0;
            for (i = 0; i < grids.Count; i++)
            {
                if (grids[i].transform.Find("item(Clone)") == null)
                {
                    GameObject newItem = Instantiate(itemPrefab);
                    newItem.transform.SetParent(grids[i].transform);
                    newItem.GetComponent<InventoryItemInfo>().SetId(id);
                    newItem.transform.position = grids[i].transform.position;
                    items.Add(newItem);
                    break;
                }
            }
            if (i >= grids.Count)
            {
                print("背包已满");
            }
        }
    }

    public void DeleteId(int id)
    {
        InventoryItemInfo info;
       
        foreach (GameObject temp in items)
        {
            info = temp.GetComponent<InventoryItemInfo>();
            if (info.GetId() == id)
            {
                info.num--;
                if (info.num <= 0)
                {
                    items.Remove(temp);
                    GameObject.Destroy(temp);

                }
                break;
            }
        }
    }

    void Chuxian_I()
    {
        ChuXian();
        if (rectTransform.anchoredPosition3D.x <= targetPosition.x + 1)
        {
            grid.SetActive(true);
        }
    }

    void CoinGuanli()
    {
        if (coincount != GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>().coin)
        {
            coincount = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>().coin;
            coinText.text = coincount.ToString();
        }
    }

    public void ShowUseItemButton(int id, Vector3 position)
    {
        useId = id;
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfo(id);
        useButton.gameObject.SetActive(true);
        useButton.GetComponent<RectTransform>().position = new Vector3(position.x + 40, position.y - 20, position.x);
        Text temptext = useButton.transform.Find("Text").GetComponent<Text>();
        if (info.type == ObjectType.Drug)
        {
            temptext.text = "使用";
        }
        else if (info.type == ObjectType.Equip)
        {
            temptext.text = "装备";
        }
    }

    public void CloseUseItemButton()
    {
        if (useButton.activeInHierarchy == true)
        {            
            useButton.SetActive(false);
        }
    }

    public void ClickUseButton()
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfo(useId);
        if (info.type == ObjectType.Drug)
        {
            
        }
        else if (info.type == ObjectType.Equip)
        {
            EquipmentUI.instance.DressEquipment(useId);
        }
    }

    public void UseDrug()
    { 
    
    }

    
}
