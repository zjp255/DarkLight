using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public List<GameObject> uiS;
    public List<Sprite> nGame;
    public List<GameObject> lGameGameObj;
    public List<Sprite> lGame;
    public float speed = 10;
    public float shijian = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            shijian -= Time.deltaTime;

        if (uiS[0].GetComponent<Image>().color.a < 255  && shijian <= 1 && shijian > -1)
        {
            uiS[0].GetComponent<Image>().color = new Vector4(255f, 255f, 255f, uiS[0].GetComponent<Image>().color.a + speed * Time.deltaTime);
            uiS[1].GetComponent<Image>().color = new Vector4(255f, 255f, 255f, uiS[1].GetComponent<Image>().color.a + speed * Time.deltaTime);
            uiS[2].GetComponent<Image>().color = new Vector4(255f, 255f, 255f, uiS[2].GetComponent<Image>().color.a + speed * Time.deltaTime);
            uiS[3].GetComponent<Image>().color = new Vector4(255f, 255f, 255f, uiS[3].GetComponent<Image>().color.a + speed * Time.deltaTime);
        }

        if (shijian <= -2 && shijian > -3)
        {
            uiS[3].GetComponent<Image>().color = new Vector4(255f, 255f, 255f, uiS[3].GetComponent<Image>().color.a - speed * 1.5f * Time.deltaTime);
        }
        if (shijian <= -3)
        {
            uiS[3].GetComponent<Image>().color = new Vector4(255f, 255f, 255f, uiS[3].GetComponent<Image>().color.a + speed * 1.5f * Time.deltaTime);
            if (shijian < -4)
            {
                shijian = -2f;
            }
        }
    }

    [System.Obsolete]
    public void NGame()
    {
        //加载选择角色的场景
        uiS[1].GetComponent<Image>().sprite = nGame[1];
        transform.GetComponent<AudioSource>().Play();
        Application.LoadLevel(1);
        
    }
    public void NGameUp()
    {
        uiS[1].GetComponent<Image>().sprite = nGame[0];
    }


    public void LGame()
    {
        //加载已经保存的游戏
        uiS[2].GetComponent<Image>().sprite = lGame[1];
        transform.GetComponent<AudioSource>().Play();

    }
    public void LGameUp()
    {
        uiS[2].GetComponent<Image>().sprite = lGame[0];
    }
}
