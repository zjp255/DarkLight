using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusPanel : MonoBehaviour
{
    public Text nameAndLvText;
    public RectTransform HpRext;
    public RectTransform MpRext;
    public static PlayerStatusPanel instance;
    private PlayerStatus ps;
    private float length = 430f;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;    
    }
    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateShow();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateShow()
    {
        float hp = (float)ps.nowHp / (float)ps.hp;
        float mp = (float)ps.nowMp / (float)ps.mp;
        nameAndLvText.text = ps.playerName + " Lv:" + ps.grade;
        HpRext.sizeDelta = new Vector2(hp * length, HpRext.sizeDelta.y);
        MpRext.sizeDelta = new Vector2(mp * length, MpRext.sizeDelta.y);
    }
}
