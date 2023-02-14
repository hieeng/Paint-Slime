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
    [SerializeField] ObjectManager stickManBlue;
    [SerializeField] ObjectManager stickManRed;

    public Transform pointBlue;
    public Transform pointRed;

    public float gameTime = 20f;
    [HideInInspector] public bool timeOver = false;

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

    private void Update() 
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
            timeOver = true;
    }

    public GameObject GetObj(int value)
    {
        return objectManager.Get(value);
    }

    public GameObject GetBlue(int value)
    {
        return stickManBlue.Get(value);
    }

    public GameObject GetRed(int value)
    {
        return stickManRed.Get(value);
    }
}
