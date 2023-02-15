using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueSlime"))
            if (GameManager.Instance.allKill)
            {
                GameManager.Instance.isWin = true;
                GameManager.Instance.KnockBack();
            }
    }
}
