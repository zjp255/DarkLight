using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public List<Sprite> IconSprites;
    public static ImageManager _instance;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Sprite GetSpriteOfIcon(string name)
    {
        
        foreach ( Sprite temp in IconSprites)
        {
            if (temp.name == name)
                return temp;
        }

        return null;
    }
}
