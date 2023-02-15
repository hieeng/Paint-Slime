using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PositionOrder;

public class Line : MonoBehaviour
{
    PositionOrderer orderer = new PositionOrderer();
    public Transform[] transforms;
    protected bool first = true;
    protected bool first2 = true;

    protected void Get()
    {
        if (!GameManager.Instance.timeOver)
            return;
        if (!first)
            return;

        first = false;

        StartCoroutine(CoroutineGet(2f));
        StartCoroutine(CoroutineSort(2.5f));
    }

    IEnumerator CoroutineGet(float dealyTime)
    {
        float time = 0f;

        while (time <= dealyTime)
        {
            yield return null;
            time += Time.deltaTime;
        }

        if (transform.childCount <= 2)
            yield break;

        transforms = new Transform[transform.childCount];
        for (int i = 0, size = transform.childCount; i < size; i++)
            transforms[i] = transform.GetChild(i);

        orderer.Transforms.Clear();
        orderer.Transforms.AddRange(transforms);

        orderer.Distance_X = 0.8f;
        orderer.Distance_Y = 0f;
        orderer.Distance_Z = 0.5f;
    }

    IEnumerator CoroutineSort(float dealyTime)
    {
        float time = 0f;

        while (time <= dealyTime)
        {
            yield return null;
            time += Time.deltaTime;
        }
        Sort();
    }

    private void Sort()
    {
        StartCoroutine(CoroutineMovePos());
    }

    IEnumerator CoroutineMovePos()
    {
        float time = 0;

        if (transform.childCount < 2)
            yield break;

        while (time <= 1f)
        {
            orderer.ApplyCubeOrder(CubeAnchor.Center, 7, 2);
            time += Time.deltaTime;

            yield return null;
        }

        time = 0;

        while (time <= 3.5f)
        {
            Move();
            time += Time.deltaTime;

            yield return null;
        }
    }

    protected void Move()
    {
        if (GameManager.Instance.doFight)
            return;
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, 3f * Time.deltaTime);
    }
}
