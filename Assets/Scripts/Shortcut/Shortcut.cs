using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShortcutType
{
    skill,
    drug,
    none
}
public class Shortcut : MonoBehaviour
{
    public KeyCode keyCode;
    private int id;
    private SkillInfo skillInfo;
    private ObjectInfo drugInfo;
    private Image image;
    public ShortcutType shortcutType = ShortcutType.none;
    private InventoryItemInfo objectItemInfo;
    private PlayerStatus ps;
    private PlayerAttack pa;

    private void Awake()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        pa = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
        image = gameObject.transform.Find("Image").GetComponent<Image>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (shortcutType == ShortcutType.drug)
            {
                UseDrug();
            }
            else if (shortcutType == ShortcutType.skill)
            {
                if (UseMp(skillInfo.mp))
                {
                    pa.UseSkill(skillInfo);
                }
            }
        }
    }
    public void SetSkillId(int id)
    {
        this.id = id;
        shortcutType = ShortcutType.skill;
        skillInfo = SkillsInfo.instance.GetSkillInfoById(id);
        image.sprite = ImageManager._instance.GetSpriteOfIcon(skillInfo.icon_name);
        image.color = new Vector4(255f, 255f, 255f, 255f);
    }
    public void SetDrugId(int id,GameObject gameObject)
    {
        this.id = id;
        objectItemInfo = gameObject.GetComponent<InventoryItemInfo>();
        shortcutType = ShortcutType.drug;
        drugInfo = ObjectsInfo._instance.GetObjectInfo(id);
        image.sprite = ImageManager._instance.GetSpriteOfIcon(drugInfo.icon_name);
        image.color = new Vector4(255f, 255f, 255f, 255f);
    }

    private void UseDrug()
    {
        PlayerStatus ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        if (objectItemInfo.num >= 1 && (ps.nowHp != ps.hp || ps.nowMp != ps.mp))
        {
            ps.nowHp += objectItemInfo.GetInfo().hp;
            ps.nowMp += objectItemInfo.GetInfo().mp;
            if (ps.nowHp > ps.hp)
            {
                ps.nowHp = ps.hp;
            }
            if (ps.nowMp > ps.mp)
            {
                ps.nowMp = ps.mp;
            }
            Inventory.instance.DeleteId(id);
        }
    }


    public bool UseMp(int count)
    {
        if (ps.nowMp >= count)
        {
            ps.nowMp -= count;
            return true;
        }
        else {
            return false;
        }
    }
}
