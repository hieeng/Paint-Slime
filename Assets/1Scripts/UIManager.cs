//미치 : UI컨트롤 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Text TimeText;
    [SerializeField] List<Canvas> CanvasList = new List<Canvas>(); // 0 : MainMenuCanvas, 1 : InGameCanvas, 2 : EndGameCanvas
    int GameTime;

    [SerializeField] float delayTime;
    [SerializeField] Text feverText;
    [SerializeField] Image feverGauge;

    void Update()
    {
        TimeUpdate();
        FeverGague();
    }

    void TimeUpdate()
    {
        GameTime = (int)GameManager.Instance.GetGameTime();

        if(GameTime >= 0.0f) 
        {
            TimeText.text = GameTime.ToString();

            if(GameTime < 4.0f) TimeText.color = new Color(255f, 0f, 0f, 1f);
        }
    }

    public bool TurnOnCanvas(int cnt)
    {
        if(cnt < CanvasList.Count)
        {
            StartCoroutine(TurnOnCorountine(cnt));
            return true;
        }
        return false;
    }

    IEnumerator TurnOnCorountine(int cnt)
    {
        CanvasList[cnt].gameObject.SetActive(true);
        float alpha = 0.0f;

        while(alpha < 1.0f)
        {
            yield return null;
            alpha += 0.01f;
            CanvasList[cnt].GetComponent<CanvasGroup>().alpha = alpha;
        }
    }

    public bool TurnOffCanvas(int cnt)
    {
        if(cnt < CanvasList.Count)
        {
            StartCoroutine(TurnOffCorountine(cnt));
            return true;
        }
        return false;
    }

    IEnumerator TurnOffCorountine(int cnt)
    {
        float alpha = 1.0f;

        while(alpha > 0.0f)
        {
            yield return null;
            alpha -= 0.01f;
            CanvasList[cnt].GetComponent<CanvasGroup>().alpha = alpha;
        }
        CanvasList[cnt].gameObject.SetActive(false);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowFever()
    {
        feverText.gameObject.SetActive(true);
        StartCoroutine(CoruotineFever(feverText));
    }

    //지오
    public void FeverGague()
    {
        feverGauge.fillAmount = GameManager.Instance.GetFeverGage() / GameManager.Instance.feverMaxGague;
    }
    
    //지오
    IEnumerator CoruotineFever(Text text)
    {
        var time = 0f;
        var origin = text.color;

        while (time <= delayTime)
        {
            yield return null;
            time += Time.deltaTime;
        }

        time = 0;

        while (time <= delayTime)
        {
            yield return null;
            time += Time.deltaTime;

            var textColor = text.color;
            textColor.a = Mathf.Lerp(origin.a, 0, time / delayTime);
            text.color = textColor;
        }
        text.color = origin;
        text.gameObject.SetActive(false);
    }


}
