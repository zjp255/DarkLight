using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public int id;
    private SkillInfo skillInfo;
    public Image image;
    public Text nameDes;
    public Text skillTypeDes;
    public Text des;
    public Text mpDes;
    public bool isOpen = false;
    private GameObject icon;
    public GameObject panel;

    private GameObject tempIcon;
    // Start is called before the first frame update
    void Start()
    {
        icon = transform.Find("SkillImage").gameObject;
        //panel = transform.Find("Panel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetId(int id)
    {
        this.id = id;
        skillInfo = SkillsInfo.instance.GetSkillInfoById(id);
        image.sprite = ImageManager._instance.GetSpriteOfIcon(skillInfo.icon_name);
        nameDes.text = skillInfo.name;
        switch (skillInfo.applyType)
        {
            case ApplyType.Passive:
                skillTypeDes.text = "增益";
                skillTypeDes.fontSize = 25;
                break;
            case ApplyType.Buff:
                skillTypeDes.text = "增强";
                skillTypeDes.fontSize = 25;
                break;
            case ApplyType.SingleTarget:
                skillTypeDes.text = "单个目标";
                skillTypeDes.fontSize = 20;
                break;
            case ApplyType.MultiTarget:
                skillTypeDes.text = "群体技能";
                skillTypeDes.fontSize = 20;
                break;
        }
        des.text = skillInfo.des;
        mpDes.text = skillInfo.mp + "MP";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isOpen)
        {
            tempIcon = GameObject.Instantiate(icon, GameObject.FindGameObjectWithTag(Tags.shortcut).transform);
            tempIcon.GetComponent<RectTransform>().position = eventData.position;
            tempIcon.GetComponent<Image>().raycastTarget = false;
            tempIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(70f, 70f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isOpen)
        {
            tempIcon.GetComponent<RectTransform>().position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isOpen)
        {
            GameObject shortcut = eventData.pointerCurrentRaycast.gameObject;
            if (shortcut != null)
            {
                if (shortcut.tag == Tags.shortcutGrid && shortcut != null)
                {

                    shortcut.transform.parent.GetComponent<Shortcut>().SetSkillId(id);
                    GameObject.Destroy(tempIcon);
                }
            }
            GameObject.Destroy(tempIcon);
        }
    }

    public void RefreshThat(int level)
    {
        if (skillInfo.level <= level)
        {
           
            isOpen = true;
            panel.SetActive(false);
        }
    }
}
