using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Player player;
    [SerializeField] Slime slime;
    [SerializeField] Projectile projectile;
    [SerializeField] AI ai;

    static public GameManager Instance;
    
    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public GameObject GetObj(int value)
    {
        return objectManager.Get(value);
    }
}
