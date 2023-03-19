using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death
}

public class WolfBaby : MonoBehaviour
{
    public WolfState state = WolfState.Idle;

    public string aniname_death;
    public string aniname_idle;
    public string aniname_walk;
    public string aniname_attack1;
    public string aniname_attack2;
    public float time_attack1;
    public float time_attack2;
    private new Animation animation;

    private string aniname_Now;

    public float time = 1;
    private float timer = 0;

    public float speed = 1;
    public int nowhp = 100;
    public int hp = 100;
    public int exp = 20;
    public float miss_rate = 0.2f;
    public int attack = 10;

    public float red_time = 1;
    private Color normal;

    private string aniname_attack_now;
    public int attack_rate = 1;
    public float attack2_rate = 0.2f;
    private float attack_timer = 0;

    public float minDistance = 2 ;
    public float maxDistance = 5;
    public Transform target;
    public AudioClip miss_sound;
    private CharacterController cc;
    private PlayerStatus ps;

    public GameObject spown;
    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        nowhp = hp;
        aniname_attack_now = aniname_attack1;
        aniname_Now = aniname_idle;
        cc = GetComponent<CharacterController>();
        animation = transform.GetComponent<Animation>();
        normal = transform.Find("Wolf_Baby").GetComponent<Renderer>().material.color;
    }
    private void Update()
    {
        if (state == WolfState.Death)
        {
            animation.CrossFade(aniname_death);
        }else if(state == WolfState.Attack)
        {
            //攻击状态
            AutoAttack();
        }
        else
        {
            animation.CrossFade(aniname_Now);
            if (aniname_Now == aniname_walk)
            {
                cc.SimpleMove(transform.forward * speed);
            }

            timer += Time.deltaTime;
            if(timer >= time)
            {
                timer = 0;
                RandomState();
            }
            //巡逻
        }
    }

    void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            aniname_Now = aniname_idle;
        }
        else
        {
            if (aniname_Now != aniname_walk)
            {
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            aniname_Now = aniname_walk;
        }

    }

    public void TakeDamage(int attack)
    {
        if (state == WolfState.Death)
            return ;
        target = GameObject.FindGameObjectWithTag(Tags.player).transform;
        state = WolfState.Attack;
        float value = Random.Range(0f, 1f);
        if (value < miss_rate)
        {
            AudioSource.PlayClipAtPoint(miss_sound, transform.position);


        }
        else {//打中效果
            this.hp -= attack;

            StartCoroutine(ShowBodyRed());
            if (hp <= 0)
            {
                state = WolfState.Death;
                spown.GetComponent<MonsterSpown>().nowCount--;
                
                Destroy(this.gameObject, 2);
            }        
        }
    }

    IEnumerator ShowBodyRed()
    {
        Renderer renderer = transform.Find("Wolf_Baby").GetComponent<Renderer>();
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        renderer.material.color = normal;
    }

    void AutoAttack()
    {
        if (target.GetComponent<PlayerAttack>().state == PlayerAttackState.death)
        {
            target = null;
            state = WolfState.Idle;
        }
        if (target != null)
        {
            
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance > maxDistance)
            {
                target = null;
                state = WolfState.Idle;
            }
            else if (distance <= minDistance)
            {
                attack_timer += Time.deltaTime;
                animation.CrossFade(aniname_attack_now);
                if (aniname_attack_now == aniname_attack1)
                {
                    if (attack_timer > time_attack1)
                    {
                        //造成伤害
                        target.GetComponent<PlayerAttack>().TakeDamage(attack);
                        aniname_attack_now = aniname_idle;
                    }
                }
                else if (aniname_attack_now == aniname_attack2)
                {
                    if (attack_timer > time_attack2)
                    {
                        target.GetComponent<PlayerAttack>().TakeDamage(attack * 2);
                        aniname_attack_now = aniname_idle;
                    }
                }

                if (attack_timer > (1f / attack_rate))
                {
                    attack_timer = 0;
                   float x = Random.Range(0f, 1f);
                    if (x <= attack2_rate)
                    {
                        aniname_attack_now = aniname_attack2;
                    }
                    else {
                        aniname_attack_now = aniname_attack1;
                    }
                    
                        
                }
            }
            else
            {
                transform.LookAt(target);
                cc.SimpleMove(transform.forward * speed);
                animation.CrossFade(aniname_walk);
            }
        }
        else {
            state = WolfState.Idle;
        }
    }

    private void OnMouseEnter()
    {
        CursorManager.instance.SetAttack();
    }
    private void OnMouseExit()
    {
        CursorManager.instance.SetNormal();
    }

    private void OnDestroy()
    {
        ps.GetExp(exp);
        BarNpc.instance.OnKillWolf();
    }

  
}
