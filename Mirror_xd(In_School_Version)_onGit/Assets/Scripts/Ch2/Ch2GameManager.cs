using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch2GameManager : MonoBehaviour
{
    Ch2Usermanager ch2Usermanager;
    Ch2VisualManager ch2VisualManager;
    Ch2DialogueManager ch2DialogueManager;
    Game2Manager game2Manager;
    Game3Manager game3Manager;
    AudioManager audioManager;
    PauseManager pauseManager;
    BGMManager bgmManager;
    public static bool startFromMain = false;

    public enum Chapter
    {
        CH1, CH2, CH3
    }
    public enum CH2State
    {
        MythGameFront, MythGameBack
    }
    public enum Games
    {
        Dickimon, BreakoutSafe, RuthlessRaccoon, Antivirus, Myth
    }
    public static Chapter nowChapter = Chapter.CH2;
    public static CH2State nowCH2State = CH2State.MythGameFront;
    public static Games nowGames = Games.Myth;
    public static Games tempGames;

    [ShowOnly] public Chapter nowChapterToshow;
    [ShowOnly] public CH2State nowCH2StateToshow;

    [ShowOnly] public Games nowGameToshow;
    // Start is called before the first frame update

    [Header("第一次")]
    public bool ChangeToBackFirst = true;
    public static bool PutBrandToDiffFrameFirst = true; // 第一次把任何名牌裝在不是他的遊戲上
    public static bool PullBrand = true;//第一次要把名牌拔下來
    public int stage = 1;

    static public int nowstage = 1;
    void Start()
    {
        //defaultset
        nowChapter = Chapter.CH2;
        nowCH2State = CH2State.MythGameFront;
        nowGames = Games.Myth;
        PutBrandToDiffFrameFirst = true;
        nowstage = 1;
        //defaultset

        ch2VisualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();
        ch2Usermanager = GameObject.Find("UserManager").GetComponent<Ch2Usermanager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        game2Manager = ch2VisualManager.Game2Frame.GetComponent<Game2Manager>();
        game3Manager = ch2VisualManager.Game3Frame.GetComponent<Game3Manager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        //if (startFromMain)
        //{
        //    // 開機音效
        //    audioManager.StartComputerAudio();
        //    startFromMain = false;
        //}
    }
    // Update is called once per frame
    void Update()
    {
        nowstage = stage;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(2))
        {
            if (!pauseManager.isOpen)
            {
                pauseManager.OpenPause();
            }
        }
        // =================================
        nowChapterToshow = nowChapter;
        nowCH2StateToshow = nowCH2State;
        nowGameToshow = nowGames;

        if (ch2VisualManager.MythGameBack.activeSelf && Input.GetMouseButtonDown(0))
        {
            ch2Usermanager.ToMoveCharacter();
        }
        else if (nowGames == Games.BreakoutSafe && Input.GetMouseButtonDown(0))
        {
            game2Manager.Move();
        }
        else if (nowGames == Games.RuthlessRaccoon && Input.GetMouseButtonDown(0))
        {
            game3Manager.Move();
        }
    }
    public void ClickSkipBtn()
    {
        pauseManager.ClickSkipBtn();
    }

    #region 正反切換
    public void ChangeToBack()
    {
        // 關閉目前的dialogue
        ch2DialogueManager.StopNowDialogue();
        // ====================
        if (Ch2AniEvents.AniDone)
        {
            if (ChangeToBackFirst)
            {
                // 觸發第一次進到背面的文本
                ch2DialogueManager.ShowNextDialogue("第一次進到反面", false);
                ChangeToBackFirst = false;
            }
            // 播放鏡子破掉後的點擊聲
            audioManager.ClickBrokenMirror();
            // ====================
            ch2VisualManager.ChangeToBack();
        }
    }
    public void ChangeToFront()
    {
        // ====================
        if (Ch2AniEvents.AniDone)
        {
            // 觸發進入黑洞音效
            audioManager.EnterBlackhole();
            // =================================
            ch2VisualManager.ChangeToFront();
            //game2Manager.CheckCharactor();
        }
    }
    #endregion

    #region 正面遊戲開關控制
    public void OpenGame1()
    {
        if (nowGames == Games.Myth)
        {
            // 呼叫開啟音效
            // 目前都先播放可以打開的音效
            ChooseOpenAudio(true);
            // =========================
            nowGames = Games.Dickimon;
            ch2VisualManager.OpenGame();
        }
    }
    public void OpenGame2()
    {
        if (nowGames == Games.Myth && stage >= 2)
        {
            // 呼叫開啟音效
            // 目前都先播放可以打開的音效
            ChooseOpenAudio(true);
            // =========================
            nowGames = Games.BreakoutSafe;
            ch2VisualManager.OpenGame();
            game2Manager.CheckCharactor();
        }
    }
    public void OpenGame3()
    {
        if (nowGames == Games.Myth && stage >= 3)
        {
            // 呼叫開啟音效
            // 目前都先播放可以打開的音效
            ChooseOpenAudio(true);
            // =========================
            nowGames = Games.RuthlessRaccoon;
            ch2VisualManager.OpenGame();
            game3Manager.CheckCharactor();
        }
    }
    public void ChooseOpenAudio(bool canOpen)
    {
        if (canOpen)
        {
            // 可以打開的音效
            audioManager.ClickWindowsIconOrDogIcon();
        }
        else
        {
            // 不能打開的音效
            audioManager.OpenCantOpenedWindow();
        }
    }
    public void CloseGame1()
    {
        ch2VisualManager.CloseGame();
    }
    public void CloseGame2()
    {
        ch2VisualManager.CloseGame();
    }
    public void CloseGame3()
    {
        ch2VisualManager.CloseGame();
        game3Manager.CharacterPos = game3Manager.Character.transform.position;
    }
    public void CloseAntiWindow()
    {
        ch2VisualManager.CloseGame();
    }
    #endregion

    #region 防毒軟體
    public void ClickViewHistory()
    {
        // ========================
        // 播放點擊按鈕音效
        audioManager.BtnClick();
        // ========================
        ch2VisualManager.ClickToViewHistory();
    }
    public void ClickViewStatus()
    {
        // ========================
        // 播放點擊按鈕音效
        audioManager.BtnClick();
        // ========================
        ch2VisualManager.ClickToViewStatus();
    }
    public void ClickViewTutorial()
    {
        // ========================
        // 播放點擊按鈕音效
        audioManager.BtnClick();
        // ========================
        ch2VisualManager.ClickToViewTutorial();
    }
    // ===============================================
    // hover效果音
    public void ButtonHoverEffect()
    {
        // hover按鈕的音效
        audioManager.BtnHover();
    }
    #endregion

    #region ToolBar
    public void OpenAntiDogHead()
    {
        if (nowGames == Games.Myth)
        {
            // 播放點擊icon音效
            audioManager.ClickWindowsIconOrDogIcon();
            // ============================
            nowGames = Games.Antivirus;
            ch2VisualManager.OpenGame();
        }
    }
    public void ClickInternetIcon()
    {
        // 播放點擊網路icon音效
        audioManager.ClickInternetIconOrShowDogSpeak();
        // =================================
        ch2VisualManager.ShowMessageBox();
    }
    #endregion

    #region 無情浣熊轉迪可丘
    public void RacToDickimon()
    {
        // 觸發文本
        ch2DialogueManager.ShowNextDialogue("小明進入跟浣熊的戰鬥畫面", true);
        // =============================
        ch2VisualManager.CloseGame();
        nowGames = Games.Dickimon;
        ch2VisualManager.RacToDickimon("無情浣熊");
        ch2VisualManager.OpenGame();
    }
    public void RacToDickimon_Horse()
    {
        // 觸發文本
        ch2DialogueManager.ShowNextDialogue("進入戰鬥", true);
        // =============================
        ch2VisualManager.CloseGame();
        nowGames = Games.Dickimon;
        ch2VisualManager.RacToDickimon("木馬病毒");
        ch2VisualManager.OpenGame();
    }
    #endregion

    #region 迪可丘轉無情浣熊
    public void DickimonToRac()
    {
        // =================
        bgmManager.StopCH2BGM();
        // =================
        ch2VisualManager.CloseDickimonWindow();
        nowGames = Games.RuthlessRaccoon;
        ch2VisualManager.OpenGame();
    }

    #endregion
}
