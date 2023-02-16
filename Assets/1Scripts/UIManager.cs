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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeUpdate();
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
}
