using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainManager : MonoBehaviour
{
    // backgroundText 的內容
    const string chooseModeText = "選擇選項以開始遊戲，或是按下離開：\n(使用滑鼠來反白您的選項，然後按下左鍵。)\n\n\n\t開始遊戲\n\n\t選擇關卡\n\n\t設定\n\n\t離開\n\n\n等待自動執行螢幕保護程式的秒數：";
    const string chooseLevelText = "選擇要開始的關卡，或是按下上一頁返回：\n\n\n\n\n\n\n\n\n\n\t上一頁\n\n\n等待自動執行螢幕保護程式的秒數：";
    // ============================================================
    public bool isFinishTyping = false;
    public bool isSelectLevel = false; // 目前是否為選擇關卡的模式

    [Header("倒數的數字")]
    public GameObject Countdown;

    [Header("主畫面Canvas")]
    public GameObject Main;
    public GameObject Level;
    public GameObject MainCanvas;
    public GameObject BackgroundText;

    [Header("設定頁面")]
    public GameObject SettingPage;
    public GameObject BGMSlider;
    public GameObject AudioSlider;

    [Header("螢幕保護程式")]
    public GameObject ScreenSaver;
    public GameObject Logo;

    [Header("各個按鈕")]
    public GameObject[] MainBtn;
    public GameObject[] LevelBtn;
    public Sprite[] darkSprite;
    public Sprite[] colorSprite;

    [Header("Loading跑條")]
    public GameObject LoadingCanvas;
    public GameObject Loading_dot;

    [Header("第一關Login畫面")]
    public GameObject LoginCanvas;

    // Manager
    AudioManager audioManager;
    BGMManager bgmManager;
    SceneManagement sceneManagement;
    MainCursor cursor;

    // 倒數的時長
    public float timeRemain = 87f;
    // 開始倒數的開關
    public bool startCount = false;
    public bool startScreenSaver = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        sceneManagement = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        cursor = GameObject.Find("MainManager").GetComponent<MainCursor>();
        cursor.ChangeState("Default");
        AniEvents.AniDone = true;
        Ch2AniEvents.AniDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCount && timeRemain > 0)
        {
            startScreenSaver = false;
            timeRemain -= Time.deltaTime;
            Countdown.GetComponent<Text>().text = Mathf.Round(timeRemain).ToString();
        }
        else if (!startScreenSaver && timeRemain <= 0)
        {
            cursor.ChangeState("Default");
            ToScreenSaver();
            startScreenSaver = true;
        }
    }

    public void FinshWriteEffect()
    {
        // 開啟倒數文字
        Countdown.SetActive(true);
        // 開始倒數
        startCount = true;
    }

    void ToScreenSaver()
    {
        // 倒數結束 關閉遊戲
        MainCanvas.SetActive(false);
        ScreenSaver.SetActive(true);
        Logo.GetComponent<ScreenSaver>().CallScreenSaver();
    }

    public void HandleSelectMode(bool isSelect)
    {
        if (isSelect)
        {
            // 切換成選擇關卡
            BackgroundText.GetComponent<Text>().text = chooseLevelText;
            // =======================
        }
        else
        {
            // 切換成選擇模式
            BackgroundText.GetComponent<Text>().text = chooseModeText;
            // =======================
        }
        // =========================
        foreach (GameObject obj in MainBtn)
        {
            obj.SetActive(!isSelect);
        }
        foreach (GameObject obj in LevelBtn)
        {
            obj.SetActive(isSelect);
        }
        // 改變目前是選擇功能還是選擇關卡
        isSelectLevel = isSelect;
    }

    int ConvertLevelToNum(string name)
    {
        switch (name)
        {
            case "Level1":
                return 0;
            case "Level2":
                return 1;
            case "Level3":
                return 2;
            default:
                return 3;
        }
    }

    public void ChangeWhenPointEnter(string name)
    {
        int num = ConvertLevelToNum(name);
        // 背景顏色
        LevelBtn[num].GetComponent<Image>().color = new Color32(217, 217, 217, 255);
        // 上面的文字顏色
        LevelBtn[num].transform.GetChild(1).GetComponent<Text>().color = new Color32(42, 42, 42, 255);
        LevelBtn[num].transform.GetChild(0).GetComponent<Image>().sprite = colorSprite[num];
    }

    public void ChangeWhenPointerExit(string name)
    {
        int num = ConvertLevelToNum(name);
        // 背景顏色
        LevelBtn[num].GetComponent<Image>().color = new Color32(42, 42, 42, 255);
        // 上面的文字顏色
        LevelBtn[num].transform.GetChild(1).GetComponent<Text>().color = new Color32(217, 217, 217, 255);
        LevelBtn[num].transform.GetChild(0).GetComponent<Image>().sprite = darkSprite[num];
    }

    public bool ReturnSelectStatus()
    {
        return isSelectLevel;
    }

    public void FinishTyping()
    {
        FinshWriteEffect();
        isFinishTyping = true;
    }

    public bool ReturnTypingStatus()
    {
        return isFinishTyping;
    }

    // =========================================
    // Loading Animation
    public void PlayLoadingAnim(int ch)
    {
        cursor.ChangeState("Default");
        startCount = false;     // 停止倒數
        MainCanvas.SetActive(false);
        ScreenSaver.SetActive(false);
        // =============================
        LoadingCanvas.SetActive(true);

        // 開始pre loading scene
        sceneManagement.LoadingAsync(ch);
        StartCoroutine(LoadingAnim(ch));

    }
    IEnumerator LoadingAnim(int ch)
    {
        
        for (int i = 0; i < 5; i++)
        {

            yield return LoadingBar();
            // 回到-180

            // Loading_dot.transform.localPosition = new Vector3(-180f, 0, 0);
        }
        Loading_dot.SetActive(false);
        if(ch == 1)
        {
            // 第一關進入登入畫面
            LoginCanvas.SetActive(true);
        }
        else
        {
            // 直接進入遊戲
            sceneManagement.StartToPlay(ch);
        }
    }

    IEnumerator LoadingBar()
    {
        float speed = 240f;
        float step = (speed / 360f) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1f)
        {
            t += step;
            Loading_dot.transform.localPosition = Vector3.Lerp(new Vector3(-180f, 0, 0), new Vector3(180f, 0, 0), t);
            //Loading_dot.transform.position = Vector3.MoveTowards(new Vector3(-180f, 0, 0), new Vector3(180f, 0, 0), 1.5f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        Loading_dot.transform.localPosition = new Vector3(-180f, 0, 0);
    }

    #region Login畫面
    public void PressLoginLeave()
    {
        // 離開遊戲
        audioManager.MainLeaveGame();
        StartCoroutine(ToQuitGame()); 
    }
    IEnumerator ToQuitGame()
    {
        yield return new WaitForSeconds(1f);
        sceneManagement.QuitGame();
    }

    public void PressLoginEnterGame1()
    {
        // 進入第一關遊戲
        audioManager.ClickUserLoginAudio();
        StartCoroutine(PressToGame1());
    }
    IEnumerator PressToGame1()
    {
        yield return new WaitForSeconds(0.1f);
        sceneManagement.StartToPlay(1);
    }
    #endregion

    #region 設定
    public void OpenSettingPage(bool toOpen)
    {
        if (toOpen)
        {
            BackgroundText.SetActive(false);
            Main.SetActive(false);
            // 開啟設定
            BGMSlider.GetComponent<Slider>().value = bgmManager.bgmVolume;
            AudioSlider.GetComponent<Slider>().value = audioManager.audioVolume;
        }
        else
        {
            // 關閉設定
            BackgroundText.SetActive(true);
            Main.SetActive(true);
        }
        SettingPage.SetActive(toOpen);
    }
    public void OnChangeAudioVol()
    {
        audioManager.UpdateAudioVolume(AudioSlider.GetComponent<Slider>().value);
    }
    public void OnChangeBGMVol()
    {
        bgmManager.UpdateBgmVolume(BGMSlider.GetComponent<Slider>().value);
    }
    #endregion
}
