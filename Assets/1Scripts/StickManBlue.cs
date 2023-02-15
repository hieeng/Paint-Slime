using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManBlue : StickMan
{
    Transform king;

    private void Awake() 
    {
        Init();
        anim.SetFloat("rand", Random.Range(0f, 1f));
        point = GameManager.Instance.pointBlue;
        king = GameManager.Instance.king;
        layer = LayerMask.GetMask("RedSlime");
    }

    private void Start() 
    {
        HitAction();
    }

    private void Update() 
    {
        Move();
        GatherPoint();
        SpritePoint();
        OffFightAnim();
        MoveKing();
        Dance();
        if (!GameManager.Instance.allKill)
        {
            Target(layer);
            ToMoveTarget();
            OnFightAnim();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueObj"))
            HitAction();
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedObj"))
        {
            Hit(red);
        }
    }

    public void Die()
    {
        StartCoroutine(CoroutineDie());
    }

    IEnumerator CoroutineDie()
    {
        float time  = 0;

        while (time <= deadTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.NumBlue--;
        gameObject.SetActive(false);
        BloodParticle();
    }

    private void MoveKing()
    {
        if (!GameManager.Instance.allKill)
            return;
        if (GameManager.Instance.isWin)
            return;

        speed = 3f;
        transform.LookAt(king);
        var dirVec = king.transform.position - rigid.position;
        var nextVec = dirVec.normalized * speed * Time.deltaTime;
 
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void Dance()
    {
        if (!GameManager.Instance.isWin)
            return;
        anim.SetBool("isWin", true);
        transform.LookAt(GameManager.Instance.pointBlue);
    }

    void BloodParticle()
    {
        var blood = GameManager.Instance.GetObj(2);
        blood.transform.position = transform.position;
        blood.transform.rotation = Quaternion.identity;
        blood.GetComponent<ParticleSystem>().Play();
    }
}
