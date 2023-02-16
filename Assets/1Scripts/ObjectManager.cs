using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs; // 0 : BlueProjectile, 1 : RedProjectile, 2 : BluePaintParticle, 3: RedPaintParticle 4: WhitePaintParticle
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
