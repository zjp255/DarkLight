using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBar : MonoBehaviour
{
    public void StatusButClick()
    {
        if (Status.instance.gameObject.activeInHierarchy == false)
        {
            Status.instance.gameObject.SetActive(true);
        }
        else
        {
            Status.instance.CancleB();
        }
    }

    public void BegButClick()
    {
        if (Inventory.instance.gameObject.activeInHierarchy == false)
        {
            Inventory.instance.gameObject.SetActive(true);
        }
        else 
        {
            Inventory.instance.CancleB();
        }
    }

    public void EquipButClick()
    {
        if (EquipmentUI.instance.gameObject.activeInHierarchy == false)
        {
            EquipmentUI.instance.gameObject.SetActive(true);
        }
        else
        {
            EquipmentUI.instance.CancleB();
        }
    }

    public void SkillButClick()
    {
        if (SkillPanel.instance.gameObject.activeInHierarchy == false)
        {
            SkillPanel.instance.gameObject.SetActive(true);
            SkillPanel.instance.UpdatePanel();
        }
        else
        {
            SkillPanel.instance.CancleB();
        }



    }

    public void SettingButClick()
    { }

}
