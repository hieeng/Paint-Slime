using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PositionOrder;

public class Line : MonoBehaviour
{
    PositionOrderer orderer = new PositionOrderer();
    public Transform[] transforms;
    bool first = true;

    private void Update() {
        temp();
    }

    private void temp()
    {
        if (!GameManager.Instance.timeOver)
            return;
        if (!first)
            return;
    }
}
