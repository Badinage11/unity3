using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public PlayerController playerController;

    private Transform attackTarget;//攻击目标

    public float moveSpeed = 1;//移动速度

    public float attackDistance = 3;//攻击距离

    //private Animator anim;

    private bool isAttack;

    public float hatredScope = 12f;//仇恨范围

    public Transform way;

    private List<Transform> wayPoints = new List<Transform>();

    private bool isRange;

    private int randomNum;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();

        foreach (Transform point in way)
        {
            wayPoints.Add(point);
        }

        randomNum = Random.Range(0, wayPoints.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.gameOver)
        {
            isAttack = false;
            if (attackTarget)
            {
                if (Vector3.Distance(attackTarget.position, transform.position) > attackDistance)
                {
                    //Debug.Log(Vector3.Distance(attackTarget.position, transform.position));
                    transform.position += (attackTarget.position - transform.position).normalized * Time.deltaTime * moveSpeed;
                    transform.forward = Vector3.Lerp(transform.forward, attackTarget.position - transform.position, 0.005f);//更改怪物朝向
                }
                else
                {
                    isAttack = true;
                }

                if (Vector3.Distance(attackTarget.position, transform.position) > hatredScope * 1.5f)
                {
                    attackTarget = null;
                }
            }
            else
            {
                if (isRange)
                {
                    randomNum = Random.Range(0, wayPoints.Count);
                }
                Transform t = wayPoints[randomNum];
                if (Vector3.Distance(transform.position, t.position) > 0.3f)
                {
                    //anim.SetFloat("Blend", 0.5f);
                    //transform.position = Vector3.Lerp(transform.position, t.position, 0.005f);
                    transform.position = Vector3.MoveTowards(transform.position, t.position, moveSpeed * Time.deltaTime);

                    //transform.forward = Vector3.Lerp(transform.forward, t.position - transform.position, 0.005f);//更改怪物朝向
                    // 计算物体应该面向的方向
                    Vector3 targetDirection = Vector3.ProjectOnPlane(t.position - transform.position, transform.up);
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection, transform.up);
                    // 旋转物体朝向目标位置
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);

                    isRange = false;
                }
                else
                {
                    isRange = true;
                }
            }
            //anim.SetBool("IsAttack", isAttack);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log(Vector3.Distance(other.transform.position, transform.position));
            if (Vector3.Distance(other.transform.position, transform.position) <= hatredScope)
            {
                if (attackTarget)
                {
                    //当存在多个攻击目标时 攻击距离最近的
                    if (Vector3.Distance(other.transform.position, transform.position) < Vector3.Distance(attackTarget.transform.position, transform.position))
                    {
                        attackTarget = other.transform;
                    }
                }
                else
                {
                    attackTarget = other.transform;
                }
            }
        }
    }
}
