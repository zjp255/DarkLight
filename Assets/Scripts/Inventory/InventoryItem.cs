using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 mouseRectOfferSet;//对应的点
    private Transform originalParent;
    public Image image;
    public GameObject text;
    private bool mouseIsInItem = false;
    private InventoryItemInfo info;
   

    // Start is called before the first frame update
    void Start()
    {
        info = this.gameObject.GetComponent<InventoryItemInfo>();
        rectTransform = this.transform.GetComponent<RectTransform>();
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && mouseIsInItem == true)
        {
            Inventory.instance.ShowUseItemButton(GetComponent<InventoryItemInfo>().GetId(), GetComponent<RectTransform>().position);
        }
       
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.tag == "UiGrid")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                rectTransform.position = eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>().position;
            }
            else
            {
                if (eventData.pointerCurrentRaycast.gameObject.name == "item(Clone)")
                {

                    transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);

                    transform.GetComponent<RectTransform>().position = eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<RectTransform>().position;


                    eventData.pointerCurrentRaycast.gameObject.transform.SetParent(originalParent);
                    eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>().position = originalParent.GetComponent<RectTransform>().position;
                }
                else
                {
                    if (eventData.pointerCurrentRaycast.gameObject.tag == Tags.shortcutGrid && info.GetInfo().type == ObjectType.Drug)
                    {
                        eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Shortcut>().SetDrugId(info.GetInfo().id, this.gameObject);
                        rectTransform.SetParent(originalParent);
                        rectTransform.position = originalParent.GetComponent<RectTransform>().position;
                    }


                    else if (eventData.pointerCurrentRaycast.gameObject.tag != "UiGrid")
                    {

                        rectTransform.SetParent(originalParent);
                        rectTransform.position = originalParent.GetComponent<RectTransform>().position;
                    }
                }
            }
        }
        else 
        {
            rectTransform.SetParent(originalParent);
            rectTransform.position = originalParent.GetComponent<RectTransform>().position;
        }
        text.SetActive(true);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out mouseRectOfferSet);
        rectTransform.position = mouseRectOfferSet;
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        transform.SetParent(transform.parent.parent);
       
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform,eventData.position,eventData.pressEventCamera,out mouseRectOfferSet);
        
        rectTransform.position = mouseRectOfferSet;

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        text.SetActive(false);
    }

    public void SetIconName(string name)
    {
        image.sprite = ImageManager._instance.GetSpriteOfIcon(name);
    }



    public void OnMouseEnter()
    {
        Inventory.instance.CloseUseItemButton();
        mouseIsInItem = true;
        InventoryDesPanel.instance.ShowItemDes(transform.GetComponent<InventoryItemInfo>().GetId(),transform.parent.position);
    }
    public void OnMouseExit()
    {
        
        mouseIsInItem = false;
        InventoryDesPanel.instance.CloseItemDes();
    }

    
}
