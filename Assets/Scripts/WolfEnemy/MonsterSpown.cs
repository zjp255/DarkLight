using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpown : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int maxMonsterCount = 5;
    public int nowCount = 0;
    public float timeofSpown = 60;
    private float timer = 0;

    private void Update()
    {
        if (nowCount <= maxMonsterCount)
        {
            timer += Time.deltaTime;
            if (timer > timeofSpown)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-2, 2);
                pos.z += Random.Range(-2, 2);
                GameObject gameObject = GameObject.Instantiate(monsterPrefab, pos, Quaternion.identity);
                gameObject.GetComponent<WolfBaby>().spown = this.gameObject;
                timer = 0;
                nowCount++;
            }
        }
    }
}
