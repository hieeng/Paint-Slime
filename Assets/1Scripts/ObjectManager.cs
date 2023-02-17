//지오 : 오브젝트 풀링 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectManager : MonoBehaviour
{
    // 0 : BlueProjectile, 1 : RedProjectile, 2,3,4 : BluePaintParticle, 5,6,7: RedPaintParticle 8: WhitePaintParticle
    [SerializeField] GameObject[] prefabs;
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0, size = pools.Length; index < size; index++)
            pools[index] = new List<GameObject>();
    }

    public GameObject Get (int index)
    {
        GameObject select = null;

    //2개씩 나오기도함   ????
/*         foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
            }
        } */

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
