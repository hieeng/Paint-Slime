//지오
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : Line
{
    private void Update() 
    {
        Get();
        CountBlue();
    }

    private void CountBlue()
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

        GameManager.Instance.NumBlue = transform.childCount;
    }
}
