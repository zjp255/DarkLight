using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfo : MonoBehaviour
{
    private int id = 0;
    private ObjectInfo info = null;
    public int num = 0;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShowNum();
     
    }

  

    void ShowNum()
    {
        if (text.text != num.ToString())
        {
            text.text = num.ToString();
        }
    }

    public void SetId(int id, int num = 1)
    {
        info = ObjectsInfo._instance.GetObjectInfo(id);
        InventoryItem item = GetComponent<InventoryItem>();
        item.SetIconName(info.icon_name);
        this.num = num;
        LodeInfo(info);
    }

    public int GetId()
    {
        return id;
    }

    void LodeInfo(ObjectInfo info)
    {
        this.id = info.id;
        this.info = info;
    }

    public void ClerInfo()
    {
        id = 0;
        info = null;
        num = 0;
    }

    public ObjectInfo GetInfo()
    {
        return info;
    }
}
