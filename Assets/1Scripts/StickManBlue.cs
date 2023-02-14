using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManBlue : StickMan
{
    private void Awake() 
    {
        Init();
        point = GameManager.Instance.pointBlue;
        layer = LayerMask.GetMask("RedSlime");
    }

    private void Start() 
    {
        HitAction();
    }

    private void Update() 
    {
        Move();
        GaterPoint();
        Target(layer);
        //ToMoveTarget();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueObj"))
            HitAction();
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedObj"))
        {
            Hit(red);
        }

        if (GameManager.Instance.timeOver)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("RedSlime"))
                Die();
        }
    }
}
