using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PlayerAttackState
{
    Walk,
    NormalAttack,
    SkillAttack,
    death
}

public enum AttackState
{
    moving,
    idle,
    attack
}
public class PlayerAttack : MonoBehaviour
{
    public PlayerAttackState state = PlayerAttackState.Walk;

    public string aniname_normalAttack;
    public string aniname_idle;
    public string aniname_death;
    private string aniname_now;
    public float time_normalAttack;
    public float normalAttackSpeed = 1;
    private float timer = 0;
    public float min_distance = 5;
    private Transform target_normalattack;
    private CharacterController cc;
    private PlayerStatus ps;
    public AttackState attackState = AttackState.idle;
    private Animation anim;

    private bool isShowEffect = false;
    public GameObject normalAttackEffect;

    public float missrate = 0.2f;
    public GameObject body;
    private Color normal;
    public List<GameObject> efxs;
    private Dictionary<string, GameObject> efxDict = new Dictionary<string, GameObject>();
    private void Start()
    {
        normal = body.GetComponent<Renderer>().material.color;
        anim = GetComponent<Animation>();
        aniname_now = aniname_normalAttack;
        ps = GetComponent<PlayerStatus>();
        cc = GetComponent<CharacterController>();
        foreach (GameObject go in efxs)
        {
            efxDict.Add(go.name, go);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            bool isCollider = Physics.Raycast(ray, out hitinfo);
            if (isCollider && hitinfo.collider.tag == Tags.enemy)
            {
                target_normalattack = hitinfo.collider.transform;
                state = PlayerAttackState.NormalAttack;
                timer = 0;
                isShowEffect = false;
                
            }
            else{
                state = PlayerAttackState.Walk;
                target_normalattack = null;
            }
        }
        if (target_normalattack == null && state != PlayerAttackState.SkillAttack)
        {
            state = PlayerAttackState.Walk;
        }
        if (state == PlayerAttackState.NormalAttack)
        {

            float distance = Vector3.Distance(target_normalattack.position, transform.position);
            if (distance <= min_distance)
            {
                //½øÐÐ¹¥»÷
                attackState = AttackState.attack;
                timer += Time.deltaTime;
                anim.CrossFade(aniname_now);
                transform.LookAt(target_normalattack);
                if (timer >= time_normalAttack)
                {
                    aniname_now = aniname_idle;
                    if (isShowEffect == false)
                    {
                        isShowEffect = true;
                        GameObject.Instantiate(normalAttackEffect, target_normalattack.position, Quaternion.identity);
                        target_normalattack.GetComponent<WolfBaby>().TakeDamage(ps.attack);
                    }
                }
                if (timer >= (1f / normalAttackSpeed))
                {
                    timer = 0;
                    isShowEffect = false;
                    aniname_now = aniname_normalAttack;
                }
            }
            else
            {
                attackState = AttackState.moving;
                transform.LookAt(target_normalattack);
                cc.SimpleMove(transform.forward * ps.speed);
            }
        }
        else if (state == PlayerAttackState.death)
        {
            anim.CrossFade(aniname_death);
        }
        
    }

    public void TakeDamage(int attack)
    {
        if (state == PlayerAttackState.death)
            return;
        float temp = attack * ((200 - ps.def) / 200);
        if (temp < 1)
            temp = 1;

        float value = Random.Range(0f, 1f);
        if (value < missrate)
        {

        }
        else {
            ps.nowHp -= (int)temp;
            StartCoroutine(ShowBodyRed());
            if (ps.nowHp < 0)
            {
                state = PlayerAttackState.death;
            }
        }
    }

    IEnumerator ShowBodyRed()
    {
        body.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);
        body.GetComponent<Renderer>().material.color = normal;
    }

    public void UseSkill(SkillInfo info)
    {
        if (ps.heroType !=  info.applicationType)
        {
            return;
        }
        switch (info.applyType)
        {
            case ApplyType.Passive:
                StartCoroutine(UsePassiveSkill(info));
                break;
            case ApplyType.Buff:
                StartCoroutine(UseBuffSkill(info));
                break;
            case ApplyType.MultiTarget:
                UseMultiTargetSkill(info);
                break;
            case ApplyType.SingleTarget:
                UseSingleTargetSkill(info);
                break;
        }
    }

    IEnumerator UsePassiveSkill(SkillInfo info)
    {
        state = PlayerAttackState.SkillAttack;
        anim.CrossFade(info.aniname);
        GameObject prefab = null;
        efxDict.TryGetValue(info.efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerAttackState.Walk;
        switch (info.applyProperty)
        {
            case ApplyProperty.Hp:
                ps.nowHp += info.applyValue;
                if (ps.nowHp > ps.hp)
                    ps.nowHp = ps.hp;
                break;
            case ApplyProperty.Mp:
                ps.nowMp += info.applyValue;
                if (ps.nowMp > ps.mp)
                    ps.nowMp = ps.mp;
                break;
        }
        
    
    }
   IEnumerator UseBuffSkill(SkillInfo info)
    {
        state = PlayerAttackState.SkillAttack;
        anim.CrossFade(info.aniname);
        GameObject prefab = null;
        efxDict.TryGetValue(info.efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(info.anitime);
        state = PlayerAttackState.Walk;
        switch (info.applyProperty)
        {
            case ApplyProperty.ATK:
                ps.attack *= (int)(info.applyValue / 100f);
                
                break;
            case ApplyProperty.Def:
                ps.def *= (int)(info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                ps.speed *= (int)(info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                normalAttackSpeed *= (int)(info.applyValue / 100f);
                break;
        }
        yield return new WaitForSeconds(info.applyTime);
        switch (info.applyProperty)
        {
            case ApplyProperty.ATK:
                ps.attack /= (int)(info.applyValue / 100f);

                break;
            case ApplyProperty.Def:
                ps.def /= (int)(info.applyValue / 100f);
                break;
            case ApplyProperty.Speed:
                ps.speed /= (int)(info.applyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                normalAttackSpeed /= (int)(info.applyValue / 100f);
                break;
        }
    }
    void UseMultiTargetSkill(SkillInfo info)
    { }
    void UseSingleTargetSkill(SkillInfo info)
    { 
    }

}
