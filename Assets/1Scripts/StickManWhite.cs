using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManWhite : StickMan
{
    private bool oneTime = true;
    private void Awake() 
    {
        Init();
        agent.enabled = false;
    }

    private void Update() 
    {
        DoWalk();
        Move();
        TimeOver();
    }

    private void DoWalk()
    {
        if (!oneTime)
            return;
        if (!GameManager.Instance.gameStart)
            return;

        agent.enabled = true;
        anim.SetBool("isMove", true);
        oneTime = false;
    }

    private void TimeOver()
    {
        if (!GameManager.Instance.timeOver)
            return;
        BloodParticle(4);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueObj"))
            Hit(blue);
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedObj"))
            Hit(red);
    }
}
