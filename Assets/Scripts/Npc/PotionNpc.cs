using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionNpc : Npc
{
    public GameObject drugShop;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drugShop.SetActive(true);
            if (gameObject.name == "Weapon_Npc")
            {
                WeaponShop.instance.UpdateWeapon();
            }
        }
    }

}
