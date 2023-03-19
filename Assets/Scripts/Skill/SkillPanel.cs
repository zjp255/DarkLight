using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : Quest
{
    public static SkillPanel instance;
    private PlayerStatus status;
    public int[] magicianSkillIdList;
    public int[] swordmanSkillIdList;
    public GameObject prefab;
    public GameObject parent;
    public List<GameObject> gameObjects;

    // Start is called before the first frame update
    new void Start()
    {
        status = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        base.Start();
        instance = this;
        ShowSkills();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void ShowSkills()
    {
        int[] skillList = null;
        switch (status.heroType)
        {
            case ApplicationType.Magician:
                skillList = magicianSkillIdList;
                break;
            case ApplicationType.Swordman:
                skillList = swordmanSkillIdList;
                break;
        }
        foreach (int id in skillList)
        {
            parent.GetComponent<RectTransform>().sizeDelta += new Vector2(0,125f);
            gameObjects.Add(GameObject.Instantiate(prefab,parent.transform));
            gameObjects[(id % 100) - 1].GetComponent<SkillItem>().SetId(id);
        }
    }

    public void UpdatePanel()
    {
        PlayerStatus playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        foreach (GameObject temp in gameObjects)
        {
            temp.GetComponent<SkillItem>().RefreshThat(playerStatus.grade);
        }
    }
}
