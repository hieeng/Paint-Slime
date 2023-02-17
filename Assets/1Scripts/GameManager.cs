//지오 : 게임매니저
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Player player;
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
    public int feverMaxGague;
    [HideInInspector] public bool timeOver = false;
    [HideInInspector] public bool timeFight = false;
    [HideInInspector] public bool doFight = false;
    public bool allKill = false;
    public bool isWin = false;
    public float startTime = 3f;

    public bool gameStart = false;

    int camerTrigger = 0;
    public int count = 0;
    static public GameManager Instance;
    
    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
       //Time.timeScale = 0.0f;
    }

    private void Update() 
    {
        if(gameStart) UpdateTime();

        if (Input.GetMouseButtonDown(0) && gameStart == false)
        {
            gameStart = true;
            //Time.timeScale = 1.0f;
            //white.OnWhiteStickMan();
            SetCamera();

            TurnOnCanvas(1);
            TurnOffCanvas(0);
        }
    }

    private void UpdateTime()
    {
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            timeOver = true;
            TurnOffCanvas(1);
        }
            
    }

    // 미치 : 현재 게임 시간 가져오는 함수입니다.
    public float GetGameTime()
    {
        return gameTime;
    }

    public void SetCamera()
    {
        camerTrigger++;
        cam.Anim.SetInteger("trigger", camerTrigger);
        //cam.Anim.SetTrigger("CameraTrigger");
    }

    public void PlusFeverGage()
    {
        count++;
        StartCoroutine(CoruotinePlusFeverGage(count));
    }

    IEnumerator CoruotinePlusFeverGage(int var)
    {
        float time = 0;

        Debug.Log(count);
        while (time <= 0.2f)
        {
            time += Time.deltaTime;
            player.feverGauge = Mathf.Lerp(player.feverGauge, var, time / 0.2f);
            yield return null;
        }
    }

    public float GetFeverGage()
    {
        return player.feverGauge;
    }
    
    public bool GetIsFever()
    {
        return player.feverTime;
    }

     // 미치 : 다른 클래스에서 uiManager에게 직접 접근하지 않게하기 위한 함수.
    public void TurnOnCanvas(int cnt)  
    {
        uIManager.GetComponent<UIManager>().TurnOnCanvas(cnt);
    }

    // 미치 : 다른 클래스에서 uiManager에게 직접 접근하지 않게하기 위한 함수.
    public void TurnOffCanvas(int cnt)  
    {
        uIManager.GetComponent<UIManager>().TurnOffCanvas(cnt);
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

    public void ShowFever()
    {
        uIManager.ShowFever();
    }
}
