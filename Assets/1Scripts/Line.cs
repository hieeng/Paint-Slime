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
    int idx;

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

        transforms = new Transform[transform.childCount];
        for (int i = 0, size = transform.childCount; i < size; i++)
            transforms[i] = transform.GetChild(i);

        if (transform.childCount <= 2)
            yield break;

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
        FindMinIdx();

        if (transform.childCount <= 2)
            yield break;

        while (time <= 1f)
        {
            orderer.ApplyCubeOrder(CubeAnchor.Custom, 5, 2, idx);
            time += Time.deltaTime;

            yield return null;
        }

        time = 0;

        GameManager.Instance.SetCamera();
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

    private void FindMinIdx()
    {
        float temp;
        float min = 0;

        for (int i = 0, size = transform.childCount; i < size; i++)
        {
            if (i == 0)
            {
                min = Vector3.Distance(transform.position, transforms[i].position);
                idx = i;
            }
            else
            {
                temp = Vector3.Distance(transform.position, transforms[i].position);

                if (temp < min)
                {
                    min = temp;
                    idx = i;
                }
            }
        }
    }
}
