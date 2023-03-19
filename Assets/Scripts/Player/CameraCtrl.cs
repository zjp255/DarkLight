using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject myMamera;
    
    private Vector3 offsetPosition;
    public float scrollSpeed = 5f;
    public float roteteSpeed = 5f;
    float distans = 0;
    bool isRotating = false;
 
    // Start is called before the first frame update
    void Start()
    {

        offsetPosition = myMamera.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        myMamera.transform.position = transform.position + offsetPosition;
       
        RotateView();

        ScrollView(Input.GetAxis("Mouse ScrollWheel"));

    }

    void ScrollView(float x)
    {

        distans = offsetPosition.magnitude;
        distans -= x * scrollSpeed;
        distans = Mathf.Clamp(distans, 2, 18);
        offsetPosition = offsetPosition.normalized * distans;
    }

    void RotateView()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            myMamera.transform.RotateAround(transform.position, Vector3.up, roteteSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = myMamera.transform.position;
            Quaternion originalQua = myMamera.transform.rotation;

            myMamera.transform.RotateAround(transform.position, myMamera.transform.right, -roteteSpeed * Input.GetAxis("Mouse Y"));

            float x = myMamera.transform.eulerAngles.x;
            if (x < 10 || x > 80)
            {
                myMamera.transform.position = originalPos;
                myMamera.transform.rotation = originalQua;
             }
        }

        offsetPosition = myMamera.transform.position - transform.position;
    }
}
