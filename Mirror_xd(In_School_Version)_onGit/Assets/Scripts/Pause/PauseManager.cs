using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseManager : MonoBehaviour
{

    AudioManager audioManager;
    BGMManager bgmManager;

    public static bool showSkip = false;
    public bool isShowingSkip = false;
    
    public float countTime = 87f;

    public GameObject SkipBtn;

    [Header("音量相關")]
    public GameObject audioSlider;
    public GameObject bgmSlider;

    [Header("暫停介面")]
    public GameObject PauseObj;
    public GameObject PauseMain;
    public GameObject PauseSetting;
    public GameObject PauseTitleText;
    public GameObject Countdown;
    public GameObject ScreenSaver;
    public GameObject ScreenSaverLogo;

    [Header("彈幕效果")]
    public GameObject BarrageText1;
    public GameObject BarrageText2;

    // 確認暫停介面是否開啟
    public bool isOpen = false;
    // 倒數的時長
    float timeRemain;
    // 開始倒數的開關
    public bool startCount = false;

    IEnumerator ie;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();

        // =========================
        audioSlider.GetComponent<Slider>().value = audioManager.GetAudioVolume();
        bgmSlider.GetComponent<Slider>().value = bgmManager.GetBgmVolume();

        // =========================
        // 確認是否開啟skip按鈕，判別第二關要不要開啟用的
        SkipBtn.SetActive(showSkip);
    }

    public void OpenSkipBtn()
    {
        showSkip = true;
        SkipBtn.SetActive(true);
    }

    public void ClickSkipBtn()
    {
        // 正在做跳過動畫
        isShowingSkip = true;
        foreach (Button btn in PauseMain.GetComponentsInChildren<Button>())
        {
            btn.interactable = false;
        }
        // =======================================
        audioManager.ClickMythSetupCancelOrX();
        StartCoroutine(SkipFall());
    }

    IEnumerator SkipFall()
    {
        SkipBtn.transform.GetChild(0).transform.DOLocalMoveY(-900f, 0.7f).SetEase(Ease.InBack).SetUpdate(true);
        yield return BarrageEffect();
        showSkip = false;
        SkipBtn.SetActive(false);
    }

    IEnumerator BarrageEffect()
    {
        Sequence seq1 = DOTween.Sequence();
        BarrageText1.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        BarrageText1.transform.DOShakePosition(2f, 20f, 20, 30f, false, true).SetUpdate(true);
        yield return new WaitForSecondsRealtime(2f);
        BarrageText1.SetActive(false);
        seq1.Append(BarrageText2.transform.DOLocalMoveX(-4270f, 15f).SetEase(Ease.Linear).SetUpdate(true));
        yield return new WaitForSecondsRealtime(15f);
        // 跳過動畫結束
        // 正在做跳過動畫
        isShowingSkip = false;
        foreach (Button btn in PauseMain.GetComponentsInChildren<Button>())
        {
            btn.interactable = true;
        }
        //seq1.Join(BarrageText2.transform.DOShakeRotation(3f, 5f, 5, 30f, true).SetUpdate(true));
    }

    public void StartCountDown()
    {
        timeRemain = countTime;
        ie = CountdownCount();
        StartCoroutine(ie);
    }

    IEnumerator CountdownCount()
    {
        while(timeRemain > 0)
        {
            timeRemain -= 1f;
            Countdown.GetComponent<Text>().text = Mathf.Round(timeRemain).ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        if(timeRemain <= 0)
        {
            handleScreenSaver(true);
            ScreenSaverLogo.GetComponent<PauseScreenSaver>().CallScreenSaver();
        }
    }

    #region 音量相關
    public void OnChangeAudioVol()
    {
        audioManager.UpdateAudioVolume(audioSlider.GetComponent<Slider>().value);
    }
    public void OnChangeBGMVol()
    {
        bgmManager.UpdateBgmVolume(bgmSlider.GetComponent<Slider>().value);
    }
    #endregion

    #region 暫停
    public void OpenPause()
    {
        // ========================
        bgmManager.PauseBgm();
        // ========================
        isOpen = true;
        // ========================
        audioManager.ClickPause();
        PauseObj.SetActive(true);
        Time.timeScale = 0; // 暫停遊戲
        StartCountDown();   // 開始倒數
    }
    public void ClosePause()
    {
        // ========================
        bgmManager.ResumeBgm();
        // ========================
        isOpen = false;
        // ========================
        PauseObj.SetActive(false);
        StopCoroutine(ie);
        Time.timeScale = 1;
    }
    public void handleSetting(bool toOpen)
    {
        PauseMain.SetActive(!toOpen);
        PauseSetting.SetActive(toOpen);
        if (toOpen)
        {
            PauseTitleText.GetComponent<Text>().text = "選擇要設定的選項，或是按下上一頁返回：";
        }
        else
        {
            PauseTitleText.GetComponent<Text>().text = "選擇選項，或是按返回遊戲：";
        }
    }
    public void handleScreenSaver(bool toOpen)
    {
        PauseObj.SetActive(!toOpen);
        ScreenSaver.SetActive(toOpen);
    }
    #endregion

}
