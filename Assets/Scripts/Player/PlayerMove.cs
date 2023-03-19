using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1;
    public PlayerState state = PlayerState.Idle;
    private PlayerDir dir;
    private CharacterController controller;
    public bool isMove = false;
    private PlayerAttack pa;
    private PlayerStatus ps;
    // Start is called before the first frame update
    void Start()
    {
        dir = transform.GetComponent<PlayerDir>();
        controller = transform.GetComponent<CharacterController>();
        pa = GetComponent<PlayerAttack>();
        ps = GetComponent<PlayerStatus>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (pa.state == PlayerAttackState.Walk)
        {
            float distance = Vector3.Distance(dir.targetPoint, transform.position);
            if (distance > 0.1f)
            {
                controller.SimpleMove(transform.forward * ps.speed);
                state = PlayerState.Moving;
                isMove = true;
            }
            else
            {
                isMove = false;
                state = PlayerState.Idle;
            }
        }
    }
}
