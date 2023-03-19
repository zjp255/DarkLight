using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private GameObject[] characterGobjs;
    private int selectedIndex = 0;
    private int length;//可选择角色的数量
    // Start is called before the first frame update
    void Start()
    {
        length = characterPrefabs.Length;
        characterGobjs = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            characterGobjs[i] = GameObject.Instantiate(characterPrefabs[i],transform.position,transform.rotation);
        }
        SetActiveOfCha();
    }

    void SetActiveOfCha()
    {
        for (int i = 0; i < length; i++)
        {
            if (i == selectedIndex)
                characterGobjs[i].SetActive(true);
            else
                characterGobjs[i].SetActive(false);
        }
    }

    public void LButton()
    {
        selectedIndex--;
        if (selectedIndex < 0)
            selectedIndex = 0;
        SetActiveOfCha();
    }

    public void RButton()
    {
        selectedIndex++;
        if (selectedIndex >= length)
            selectedIndex = length - 1;
        SetActiveOfCha();
   
    }

  
}
