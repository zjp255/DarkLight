using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Quest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 targetPosition;
    public Vector3 originalPosition;
    public float speed = 5f;
    public bool hideQuest = false;
    public RectTransform rectTransform;
    private Vector2 lastMousePosition;
    public int chuXianCount = 1;

    // Start is called before the first frame update
    public void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition3D;
        
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (chuXianCount == 1)
            ChuXian();

        if (hideQuest)
            HidQuest();
    }

    public void ChuXian()
    {
        
        rectTransform.anchoredPosition3D = Vector3.Lerp(rectTransform.anchoredPosition3D, targetPosition, Time.deltaTime * speed);

    }
    public void HidQuest()
    {
        rectTransform.anchoredPosition3D = Vector3.Lerp(rectTransform.anchoredPosition3D, originalPosition * 2, Time.deltaTime * speed / 2);
        if (rectTransform.anchoredPosition3D.x > originalPosition.x - 90)
        {
            rectTransform.anchoredPosition3D = originalPosition;
            hideQuest = false;
            chuXianCount = 1;
            gameObject.SetActive(false);
            
        }
    }

    public void CancleB()
    {
        hideQuest = true;
       
    }

    public void AcceptB()
    { 
    
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
        chuXianCount = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 offerSet = eventData.position - lastMousePosition;
        transform.GetComponent<RectTransform>().position += offerSet;
        lastMousePosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
    }
}