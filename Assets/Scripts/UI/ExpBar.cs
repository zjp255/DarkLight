using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : MonoBehaviour
{
    private PlayerStatus ps;
    public GameObject expBar;
    private float length;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        length = transform.GetComponent<RectTransform>().sizeDelta.x;
    }

    private void FixedUpdate()
    {
        UpdateExpBar();
    }

    public void UpdateExpBar()
    {
        if((float)ps.nowExp / (float)ps.targetExp != expBar.GetComponent<RectTransform>().sizeDelta.x / length)
        {
            expBar.GetComponent<RectTransform>().sizeDelta = new Vector2(((float)ps.nowExp / (float)ps.targetExp )* length, expBar.GetComponent<RectTransform>().sizeDelta.y);

        }
    }
}
