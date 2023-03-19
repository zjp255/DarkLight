using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeletPanel : MonoBehaviour
{
    string nameplayer;
    public InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   

    public void LButton()
    {
        transform.GetComponent<AudioSource>().Play();
    }
    public void RButton()
    {
        transform.GetComponent<AudioSource>().Play();
    }

    [System.Obsolete]
    public void OkBUtton()
    {
        transform.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetString("name", inputField.text);
        Application.LoadLevel(2);
    }
}
