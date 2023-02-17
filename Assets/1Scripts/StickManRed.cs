using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManRed : StickMan
{
    private void Awake() 
    {
        Init();
        point = GameManager.Instance.pointRed;
        layer = LayerMask.GetMask("BlueSlime");
        objNum = Random.Range(5, 8);
    }

    private void Start() 
    {
        HitAction();
    }

    private void Update() 
    {
        Move();
        GatherPoint();
        Target(layer);
        ToMoveTarget();
        OnFightAnim();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueObj"))
        {
            Hit(blue);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedObj"))
        {
            HitAction();
            MovePoint();
        }

        if (GameManager.Instance.timeFight)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("BlueSlime"))
            {
                GameManager.Instance.doFight = true;
                if (!other.gameObject.GetComponent<StickManBlue>().isFighting)
                {
                    if (isFighting)
                        return;

                    other.gameObject.GetComponent<StickManBlue>().deadTime = deadTime;
                    other.gameObject.GetComponent<StickManBlue>().isFighting = true;
                    other.gameObject.GetComponent<StickManBlue>().Die();
                    isFighting = true;
                    Die();
                }
            }
        }
    }

    private void Die()
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
        GameManager.Instance.NumRed--;
        if (GameManager.Instance.NumRed == 0)
        {
            GameManager.Instance.allKill = true;
            GameManager.Instance.SetCamera();
        }
        gameObject.SetActive(false);
        BloodParticle(objNum);
    }
}
