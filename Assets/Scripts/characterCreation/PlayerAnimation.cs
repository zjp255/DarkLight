using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animation anim;
    private PlayerMove move;
    private PlayerAttack pa;
    // Start is called before the first frame update
    void Start()
    {
        pa = transform.GetComponent<PlayerAttack>();
        move = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (pa.state == PlayerAttackState.Walk)
        {
            if (move.state == PlayerState.Moving)
            {
                PlayAnim("Run");
            }
            else if (move.state == PlayerState.Idle)
            {
                PlayAnim("Idle");
            }
        }
        else if (pa.state == PlayerAttackState.NormalAttack)
        {
            if (pa.attackState == AttackState.moving)
            {
                PlayAnim("Run");
            }
            else if (pa.attackState == AttackState.idle)
            {
                PlayAnim("Idle");
            }
        }
    }

    void PlayAnim(string animName)
    {
        anim.CrossFade(animName);
    }
}
