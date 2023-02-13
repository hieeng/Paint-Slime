using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    [SerializeField] Mat mat;
    [SerializeField] Transform[] point;
    Rigidbody rigid;
    NavMeshAgent agent;
    Animator anim;
    Vector3 nextPoint;

    [SerializeField] float speed;
    int blue = 0;
    int red = 1;

    int blueSlimeMask;
    int redSlimeMask;

    bool timeOver = false;
    bool doFight = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        blueSlimeMask = LayerMask.GetMask("BlueSlime");
        redSlimeMask = LayerMask.GetMask("RedSlime");
 
    }

    private void Update()
    {
        rigid.velocity = Vector3.zero;
        Move();
        GatherPoint();
        if (Input.GetKeyDown("space"))
        {
            timeOver = true;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        //탄 맞을 때
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueObj"))
        {
            mat.changeMat(blue);
            gameObject.layer = LayerMask.NameToLayer("BlueSlime");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedObj"))
        {
            mat.changeMat(red);
            gameObject.layer = LayerMask.NameToLayer("RedSlime");
        }

        //싸울 때
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueSlime"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("RedSlime"))
                Fight();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedSlime"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("BlueSlime"))
                Fight();
        }
    }

    private void Move()
    {
        if (agent.enabled == false)
            return;
        if (agent.remainingDistance > 0.5f)
            return;
        else
            transform.transform.position = agent.destination;
        
        if (RandomPoint(transform.position, 3f, out nextPoint))
            agent.SetDestination(nextPoint);
        anim.SetBool("isMove", true);

    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 3.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        Debug.Log("1");
        result = Vector3.zero;
        return false;
    }

    private void GatherPoint()
    {
        if (!timeOver)
            return;
        if (doFight)
            return;

        if (gameObject.layer == LayerMask.NameToLayer("BlueSlime"))
            Point(blue);
        else
            Point(red);
    }

    private void Point(int color)
    {
        agent.enabled = false;
        var pointVec = point[color].position - rigid.position;
        var pointNextVec = pointVec.normalized * speed * Time.deltaTime;

        rigid.MovePosition(rigid.position + pointNextVec);
        transform.LookAt(point[color]);
    }

    private void Fight()
    {
        if (!doFight)
            return;
        StartCoroutine(CoroutineDie());
    }

    IEnumerator CoroutineDie()
    {
        float time  = 0;

        while (time <= 3f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        //파티클
    }
}
