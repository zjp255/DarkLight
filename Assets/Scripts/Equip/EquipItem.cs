using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    public int id;
    bool mouseIsInItem = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && mouseIsInItem == true)
        {
            EquipmentUI.instance.ShowUseButton(this.gameObject);
        }

    }

    public void EnterItem()
    {
        mouseIsInItem = true;
        EquipmentUI.instance.CloseUseButton();
        
    }
    public void ExsitItem()
    {
        mouseIsInItem = false;
        
    }
}
