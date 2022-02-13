using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static int stage = 1;
    public static bool startFromMain = false;

    // public int nowstagetoshow = 0;
    public enum Chapter
    {
        CH1, CH2, CH3
    }

    public enum CH1State
    {
        // BEFORE, IDLE, GUIDE_CHOOSE, CHOOSING_CHARACTER, CONFIRM_CHARACTER,
        // GUIDE_PLAY, NORMAL_GAME, SUPER_GAME, ENDINGCUT, FINAL_SCENE, SCREENSHOT_SCENE
        DeskTopFront, DeskTopBack, CMailFront, CMailBack, BrowserFront, BrowserBack, FolderFront, FolderBack, NoteFront, NoteBack, MythFront, MythBack, AntivirusFront, AntivirusBack

    }
    public enum CH2State
    {
        MythGameFront, MythGameBack
    }

    public enum CH3State
    { }
    public enum Zoomin
    {
        Clock, MailBox, PicFrame, GUEST, Poster
    }
    public enum APPs
    {
        Pick_Branch, Landmine, Hunk_Mora, NotOpen
    }
    public enum Games
    {
        Dickimon, BreakoutSafe, RuthlessRaccoon
    }

    int GotitemAccandPasw = 0;

    public static Chapter nowChapter = Chapter.CH1;

    public static CH1State nowCH1State = CH1State.DeskTopFront;

    public static APPs nowApps = APPs.NotOpen;

    // public static CH1State nowCH1State = CH1State.MythBack;


    // public static CH2State nowCH2State = CH1State.DeskTopFront;

    // public static CH3State nowCH3State = CH1State.DeskTopFront;

    [Header("判斷觸發次數")]
    public bool toMythBackFirst = true;    // 第一次到myth背面
    public bool triggered_GUEST = false;    // 第一次trigger GUEST板子
    public bool toFolderBackFirst = true;   // 第一次到資料夾背面
    public bool toCmailBackFirst = true;    // 第一次到Cmail背面
    public bool toOpenedFolderFrontFirst = true;    // 開啟倉庫後第一次回到正面
    public bool clickFishingLink_first = true;  // 第一次點擊釣魚網站連結

    [Header("音效開關")]
    public bool callFromBtn = true;

    #region Managers
    VisualManager visualManager;
    UserManager userManager;
    DialogueManager dialogueManager;
    CursorSetting cursorSetting;
    AudioManager audioManager;
    PauseManager pauseManager;
    #endregion

    [Header("MainParameters")]
    [ShowOnly] public Chapter nowChaptertoshow = Chapter.CH1;

    [ShowOnly] public CH1State nowCH1Statetoshow = CH1State.DeskTopFront;

    [ShowOnly] public int nowstagetoshow = 0;

    // Start is called before the first frame update
    void Start()
    {
        //defaultset
        stage = 1;
        nowChapter = Chapter.CH1;
        nowCH1State = CH1State.DeskTopFront;
        //defaultset
        visualManager = GameObject.Find("VisualManager").GetComponent<VisualManager>();
        userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<CursorSetting>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        // 音效
        callFromBtn = true;
        // stage = 24;
        // nowCH1State = CH1State.BrowserFront;
        // stageControl(stage);
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
        // nowChaptertoshow = nowChapter;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(2))
        {
            if (!pauseManager.isOpen)
            {
                pauseManager.OpenPause();
            }
        }
        // if ((int)nowCH1State % 2 != 0 && Input.GetMouseButtonDown(0))//(int)nowCH1State % 2 != 0 背後世界
        // {
        //     userManager.ToMoveCharacter();
        // }
        nowCH1Statetoshow = nowCH1State;
        nowstagetoshow = stage;
        if ((int)nowCH1State % 2 != 0 && Input.GetMouseButtonDown(0))
        {
            // audioManager.PlayMouseClickAudio();
            // 在背面點擊
            if (Input.GetMouseButtonDown(0))
            {
                userManager.ToMoveCharacter();
            }
        }
        else if ((int)nowCH1State % 2 == 0 && Input.GetMouseButtonDown(0))
        {
            // 在正面點擊
            // audioManager.PlayMouseClickAudio();
        }

    }

    public void ClickSkipBtn()
    {
        if (stage >= 10)
        {
            pauseManager.ClickSkipBtn();
        }
    }

    public void SetSceneWithChapter(Chapter chapterToSet)
    {
        // Change Scene
    }

    public void ChangeToBack()
    {
        // 關閉目前的dialogue
        dialogueManager.StopNowDialogue();
        // 木馬搶走密碼後才能進入到背面
        if (stage >= 10)
        {
            // ========================================
            // 音效
            // 播放鏡子破掉後的點擊聲
            audioManager.ClickBrokenMirror();

            // =========================================
            if (stage == 10)//第一次切到背面
            {
                
                visualManager.ChangeToBack();
                visualManager.DeletMirrorTween();

                if (toMythBackFirst)
                {
                    // 開啟跳過劇情功能
                    pauseManager.OpenSkipBtn();
                    // ==========================
                    dialogueManager.ShowNextDialogue("back_first", false); //第一次進到背面要講的文本
                    toMythBackFirst = false;
                }

                // 讓帳號先倒下
                userManager.DownAni();
                /*userManager.CharacterAni.SetBool("DownToidle",true);
                userManager.CharacterAni.SetBool("Walk",false);
                userManager.CharacterAni.SetBool("idle",false);*/

            }

            if (toFolderBackFirst && nowCH1State == CH1State.FolderFront)
            {
                // 第一次到資料夾背面，觸發文本
                dialogueManager.ShowNextDialogue("to_folderback_first", false);
                toFolderBackFirst = false;
            }
            if (toCmailBackFirst && nowCH1State == CH1State.CMailFront)
            {
                // 第一次到Cmail背面，觸發文本
                dialogueManager.ShowNextDialogue("open_cmailback_first", false);
                toCmailBackFirst = false;
            }


            if (GameManager.stage == 27 && nowCH1State == CH1State.BrowserFront)
            {
                GameManager.stage++;
            }

            // 切換到背面
            visualManager.ChangeToBack();


            cursorSetting.ChangeState("Back_Normal");
        }
        else
        {
            // 播放鏡子破掉前的點擊聲
            audioManager.ClickGoodMirror();
        }
    }

    public void ChangeToFront()
    {
        if (AniEvents.AniDone && !userManager.isZoomin)
        {
            if (stage == 11)
            {
                stageNext();
            }
            if (toOpenedFolderFrontFirst && nowCH1State == CH1State.FolderBack && stage == 14)
            {
                // 第一次從開啟的資料夾背面回到正面，觸發文本
                dialogueManager.ShowNextDialogue("to_opened_folder_first", true);
                toOpenedFolderFrontFirst = false;
            }

            else if (GameManager.stage == 23 && nowCH1State == CH1State.CMailBack)
            {
                stageNext();
            }

            // 觸發進入黑洞音效
            audioManager.EnterBlackhole();
            // 改變mousePosition的位置，讓角色每次進到背面都會先往前走幾步
            userManager.ChangeToFrontSetMousePos();
            userManager.FirstToBack = false;
            visualManager.ChangeToFront();

            cursorSetting.ChangeState("Front_Normal");
        }
    }

    #region Zoomin控制
    public void ClickClock()
    {
        // ========================
        // 點擊zoom in按鈕音效
        audioManager.BackZoomInBtnClick();
        // ========================
        //SetSceneWithZoomin(Chapter.CH1, Zoomin.Clock, true);
        AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        userManager.ClickZoomin = true;
        userManager.ZoominItem = "Clock";

        userManager.walk = true;
        userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
        userManager.mouseWorldPosition.x = 5.39f;

    }
    public void ClickPoster()
    {
        // ========================
        // 點擊zoom in按鈕音效
        audioManager.BackZoomInBtnClick();
        // ========================
        //SetSceneWithZoomin(Chapter.CH1, Zoomin.Poster, true);
        AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        userManager.ClickZoomin = true;
        userManager.ZoominItem = "Poster";

        if (userManager.Character.transform.position.x <= -4.94f)
        {
            userManager.walk = true;
            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
            userManager.mouseWorldPosition.x = -4.94f;
        }
        else if (userManager.Character.transform.position.x >= -1.097f)
        {
            userManager.walk = false;
            userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
            userManager.mouseWorldPosition.x = -1.097f;
        }
        else
        {
            if (userManager.walk)
            {
                userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                userManager.mouseWorldPosition.x = -4.94f;
            }
            else
            {
                userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                userManager.mouseWorldPosition.x = -1.097f;
            }
        }
    }

    public void ClickPicFrame()
    {
        // ========================
        // 點擊zoom in按鈕音效
        audioManager.BackZoomInBtnClick();
        // ========================
        // 把那個男人的提示按鈕關掉
        visualManager.ControlFolderBackManBtn(false);
        // =============================
        //SetSceneWithZoomin(Chapter.CH1, Zoomin.PicFrame, true);
        AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        userManager.ClickZoomin = true;
        userManager.ZoominItem = "Picture";

        if (userManager.Character.transform.position.x <= -4.396f)
        {
            userManager.walk = true;
            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
            userManager.mouseWorldPosition.x = -4.396f;
        }
        else if (userManager.Character.transform.position.x >= -1.279f)
        {
            userManager.walk = false;
            userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
            userManager.mouseWorldPosition.x = -1.279f;
        }
        else
        {
            if (userManager.walk)
            {
                userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                userManager.mouseWorldPosition.x = -4.396f;
            }
            else
            {
                userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                userManager.mouseWorldPosition.x = -1.279f;
            }
        }
        if (GameManager.stage == 19)
        {
            GameManager.stage++;
        }
    }
    public void ClosePicFrame()
    {
        SetSceneWithZoomin(Chapter.CH1, Zoomin.PicFrame, false);
        // 把那個男人的提示按鈕開啟
        visualManager.ControlFolderBackManBtn(true);
    }

    public void ClickGUEST()
    {
        // ========================
        // 點擊zoom in按鈕音效
        audioManager.BackZoomInBtnClick();
        // ========================
        if (!triggered_GUEST)
        {
            // 改變點擊狗會說的話
            dialogueManager.changeDogStage("click_guest");
            // =================================
            triggered_GUEST = true;
        }
        //SetSceneWithZoomin(Chapter.CH1, Zoomin.GUEST, true);
        AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        userManager.ClickZoomin = true;
        userManager.ZoominItem = "Guest";

        if (userManager.Character.transform.position.x <= -0.286f)
        {
            userManager.walk = true;
            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
            userManager.mouseWorldPosition.x = -0.286f;
        }
        else if (userManager.Character.transform.position.x >= 3.15f)
        {
            userManager.walk = false;
            userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
            userManager.mouseWorldPosition.x = 3.15f;
        }
        else
        {
            if (userManager.walk)
            {
                userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                userManager.mouseWorldPosition.x = 0.286f;
            }
            else
            {
                userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                userManager.mouseWorldPosition.x = 3.15f;
            }
        }
        visualManager.ResetGUEST(false);
    }

    public void ClickCloseGUEST()
    {
        visualManager.ResetGUEST(true);
        visualManager.DestroyGUESTBoard();
        if (visualManager.GUESTBTN.activeSelf)
        {
            userManager.isClickProps = true;
            userManager.Props = "GUESTBtnProp";
        }
    }

    public void ClickGUESTLoginBTN()
    {
        // =========================
        // 觸發點擊GUEST音效
        audioManager.ClickGUESTandShowErr();
        // =========================
        // 把GUEST放到訪客登入的位置，觸發文本
        dialogueManager.ShowNextDialogue("put_on_guest_to", true);
        // =======================================
        visualManager.ClickGUESTLoginAni();
    }

    public void ClickWorm()
    {
        if (GameManager.stage == 20)
        {
            GameManager.stage++;
        }
        // 點擊畫布上的S，觸發文本
        dialogueManager.ShowNextDialogue("click_s", false);

        visualManager.PigeonAniStart();
    }

    public void ClickBlackMan(GameObject obj)
    {
        audioManager.ClickBlackMan(obj);
    }

    public void ClickHan(GameObject obj)
    {
        // 點擊韓國瑜
        audioManager.ClickHan(obj);
    }

    public void SetSceneWithZoomin(Chapter chapterToSet, Zoomin zoominitem, bool state)
    {

        GameObject.Find("Player").GetComponent<SpriteRenderer>().sortingLayerName = "PlayerBehindZoomin";
        GameObject.Find("Player").transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "PlayerBehindZoomin";
        if (chapterToSet == Chapter.CH1)
        {
            visualManager.Zoomin(zoominitem, state);

            // switch (zoominitem)
            // {
            //     case Zoomin.Clock:
            //         visualManager.Zoomin(zoominitem);
            //         break;
            //     case Zoomin.PicFrame:
            //         visualManager.Zoomin(zoominitem);
            //         break;
            //     default:
            //         break;
            // }
        }
    }

    public void CloseBtnClick()
    {
        // 關閉視窗的音效
        audioManager.CloseWindow();
        // =========
        userManager.CharacterAni.speed = 1;
        userManager.isZoomin = false;
        userManager.ClickZoomin = false;
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        GameObject.Find("Player").transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Player";
    }
    #endregion

    #region 正面應用程式開關控制

    [Header("紀錄click次數")]
    private int clickCount = 0;

    private int LastClick = 0;//尚未點擊
    IEnumerator doubleClickEvent()//偵測有沒有點雙擊
    {
        yield return new WaitForSeconds(0.3f);
        clickCount = 0;
    }

    public void ClickBackground()
    {
        visualManager.CleanBlue();
    }

    public void OpenCMail() //LastClick=1
    {
        // 播放點擊icon音效
        audioManager.ClickWindowsIconOrDogIcon();
        // ============================

        visualManager.ClickToBlue("CMailFront");

        clickCount++;
        if (clickCount == 1)
        {
            StartCoroutine("doubleClickEvent");
            LastClick = 1;
        }
        else if (clickCount > 1 && LastClick == 1)
        {
            setWindowStatus(CH1State.CMailFront, true);
        }

        if (stage == 24)
        {
            ClickFishingAD();
        }
    }

    public void OpenBrowser()//LastClick=2
    {
        // 播放點擊icon音效
        audioManager.ClickWindowsIconOrDogIcon();
        // ============================

        visualManager.ClickToBlue("BrowserFront");
        clickCount++;

        if (clickCount == 1)
        {
            StartCoroutine("doubleClickEvent");
            LastClick = 2;
        }
        else if (clickCount > 1 && LastClick == 2)
        {
            setWindowStatus(CH1State.BrowserFront, true);
            if (stage == 1)
            {
                // stage++;
                // stageControl(stage);
                stageNext();
            }
        }
    }

    public void OpenFolder()//LastClick=3
    {
        // 播放點擊icon音效
        audioManager.ClickWindowsIconOrDogIcon();
        // ============================

        visualManager.ClickToBlue("FolderFront");

        clickCount++;
        if (clickCount == 1)
        {
            StartCoroutine("doubleClickEvent");
            LastClick = 3;
        }
        else if (clickCount > 1 && LastClick == 3)
        {
            setWindowStatus(CH1State.FolderFront, true);
        }
    }

    public void OpenNote()//LastClick=4
    {
        // 播放點擊icon音效
        audioManager.ClickWindowsIconOrDogIcon();
        // ============================

        visualManager.ClickToBlue("NoteFront");

        clickCount++;
        if (clickCount == 1)
        {
            StartCoroutine("doubleClickEvent");
            LastClick = 4;
        }
        else if (clickCount > 1 && LastClick == 4)
        {
            setWindowStatus(CH1State.NoteFront, true);
        }
    }

    public void OpenMyth()//LastClick=5
    {
        // 播放點擊icon音效
        audioManager.ClickWindowsIconOrDogIcon();
        // ============================

        visualManager.ClickToBlue("MythFront");

        clickCount++;
        if (clickCount == 1)
        {
            StartCoroutine("doubleClickEvent");
            LastClick = 5;
        }
        else if (clickCount > 1 && LastClick == 5)
        {
            setWindowStatus(CH1State.MythFront, true);
        }
    }

    public void OpenAntivirus()//LastClick=6
    {
        // 播放點擊icon音效
        audioManager.ClickWindowsIconOrDogIcon();
        // ============================

        visualManager.ClickToBlue("AntivirusFront");
        clickCount++;
        if (clickCount == 1)
        {
            StartCoroutine("doubleClickEvent");
            LastClick = 6;
        }
        else if (clickCount > 1 && LastClick == 6)
        {
            setWindowStatus(CH1State.AntivirusFront, true);
        }
    }

    bool firstmyth = true;
    public void OpenAntiDogHead()
    {
        if (stage != 10 || !toMythBackFirst)
        {
            // 播放點擊icon音效
            audioManager.ClickWindowsIconOrDogIcon();
            // ============================
            if (nowCH1State == CH1State.FolderFront && nowApps != APPs.NotOpen)
            {
                // 先關掉應用程式
                visualManager.CloseApps();
            }
            // ============================
            if (nowCH1State.ToString() != "DeskTopFront" && stage != 2 && stage != 3 && stage != 6 && stage != 7 && stage != 8 && stage != 31 && nowCH1State.ToString() != "AntivirusFront")
            {
                if (nowCH1State.ToString() == "MythFront" && stage == 9 && firstmyth)
                {
                    return;
                }
                callFromBtn = false;
                CloseWindow();
                setWindowStatus(CH1State.AntivirusFront, true);
            }
            else if (stage != 2 && stage != 3 && stage != 6 && stage != 7 && stage != 8 && stage != 31 && nowCH1State.ToString() != "AntivirusFront")
            {
                setWindowStatus(CH1State.AntivirusFront, true);
            }
        }
    }

    public void ClickInternetIcon()
    {
        // 播放點擊網路icon音效
        audioManager.ClickInternetIconOrShowDogSpeak();
        // =================================
        visualManager.ShowMessageBox();
    }

    public void CloseWindow()
    {
        if (callFromBtn)
        {
            // 關閉視窗的音效
            audioManager.CloseWindow();
        }
        if (stage == 3)
        {
            stageNext();
        }
        else if (stage == 23 && VisualManager.GetS && nowCH1State == CH1State.FolderFront)
        {
            stageNext();
        }
        else if (stage == 9 && GotitemAccandPasw < 2 && nowCH1State == CH1State.MythFront)
        {
            visualManager.showhorseTips();
            firstmyth = false;
        }
        setWindowStatus(CH1State.DeskTopFront, false);
        // 把關閉音效預設開為true
        callFromBtn = true;
    }

    private void setWindowStatus(CH1State state, bool toOpen)
    {
        //==================Cursor=========================
        if ((int)GameManager.nowCH1State % 2 == 0)   // 代表在正面世界
        {
            cursorSetting.ChangeState("Front_Normal");
        }
        else
        {
            cursorSetting.ChangeState("Back_Normal");
        }
        //=================================================

        if (toOpen)
        {

            // 關掉桌面開啟目標畫面
            nowCH1State = state;
            visualManager.setWindowActivate(nowCH1State.ToString(), toOpen);
            if (stage == 9 && state.ToString() != "NoteFront" && state.ToString() != "AntivirusFront")
            {
                if (GotitemAccandPasw == 2)
                {
                    visualManager.StartCoroutine(visualManager.closeHorseTips());
                    GotitemAccandPasw++;
                }
            }
        }
        else
        {
            // 關掉目標畫面開啟桌面
            visualManager.setWindowActivate(nowCH1State.ToString(), toOpen);
            nowCH1State = state;
        }

    }

    // ===============================================
    // hover效果音
    public void ButtonHoverEffect()
    {
        // hover按鈕的音效
        audioManager.BtnHover();
    }

    public void BrokenMirrorHoverEffect()
    {
        if (stage >= 10)
        {
            // hover破掉鏡子的音效
            audioManager.HoverBrokenMirror();
        }
    }
    public void HoverAnyPropsInBack(GameObject obj)
    {
        if ((int)nowCH1State % 2 != 0 && obj != null && !obj.GetComponent<ItemEventManager>().PlayerGet)
        {
            // 在背面hover各種道具
            audioManager.HoverAnyPropsAndZoomInInBack();
        }
    }
    public void HoverBlackhole()
    {
        // hover黑洞的音效
        audioManager.HoverBlackhole();
    }
    // ===============================================

    public void ClickMythDownloadWindow()
    {
        if (stage == 4)
        {
            // ============================
            // 點擊右下釣魚網站廣告
            audioManager.ClickAD();
            // ============================
            stageNext();
        }
    }

    public void ClickDickmonAdDownload()
    {
        if (stage == 4)
        {
            CloseWindow();
            visualManager.ShowMythDownloadWindow(false);
            // ============================
            // 點擊Myth下載連結
            audioManager.ClickCMailDownload();
            // ============================
            // GameObject.Find("dickmon_btn").GetComponent<Button>().interactable = false;
            Destroy(GameObject.Find("dickmon_btn"));
            stage = 5;
            stageNext();
        }
        else if (stage == 5)
        {
            // ============================
            // 點擊Myth下載連結
            audioManager.ClickCMailDownload();
            // ============================
            // GameObject.Find("dickmon_btn").GetComponent<Button>().interactable = false;
            Destroy(GameObject.Find("dickmon_btn"));
            stageNext();
        }
        visualManager.DickimonADImg.sprite = Resources.Load<Sprite>("Textures/mail_myth_ad_2");

    }

    public void ClickFishingAD()
    {
        // ============================
        // 點擊右下釣魚網站廣告
        audioManager.ClickAD();
        // ============================
        visualManager.ShowFishingADWindow(false);
        visualManager.ClickFishingAD();
        setWindowStatus(CH1State.CMailFront, true);
        GameObject.Find("FishingText").GetComponent<Text>().color = new Color32(42, 42, 42, 255);
        // tag
        if (stage == 24)
        {
            stageNext();
        }
    }

    public void ClickFishingLink()
    {
        // ============================
        // 點擊釣魚網站開啟連結
        audioManager.ClickCMailDownload();
        // ============================
        if (clickFishingLink_first)
        {
            // 開啟釣魚網站的連結，觸發文本
            dialogueManager.ShowNextDialogue("after_click_fishing_link", true);
            clickFishingLink_first = false;
        }

        visualManager.ChangeLinkColor();
        // 關閉音效
        callFromBtn = false;
        CloseWindow();
        setWindowStatus(CH1State.BrowserFront, true);
        visualManager.showFishingWeb();
    }

    public void ClickBitcoin()
    {
        visualManager.ClickBitcoinAni();
    }
    #endregion

    #region 瀏覽器

    public void ClickBrowserFrontClick()
    {
        // visualManager.BrowserFrontMouseClick();
        visualManager.PlayDinoEatAni();
    }

    public void ClickDino()
    {
        visualManager.PlayDinoYee();
    }

    public void ClickBtnPlayerClick()
    {
        visualManager.BrowserPlayerClick();
    }

    bool BirdSound = true;
    public void ClickCMailBird()
    {
        if (BirdSound)
        {
            BirdSound = false;
            audioManager.ClickBird();
            StartCoroutine(Wait());
        }
        IEnumerator Wait()
        {
            visualManager.Bird[0].GetComponent<SpriteRenderer>().sprite = visualManager.BirdMouse[1];
            yield return new WaitForSeconds(0.5f);
            visualManager.Bird[0].GetComponent<SpriteRenderer>().sprite = visualManager.BirdMouse[0];
            BirdSound = true;
        }
    }


    #endregion

    #region 資料夾
    public void Pick_Branch_Click()
    {
        audioManager.ClickWindowsIconOrDogIcon();
        visualManager.FolderClickToBlue("Pick_Branch");
        clickCount++;
        if (clickCount == 1)
        {
            LastClick = 7;
            StartCoroutine("doubleClickEvent");
        }
        else if (clickCount > 1 && LastClick == 7)
        {
            nowApps = APPs.Pick_Branch;
            visualManager.OpenApps();

            //==================Cursor=========================

            cursorSetting.ChangeState("Front_Normal");

            //=================================================
        }
    }
    public void Landmine_Click()
    {
        audioManager.ClickWindowsIconOrDogIcon();
        visualManager.FolderClickToBlue("Landmine");
        clickCount++;
        if (clickCount == 1)
        {
            LastClick = 8;
            StartCoroutine("doubleClickEvent");
        }
        else if (clickCount > 1 && LastClick == 8)
        {
            nowApps = APPs.Landmine;
            visualManager.OpenApps();

            //==================Cursor=========================

            cursorSetting.ChangeState("Front_Normal");

            //=================================================
        }
    }
    public void Hunk_mora_Click()
    {
        audioManager.ClickWindowsIconOrDogIcon();
        visualManager.FolderClickToBlue("Finger-guess");
        clickCount++;
        if (clickCount == 1)
        {
            LastClick = 9;
            StartCoroutine("doubleClickEvent");
        }
        else if (clickCount > 1 && LastClick == 9)
        {
            nowApps = APPs.Hunk_Mora;
            visualManager.OpenApps();

            //==================Cursor=========================

            cursorSetting.ChangeState("Front_Normal");

            //=================================================

            if (GameManager.stage == 16)
            {
                GameManager.stage++;
            }
        }
    }
    public void Close_Pick_Branch_Click()
    {
        visualManager.CloseApps();
    }
    public void Close_Landmine_Click()
    {
        visualManager.CloseApps();
    }
    public void Close_Hunk_mora_Click()
    {
        visualManager.CloseApps();
    }
    public void ClickDoorKeeper()//加上帳號要走過去的動畫
    {
        if (AniEvents.AniDone)
        {
            // ===========================

            // ===========================
            AniEvents.AniDone = false;
            cursorSetting.CursorHourglassAni();

            //visualManager.ShowBitcoinTips();
            userManager.walk = true;
            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
            userManager.mouseWorldPosition.x = 0.73f;
            userManager.ClickGateKeeper = true;


        }
    }
    public void ClickTheManCallTips()
    {
        //visualManager.ToShowETips();
        AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        userManager.ClickHunk = true;

        userManager.walk = true;
        userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
        userManager.mouseWorldPosition.x = 3.888f;
    }
    #endregion

    #region 記事本
    public void ClickAccUIBtn()
    {
        // ===========================
        // 播放點擊帳號密碼音效
        audioManager.ClickAandP();
        // ===========================
        visualManager.ClickAccAndPassObject(true);  // true => isAcc 代表是點擊帳號

        GotitemAccandPasw++;//紀錄得到帳號/密碼
    }
    public void ClickPassUIBtn()
    {
        // ===========================
        // 播放點擊帳號密碼音效
        audioManager.ClickAandP();
        // ===========================
        visualManager.ClickAccAndPassObject(false); // false => isAcc 代表是點擊密碼

        GotitemAccandPasw++;//紀錄得到帳號/密碼
    }

    #endregion

    #region Myth安裝
    public void ClickMythSetupCancel()
    {
        visualManager.MythSetupCancelBtnFall();
    }

    public void ClickMythStepClose()
    {
        visualManager.ClickMythStepClose();
    }
    public void ClickMythInstallingClose()
    {
        visualManager.ClickMythInstallingClose();
    }

    public void ClickMythSetupInstall()
    {
        if (stage == 6 || stage == 7)
        {
            stageNext();
        }
    }
    public void ClickMythSetupAgree()
    {
        visualManager.MythSetupSelectAgree(true);
    }
    public void ClickMythSetupDisagree()
    {
        visualManager.MythSetupSelectAgree(false);
    }
    public void ClickMythSetupFinish()
    {
        if (stage == 8)
        {
            stageNext();
        }
        // 安裝完成立刻開啟Myth
        setWindowStatus(CH1State.MythFront, true);
    }
    #endregion

    #region Myth

    public void LoginBtnClick()
    {
        // ====================
        // 播放點擊login的音效
        audioManager.ClickLogin();
        // ====================
        //GameObject.Find("Log in").SetActive(false);
        visualManager.Stole_Password();
    }

    #endregion

    #region 防毒軟體
    public void ClickViewHistory()
    {
        // ========================
        // 播放點擊按鈕音效
        audioManager.BtnClick();
        // ========================
        visualManager.ClickToViewHistory();
    }
    public void ClickViewStatus()
    {
        // ========================
        // 播放點擊按鈕音效
        audioManager.BtnClick();
        // ========================
        visualManager.ClickToViewStatus();
    }
    public void ClickViewTutorial()
    {
        // ========================
        // 播放點擊按鈕音效
        audioManager.BtnClick();
        // ========================
        visualManager.ClickToViewTutorial();
    }
    #endregion

    #region 流程控制

    public void stageNext()
    {
        stage++;
        stageControl(stage);
    }

    public void stageControl(int stage)
    {
        switch (stage)
        {
            case 1:
                setWindowStatus(CH1State.DeskTopFront, false);  //開啟桌面，其他關閉
                break;
            case 2:
                visualManager.Mail_Internet.SetActive(true);
                visualManager.Mail_noInternet.SetActive(false);
                break;
            case 3:
                // 在VisualManager做stage++了
                break;
            case 4:
                visualManager.ShowBrowerConnected();
                visualManager.ShowMythDownloadWindow(true);
                break;
            case 5:
                callFromBtn = false;
                CloseWindow();
                visualManager.ShowMythDownloadWindow(false);
                // OpenCMail();
                setWindowStatus(CH1State.CMailFront, true);
                break;
            case 6:
                callFromBtn = false;
                CloseWindow();
                visualManager.ShowMsiOpenWindow();
                break;
            case 7:
                // 跳出警告視窗
                visualManager.ShowMythDownloadWaringWindow();
                break;
            case 8:
                // 顯示安裝中畫面
                visualManager.ShowInstallingWindow();
                break;
            case 9:
                // 關閉安裝視窗並開啟Myth
                visualManager.CloseSetupWindowAndOpenMyth();
                break;
            case 10:
                // 點擊login
                // userManager.FirstToBack = true;
                break;

            case 11:

                break;

            case 12:

                break;
            case 13:

                break;

            case 14:

                break;
            case 15:

                break;
            case 16:

                break;
            case 17:

                break;
            case 18:

                break;
            case 19:

                break;
            case 20:

                break;
            case 21:

                break;
            case 22:

                break;
            case 23:

                break;
            case 24://出現釣魚廣告
                if (nowCH1State == CH1State.CMailBack)
                {
                    visualManager.showfishingCmail();
                }
                else
                {
                    visualManager.ShowFishingADWindow(true);
                }
                break;
            case 25://按下釣魚廣告後，開啟IC瀏覽器，並且到抽獎的網站去

                break;
            case 26:
                break;

            default:
                break;
        }
    }


    #endregion

    #region 背面狗
    public void clickBackDog()
    {
        // 觸發狗講話
        // dialogueManager.DogSpeak();
    }
    #endregion


}
