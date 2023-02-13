using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat : MonoBehaviour
{
    [SerializeField] Material[] mat;

    public void changeMat(int color)
    {
        gameObject.GetComponent<SkinnedMeshRenderer>().material = mat[color];
    }
}
