using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradualClear : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<Image>().color.a > 0)
        {
            transform.GetComponent<Image>().color = new Vector4(255f, 255f, 255f, transform.GetComponent<Image>().color.a - speed * Time.deltaTime);

        }
        else 
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
