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
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueObj"))
        {
            Hit(blue);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("RedObj"))
            HitAction();

        if (GameManager.Instance.timeFight)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("BlueSlime"))
            {
                var check = other.gameObject.GetComponent<StickManBlue>().firstFight;
                if (!check)
                    return;
                firstFight = false;
                Die();
            }
        }
    }
}
