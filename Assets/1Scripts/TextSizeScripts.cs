// 미치 : UI의 텍스트의 크기 확대/축소 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSizeScripts : MonoBehaviour
{
    float time = 0f;
    bool flag = true;

    void Update()
    {
        if(flag)
        {
            transform.localScale = Vector3.one * (0.9f + time/6);
            time += Time.deltaTime;
            
            if(time > 1.0f)
            {
                flag = false;
            }
        }

        else
        {
            transform.localScale = Vector3.one * (0.9f + time/6);
            time -= Time.deltaTime;
            
            if(time < 0f)
            {
                flag = true;
            }
        }
    }
}
