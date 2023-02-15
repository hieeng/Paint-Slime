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
    [SerializeField] CameraScript cam;
    [SerializeField] ObjectManager stickManBlue;
    [SerializeField] ObjectManager stickManRed;
    [SerializeField] RagdollScript ragdoll;

    public Transform pointBlue;
    public Transform pointRed;
    public Transform king;

    public float gameTime = 20f;
    public int NumBlue = 0;
    public int NumRed = 0;
    [HideInInspector] public bool timeOver = false;
    [HideInInspector] public bool timeFight = false;
    [HideInInspector] public bool doFight = false;
    public bool allKill = false;
    public bool isWin = false;
    public float startTime = 3f;

    bool gameStart = false;

    static public GameManager Instance;
    
    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        Time.timeScale = 0.0f;
    }

    private void Update() 
    {
        UpdateTime();

        if (Input.GetMouseButtonDown(0) && gameStart == false)
        {
            gameStart = true;
            Time.timeScale = 1.0f;
            SetCamera();
        }
    }

    private void UpdateTime()
    {
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
            timeOver = true;
    }

    public void SetCamera()
    {
        cam.Anim.SetTrigger("CameraTrigger");
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

    public void KnockBack()
    {
        ragdoll.KnockBack();
    }
}
