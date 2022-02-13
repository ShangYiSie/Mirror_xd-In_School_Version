using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseBtn : MonoBehaviour
{
    AudioManager audioManager;
    SceneManagement sceneManagement;
    PauseManager pauseManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        sceneManagement = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter()
    {
        if (!pauseManager.isShowingSkip)
        {
            audioManager.MainPointerEnter();
            // 改樣式
            // this.GetComponent<Image>().color = new Color32(217, 217, 217, 255);
            this.transform.GetChild(0).GetComponent<Text>().color = new Color32(42, 42, 42, 255);
        }
    }

    public void OnPointerLeave()
    {
        if (!pauseManager.isShowingSkip)
        {
            // 改樣式
            // this.GetComponent<Image>().color = new Color32(42, 42, 42, 255);
            this.transform.GetChild(0).GetComponent<Text>().color = new Color32(217, 217, 217, 255);
        }
    }

    public void OnClick()
    {
        // 把文字都改回D9D9D9
        this.transform.GetChild(0).GetComponent<Text>().color = new Color32(217, 217, 217, 255);
        // =========================
        switch (this.name)
        {
            case "BackToGame":
                // 繼續遊戲
                audioManager.ClosePause();
                pauseManager.ClosePause();
                break;
            case "Skip":
                audioManager.MainButtonClick(false);
                // 跳過劇情
                break;
            case "Setting":
                // 設定
                audioManager.MainButtonClick(false);
                pauseManager.handleSetting(true);
                break;
            case "BackToMain":
                // 回主畫面
                audioManager.MainButtonClick(false);
                Time.timeScale = 1;
                sceneManagement.ToMainScene();
                break;
            case "QuitGame":
                // 離開遊戲
                sceneManagement.QuitGame();
                break;
            case "ProducerBtn":
                audioManager.MainButtonClick(false);
                Debug.Log("製作者名單");
                break;
            case "BackPrev":
                // 上一頁
                audioManager.ClosePause();
                pauseManager.handleSetting(false);
                break;
        }
    }
}
