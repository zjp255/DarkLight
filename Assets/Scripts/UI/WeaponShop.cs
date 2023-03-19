using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : Quest
{
    public static WeaponShop instance;
    public int start, end;
    private List<int> magicianWeaponId;
    private List<int> sowrdManWeaponId;
    private PlayerStatus ps;
    public GameObject prefab;
    public GameObject content;
    // Start is called before the first frame update
    new void Start()
    {
        instance = this;
        magicianWeaponId = new List<int>();
        sowrdManWeaponId = new List<int>();
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        base.Start();
        ObjectInfo info = null;
        for (int i = start; i <= end; i++)
        {
         
            info = ObjectsInfo._instance.GetObjectInfo(i);
            
            if (info.applicationType == ApplicationType.Magician)
            {
                magicianWeaponId.Add(i);
            }
            else if (info.applicationType == ApplicationType.Swordman)
            {
                sowrdManWeaponId.Add(i);
            }
            else {
                magicianWeaponId.Add(i);
                sowrdManWeaponId.Add(i);
            }
        }


        gameObject.SetActive(false);
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public void UpdateWeapon()
    {
        if (ps.heroType == ApplicationType.Magician)
        {
            AddPreFab(magicianWeaponId);
        }
        else
        {
            AddPreFab(sowrdManWeaponId);
        }
    }
    public void AddPreFab(List<int> ids)
    {
        foreach (int id in ids)
        {
            GameObject des = GameObject.Instantiate(prefab, content.transform);
            des.GetComponent<WeaponDes>().SetInt(id);
            content.GetComponent<RectTransform>().sizeDelta += new Vector2(0,130);
        }
    }
}
