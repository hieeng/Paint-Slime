//지오 : 빨간색 스틱맨들을 모이게 하는 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PositionOrder;

public class Red : Line
{
    private void Update()
    {
        Get();
        CountRed();
    }

    private void CountRed()
    {
        if (!GameManager.Instance.timeOver)
            return;
        if (!first2)
            return;

        first2 = false;

        StartCoroutine(CoruotineWait(2f));
    }

    IEnumerator CoruotineWait(float dealyTime)
    {
        float time = 0f;

        while (time <= dealyTime)
        {
            yield return null;
            time += Time.deltaTime;
        }

        GameManager.Instance.NumRed = transform.childCount;
    }
}
