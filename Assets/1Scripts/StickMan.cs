using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StickMan : MonoBehaviour
{
    protected Rigidbody rigid;
    protected NavMeshAgent agent;
    protected Animator anim;
    Vector3 nextPoint;

    [SerializeField] ParticleSystem particle;
    [SerializeField] protected float speed;
    [SerializeField]GameObject target;

    protected Transform point;
    protected int blue = 0;
    protected int red = 1;
    protected int layer;
    protected GameObject obj;
    float gatherTime = 0f;
    public float deadTime;
    public bool isFighting = false;

    protected void Init()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetFloat("rand", Random.Range(0f, 1f));
        deadTime = Random.Range(2f, 3f);
    }

    protected void Move()
    {
        if (agent.enabled == false)
            return;
        if (agent.remainingDistance > 0.05f)
            return;
        else
            transform.transform.position = agent.destination;
        
        anim.SetBool("isMove", true);
        if (RandomPoint(transform.position, 3f, out nextPoint))
            agent.SetDestination(nextPoint);
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
        result = Vector3.zero;
        return false;
    }

    protected void Hit(int color) 
    { 
        if (color == blue)
            obj = GameManager.Instance.GetBlue(0);
        else
            obj = GameManager.Instance.GetRed(0);
        
        obj.transform.position = transform.position;
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    protected void HitAction()
    {
        anim.SetTrigger("isHit");
        //StartCoroutine(CoroutineHit());
    }

    protected IEnumerator CoroutineHit()
    {
        float time = 0;
        agent.speed = 0.5f;

        while (time < 0.5f)
        {
            yield return null;

            time += Time.deltaTime;
        }
        agent.speed = 1.5f;
    }

    protected void GatherPoint()
    {
        if (!GameManager.Instance.timeOver)
            return;
        if (GameManager.Instance.timeFight)
            return;
        gatherTime += Time.deltaTime;
        GPoint();
        if (gatherTime >= 3f)
        {
            GameManager.Instance.timeFight = true;
            gatherTime = 0;
        }
    }

    private void GPoint()
    {
        agent.enabled = false;
        var pointVec = point.position - rigid.position;
        transform.LookAt(point);
        speed = pointVec.magnitude;

        if (pointVec.magnitude < 1f)
            speed = 0.3f;

        var pointNextVec = pointVec.normalized * speed * Time.deltaTime;

        rigid.MovePosition(rigid.position + pointNextVec);
    }

    protected void Target(int layer)
    {
        if (!GameManager.Instance.timeFight)
            return;

        Collider[] cols = Physics.OverlapSphere(this.transform.position, 10f, layer);
        float temp;
        float min = 0;

        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (i == 0)
                {
                    min = Vector3.Distance(cols[i].transform.position, transform.position);
                    target = cols[i].gameObject;
                }
                else
                {
                    temp = Vector3.Distance(cols[i].transform.position, transform.position);
                    if (temp < min)
                    {
                        min = temp;
                        target = cols[i].gameObject;
                    }
                }
            }
        }
        else
        {
            target = null;
            min = 9999;
        }
    }

    protected void ToMoveTarget()
    {
        if (target == null)
            return;
        speed = 0.5f;
        var dirVec = target.transform.position - rigid.position;
        var nextVec = dirVec.normalized * speed * Time.deltaTime;
 
        rigid.MovePosition(rigid.position + nextVec);
        transform.LookAt(target.transform.position);
    }

    protected void OnFightAnim()
    {
        if (!GameManager.Instance.doFight)
            return;
        anim.SetBool("isFight", true);
    }

    protected void OffFightAnim()
    {
        if (!GameManager.Instance.allKill)
            return;
        anim.SetBool("isFight", false);
    }

    protected void BloodParticle(int num)   // 미치 : 파티클 출력부. 지오님 코드에 맞춰서 Stickman에서 선언하여 자식 오브젝트에서 오버라이드하여 사용
    {
        var blood = GameManager.Instance.GetObj(num);
        blood.transform.position = transform.position;
        blood.transform.rotation = Quaternion.identity;
        blood.GetComponent<ParticleSystem>().Play();
    }

/*     protected IEnumerator CoroutineDie()
    {
        float time  = 0;

        while (time <= deadTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        //파티클
    } */
}
