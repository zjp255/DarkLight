using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerState
{
    Moving,
    Idle
}

public class PlayerDir : MonoBehaviour
{
    public GameObject effect_click_prefab;
    private bool isMoving = false;//表示鼠标是否按下
    public Vector3 targetPoint;
    private PlayerMove playerMove;
    private PlayerAttack attack;
    
    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<PlayerAttack>();
        targetPoint = transform.position;
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.state == PlayerAttackState.death)
            return;
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.ground )
            {
                isMoving = true;
                ShoClickEffect(hitInfo.point);
                LookAtTarget(hitInfo.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.ground)
            {
                LookAtTarget(hitInfo.point);
            }
        }
        else
        {
            if (playerMove.isMove)
            {
                LookAtTarget(targetPoint);
            }
        }
    }

    void ShoClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.1f, hitPoint.z);
        GameObject.Instantiate(effect_click_prefab, hitPoint,Quaternion.identity);
    }

    void LookAtTarget(Vector3 hitPoint)
    {
        targetPoint = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
        this.transform.LookAt(targetPoint);
    }
}
