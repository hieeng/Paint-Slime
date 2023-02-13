using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    [SerializeField] Mat mat;
    NavMeshAgent agent;
    Animator anim;
    Vector3 nextPoint;

    int blue = 1;
    int red = 2;
    int blueProMask;
    int redProMask;
    int blueSlimeMask;
    int redSlimeMask;

    bool timeOver = false;
    bool doFight = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        blueProMask = LayerMask.GetMask("BluePro");
        redProMask = LayerMask.GetMask("RedPro");
        blueSlimeMask = LayerMask.GetMask("BlueSlime");
        redSlimeMask = LayerMask.GetMask("RedSlime");
    }

    private void Update()
    {
        Move();
        if (Input.GetKeyDown("space"))
            mat.changeMat(1);
    }

    private void Move()
    {
        if (agent.enabled == false)
            return;
        if (agent.remainingDistance > 0.1f)
            return;
        else
            transform.transform.position = agent.destination;
        
        if (RandomPoint(transform.position, 5f, out nextPoint))
            agent.SetDestination(nextPoint);
        anim.SetBool("isMove", true);

    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BluePro"))
        {
            mat.changeMat(blue);
            gameObject.layer = blueSlimeMask;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedPro"))
        {
            mat.changeMat(red);
            gameObject.layer = redSlimeMask;
        }
    }
}
