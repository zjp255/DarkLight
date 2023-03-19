using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Npc : MonoBehaviour
{
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
            CursorManager.instance.SetNpcTalk();
    }

    private void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
            CursorManager.instance.SetNormal();
    }
}
