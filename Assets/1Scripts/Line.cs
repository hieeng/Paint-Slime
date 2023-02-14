using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PositionOrder;

public class Line : MonoBehaviour
{
    PositionOrderer orderer = new PositionOrderer();
    public Transform[] transforms;
    bool first = true;

    private void Update() 
    {
        Get();
    }

    private void Get()
    {
        if (!GameManager.Instance.timeOver)
            return;
        if (!first)
            return;

        first = false;
        Debug.Log("#");

        StartCoroutine(CoroutineWait(3f));

        if (transform.childCount <= 2)
            return;

        transforms = new Transform[transform.childCount];
        for (int i = 0, size = transform.childCount; i < size; i++)
            transforms[i] = transform.GetChild(i);

        orderer.Transforms.Clear();
        orderer.Transforms.AddRange(transforms);

        orderer.Distance_X = 0.5f;
        orderer.Distance_Y = 0f;
        orderer.Distance_Z = 0.5f;

        StartCoroutine(CoroutineWait(1f));

        Debug.Log(("@"));
        Move();
    }

    IEnumerator CoroutineWait(float dealyTime)
    {
        float time = 0f;

        while (time <= dealyTime)
        {
            yield return null;
            time += Time.deltaTime;
        }

    }

    private void Move()
    {
        StartCoroutine(CoroutineMovePos());
    }

    IEnumerator CoroutineMovePos()
    {
        float time = 0;

        while (time <= 1f)
        {
            orderer.ApplyCubeOrder(CubeAnchor.Left, 5, 2);
            yield return null;
        }
    }
}
