using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCamera : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 20f, player.transform.position.z);
    }

    public void boost()
    {
        if (this.GetComponent<Camera>().orthographicSize > 1)
            this.GetComponent<Camera>().orthographicSize--;
    }

    public void reduce()
    {
        if (this.GetComponent<Camera>().orthographicSize < 20)
            this.GetComponent<Camera>().orthographicSize++;
    }
}
