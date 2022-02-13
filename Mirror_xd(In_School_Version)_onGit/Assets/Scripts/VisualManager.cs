using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


public class VisualManager : MonoBehaviour
{
    #region 角色
    [Header("主角")]
    public GameObject Player;
    public GameObject Dog;
    [Header("通道")]
    public GameObject Portal;
    public Animator PortalAni;
    #endregion

    #region 道具
    public GameObject FishingU;
    public GameObject FishingE;
    public GameObject FishingS;
    public GameObject FishingT;

    public GameObject FishingGift;

    #endregion

    #region  工具列
    public Collider2D FrontBar;
    public Collider2D BackBar;
    //========================正面==========================
    [Header("工具列")]
    public GameObject ToolBarFront;
    public GameObject Tools;
    public GameObject Bar;
    public GameObject MessageBox;
    public GameObject ToBackBtn;
    public GameObject AntiMessageBox;
    //========================背面==========================
    [Header("背面背包")]
    public GameObject ToolBarBack;
    public GameObject Tools_Back;
    public GameObject Bar_Back;
    public GameObject BackBarTrigger;
    public Sprite[] UpandDown = new Sprite[2];
    // =======================圖片==========================
    [Header("原始圖")]
    public Sprite[] source_sprite = new Sprite[14];

    [Header("正面bar的圖")]
    public Sprite[] bar_sprite = new Sprite[14];

    [Header("背面Bar的圖")]
    public Sprite[] backbar_sprite = new Sprite[14];

    public bool ShowBag;

    #endregion

    #region  桌面

    //========================正面==========================

    [Header("桌面正面的Object")]
    public GameObject DesktopFront;
    [Header("瀏覽器正面的Object")]
    public GameObject BrowserFront;
    [Header("Myth正面的Object")]
    public GameObject MythFront;
    [Header("信箱正面的Object")]
    public GameObject CMailFront;
    [Header("資料夾正面的Object")]
    public GameObject FolderFront;
    [Header("記事本正面的Object")]
    public GameObject NoteFront;
    [Header("防毒軟體正面的Object")]
    public GameObject AnitvirusFrame;
    public GameObject AntivirusFront;
    public GameObject AntivirusBtnImage;
    public GameObject AntiTutorialCanvas;
    public GameObject ScrollPre;
    public GameObject ScrollCanvas;
    public GameObject ScrollView;
    public GameObject ScrollBar;
    public GameObject AntiSleep;
    public GameObject AntiAlert;
    public Sprite[] AntivirusBtnSprite;

    [Header("Icon和圖片圖示(新手教學)")]
    public GameObject TutorialPre;
    public GameObject TutorialView;
    public GameObject BtnHistory;
    public GameObject TutorialIconSelector;
    public GameObject TutorialIconHover;
    public GameObject TutorialiconShower;
    public GameObject TutorialLeftContainer;
    public GameObject TutorialLeftPrepare;
    public GameObject TutorialLeftShower;

    public GameObject TutorialRightContainer;
    public GameObject TutorialRightPrepare;
    public GameObject TutorialRightShower;
    public Sprite[] TutorialHoverIcon;
    public Sprite[] TutorialiconArr;
    public Sprite[] TutorialLeftArr;
    public Sprite[] TutorialRightArr;

    public GameObject AntiGoOutSide;
    [Header("桌面的Canvas")]
    public GameObject FrontCanvas;
    [Header("IconBtn的Canvas")]
    public GameObject BtnsCanvas;

    public GameObject littleWindowCanvas;
    [Header("Myth下載彈出視窗")]
    public GameObject MythDownloadWindow;
    public GameObject MythDownLoadWindow;

    [Header("MythIcon")]
    public GameObject MythIcon;

    public GameObject MythIconBtn;

    [Header("CmailIcon")]
    public GameObject CmailIcon;
    public GameObject CmailWormIcon;

    public GameObject CmailWindowWorm;

    [Header("桌面數字時鐘")]
    public GameObject ClockCanvas;
    public GameObject DigitalClock;

    [Header("反藍")]
    public GameObject CmailBlue;
    public GameObject CmailWormBlue;
    public GameObject BrowserBlue;
    public GameObject FloderBlue;
    public GameObject FloderUnlockBlue;
    public GameObject NoteBlue;
    public GameObject AntivirusBlue;
    public GameObject MythBlue;

    [Header("FolderIconObj")]

    public GameObject Floder;
    public GameObject FloderUnlock;
    //========================反面==========================
    [Header("時鐘Zoomin畫面")]
    public GameObject ClockZoominObj;
    [Header("Zoomin時針")]
    public GameObject Hour_hand;
    [Header("海報Zoomin畫面")]
    public GameObject PosterZoominObj;

    #endregion

    #region 反面畫面
    [Header("桌面反面Object")]
    public GameObject DesktopBack;
    [Header("Cmail反面Object")]
    public GameObject CMailBack;
    [Header("Browser反面Object")]
    public GameObject BrowserBack;
    [Header("Folder反面Object")]
    public GameObject FolderBack;
    [Header("Myth反面Object")]
    public GameObject MythBack;
    #endregion


    #region Myth安裝
    [Header("安裝畫面canvas")]
    public GameObject MythInstallingCanvas;
    [Header("Msi開啟視窗")]
    public GameObject MsiOpenWindow;
    [Header("Msi開啟文字")]
    public Text MsiOpenText;
    [Header("Myth安裝視窗")]
    public GameObject MythSetupWindow;
    [Header("下一步警告視窗")]
    public GameObject MythDownloadWarningWindow;
    [Header("Myth安裝結束按鈕")]
    public GameObject MythSetupCancel;
    [Header("Myth安裝按鈕")]
    public GameObject MythSetupInstall;
    [Header("Myth安裝同意按鈕")]
    public GameObject MythSetupAgree;
    [Header("Myth安裝不同意按鈕")]
    public GameObject MythSetupDisagree;
    [Header("Myth安裝黑色點點")]
    public GameObject MythSetupBlackDot;
    [Header("預設的Myth警告視窗個數")]
    public int WarningWinCount;
    [Header("Myth進行安裝畫面")]
    public GameObject MythInstallingWindow;
    [Header("Myth進行安裝的取消btn")]
    public GameObject MythInstallingCancel;
    [Header("Myth進行安裝的Bar")]
    public Image SetupProgressBar;
    [Header("Myth進行安裝時的文字")]
    public Text SetupProgressText;
    [Header("Myth安裝完成畫面")]
    public GameObject MythSetupFinishWindow;
    [Header("Myth_Step右上角X")]
    public GameObject MythStepBtnClose;
    [Header("Myth_Installing右上角X")]
    public GameObject MythInstallingBtnClose;
    #endregion
    #region 瀏覽器
    //========================正面==========================
    [Header("連接上網路畫面")]
    public GameObject BrowserConnected;

    // [Header("進度條")]
    // public GameObject Progressbar;
    // public GameObject dinoHead;

    [Header("隕石")]

    public GameObject Meteorite;


    [Header("小恐龍")]
    public GameObject Dino;

    public Sprite[] DinoSprite;
    // int spriteNum = 0;

    bool dinoidel = true;

    [Header("小恐龍地板")]
    public GameObject Ground;

    [Header("小恐龍磚頭")]
    public GameObject[] Bricks;

    public SpriteRenderer DinoitemIconSR;

    public Sprite[] DinoitemIconimg;

    public GameObject BrickSpawn;

    GameObject[] BrickinGround;

    [Header("地板是否移動")]
    public bool BrowserGroundMoving = false;

    [Header("BTNBrowser")]

    public GameObject BTNBrowser;

    [Header("Dinosaur Ani")]

    public Animator DinAni;

    [Header("地板移動速度")]
    public float Groundspeed = 5f;    // Ground move speed

    public GameObject City;
    AnimatorStateInfo animatorInfo;

    [Header("關閉瀏覽器的X")]
    public GameObject CloseBrowserBtn;

    // [Header("小恐龍ClickToStart字串")]
    // public GameObject StringCTS;

    [Header("Wifi")]
    public GameObject Wifi;

    [Header("小恐龍吃叉叉動畫")]
    public Animator DinoEat;

    [Header("ExitBTN")]
    public GameObject ExitBTN;

    [Header("FishingWeb")]
    public GameObject FishingWeb;

    [Header("Fishing廣告彈出視窗")]
    public GameObject FishingADloadWindow;
    public GameObject FishingADLoadWindow;

    [Header("轉盤指針")]
    public GameObject Pointer;

    [Header("轉盤燈泡高光")]
    public GameObject Hightlight;

    [Header("轉盤色塊高光")]
    public GameObject TableLight;

    [Header("轉盤開始按鈕")]
    public GameObject TurnTableStartBtn;

    [Header("得到禮物對話框")]
    public GameObject GetGiftMesBox;

    [Header("Gift")]
    public GameObject Gift;

    //========================反面==========================
    [Header("BtnBackFrame")]
    public GameObject BtnBackFrame;

    [Header("釣魚被吃")]
    public GameObject Fishing_Eat;
    public Animator Fishing_Eat_Ani;

    [Header("道具G")]
    public GameObject G;

    [Header("圖書館")]
    public GameObject Browserback_back;

    [Header("釣魚網站")]
    public GameObject Fishing_Back;

    [Header("釣魚參數")]
    public bool GiftorOther = false;
    #endregion

    #region 資料夾
    //========================正面==========================
    [Header("解鎖後畫面")]
    public GameObject ForderLock;
    public GameObject ForderUnlock;
    [Header("關閉資料夾的按鈕")]
    public GameObject FolderCloseBtn;
    [Header("猛男猜拳動畫")]
    public Animator HunkAni;
    [Header("放剪刀石頭布的圖和紀錄位置")]
    public GameObject[] moraSprite = new GameObject[3];
    Vector3[] moraPos = new Vector3[3];
    [Header("應用程式的Canvas")]
    public GameObject AppBtn;
    [Header("撿樹枝")]
    public GameObject Pick_Branch_App;
    [Header("踩地雷")]
    public GameObject Landmine_App;
    [Header("野球拳")]
    public GameObject Hunk_Mora_App;
    [Header("野球拳放置目標位置")]
    public Collider2D Target;
    [Header("猜輸和猜贏畫面")]
    public GameObject Lose;
    public GameObject Win;
    [Header("猛男野球爆炸輸後畫面")]
    public GameObject FoderBack;
    public Sprite HunkExplodeSprite;

    //踩地雷暫定
    public Text TextFlagSum;

    public int Flagsum = 6;

    public SpriteRenderer SpriteLandmineEmoji;

    [Header("挖炸彈爆炸特效")]
    public GameObject BombAni;
    public GameObject BombBg;
    public Sprite BombBg2;
    [Header("道具炸彈和T")]
    public GameObject Bomb;
    public GameObject T;
    [Header("道具放到野球拳上")]
    public Sprite[] Hunkmora = new Sprite[4];
    //========================反面==========================
    [Header("鐵捲門")]
    public GameObject Door;
    [Header("守門人")]
    public GameObject Gatekeeper;
    [Header("兩個守門人動畫")]
    public Animator ThinAni;
    public Animator FatAni;
    [Header("想要比特幣")]
    public GameObject WantBitCion;
    [Header("比特幣")]
    public GameObject BitCoin;
    [Header("爆炸後的場景")]
    public Sprite Exploded;
    [Header("道具E")]
    public GameObject E;

    public bool isGetE = false;

    [Header("畫框Zoomin")]
    public GameObject PicFrameZoomin;

    [Header("鴿子叼蚯蚓")]
    public GameObject PigeonAni;

    [Header("畫框BTN")]
    public GameObject PicFrameZoominBtn;

    [Header("那個男人的button")]
    public GameObject ManButton;

    [Header("win = e 提示框")]
    public GameObject FolderBackWinTips;

    public bool EarthwormMissed;

    #endregion

    #region 信箱
    //========================正面==========================
    [Header("四封信的內容")]
    public GameObject Right_Fix;
    public GameObject Right_Dickmon;

    public Image DickimonADImg;

    public GameObject Right_Bitcon;
    public GameObject Right_Fishing;

    [Header("FishingLetterBtn")]

    public GameObject Left_Fishing;

    [Header("信箱沒網路畫面")]
    public GameObject Mail_noInternet;
    [Header("信箱有網路畫面")]
    public GameObject Mail_Internet;
    //========================反面==========================
    [Header("鴿子的三種型態")]
    public GameObject[] Bird = new GameObject[3];
    public Sprite[] BirdMouse = new Sprite[2];
    public GameObject VovoBox;
    #endregion

    #region Myth

    //========================正面==========================
    [Header("Stole_Password Ani")]
    public Animator StoleAin;
    [Header("帳號密碼登入框")]
    public GameObject AcountSprit;
    public GameObject PasswardSprite;
    [Header("Myth帳號密碼未放上")]
    public GameObject Myth_notPuton;
    [Header("Myth開場動畫Obj")]
    public GameObject Myth_StolePassword;
    public GameObject MythCloseBtn;
    public GameObject MirrorHightlight;
    //========================反面==========================
    [Header("GUSET ZOOMIN")]
    public GameObject GUEST_Zoomin;

    [Header("GUEST大圖")]
    public Sprite[] GUEST;

    [Header("GusetBigObj")]

    public GameObject[] BigGUEST;
    bool[] PuttedGuest = new bool[5];

    [Header("GusetAni")]
    public GameObject GUESTBrokenAni;

    [Header("guestButtonZoomin")]

    public GameObject GUESTBtn;

    [Header("guestButtonProp")]

    public GameObject guestButtonProp;

    [Header("GUESTPressed")]
    public Sprite GuestPressed;

    [Header("黃色GUEST")]
    public GameObject GUESTBTN;

    #endregion

    #region 記事本正面
    [Header("帳號牌子GameObject")]
    public GameObject AccObject;
    [Header("帳號UI Btn")]
    public GameObject AccUIBtn;
    [Header("密碼牌子GameObject")]
    public GameObject PassObject;
    [Header("密碼UI Btn")]
    public GameObject PassUIBtn;
    #endregion


    UserManager userManager;
    DialogueManager dialogueManager;
    CursorSetting cursorSetting;
    AudioManager audioManager;
    BGMManager bgmManager;
    PauseManager pauseManager;

    // 設定第一次的bool
    public bool openPickBranchFirst = true;  //第一次開啟猛男撿樹枝
    public bool openLandminesFirst = true; // 第一次開啟踩地雷
    public bool openMoraFirst = true; // 第一次開啟野球拳
    public int loseMoraCount = 0;
    public bool openBrowserBackFirst = true;    //第一次開啟browser背面
    public bool openFishingBackFirst = true;    //第一次開啟釣魚網站背面
    public bool clickFishingLeftFirst = true;   // 第一次點擊Cmail左邊的fishing tab
    public bool clickWormDiaFirst = true;
    public bool ChangeBackToFront_first = true;    // 第一次從背面到正面
    public bool cancelIsEntered = true; // 是否點擊過cancel btn

    // 判斷開關
    public bool theManIsAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        //defaultset
        PigeonAniPlayed = false;
        GetS = false;
        GuestAllPutted = false;
        GuestReset = false;
        //defaultset

        userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<CursorSetting>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();

        Meteorite.SetActive(false);

        ClockZoominObj.SetActive(false);
        PicFrameZoomin.SetActive(false);
        PicFrameZoominBtn.SetActive(false);
        PigeonAni.SetActive(false);
        // Progressbar.SetActive(false);
        MythIcon.SetActive(false);
        MythIconBtn.SetActive(false);
        AntiAlert.SetActive(false);
        ExitBTN.SetActive(false);
        BtnBackFrame.SetActive(false);
        GUEST_Zoomin.SetActive(false);
        ShowBag = false;
        EarthwormMissed = false;
        Left_Fishing.SetActive(false);
        guestButtonProp.SetActive(false);
        Fishing_Back.SetActive(false);
        CmailWindowWorm.SetActive(false);

        if (GameManager.stage > 3)
        {
            MessageBox = GameObject.Find("MessageBox_connect_interent");
            AntiAlert = GameObject.Find("AntiMessageBox_Alert");
        }
        // StartCoroutine(OpenDoor());
        for (int i = 0; i < 3; i++)
        {
            moraPos[i] = moraSprite[i].transform.position;
        }
        // DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 800, sequencesCapacity: 200);

        // ShowFishingADWindow(true);

        // GetS = true;
        // PigeonAniPlayed = true;

        StartCoroutine(Ch1Transition());

    }
    #region  轉盤特效參數
    float Highlight_Z = 0;
    int HighMul = 0;
    int nextHighMul = 14;
    float tablelight_Z = 0;
    int tableLight = 0;
    int nextTableLight = 5;
    #endregion
    // Update is called once per frame


    void Update()
    {
        #region 瀏覽器
        if (GameManager.nowChapter == GameManager.Chapter.CH1 && GameManager.nowCH1State == GameManager.CH1State.BrowserFront && GameManager.stage == 2)// 判斷現在的場景
        {
            AniBrickMoving();
            if (!ExitAniPlayed)
                CheckMeteoritePos();
        }
        //=========指針轉=========

        if (pointerRotate)
        {
            #region =====16顆燈泡高光控制======
            HighMul = (int)((Mathf.Abs(Pointer.transform.rotation.eulerAngles.z) + 11.25) / 22.5);
            if (HighMul <= nextHighMul)
            {
                // ======================================
                // 觸發轉盤燈光音效
                audioManager.TurntableLight();
                // ======================================
                if (HighMul == 0)
                {
                    nextHighMul = 15;
                }
                else
                {
                    nextHighMul -= 1;
                }
                Highlight_Z = (HighMul * 22.5f);
                Hightlight.transform.rotation = (new Quaternion(0, 0, 0, Pointer.transform.rotation.w));
                Hightlight.transform.Rotate(new Vector3(0, 0, Highlight_Z));
            }
            #endregion

            #region =====轉盤色塊高光控制======
            tableLight = (int)((Mathf.Abs(Pointer.transform.rotation.eulerAngles.z) + 30) % 360 / 60);

            if (tableLight <= nextTableLight)
            {
                if (tableLight == 0)
                {
                    nextTableLight = 5;
                }
                else
                {
                    nextTableLight -= 1;
                }
                tablelight_Z = ((tableLight) * 60f);
                TableLight.transform.rotation = (new Quaternion(0, 0, 0, Pointer.transform.rotation.w));
                TableLight.transform.Rotate(new Vector3(0, 0, tablelight_Z));
            }
            #endregion


        }
        #endregion


        #region 背面
        if ((int)GameManager.nowCH1State % 2 != 0)//如果在背面
        {
            if (ShowBag && CursorSetting.CursorNotTrigger && !userManager.isZoomin && CursorFX.PlayerMovable)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // Debug.Log("test");
                    Bag_Disappear();
                    ShowBag = !ShowBag;
                }
            }
        }
        #endregion
    }

    #region 第一關轉場

    public GameObject Ch1TransObj;


    public IEnumerator Ch1Transition()
    {
        FrontCanvas.SetActive(false);
        Ch1TransObj.SetActive(true);
        GameObject Black = Ch1TransObj.transform.Find("Black").gameObject;
        GameObject TextCh1 = Ch1TransObj.transform.Find("Text_CH1").gameObject;
        GameObject TextCh1tittle = TextCh1.transform.GetChild(0).gameObject;
        yield return new WaitForSeconds(3f);
        Black.GetComponent<Image>().DOFade(0, 0.5f);
        TextCh1.GetComponent<Text>().DOFade(0, 0.5f);
        TextCh1tittle.GetComponent<Text>().DOFade(0, 0.5f).OnComplete(() =>
        {
            // 開機音效
            audioManager.StartComputerAudio();
            Ch1TransObj.SetActive(false);
            FrontCanvas.SetActive(true);
        });
    }

    #endregion


    #region 正反切換
    public void ChangeToBack()
    {
        if (GameManager.nowChapter == GameManager.Chapter.CH1)
        {

            //set playermovable
            CursorFX.PlayerMovable = true;

            switch (GameManager.nowCH1State)
            {
                case GameManager.CH1State.DeskTopFront:
                    Chapter1ChangeToBack(DesktopBack, DesktopFront);
                    GameManager.nowCH1State = GameManager.CH1State.DeskTopBack;
                    bgmManager.PlayDesktopBgm();
                    break;
                case GameManager.CH1State.NoteFront:
                    Chapter1ChangeToBack(DesktopBack, NoteFront);
                    GameManager.nowCH1State = GameManager.CH1State.NoteBack;
                    bgmManager.PlayDesktopBgm();
                    break;
                case GameManager.CH1State.AntivirusFront:
                    Chapter1ChangeToBack(DesktopBack, AntivirusFront);
                    GameManager.nowCH1State = GameManager.CH1State.AntivirusBack;
                    bgmManager.PlayDesktopBgm();
                    break;

                case GameManager.CH1State.CMailFront:
                    if (Bird[1].activeSelf && clickWormDiaFirst)
                    {
                        // 鳥醒著，且嘴上叼著S，觸發文本
                        dialogueManager.ShowNextDialogue("bird_wakeup_and_eat_s", false);
                        clickWormDiaFirst = false;
                    }
                    Chapter1ChangeToBack(CMailBack, CMailFront);
                    GameManager.nowCH1State = GameManager.CH1State.CMailBack;
                    bgmManager.PlayCmailBgm();
                    break;
                case GameManager.CH1State.BrowserFront:
                    if (Fishing_Back.activeSelf && openFishingBackFirst)
                    {
                        // 開啟釣魚網站的背面，觸發文本
                        dialogueManager.ShowNextDialogue("to_fishing_web_back", false);
                        openFishingBackFirst = false;
                    }
                    else if (!Fishing_Back.activeSelf && openBrowserBackFirst)
                    {
                        // 第一次開啟一般browser的背面，觸發文本
                        dialogueManager.ShowNextDialogue("to_connected_browser_back", false);
                        openBrowserBackFirst = false;
                    }
                    if (Fishing_Back.activeSelf)
                    {
                        bgmManager.PlayFishingBgm();
                    }
                    else
                    {
                        bgmManager.PlayLibraryBgm();
                    }
                    Chapter1ChangeToBack(BrowserBack, BrowserFront);
                    GameManager.nowCH1State = GameManager.CH1State.BrowserBack;
                    break;
                case GameManager.CH1State.FolderFront:
                    Chapter1ChangeToBack(FolderBack, FolderFront);
                    GameManager.nowCH1State = GameManager.CH1State.FolderBack;
                    bgmManager.PlayFolderBgm();
                    break;
                case GameManager.CH1State.MythFront:
                    Chapter1ChangeToBack(MythBack, MythFront);
                    GameManager.nowCH1State = GameManager.CH1State.MythBack;
                    if (GameManager.stage >= 10)
                    {
                        Destroy(ScrollPre);
                        Destroy(TutorialPre);
                        ScrollView.SetActive(true);
                        TutorialView.SetActive(true);
                    }
                    bgmManager.PlayMythBgm();
                    break;
                default:
                    break;
            }
        }
    }

    public void Chapter1ChangeToBack(GameObject ToOpen, GameObject ToClose)
    {
        // 桌面正面關閉
        DesktopFront.SetActive(false);
        // 開啟反面
        ToOpen.SetActive(true);
        // 關閉正面
        ToClose.SetActive(false);
        // ToolBar 處理
        ToolBarFront.SetActive(false);
        ToolBarBack.SetActive(true);
        // 開啟角色
        /*userManager.mouseWorldPosition.x = Player.transform.position.x;
        Player.transform.localPosition = Player.transform.localPosition;*/
        Player.SetActive(true);
        // 開啟狗
        Dog.SetActive(true);
        //通道開啟
        Portal.SetActive(true);
        // 把所有tools裡面的props的sortinglayer都設為backitem
        SpriteRenderer[] component = Tools.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in component)
        {
            s.sortingLayerName = "BackItem";
        }
        // 把所有正面的道具丟到背面
        Tools.transform.SetParent(Bar_Back.transform);
        // Tools.GetComponent<RectTransform>().sizeDelta = new Vector2(11.75f, 1.031601f);
        Tools.transform.localPosition = new Vector3(0, 0, 0);
        if (Fishing_Back.activeSelf && GameManager.nowCH1State == GameManager.CH1State.BrowserFront)
        {
            // 把狗動畫切換
            Dog.GetComponent<Animator>().SetBool("water_idle", true);
        }
    }


    // =============== 反到正 ======================
    public void ChangeToFront()
    {
        // 關閉目前的dialogue
        dialogueManager.StopNowDialogue();
        // ==========================
        audioManager.UpdateIsChangeToFront(true);

        if (GameManager.nowChapter == GameManager.Chapter.CH1)
        {
            switch (GameManager.nowCH1State)
            {
                case GameManager.CH1State.DeskTopBack:
                    bgmManager.StopBackBgm("Desktop");
                    GameManager.nowCH1State = GameManager.CH1State.DeskTopFront;
                    Chapter1ChangeToFront(DesktopFront, DesktopBack);
                    break;
                case GameManager.CH1State.NoteBack:
                    bgmManager.StopBackBgm("Desktop");
                    GameManager.nowCH1State = GameManager.CH1State.NoteFront;
                    Chapter1ChangeToFront(NoteFront, DesktopBack);
                    break;
                case GameManager.CH1State.AntivirusBack:
                    bgmManager.StopBackBgm("Desktop");
                    GameManager.nowCH1State = GameManager.CH1State.AntivirusFront;
                    Chapter1ChangeToFront(AntivirusFront, DesktopBack);
                    break;
                case GameManager.CH1State.CMailBack:
                    bgmManager.StopBackBgm("Cmail");
                    GameManager.nowCH1State = GameManager.CH1State.CMailFront;
                    Chapter1ChangeToFront(CMailFront, CMailBack);
                    break;
                case GameManager.CH1State.BrowserBack:
                    if (Fishing_Back.activeSelf)
                    {
                        bgmManager.StopBackBgm("Fishing");
                    }
                    else
                    {
                        bgmManager.StopBackBgm("Library");
                    }
                    GameManager.nowCH1State = GameManager.CH1State.BrowserFront;
                    Chapter1ChangeToFront(BrowserFront, BrowserBack);
                    break;
                case GameManager.CH1State.FolderBack:
                    bgmManager.StopBackBgm("Folder");
                    GameManager.nowCH1State = GameManager.CH1State.FolderFront;
                    Chapter1ChangeToFront(FolderFront, FolderBack);
                    break;
                case GameManager.CH1State.MythBack:
                    bgmManager.StopBackBgm("Myth");
                    GameManager.nowCH1State = GameManager.CH1State.MythFront;
                    Chapter1ChangeToFront(MythFront, MythBack);
                    MythCloseBtnOpen();//打開Myth的closeBtn
                    break;
                default:
                    break;
            }
        }
        StartCoroutine(WaitToReset());
    }
    IEnumerator WaitToReset()
    {
        yield return new WaitForSeconds(1f);
        audioManager.UpdateIsChangeToFront(false);
    }
    public void Chapter1ChangeToFront(GameObject ToOpen, GameObject ToClose)
    {
        // 把狗動畫切換
        if (Fishing_Back.activeSelf)
        {
            Dog.GetComponent<Animator>().SetBool("water_idle", false);
        }
        // 如果背包是開的就關掉
        if (ShowBag)
        {
            // 關閉背包
            Bag_Appear();
        }
        // Debug.Log("Check: " + GameManager.stage);
        // 桌面正面開啟
        DesktopFront.SetActive(true);
        // 開啟正面
        ToOpen.SetActive(true);
        // 關閉反面
        ToClose.SetActive(false);
        // ToolBar 處理
        ToolBarFront.SetActive(true);
        ToolBarBack.SetActive(false);
        // 關閉角色
        Player.SetActive(false);
        userManager.mouseWorldPosition.x = -4f;
        userManager.mouseWorldPosition.y = Player.transform.localPosition.y;
        Player.transform.localPosition = userManager.mouseWorldPosition;
        // 關閉狗
        Dog.SetActive(false);
        //通道關閉
        Portal.SetActive(false);
        // 把所有tools裡面的props的sortinglayer都設為item
        SpriteRenderer[] component = Tools.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in component)
        {
            s.sortingLayerName = "Item";
        }
        // 把所有背面的道具丟到前面
        Tools.transform.SetParent(ToolBarFront.transform);
        // Tools.GetComponent<RectTransform>().sizeDelta = new Vector2(12.24095f, 1.031601f);
        Tools.transform.localPosition = new Vector3(-1f, -4.5f, 0);


        if (GameManager.stage >= 10 && ChangeBackToFront_first)
        {
            dialogueManager.ShowNextDialogue("backtofront_first", true); //第一次從背面回到正面
            ChangeBackToFront_first = false;
        }
        // ===============================


    }

    public void showhorseTips()
    {
        GameObject.Find("Desktop_front_Background").transform.Find("NoticeAni").gameObject.SetActive(true);
    }

    bool Notcloseyet = true;
    public IEnumerator closeHorseTips()
    {
        if (Notcloseyet)
        {
            Notcloseyet = false;
            yield return new WaitForSeconds(0.6f);
            Destroy(GameObject.Find("Desktop_front_Background").transform.Find("NoticeAni").gameObject);//.SetActive(false);
        }
    }
    #endregion

    #region 道具bar換圖
    int ConvertNameToNum(string name)
    {
        switch (name)
        {
            case "note_account":
                return 0;
            case "Bitcoin":
                return 1;
            case "E":
                return 2;
            case "G":
                return 3;
            case "Gift":
                return 4;
            case "GUESTBtnProp":
                return 5;
            case "Meteorite":
                return 6;
            case "Bomb":
                return 7;
            case "note_password":
                return 8;
            case "S":
                return 9;
            case "Shovel":
                return 10;
            case "T":
                return 11;
            case "U":
                return 12;
            case "ExitBTN":
                return 13;
        }
        // error
        return 1000;
    }
    public void ToolsTriggerBar(GameObject obj, bool isFront)
    {
        int num = ConvertNameToNum(obj.name);
        if (isFront)
        {
            // 更換正面的bar圖
            obj.GetComponent<SpriteRenderer>().sprite = bar_sprite[num];
            Vector2 v = new Vector2((float)bar_sprite[num].texture.width / 100, (float)bar_sprite[num].texture.height / 100);
            // 根據要轉換的圖片大小更改gameobject的width & height & scale
            obj.GetComponent<RectTransform>().sizeDelta = v;
            obj.GetComponent<RectTransform>().localScale = new Vector3(0.2183149f, 0.2183149f, 0);
            // 根據gameobject的大小改變collider的大小
            obj.GetComponent<BoxCollider2D>().size = v;
        }
        else
        {
            // 更換背面的bar圖
            obj.GetComponent<SpriteRenderer>().sprite = backbar_sprite[num];
            Vector2 v = new Vector2((float)backbar_sprite[num].texture.width / 100, (float)backbar_sprite[num].texture.height / 100);
            // 根據要轉換的圖片大小更改gameobject的width & height & scale
            obj.GetComponent<RectTransform>().sizeDelta = v;
            obj.GetComponent<RectTransform>().localScale = new Vector3(0.2183149f, 0.2183149f, 0);
            // 根據gameobject的大小改變collider的大小
            obj.GetComponent<BoxCollider2D>().size = v;
            obj.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        }
        if (obj.name == "T")
        {
            obj.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    public void ToolsExitBar(GameObject obj, Vector3 scale, Vector2 colliderSize)
    {
        int num = ConvertNameToNum(obj.name);
        obj.GetComponent<SpriteRenderer>().sprite = source_sprite[num];

        // if (obj.name == "Meteorite")
        // {
        //     // obj.GetComponent<Transform>().localScale = scale;
        // }
        // else
        // {
        Vector2 v = new Vector2((float)source_sprite[num].texture.width / 100, (float)source_sprite[num].texture.height / 100);
        // 根據要轉換的圖片大小更改gameobject的width & height & scale
        obj.GetComponent<RectTransform>().sizeDelta = v;
        obj.GetComponent<RectTransform>().localScale = scale;
        // 根據gameobject的大小改變collider的大小
        if (obj.name == "Gift")
        {
            obj.GetComponent<BoxCollider2D>().size = new Vector2(1.392334f, 1.392334f);
        }
        else
        {
            obj.GetComponent<BoxCollider2D>().size = colliderSize;
        }
        obj.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        // }

    }
    #endregion

    #region Zoomin畫面控制
    public void Zoomin(GameManager.Zoomin item, bool toShow)
    {
        switch (item)
        {
            case GameManager.Zoomin.Clock:
                ClockZoominObj.SetActive(toShow);
                break;
            case GameManager.Zoomin.PicFrame:
                PicFrameZoomin.SetActive(toShow);
                PicFrameZoominBtn.SetActive(toShow);
                break;
            case GameManager.Zoomin.GUEST:
                for (int i = 0; i < 5; i++)
                {
                    PuttedGuest[i] = false;
                    BigGUEST[i].GetComponent<SpriteRenderer>().sprite = null;
                }
                GUEST_Zoomin.SetActive(toShow);
                break;
            case GameManager.Zoomin.Poster:
                PosterZoominObj.SetActive(toShow);
                break;
            default:
                break;
        }
        userManager.isZoomin = true;
        CursorSetting.CursorNotTrigger = true;
    }
    #endregion

    #region 桌面

    //========================正面==========================
    public void ShowMythDownloadWindow(bool toShow)
    {
        MythDownloadWindow.SetActive(toShow);
        MythDownLoadWindow.SetActive(toShow);

        if (toShow)
        {
            // =========================
            // Myth廣告出現音效
            audioManager.ShowAD();
            // =========================
        }

        if (toShow) MythDownloadWindow.transform.DOLocalMoveY(-173f, 2f).SetEase(Ease.Linear);//DOMoveY(-173f, 0.5f);

        if (toShow) MythDownLoadWindow.transform.DOLocalMoveY(-4.48f, 2f).SetEase(Ease.Linear);
    }

    public void ShowFishingADWindow(bool toShow)
    {
        FishingADloadWindow.SetActive(toShow);
        FishingADLoadWindow.SetActive(toShow);
        if (toShow)
        {
            // =========================
            // Myth廣告出現音效
            audioManager.ShowAD();
            // =========================
            VovoBox.SetActive(true);//showvovobox
        }

        if (toShow) FishingADloadWindow.transform.DOLocalMoveY(-173f, 2f).SetEase(Ease.Linear);//DOMoveY(-173f, 0.5f);

        if (toShow) FishingADLoadWindow.transform.DOLocalMoveY(-4.48f, 2f).SetEase(Ease.Linear);
    }

    //========================反面==========================
    public void CheckClockTime()
    {
        Vector3 HourRot = Hour_hand.transform.eulerAngles;
        //float angle = (HourRot.z - 360) * -1;
        float angle = Mathf.Abs(HourRot.z - 360);
        // Debug.Log(angle);
        int hour = (int)(angle / 30);
        int min = (int)((angle % 30) * 2);
        DigitalClock.GetComponent<Text>().text = (hour < 10 ? "0" + hour.ToString() : hour.ToString()) + " : " + (min < 10 ? "0" + min.ToString() : min.ToString());

        if (angle <= 90 && angle >= 60 && EarthwormMissed == true && GetS == false)
        {
            // 調到正確時間，觸發文本
            dialogueManager.ShowNextDialogue("change_time_to_2pm", false);
            // =================================================
            Bird[0].SetActive(false);
            Bird[1].SetActive(false);
            Bird[2].SetActive(true);

            // Debug.Log(HourRot.z - 360);

            if (GameManager.stage == 21)
            {
                GameManager.stage++;
            }
            CmailWormIcon.SetActive(true);
            CmailWindowWorm.SetActive(true);
            CmailWormBlue.SetActive(false);
            CmailIcon.SetActive(false);
        }
        else if (EarthwormMissed == true)
        {
            if (GetS)
            {
                Bird[0].SetActive(true);
                Bird[1].SetActive(false);
                Bird[2].SetActive(false);
                CmailWormIcon.SetActive(false);
                CmailWindowWorm.SetActive(false);
                CmailWormBlue.SetActive(false);
                CmailIcon.SetActive(true);
            }
            else
            {
                Bird[0].SetActive(false);
                Bird[1].SetActive(true);
                Bird[2].SetActive(false);
                CmailWormIcon.SetActive(true);
                CmailWindowWorm.SetActive(true);
                CmailWormBlue.SetActive(false);
                CmailIcon.SetActive(false);
            }

        }
        else
        {
            Bird[0].SetActive(true);
            Bird[1].SetActive(false);
            Bird[2].SetActive(false);
            CmailWormIcon.SetActive(false);
            CmailWindowWorm.SetActive(false);
            CmailIcon.SetActive(true);
        }
    }


    #endregion

    #region 瀏覽器

    #region 小恐龍
    public bool DinoEatAniDone = false;
    public void PlayDinoEatAni()//播放吃叉叉動畫
    {
        // PlayDinoYee(false);
        DinoEat.SetTrigger("Eat");
    }

    public void PlayDinoYee()//播放Yee動畫
    {
        if (DinoEat.GetCurrentAnimatorStateInfo(0).IsName("dinosaur_idle00"))
        {
            DinoEat.SetBool("Yee", true);
        }
        else
        {
            DinoEat.SetBool("Yee", false);
        }
    }

    public void BrowserFrontMouseClick()//開始小恐龍遊戲
    {
        BrowserGroundMoving = true;
        DinAni.SetBool("Din_Run", true);
        BTNBrowser.SetActive(false);

        // StringCTS.SetActive(false);
        // Progressbar.SetActive(true);

        // dinoHead.transform.DOLocalMoveX(1.5f, 10f).SetEase(Ease.Linear) //5 15f//更改遊戲時間
        // .OnComplete(() =>
        // {
        //     GameObject.Find("BtnPlayerClick").GetComponent<CursorFX>().OnMouseExit();
        //     Destroy(GameObject.Find("BtnPlayerClick"));
        //     Meteorite.transform.DOLocalMoveX(-0.2f, 1.16f).SetEase(Ease.Linear)
        //     .OnComplete(() =>
        //     {
        //         // ============================
        //         // 遊戲結束音樂結束
        //         bgmManager.StopBGM();
        //         audioManager.DinasorEnd();
        //         // ============================
        //         BrowserGroundMoving = false;
        //     });
        // });

        // AniHead();
    }

    void AniHead()//進度條DinoHead旋轉
    {
        // dinoHead.transform.DOLocalRotate(new Vector3(0, 0, -40), 1f)
        // .OnComplete(() =>
        // {
        //     dinoHead.transform.DOLocalRotate(new Vector3(0, 0, 40), 1f).SetEase(Ease.OutSine)
        //     .OnComplete(() =>
        //     {
        //         AniHead();
        //     });
        // });
    }

    int brickcounter = 0;
    bool birckonCD = false;
    public void BrowserPlayerClick()//生成磚塊
    {
        if (BrowserGroundMoving && Meteorite.transform.position.x > 10 && brickcounter <= 5 && !birckonCD)
        {
            // 生成磚塊音效
            audioManager.DinasorPutBricks();
            // ==================
            if (brickcounter != 5)
            {
                birckonCD = true;
                Instantiate(Bricks[brickcounter], BrickSpawn.transform);
                DinoitemIconSR.sprite = DinoitemIconimg[brickcounter];
                StartCoroutine(BrickCD());
            }
            else
            {
                GameObject.Find("BtnPlayerClick").GetComponent<CursorFX>().OnMouseExit();
                Destroy(GameObject.Find("BtnPlayerClick"));
                Destroy(DinoitemIconSR.gameObject);
                Meteorite.transform.DOLocalMoveX(-0.2f, 1.16f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // ============================
                    // 遊戲結束音樂結束
                    bgmManager.StopBGM();
                    audioManager.DinasorEnd();
                    // ============================
                    BrowserGroundMoving = false;
                });
            }
            brickcounter++;
        }
    }

    IEnumerator BrickCD()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        birckonCD = false;
        StopCoroutine(BrickCD());
    }

    public void AniBrickMoving()//移動
    {

        if (BrowserGroundMoving && Ground.transform.position.x > -46.75)
        {
            Ground.transform.position = new Vector3(Ground.transform.position.x - Groundspeed * Time.deltaTime, Ground.transform.position.y, 0);
        }
        else
        {
            Ground.transform.position = new Vector3(-1.04f, Ground.transform.position.y, 0);
        }

        // if (BrickSpawn.transform.childCount == 0)
        // {
        //     Dino.transform.DOPlay();
        // }

        if (DinoEatAniDone)
        {
            if (!BrowserGroundMoving && DinAni.GetBool("Din_Run"))
            {
                DinAni.SetBool("Din_Run", false);
                DinAni.SetBool("Din_Idle", true);
            }
        }



        if (BrickSpawn.transform.childCount > 0)
        {
            for (int i = 0; i < BrickSpawn.transform.childCount; i++)
            {
                if (BrickSpawn.transform.GetChild(i).transform.position.x < -10)
                {
                    Destroy(BrickSpawn.transform.GetChild(i).gameObject);
                }
                else if (BrickSpawn.transform.GetChild(i).transform.position.x <= -3.5 && BrickSpawn.transform.GetChild(i).transform.position.x >= -4)//&& BrickinGroun0[i].transform.position.x >= -7)
                {
                    BrickSpawn.transform.GetChild(i).transform.position = new Vector3(BrickSpawn.transform.GetChild(i).transform.position.x - Groundspeed * Time.deltaTime, BrickSpawn.transform.GetChild(i).transform.position.y, 0);

                    // if (!dinojumping || Dino.transform.position.y >= 1.99f) { Dino.transform.DOPause(); }

                    Dino.GetComponent<Animator>().enabled = false;

                    if (dinoidel)//Dino.transform.position.y <= 0.5 && 
                    {
                        dinoidel = false;
                        // 加小恐龍跳躍音效
                        audioManager.DinasorJump();
                        // ====================
                        Dino.transform.DOLocalMoveY(2f, 0.25f).SetEase(Ease.OutCubic)
                        .OnComplete(() =>
                          {
                              Dino.transform.DOLocalMoveY(0.3f, 0.25f).SetEase(Ease.InCubic)
                              .OnComplete(() =>
                              {
                                  Dino.GetComponent<Animator>().enabled = true;
                                  DinAni.SetBool("Din_Run", true);
                                  dinoidel = true;
                              });

                          });
                    }
                }
                else
                {
                    BrickSpawn.transform.GetChild(i).transform.position = new Vector3(BrickSpawn.transform.GetChild(i).transform.position.x - Groundspeed * Time.deltaTime, BrickSpawn.transform.GetChild(i).transform.position.y, 0);
                }
            }
        }
    }

    public void AniMeteorite(GameObject Meteorite)//隕石砸小恐龍動畫
    {
        Meteorite.transform.GetChild(1).gameObject.SetActive(true);
        Meteorite.transform.DOLocalMoveY(2.8f, 0.2f).SetEase(Ease.Linear)
        .OnComplete(
            () =>
            {
                Meteorite.transform.DOLocalMoveY(-8.81f, 0.8f).SetEase(Ease.InQuad);
                Dino.transform.DOLocalMoveY(-8.81f, 0.8f).SetEase(Ease.InQuad);
            }
        );
        Dino.GetComponent<Animator>().enabled = false;
        Dino.GetComponent<SpriteRenderer>().sprite = DinoSprite[2];

        Meteorite.transform.DOShakeRotation(2, 10, 90, 50, true)
        .OnComplete(
            () =>
            {
                // Progressbar.SetActive(false);
                // dinoHead.SetActive(false);
                AniDinoCityRaising();
            }
        );

    }

    bool Masked = false;
    bool ExitAniPlayed = false;
    public void CheckMeteoritePos()//隕石砸地板動畫
    {
        if (Meteorite.transform.localPosition.y < -0.75 && !Meteorite.GetComponent<ItemEventManager>().PlayerGet && !Masked)
        {
            // ==================================
            // 隕石砸地板音效
            audioManager.MetoriteHitGroundAudio();
            // ==================================
            Meteorite.transform.GetChild(1).gameObject.transform.parent = Meteorite.transform.parent.transform;
            Masked = true;
        }

        if (Meteorite.transform.localPosition.y < 1.1 && !Masked && !Meteorite.GetComponent<ItemEventManager>().PlayerGet)
        {
            Meteorite.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (Meteorite.transform.localPosition.y < -2.4 && !Meteorite.GetComponent<ItemEventManager>().PlayerGet)
        {
            // ==================================
            // 隕石破掉音效
            audioManager.MetoriteBroken();
            // ==================================
            Meteorite.GetComponent<SpriteRenderer>().sprite = null;

            Meteorite.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(Meteorite.GetComponent<ItemEventManager>());
            // if (!ExitAniPlayed)
            {
                PlayExitBtnAni();
            }// ExitBTN.SetActive(true);
        }
    }

    public void AniDinoCityRaising()//城市升起動畫
    {
        // ==================================
        // 城市升起音效
        audioManager.CityRising();
        // ==================================
        City.transform.GetChild(0).DOLocalMoveY(0.76f, 3f);
        City.transform.GetChild(1).DOLocalMoveY(0.76f, 5f);
        City.transform.GetChild(2).DOLocalMoveY(0.76f, 6f);
        City.transform.GetChild(0).DOShakeRotation(2.5f, 5, 30, 40, true);
        City.transform.GetChild(1).DOShakeRotation(4.5f, 5, 30, 40, true);
        City.transform.GetChild(2).DOShakeRotation(5.5f, 5, 30, 40, true).OnComplete(
            () =>
            {
                // CloseBrowserBtn.SetActive(true);
                if (GameManager.stage == 2)
                {
                    GameManager.stage++;
                    Wifi.SetActive(true);
                    ChangeMessageBox();
                    ChangeAntiMessageBox();
                }
            })
            ;
    }
    public void PlayExitBtnAni()
    {
        ExitAniPlayed = true;
        ExitBTN.SetActive(true);
        ExitBTN.transform.parent = GameObject.Find("Tools").gameObject.transform;
        Sequence s = DOTween.Sequence();
        s.Append(ExitBTN.transform.DOMoveX(0f, 0.5f).SetEase(Ease.Linear));//ExitBTN.transform.position.x + 
        s.Insert(0, ExitBTN.transform.DOMoveY(-1.54f, 0.25f).SetEase(Ease.OutCirc));//ExitBTN.transform.position.y +
        //下落
        s.Insert(0.25f, ExitBTN.transform.DOMoveY(-4.64f, 0.25f).SetEase(Ease.InCirc)).OnComplete(() =>
        {
            s.Kill();
        });//ExitBTN.transform.position.y

    }

    public void ShowBrowerConnected()//顯示連線提示框
    {
        BrowserConnected.SetActive(true);
        Destroy(BrowserConnected.transform.parent.gameObject.transform.GetChild(0).gameObject);
    }
    #endregion


    #region  釣魚網站

    public void showFishingWeb()
    {
        Fishing_Back.SetActive(true);
        Browserback_back.SetActive(false);

        BrowserConnected.SetActive(false);
        FishingWeb.SetActive(true);
    }

    bool clickLink = false;
    public void ChangeLinkColor()
    {
        clickLink = true;
        GameObject.Find("link").GetComponent<Text>().color = new Color32(159, 65, 127, 255);
        GameObject.Find("underline").GetComponent<Text>().color = new Color32(159, 65, 127, 255);
    }

    float rotateAngle = 0;  // 旋轉的角度
    bool pointerRotate = false;
    public void rotatePointer()
    {
        // =================================
        // 觸發點擊開始轉盤音效
        audioManager.TurntableAudio();
        // =================================

        GameObject.Find("startTrigger").SetActive(false);
        //===========設定鼠標============
        AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        //==============================

        TurnTableStartBtn.SetActive(false);
        pointerRotate = true;
        rotateAngle = Random.Range(-686f, -393f);

        Pointer.transform.DORotate(new Vector3(0, 0, rotateAngle), 2f, RotateMode.FastBeyond360).OnComplete(
        () =>
        {
            // ======================================
            // 觸發得到失敗音效
            audioManager.TurntableFail();
            // ======================================
            pointerRotate = false;
            TurnTableStartBtn.SetActive(true);
            //===========設定鼠標============
            AniEvents.AniDone = true;
            cursorSetting.StopCursorHourglassAni();
            //==============================
            GameObject.Find("pointer").transform.GetChild(0).gameObject.SetActive(true);
        }
        );
    }

    public int turntableAfterU()//用磁鐵吸引指針後
    {
        HighMul = (int)((Mathf.Abs(Pointer.transform.rotation.eulerAngles.z) + 11.25) / 22.5);
        Highlight_Z = (HighMul * 22.5f);
        Hightlight.transform.rotation = (new Quaternion(0, 0, 0, Pointer.transform.rotation.w));
        Hightlight.transform.Rotate(new Vector3(0, 0, Highlight_Z));

        tableLight = (int)((Mathf.Abs(Pointer.transform.rotation.eulerAngles.z) + 30) % 360 / 60);
        tablelight_Z = ((tableLight) * 60f);
        TableLight.transform.rotation = (new Quaternion(0, 0, 0, Pointer.transform.rotation.w));
        TableLight.transform.Rotate(new Vector3(0, 0, tablelight_Z));

        if (tableLight == 0)
        {
            // ======================================
            // 觸發得到大獎音效
            audioManager.TurntableWin();
            // ======================================
            GetGiftMesBox.transform.DOScale(new Vector3(1, 1, 0), 0.8f).OnComplete(
                        () =>
                        {
                            GetGiftMesBox.transform.DOShakeScale(0.5f, 0.25f, 10, 90, true).OnComplete(() =>
                            {
                                GetGiftMesBox.transform.DOScale(new Vector3(1, 1, 0), 0.1f).OnComplete(() =>
                                {
                                    // ======================================
                                    // 開啟得到大獎的視窗，觸發文本
                                    dialogueManager.ShowNextDialogue("show_big_prize_window", true);
                                });
                            });
                        }
                        );

        }
        return tableLight;
    }

    public void BtnGetGift()//取得禮物盒
    {
        if (Gift.GetComponent<ItemEventManager>() == null)
        {
            Gift.AddComponent<ItemEventManager>();
        }
        if (GameManager.stage == 26)
        {
            GameManager.stage++;
        }
    }
    public void delGiftMesBox()
    {
        Destroy(GetGiftMesBox);
    }
    #endregion

    #endregion

    #region 資料夾

    //========================正面==========================
    public void OpenApps()
    {
        FolderCloseBtn.SetActive(false);
        switch (GameManager.nowApps)
        {
            case GameManager.APPs.Pick_Branch:
                if (openPickBranchFirst)
                {
                    // 第一次開啟猛男撿樹枝，觸發文本
                    dialogueManager.ShowNextDialogue("open_strongman_pick_branch_window", true);
                    openPickBranchFirst = false;
                }
                Pick_Branch_App.SetActive(true);
                AppBtn.SetActive(false);
                break;
            case GameManager.APPs.Landmine:
                if (openLandminesFirst)
                {
                    // 第一次開啟踩地雷，觸發文本
                    dialogueManager.ShowNextDialogue("open_landmines_window", true);
                    openLandminesFirst = false;
                }
                Landmine_App.SetActive(true);
                AppBtn.SetActive(false);
                break;
            case GameManager.APPs.Hunk_Mora:
                if (openMoraFirst)
                {
                    // 第一次開啟野球拳，觸發文本
                    dialogueManager.ShowNextDialogue("open_mora_window_first", true);
                    openMoraFirst = false;
                }
                Hunk_Mora_App.SetActive(true);
                AppBtn.SetActive(false);
                // ==================================
                // 播放猛男野球拳bgm
                if (theManIsAlive)
                {
                    bgmManager.MoraBGM();
                }
                // ==================================
                break;
            default:
                break;
        }
    }
    public void CloseApps()
    {
        // 關閉視窗音效
        audioManager.CloseWindow();
        // ================================
        FolderCloseBtn.SetActive(true);
        switch (GameManager.nowApps)
        {
            case GameManager.APPs.Pick_Branch:
                Pick_Branch_App.SetActive(false);
                AppBtn.SetActive(true);
                break;
            case GameManager.APPs.Landmine:
                Landmine_App.SetActive(false);
                AppBtn.SetActive(true);
                break;
            case GameManager.APPs.Hunk_Mora:
                Hunk_Mora_App.SetActive(false);
                AppBtn.SetActive(true);
                // ==================================
                // 關閉猛男野球拳bgm
                bgmManager.StopBGM();
                // ==================================
                break;
            default:
                break;
        }
        // 設為default
        GameManager.nowApps = GameManager.APPs.NotOpen;
    }
    public void Mora(int n)//猛男野球拳猜拳
    {
        loseMoraCount++;

        switch (n)
        {
            case 1:
                HunkAni.SetBool("Hunk_stone", true);
                Target.enabled = false;
                StartCoroutine(WaitThreeSeconds(1));
                break;
            case 2:
                HunkAni.SetBool("Hunk_paper", true);
                Target.enabled = false;
                StartCoroutine(WaitThreeSeconds(2));
                break;
            case 3:
                HunkAni.SetBool("Hunk_scissors", true);
                Target.enabled = false;
                StartCoroutine(WaitThreeSeconds(3));
                break;
            case 4:
                theManIsAlive = false;
                // 打敗猛男，觸發文本
                dialogueManager.ShowNextDialogue("kill_the_strongman", true);
                // 關閉背面E提示
                CloseETips();
                // ============================
                HunkAni.SetBool("Hunk_explode", true);
                Destroy(Bomb);
                FoderBack.GetComponent<SpriteRenderer>().sprite = Exploded;
                E.SetActive(true);
                break;
            case 5:
                HunkAni.SetBool("Hunk_U", true);
                Target.enabled = false;
                StartCoroutine(WaitThreeSeconds(5));
                break;
            case 6:
                HunkAni.SetBool("Hunk_S", true);
                Target.enabled = false;
                StartCoroutine(WaitThreeSeconds(6));
                break;
            case 7:
                HunkAni.SetBool("Hunk_T", true);
                Target.enabled = false;
                StartCoroutine(WaitThreeSeconds(7));
                break;
            case 8:
                HunkAni.SetBool("Hunk_Shovel", true);
                Target.enabled = false;
                StartCoroutine(WaitThreeSeconds(8));
                break;
            default:
                break;
        }
        IEnumerator WaitThreeSeconds(int x)
        {
            switch (x)
            {
                case 1:
                    yield return new WaitForSeconds(4.5f);
                    HunkAni.SetBool("Hunk_stone", false);
                    Target.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    moraSprite[0].transform.position = moraPos[0];
                    moraSprite[0].GetComponent<Collider2D>().enabled = true;
                    break;
                case 2:
                    yield return new WaitForSeconds(4.5f);
                    HunkAni.SetBool("Hunk_paper", false);
                    Target.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    moraSprite[1].transform.position = moraPos[1];
                    moraSprite[1].GetComponent<Collider2D>().enabled = true;
                    break;
                case 3:
                    yield return new WaitForSeconds(4.5f);
                    HunkAni.SetBool("Hunk_scissors", false);
                    Target.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    moraSprite[2].transform.position = moraPos[2];
                    moraSprite[2].GetComponent<Collider2D>().enabled = true;
                    break;
                case 5:
                    yield return new WaitForSeconds(4.5f);
                    HunkAni.SetBool("Hunk_U", false);
                    Target.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    GameObject.Find("U").GetComponent<Collider2D>().enabled = true;
                    GameObject.Find("U").GetComponent<LayoutElement>().ignoreLayout = false;
                    break;
                case 6:
                    yield return new WaitForSeconds(4.5f);
                    HunkAni.SetBool("Hunk_S", false);
                    Target.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    GameObject.Find("S").GetComponent<Collider2D>().enabled = true;
                    GameObject.Find("S").GetComponent<LayoutElement>().ignoreLayout = false;
                    break;
                case 7:
                    yield return new WaitForSeconds(4.5f);
                    HunkAni.SetBool("Hunk_T", false);
                    Target.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    GameObject.Find("T").GetComponent<Collider2D>().enabled = true;
                    GameObject.Find("T").GetComponent<LayoutElement>().ignoreLayout = false;
                    break;
                case 8:
                    yield return new WaitForSeconds(4.5f);
                    HunkAni.SetBool("Hunk_Shovel", false);
                    Target.enabled = true;
                    yield return new WaitForSeconds(0.5f);
                    GameObject.Find("Shovel").GetComponent<Collider2D>().enabled = true;
                    GameObject.Find("Shovel").GetComponent<LayoutElement>().ignoreLayout = false;
                    break;
                default:
                    break;
            }
            // 觸發文本
            if (loseMoraCount == 1)
            {
                // 野球拳輸一次，觸發文本
                dialogueManager.ShowNextDialogue("lose_mora_first", true);
            }
            else if (loseMoraCount == 3)
            {
                // 野球拳輸三次，觸發文本
                dialogueManager.ShowNextDialogue("lose_mora_third", true);
            }
            //yield return new WaitForSeconds(3);
        }
    }

    //========================反面==========================
    public void PutFishingObj(int Direction)
    {
        AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        switch (Direction)
        {
            case 1:
                userManager.mouseWorldPosition.x = -4.6f;
                userManager.walk = true;
                userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                userManager.Character.transform.GetChild(0).gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                userManager.PutFishing = true;
                break;
            case 2:
                userManager.mouseWorldPosition.x = 0.13f;
                userManager.walk = false;
                userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                userManager.Character.transform.GetChild(0).gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                userManager.PutFishing = true;
                break;
            case 3:
                if (userManager.walk)
                {
                    userManager.mouseWorldPosition.x = -4.6f;
                    userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                    userManager.Character.transform.GetChild(0).gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    userManager.walk = true;
                    userManager.PutFishing = true;
                }
                else
                {
                    userManager.mouseWorldPosition.x = 0.13f;
                    userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                    userManager.Character.transform.GetChild(0).gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                    userManager.walk = false;
                    userManager.PutFishing = true;
                }
                break;
        }
        /*AniEvents.AniDone = false;
        userManager.isPayMoney = true;*/
    }

    public void GiveU()
    {
        ThinAni.SetBool("GiveU", true);
    }
    public IEnumerator OpenDoor() //守門人消失+把門打開的動畫
    {
        /*AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();

        userManager.walk = true;
        userManager.Character.transform.rotation = new Quaternion(0,0,0,0);
        userManager.mouseWorldPosition.x = 0.73f;*/
        /*userManager.Character.transform.DOLocalMoveX(0.73f, 2f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            userManager.isPayMoney = false;
            userManager.PayMoney = true;
            userManager.mouseWorldPosition.x = userManager.Character.transform.position.x;
            userManager.charPos.x = userManager.mouseWorldPosition.x;
            userManager.walk = true;
        });*/
        //yield return new WaitForSeconds(3.5f);

        Floder.SetActive(false);
        FloderUnlock.SetActive(true);

        //ThinAni.enabled = true;
        ThinAni.SetBool("GetMoney", true);
        FatAni.enabled = true;
        Destroy(WantBitCion);
        Destroy(BitCoin);
        yield return new WaitForSeconds(2f);

        Gatekeeper.transform.DOLocalMoveX(-17f, 6f, false);
        yield return new WaitForSeconds(1.5f);

        // ==================================
        // 門震動的音效
        audioManager.DoorShake();
        // ==================================
        Door.transform.DOShakePosition(2.5f, 0.2f, 10, 10f, false, true);
        //yield return new WaitForSeconds(3.5f);

        // ==================================
        // 門開啟的音效
        audioManager.DoorUp();
        // ==================================
        Door.transform.DOMoveY(12.5f, 17.5f, false);
        yield return new WaitForSeconds(7.5f);
        Destroy(Door);
        Destroy(Gatekeeper);
        ForderLock.SetActive(false);
        ForderUnlock.SetActive(true);
        BtnBackFrame.SetActive(true);
        // 開啟那個男人的提示
        ManButton.SetActive(true);
        // 倉庫門開啟，觸發文本
        dialogueManager.ShowNextDialogue("open_folder_door", false);
        // 改變點擊狗會說的話
        dialogueManager.changeDogStage("open_door");
        // =================================

        AniEvents.AniDone = true;
        cursorSetting.StopCursorHourglassAni();
    }

    public static bool PigeonAniPlayed = false;
    //觸發鴿子叼蚯蚓動畫
    public void PigeonAniStart()
    {
        Destroy(GameObject.Find("Btn_ClickWorm"));
        PigeonAni.SetActive(true);
        CmailWormIcon.SetActive(true);
        CmailWindowWorm.SetActive(true);
        CmailWormBlue.SetActive(false);
        CmailIcon.SetActive(false);
        PigeonAniPlayed = true;
    }
    public static bool GetS = false;

    public void ShowBitcoinTips()
    {
        if (AniEvents.AniDone)
        {
            StartCoroutine(WaitForSecs(WantBitCion, 3f, false));
            // 播放點擊看門人
            audioManager.ClickDoorKeeper();
        }
    }

    public void ToShowETips()
    {
        // ===========================
        // 播放點擊看門人
        audioManager.ClickTheMan();
        // ===========================
        StartCoroutine(WaitForSecs(FolderBackWinTips, 3f, true));
    }

    IEnumerator WaitForSecs(GameObject obj, float secs, bool isUI)
    {
        obj.SetActive(true);
        if (isUI)
        {
            obj.GetComponent<Image>().DOFade(1, 1).OnComplete(() =>
            {
                userManager.CharacterAni.speed = 1;
            });
            yield return new WaitForSeconds(secs);
            obj.GetComponent<Image>().DOFade(0, 1).OnComplete(() =>
            {
                obj.SetActive(false);
            });
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().DOFade(1, 1);
            yield return new WaitForSeconds(secs);
            obj.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
            {
                obj.SetActive(false);
            });
        }
    }

    public void CloseETips()
    {
        Destroy(ManButton);
        // ManButton.SetActive(false);
    }

    public void ClickShovelAni()
    {
        // ===========================
        // 點擊鏟子的音效
        audioManager.ClickShovel();
        // ===========================
        GameObject.Find("Shovel").transform.DOShakePosition(1.2f, new Vector3(0.2f, 0, 0), 8, 45).SetId<Tween>("Shovel");
    }

    //換置CmailIcon
    /*public void SetCmailIcon(int number)
    {
        //CMailIcon.GetComponent<SpriteRenderer>().sprite = CmailIcomImg[0];
        CmailWormBlue.SetActive(false);
        CmailBlue.SetActive(true);
    }*/
    #endregion

    #region 控制第一關視窗開啟或關閉 反藍

    public void setWindowActivate(string state, bool toOpen)
    {
        if (toOpen)
        {
            // 關掉桌面開啟目標畫面
            switch (state)
            {
                case "BrowserFront":
                    WindowOpenAni(BrowserFront);
                    BrowserFront.SetActive(true);
                    FrontCanvas.SetActive(false);
                    if (GameManager.stage >= 4 && GameManager.stage < 25)
                    {
                        BrowserConnected.SetActive(true);
                        FishingWeb.SetActive(false);
                        Fishing_Back.SetActive(false);
                        Browserback_back.SetActive(true);
                    }
                    else if (GameManager.stage >= 25 && clickLink)
                    {
                        BrowserConnected.SetActive(false);
                        FishingWeb.SetActive(true);
                        Fishing_Back.SetActive(true);
                        Browserback_back.SetActive(false);
                    }
                    else if (GameManager.stage >= 25 && !clickLink)
                    {
                        BrowserConnected.SetActive(true);
                        FishingWeb.SetActive(false);
                        Fishing_Back.SetActive(false);
                        Browserback_back.SetActive(true);
                    }
                    else if (GameManager.stage < 4)
                    {
                        BrowserConnected.SetActive(false);
                        FishingWeb.SetActive(false);
                        Fishing_Back.SetActive(false);
                        Browserback_back.SetActive(false);
                    }

                    if (GameManager.stage <= 2)
                    {
                        GameObject.Find("BtnPlayerClick").GetComponent<EventTrigger>().enabled = false;
                    }

                    GameManager.nowCH1State = GameManager.CH1State.BrowserFront;
                    break;
                case "MythFront":
                    WindowOpenAni(MythFront);
                    MythFront.SetActive(true);
                    FrontCanvas.SetActive(false);
                    GameManager.nowCH1State = GameManager.CH1State.MythFront;
                    break;
                case "CMailFront":
                    WindowOpenAni(CMailFront);
                    CMailFront.SetActive(true);
                    FrontCanvas.SetActive(false);
                    GameManager.nowCH1State = GameManager.CH1State.CMailFront;
                    GameManager.nowCH1State = GameManager.CH1State.CMailFront;
                    if (GameManager.stage == 4)
                    {
                        littleWindowCanvas.SetActive(false);
                    }
                    break;
                case "FolderFront":
                    WindowOpenAni(FolderFront);
                    FolderFront.SetActive(true);
                    FrontCanvas.SetActive(false);
                    GameManager.nowCH1State = GameManager.CH1State.FolderFront;
                    break;
                case "NoteFront":
                    WindowOpenAni(NoteFront);
                    NoteFront.SetActive(true);
                    FrontCanvas.SetActive(false);
                    GameManager.nowCH1State = GameManager.CH1State.NoteFront;
                    break;
                case "AntivirusFront":
                    WindowOpenAni(AntivirusFront);
                    // (AntivirusBtnImage);
                    if (ScrollCanvas.activeSelf)
                    {
                        // ScrollViewOpenAni(ScrollView);
                    }
                    AntivirusFront.SetActive(true);
                    FrontCanvas.SetActive(false);
                    GameManager.nowCH1State = GameManager.CH1State.AntivirusFront;
                    break;
                default:
                    break;
            }
            // DesktopFront.SetActive(false);
        }
        else
        {
            // 關掉目標畫面開啟桌面
            DesktopFront.SetActive(true);
            GameManager.nowCH1State = GameManager.CH1State.DeskTopFront;
            switch (state)
            {
                case "BrowserFront":
                    BrowserFront.SetActive(false);
                    FrontCanvas.SetActive(true);
                    break;
                case "MythFront":
                    MythFront.SetActive(false);
                    FrontCanvas.SetActive(true);
                    break;
                case "CMailFront":
                    CMailFront.SetActive(false);
                    FrontCanvas.SetActive(true);
                    if (GameManager.stage == 4)
                    {
                        littleWindowCanvas.SetActive(true);
                    }
                    break;
                case "FolderFront":
                    FolderFront.SetActive(false);
                    Pick_Branch_App.SetActive(false);
                    Landmine_App.SetActive(false);
                    Hunk_Mora_App.SetActive(false);
                    FrontCanvas.SetActive(true);
                    break;
                case "NoteFront":
                    NoteFront.SetActive(false);
                    FrontCanvas.SetActive(true);
                    break;
                case "AntivirusFront":
                    AntivirusFront.SetActive(false);
                    FrontCanvas.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        // Debug.Log(GameManager.nowCH1State);
    }

    public void ClickToBlue(string state)
    {
        CleanBlue();
        switch (state)
        {
            case "BrowserFront":
                BrowserBlue.SetActive(true);
                break;
            case "MythFront":
                MythBlue.SetActive(true);
                break;
            case "CMailFront":
                CmailBlue.SetActive(true);
                CmailWormBlue.SetActive(true);
                break;
            case "FolderFront":
                FloderBlue.SetActive(true);
                FloderUnlockBlue.SetActive(true);
                break;
            case "NoteFront":
                NoteBlue.SetActive(true);
                break;
            case "AntivirusFront":
                AntivirusBlue.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void CleanBlue()
    {
        GameObject[] Blue;
        Blue = GameObject.FindGameObjectsWithTag("ClickToBlue");

        foreach (GameObject go in Blue)
        {
            go.SetActive(false);
        }
    }

    public void FolderClickToBlue(string state)
    {
        FolderCleanBlue();
        switch (state)
        {
            case "Pick_Branch":
                GameObject.Find("FolderClickBlue").transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Landmine":
                GameObject.Find("FolderClickBlue").transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "Finger-guess":
                GameObject.Find("FolderClickBlue").transform.GetChild(2).gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void FolderCleanBlue()
    {
        GameObject[] Blue;
        Blue = GameObject.FindGameObjectsWithTag("FolderClickToBlue");

        foreach (GameObject go in Blue)
        {
            go.SetActive(false);
        }
    }

    public void ControlFolderBackManBtn(bool toOpen)
    {
        if (ManButton != null)
        {
            ManButton.SetActive(toOpen);
        }
    }

    #endregion

    #region 信箱

    public void MailChangeToDickmon()
    {
        Right_Fix.SetActive(false);
        Right_Bitcon.SetActive(false);
        Right_Dickmon.SetActive(true);
        Right_Fishing.SetActive(false);

        // ==============================
        // 觸發點擊音效
        audioManager.BtnClick();
        // ==============================

        GameObject.Find("Left_Fix").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        GameObject.Find("Left_Bitcon").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        GameObject.Find("Left_Dickmon").GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        if (GameManager.stage >= 25)
        {
            GameObject.Find("FishingText").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        }
    }
    public void MailChangeToFix()
    {
        Right_Fix.SetActive(true);
        Right_Bitcon.SetActive(false);
        Right_Dickmon.SetActive(false);
        Right_Fishing.SetActive(false);

        // ==============================
        // 觸發點擊音效
        audioManager.BtnClick();
        // ==============================

        GameObject.Find("Left_Fix").GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        GameObject.Find("Left_Bitcon").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        GameObject.Find("Left_Dickmon").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        if (GameManager.stage >= 25)
        {
            GameObject.Find("FishingText").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        }
    }
    public void MailChangeToBitcon()
    {
        Right_Fix.SetActive(false);
        Right_Bitcon.SetActive(true);
        Right_Dickmon.SetActive(false);
        Right_Fishing.SetActive(false);

        // ==============================
        // 觸發點擊音效
        audioManager.BtnClick();
        // ==============================

        GameObject.Find("Left_Fix").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        GameObject.Find("Left_Bitcon").GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        GameObject.Find("Left_Dickmon").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        if (GameManager.stage >= 25)
        {
            GameObject.Find("FishingText").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        }
    }

    public void MailChangeToFishing()
    {
        NotClick = false;
        DOTween.Kill("Fishing");

        // ==============================
        // 觸發點擊音效
        audioManager.BtnClick();
        // ==============================

        if (clickFishingLeftFirst)
        {
            Debug.Log("Tab inin");
            // 點擊釣魚網站廣告，觸發文本
            dialogueManager.ShowNextDialogue("click-cmail-left-fishing-mail", true);
            clickFishingLeftFirst = false;
        }

        Right_Fix.SetActive(false);
        Right_Bitcon.SetActive(false);
        Right_Dickmon.SetActive(false);
        Right_Fishing.SetActive(true);



        GameObject.Find("Left_Fix").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        GameObject.Find("Left_Bitcon").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        GameObject.Find("Left_Dickmon").GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
        GameObject.Find("FishingText").GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
        Left_Fishing.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void ClickFishingAD()//點擊右下角FishingAD視窗 跳轉至CMail
    {
        if (clickFishingLeftFirst)
        {
            // Debug.Log("AD inin");
            // 點擊釣魚網站廣告，觸發文本
            dialogueManager.ShowNextDialogue("click-cmail-left-fishing-mail", true);
            clickFishingLeftFirst = false;
        }
        Right_Fix.SetActive(false);
        Right_Bitcon.SetActive(false);
        Right_Dickmon.SetActive(false);
        Right_Fishing.SetActive(true);
        Left_Fishing.SetActive(true);
    }


    public void showfishingCmail()
    {
        // =========================
        // 釣魚廣告出現音效
        StartCoroutine(WaitToPlayAudio());
        // =========================
        Left_Fishing.SetActive(true);
        LeftFishingButtonAni1();
        if (GameManager.stage == 24)
        {
            GameManager.stage++;
            VovoBox.SetActive(true);//showvovobox
        }
    }
    IEnumerator WaitToPlayAudio()
    {
        yield return new WaitForSeconds(1f);
        audioManager.ShowAD();
    }
    bool NotClick = true;
    public void LeftFishingButtonAni1()
    {
        if (NotClick)
        {
            Color btncolor = new Color32(255, 255, 255, 255);
            Color textcolor = new Color32(0, 0, 255, 255);
            Left_Fishing.transform.Find("FishingText").GetComponentInChildren<Text>().DOColor(textcolor, 1f).SetId<Tween>("Fishing");
            Left_Fishing.GetComponent<Image>().DOColor(btncolor, 1f).SetId<Tween>("Fishing").OnComplete(
                () =>
                {

                    LeftFishingButtonAni2();

                }
            );
        }
    }

    public void LeftFishingButtonAni2()
    {
        if (NotClick)
        {
            Color btncolor = new Color32(0, 0, 255, 255);
            Color textcolor = new Color32(255, 255, 255, 255);
            Left_Fishing.transform.Find("FishingText").GetComponentInChildren<Text>().DOColor(textcolor, 1f).SetId<Tween>("Fishing");
            Left_Fishing.GetComponent<Image>().DOColor(btncolor, 1f).SetId<Tween>("Fishing").OnComplete(
                () =>
                {
                    LeftFishingButtonAni1();
                    GameObject.Find("FishingText").GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
                }
            );
        }
    }

    public void ClickBitcoinAni()
    {
        // ==========================
        // 觸發點擊bitcoin音效
        audioManager.ClickBitcoin();
        // ==========================
        GameObject.Find("Bitcoin").transform.DOShakePosition(1.2f, new Vector3(5, 5, 0), 8, 45).SetId<Tween>("BitCoinAni");
    }

    public GameObject[] Datacolumns;

    public GameObject DogeArrow;

    public Animator DogeAnitor;

    public IEnumerator GetDogeCoinAni()
    {
        GameObject Arrow;
        Text number;
        Text percentage;

        GameObject[] items = new GameObject[33];

        int k = 0;

        // for (int j = 0; j < 14; j++)
        // {
        //     for (int i = 0; i < 3; i++)
        //     {
        //         Arrow = Datacolumns[i].transform.GetChild(j).GetChild(0).gameObject;
        //         Arrow.transform.DOLocalRotate(new Vector3(180, 0, 0), 0.1f).SetEase(Ease.Linear);
        //         Arrow.GetComponent<Text>().color = Color.red;
        //     }
        //     yield return new WaitForSeconds(0.125f);
        // }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                items[k] = Datacolumns[i].transform.GetChild(j).gameObject;
                k++;
            }
        }
        Shuffle(items);

        DogeAnitor.SetTrigger("DogeCry");
        // 旋轉音效
        audioManager.DogeCoinPointerRotate();
        // 旋轉
        DogeArrow.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.75f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.75f);
        // 旋轉完
        for (int i = 0; i < items.Length; i++)
        {
            Arrow = items[i].transform.GetChild(0).gameObject;
            number = items[i].transform.GetChild(1).gameObject.GetComponent<Text>();
            percentage = items[i].transform.GetChild(2).gameObject.GetComponent<Text>();
            Arrow.transform.DOLocalRotate(new Vector3(180, 0, 0), 0.1f).SetEase(Ease.Linear);
            Arrow.GetComponent<Text>().color = new Color32(81, 60, 158, 255);
            number.text = "0.00000000";
            // number.color = new Color32(81, 60, 158, 255);
            percentage.text = "-100 %";
            percentage.color = new Color32(81, 60, 158, 255);
            yield return new WaitForSeconds(0.025f);
        }
    }

    GameObject[] Shuffle(GameObject[] items)//打亂順序
    {
        for (int i = 0; i < items.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, items.Length);
            GameObject temp = items[randomIndex];
            items[randomIndex] = items[i];
            items[i] = temp;
        }
        return items;
    }
    #endregion

    #region Myth

    public void Stole_Password()
    {
        if (AcountSprit.GetComponent<SpriteRenderer>().color == new Color(255, 255, 255, 255) && PasswardSprite.GetComponent<SpriteRenderer>().color == new Color(255, 255, 255, 255) && GameManager.stage == 9)
        {
            // 把數字時鐘的layer提高
            // ClockCanvas.GetComponent<Canvas>().sortingOrder = 4;
            // ======================
            GameObject.Find("CloseWindow_FX").GetComponent<Button>().interactable = false;
            Destroy(GameObject.Find("Log in"));
            Destroy(AcountSprit);
            Destroy(PasswardSprite);
            Destroy(Myth_notPuton);
            Myth_StolePassword.SetActive(true);
            StoleAin.enabled = true;
            GameManager.stage++;

            AntiGoOutSide.SetActive(true);//防毒軟體變成外出中
            AntiAlert.SetActive(false);
            AntiSleep.SetActive(false);
        }
    }
    public void MythLightFX()
    {
        GameObject.Find("MirrorHighLight").GetComponent<SpriteRenderer>().DOFade(0.7f, 1f).SetId<Tween>("MirrorFX").OnComplete(() =>
        {
            MythDarkFX();
        });
    }
    void MythDarkFX()
    {
        GameObject.Find("MirrorHighLight").GetComponent<SpriteRenderer>().DOFade(0, 1f).SetId<Tween>("MirrorFX").OnComplete(() =>
        {
            MythLightFX();
        });
    }
    public void DeletMirrorTween()
    {
        DOTween.Kill("MirrorFX");
        Destroy(MirrorHightlight);
    }



    public void MythCloseBtnOpen()
    {
        if (userManager.FirstToBack == false)
        {
            MythCloseBtn.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            MythCloseBtn.GetComponent<Button>().enabled = true;
            MythCloseBtn.transform.parent.GetChild(1).gameObject.SetActive(true);
            MythCloseBtn.transform.parent.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        }
    }

    #region GUSET
    bool allPut = true;

    public static bool GuestAllPutted = false;

    public static bool GuestReset = false;

    public GameObject BtnGuestZoominExit;
    public void PutGUEST(GameObject letter, int order)
    {
        letter.GetComponent<SpriteRenderer>().sprite = GUEST[order];
        PuttedGuest[order] = true;
        allPut = true;
        for (int i = 0; i < 5; i++)
        {
            if (!PuttedGuest[i])
            {
                allPut = false;
            }
        }
        if (allPut)
        {
            if (GameManager.stage == 29)
            {
                GameManager.stage++;
            }

            BtnGuestZoominExit.GetComponent<EventTrigger>().enabled = false;
            AniEvents.AniDone = false;//鼠標換置
            cursorSetting.CursorHourglassAni();

            GuestAllPutted = true;

            // ===============================
            // GUEST出現裂痕音效
            audioManager.GUESTBroken();
            // ===============================
            GameObject.Find("Stone_GUEST").gameObject.GetComponent<SpriteRenderer>().DOFade(1, 1f).OnComplete(
            () =>
            {
                Destroy(GameObject.Find("GuestBoard"));
                GUESTBtn.SetActive(true);
                GUESTBrokenAni.SetActive(true);
            }
            );

        }
    }

    public void ResetGUEST(bool ToReset)
    {
        GuestReset = ToReset;
    }

    public void DestroyGUESTBoard()
    {
        if (GuestAllPutted)
        {
            guestButtonProp.SetActive(true);
            Destroy(GameObject.Find("GUEST"));
        }
    }

    public void GUESTBurstAni()
    {
        GameObject[] Fragments = new GameObject[6];

        for (int i = 0; i < 6; i++)
        {
            Fragments[i] = GameObject.Find("StoneFragments").transform.GetChild(i).gameObject;
            Fragments[i].gameObject.SetActive(true);
        }

        GameObject.Find("StoneFragments").transform.DOShakePosition(1.2f, 1, 10, 90, false, true);
        // ===============================
        // GUEST出現裂痕並分開音效
        audioManager.GUESTBrokenAndTear();
        // ===============================
        Fragments[0].transform.DOLocalMove(new Vector3(-12.87f, 8.65f, 0f), 1.2f);
        Fragments[1].transform.DOLocalMove(new Vector3(-1.2f, 9.4f, 0f), 1.2f);
        Fragments[2].transform.DOLocalMove(new Vector3(11.39f, 8.65f, 0f), 1.2f);
        Fragments[3].transform.DOLocalMove(new Vector3(-12.87f, -9.32f, 0f), 1.2f);
        Fragments[4].transform.DOLocalMove(new Vector3(-1.47f, -9.32f, 0f), 1.2f);
        Fragments[5].transform.DOLocalMove(new Vector3(11.39f, -9.32f, 0f), 1.2f).OnComplete(() =>
        {
            Destroy(GameObject.Find("Stone_GUEST"));

            AniEvents.AniDone = true;//鼠標換置
            cursorSetting.StopCursorHourglassAni();
            BtnGuestZoominExit.GetComponent<EventTrigger>().enabled = true;
            // 放上五個字母並且爆炸後，觸發文本
            dialogueManager.ShowNextDialogue("complete_guest", false);
        });
    }

    #region 按下訪客登入按鈕

    public GameObject CanvasErrorWindow;

    public GameObject CH2Text;
    public void ClickGUESTLoginAni()
    {
        CanvasErrorWindow.SetActive(true);
    }

    public IEnumerator SceneChangeToCH2()
    {
        CanvasErrorWindow.transform.Find("BlackLayer").gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        CanvasErrorWindow.transform.Find("BlackLayer").GetComponent<Image>().DOFade(1, 2f).OnComplete(
            () =>
            {
                // 把藍骨頭關掉
                CanvasErrorWindow.transform.Find("BlueLayer").gameObject.SetActive(false);
                // 顯示文字
                CH2Text.SetActive(true);
            }
        );
    }


    public void SceneToCh2()
    {

    }

    #endregion


    #endregion



    #endregion

    #region 記事本
    public void ClickAccAndPassObject(bool isAcc)
    {
        if (isAcc)
        {
            // 處理點擊記事本中的Acc物件
            AccObject.SetActive(true);
            AccUIBtn.SetActive(false);
            AccObject.transform.DOLocalMoveY(-4.65f, 2f).SetEase(Ease.OutBounce).SetId<Tween>(AccObject.name);
            AccObject.GetComponent<ItemEventManager>().PlayerGet = true;
            AccObject.transform.parent = GameObject.Find("Tools").transform;
        }
        else
        {
            // 處理點擊記事本中的Pass物件
            PassObject.SetActive(true);
            PassUIBtn.SetActive(false);
            PassObject.transform.DOLocalMoveY(-4.65f, 2f).SetEase(Ease.OutBounce).SetId<Tween>(PassObject.name);
            PassObject.GetComponent<ItemEventManager>().PlayerGet = true;
            PassObject.transform.parent = GameObject.Find("Tools").transform;
        }
    }
    #endregion

    #region Myth安裝畫面

    public void ShowMsiOpenWindow()
    {
        BtnsCanvas.SetActive(false);
        CleanBlue();
        MsiOpenWindow.SetActive(true);
        StartCoroutine(ShowMsiAnim());
    }
    IEnumerator ShowMsiAnim()
    {
        yield return new WaitForSeconds(1f);
        MsiOpenText.text = "0%";
        for (var i = 1; i <= 100; i++)
        {
            MsiOpenText.text = i + "%";
            yield return new WaitForSeconds(0.02f);//0.02f
        }
        yield return new WaitForSeconds(1.2f);//1.2f
        MsiOpenWindow.SetActive(false);
        yield return new WaitForSeconds(0.5f);//0.5f
        ShowMythSetupWindow();
    }

    void ShowMythSetupWindow()
    {
        MythSetupInstall.GetComponent<EventTrigger>().enabled = false;
        MythSetupWindow.SetActive(true);
    }

    public void ShowMythSetupWindow(bool toShow)
    {
        MythSetupWindow.SetActive(toShow);
    }

    public void ClickMythStepClose()
    {
        // ============================
        // 播放點擊myth安裝下墜按鈕的音效
        audioManager.ClickMythSetupCancelOrX();
        // ============================
        MythStepBtnClose.transform.DOLocalMoveY(-700f, 0.7f).SetEase(Ease.InBack);
    }

    public void ClickMythInstallingClose()
    {
        MythInstallingBtnClose.GetComponent<Button>().enabled = false;
        // ============================
        // 播放點擊myth安裝下墜按鈕的音效
        audioManager.ClickMythSetupCancelOrX();
        // ============================
        MythInstallingBtnClose.transform.DOLocalMoveY(-700f, 0.7f).SetEase(Ease.InBack);
    }

    // === 警告視窗 ===
    public void ShowMythDownloadWaringWindow()
    {
        // ============================
        // 播放點擊下一步或結束音效
        audioManager.ClickMythSetupNextOrFinish();
        // ============================
        // 先關閉Myth安裝按鈕
        MythSetupInstall.GetComponent<Button>().enabled = false;
        // 產生警告視窗
        StartCoroutine(InstantiateWarningWin());
    }
    IEnumerator InstantiateWarningWin()
    {
        for (var i = 0; i < 5; i++)
        {
            // ============================
            // 警告視窗彈出
            audioManager.ShowMythSetupWarning();
            // ============================
            // 產生警告視窗
            GameObject newObj;
            newObj = Instantiate(MythDownloadWarningWindow, MythInstallingCanvas.transform);
            float posx = Random.Range(-494, 494);
            float posy = Random.Range(-214, 315);
            newObj.transform.localPosition = new Vector3(posx, posy, 0);

            // 動態新增eventtrigger
            EventTrigger trigger = newObj.transform.GetChild(1).GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { audioManager.BtnHover(); });
            trigger.triggers.Add(entry);

            yield return new WaitForSeconds(0.3f);
            newObj.transform.DOShakePosition(1f, 30f, 40, 10);
            foreach (Transform child in newObj.transform)
            {
                // 處理hover事件

                // 處理點擊事件
                child.GetComponent<Button>().onClick.AddListener(() =>
                {
                    DestroyMythDownloadWarningWindow(newObj);
                });
            }
        }
    }

    void DestroyMythDownloadWarningWindow(GameObject CloseBtn)
    {
        // ============================
        // 播放點擊下一步或結束音效
        audioManager.CloseWindow();
        // ============================
        WarningWinCount--;  // 每關閉一個警告視窗就減一
        // 刪除安裝警告視窗 
        Destroy(CloseBtn);
        // 判斷是否全部都關閉
        if (WarningWinCount == 0)
        {
            MythSetupInstall.GetComponent<Button>().enabled = true;
        }
    }

    public void MythSetupCancelBtnFall()
    {
        if (GameManager.stage < 8)
        {
            // ============================
            // 播放點擊myth安裝下墜按鈕的音效
            audioManager.ClickMythSetupCancelOrX();
            // ============================
            MythSetupCancel.transform.DOLocalMoveY(-700f, 0.7f).SetEase(Ease.InBack);
        }
        else if (GameManager.stage == 8 && cancelIsEntered)
        {
            MythInstallingCancel.GetComponent<Button>().enabled = false;
            // ============================
            // 播放點擊myth安裝下墜按鈕的音效
            audioManager.ClickMythSetupCancelOrX();
            // ============================
            MythInstallingCancel.transform.DOLocalMoveY(-700f, 0.7f).SetEase(Ease.InBack);
            cancelIsEntered = false;
        }

    }
    void MythSetupInstallBtnShow(bool toInteract)
    {
        if (toInteract)
        {
            MythSetupInstall.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            MythSetupInstall.GetComponent<Image>().color = new Color32(200, 200, 200, 56);
        }
        MythSetupInstall.GetComponent<EventTrigger>().enabled = toInteract;
        MythSetupInstall.GetComponent<Button>().enabled = toInteract;
    }
    public void MythSetupSelectAgree(bool toAgree)
    {
        // ============================
        // 播放點擊同意或不同意音效
        audioManager.ClickMythSetupAgreeOrDisagree();
        // ============================
        if (!MythSetupBlackDot.activeSelf)
        {
            MythSetupBlackDot.SetActive(true);
        }
        if (toAgree)
        {
            // 點擊 "同意"
            MythSetupBlackDot.transform.localPosition = new Vector3(-453.07f, -213.98f, 0);
            // 安裝按鈕顯示
            MythSetupInstallBtnShow(true);
        }
        else
        {
            // 關閉不同意按鈕
            MythSetupDisagree.SetActive(false);
            // 點擊 "不同意"
            MythSetupBlackDot.transform.localPosition = new Vector3(-453.07f, -259.62f, 0);
            MythSetupInstallBtnShow(false);
            StartCoroutine(DotWaitForSecond());
        }
    }
    IEnumerator DotWaitForSecond()
    {

        yield return new WaitForSeconds(0.5f);
        // ============================
        // 播放黑點點上升音效
        audioManager.ClickMythSetupDisagreeDotUp();
        // ============================

        MythSetupBlackDot.transform.DOLocalMoveY(-213.98f, 0.5f).OnComplete(() =>
        {
            MythSetupDisagree.SetActive(true);
            // 安裝按鈕顯示
            MythSetupInstallBtnShow(true);
        });
    }

    public void ShowInstallingWindow()
    {
        // ============================
        // 播放點擊下一步或結束音效
        audioManager.ClickMythSetupNextOrFinish();
        // ============================
        MythInstallingWindow.SetActive(true);
        MythSetupWindow.SetActive(false);
        StartCoroutine(DoingInstall());
    }
    IEnumerator DoingInstall()
    {
        audioManager.MythSetupLoading();
        string[] SetupText = { "\n載入項目：“Myth主程式”", "\n載入項目：“木馬程式”", "\n無法建立解除安裝程式", "\n建立捷徑：“Desktop / Myth games store”", "\n入侵完成" };
        for (var i = 1; i <= 50; i++)    // i <= 50
        {
            SetupProgressBar.fillAmount -= 0.02f;   // 0.02
            if (i % 10 == 0)
            {
                // 新增一行
                SetupProgressText.text = SetupProgressText.text + SetupText[i / 10 - 1];
            }
            yield return new WaitForSeconds(0.2f);  //0.2f
        }
        // 安裝完成停止音效
        audioManager.DestroyLoopAudio();
        // 安裝完成開啟完成畫面
        MythSetupFinishWindow.SetActive(true);
        MythInstallingWindow.SetActive(false);

        // 安裝完成後顯示MythIcon
        ShowMythIcon();
        AntiAlert.SetActive(true);//將防毒軟體切換成目前正受到威脅
        AntiSleep.SetActive(false);
    }

    void ShowMythIcon()//安裝完成顯示MythIcon
    {
        MythIcon.SetActive(true);
        MythIconBtn.SetActive(true);
    }
    public void CloseSetupWindowAndOpenMyth()
    {
        // ============================
        // 播放點擊下一步或結束音效
        audioManager.ClickMythSetupNextOrFinish();
        // ============================
        BtnsCanvas.SetActive(true);//將桌面IconBtnCanvas打開 
        Destroy(MythSetupFinishWindow.transform.parent.gameObject);    // 把整個安裝畫面canvas刪掉
        MythSetupFinishWindow.SetActive(false);
    }
    #endregion

    #region  打開視窗動畫
    public void WindowOpenAni(GameObject WindowToOpen)
    {
        WindowToOpen.transform.localPosition = new Vector3(0, -5.25f, 0f);

        WindowToOpen.transform.localScale = new Vector3(0, 0, 0);
        WindowToOpen.transform.DOScale(new Vector3(1, 1f, 1f), 0.5f);

        if (GameManager.stage == 1 && WindowToOpen.name == "Browser_Frontside")
        {
            WindowToOpen.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0.5f).OnComplete(() =>
            {
                Meteorite.SetActive(true);
            });
        }
        else
        {
            WindowToOpen.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0.5f);
        }
    }
    public void AntivirusOpenAni(GameObject winToOpen)
    {
        //winToOpen.transform.localPosition = new Vector3(0, -504f, 0);
        //winToOpen.transform.localScale = new Vector3(0, 0, 0);
        //winToOpen.transform.DOScale(new Vector3(0.24f, 0.24f, 1f), 0.5f);
        //winToOpen.transform.DOLocalMove(new Vector3(0f, 2f, 0f), 0.5f);
    }
    public void ScrollViewOpenAni(GameObject winToOpen)
    {
        //winToOpen.transform.localPosition = new Vector3(0, -702f, 0);
        //winToOpen.transform.localScale = new Vector3(0, 0, 0);
        //winToOpen.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        //winToOpen.transform.DOLocalMove(new Vector3(0f, -113.5f, 0f), 0.5f);
    }
    #endregion

    #region 背面背包
    public void Bag_Appear()
    {
        ShowBag = !ShowBag;
        if (ShowBag)//true打開
        {
            // =================================
            // 顯示包包音效
            audioManager.ShowBackBar();
            // =================================
            // BackBarTrigger.transform.DOLocalMoveY(-4.35f, 0.5f, false);
            Bar_Back.transform.DOLocalMoveY(-4.35f, 0.5f, false);
            GameObject.Find("BtnShowBag").GetComponent<SpriteRenderer>().sprite = UpandDown[1];
        }
        else//false關掉
        {
            // =================================
            // 隱藏包包音效
            audioManager.HideBackBar();
            // =================================
            Bag_Disappear();
        }
        //ShowBag = !ShowBag;

    }
    public void Bag_Disappear()
    {
        // BackBarTrigger.transform.DOLocalMoveY(-5.7f, 0.5f, false);
        Bar_Back.transform.DOLocalMoveY(-5.7f, 0.5f, false);
        GameObject.Find("BtnShowBag").GetComponent<SpriteRenderer>().sprite = UpandDown[0];
    }


    #endregion

    #region 防毒軟體
    public void ClickToViewStatus()
    {
        AnitvirusFrame.SetActive(true);
        ScrollCanvas.SetActive(false);
        AntiTutorialCanvas.SetActive(false);
        AntivirusBtnImage.GetComponent<Image>().sprite = AntivirusBtnSprite[0];
    }
    public void ClickToViewHistory()
    {
        AnitvirusFrame.SetActive(false);
        ScrollCanvas.SetActive(true);
        AntiTutorialCanvas.SetActive(false);
        AntivirusBtnImage.GetComponent<Image>().sprite = AntivirusBtnSprite[1];
    }
    public void ClickToViewTutorial()
    {
        AnitvirusFrame.SetActive(false);
        ScrollCanvas.SetActive(false);
        AntiTutorialCanvas.SetActive(true);
        AntivirusBtnImage.GetComponent<Image>().sprite = AntivirusBtnSprite[2];
    }
    // =======================
    // 教學
    public void OpenSelector()
    {
        // disabled 上面按鈕
        BtnHistory.GetComponent<Button>().enabled = false;
        // ======================================
        TutorialIconSelector.SetActive(true);
        TutorialIconSelector.GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() =>
        {
            TutorialIconHover.SetActive(true);
        });
    }
    public void ClickIcon(int index)
    {
        Sprite img = TutorialiconArr[index];
        // 替換下框的icon
        TutorialiconShower.GetComponent<Image>().sprite = img;

        // 把hover圖片移除(變回預設圖片)
        // TutorialIconHover.GetComponent<Image>().sprite = TutorialHoverIcon[7];

        // 更換左右的圖片
        TutorialLeftPrepare.GetComponent<Image>().sprite = TutorialLeftArr[index];
        TutorialRightPrepare.GetComponent<Image>().sprite = TutorialRightArr[index];

        // 圖片往右移動
        TutorialLeftContainer.transform.DOLocalMove(new Vector3(140f, 0, 0), 0.5f, false).OnComplete(() =>
        {
            TutorialLeftShower.GetComponent<Image>().sprite = TutorialLeftArr[index];
            TutorialLeftContainer.transform.localPosition = new Vector3(-140f, 0, 0);
        });
        TutorialRightContainer.transform.DOLocalMove(new Vector3(140f, 0, 0), 0.5f, false).OnComplete(() =>
        {
            TutorialRightShower.GetComponent<Image>().sprite = TutorialRightArr[index];
            TutorialRightContainer.transform.localPosition = new Vector3(-140f, 0, 0);
        });

        // 關閉selector
        TutorialIconHover.SetActive(false);
        TutorialIconSelector.SetActive(false);
        // enabled 上面按鈕
        BtnHistory.GetComponent<Button>().enabled = true;
        //TutorialIconSelector.GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(() =>
        //{

        //});
    }
    public void IconPointerEnter(int index)
    {
        TutorialIconHover.GetComponent<Image>().sprite = TutorialHoverIcon[index];
    }
    public void IconPointerExit()
    {
        TutorialIconHover.GetComponent<Image>().sprite = TutorialHoverIcon[7];
    }
    #endregion

    #region 工具列 

    void ChangeMessageBox()//更改連線提示框
    {
        MessageBox = GameObject.Find("MessageBox_connect_interent");
        GameObject.Find("MessageBox_disconnect_interent").GetComponent<SpriteRenderer>().DOFade(0, 1);
    }

    public void ShowMessageBox()
    {
        AntiMessageBox.transform.GetComponent<SpriteRenderer>().sortingOrder = 0;
        MessageBox.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
        MessageBox.GetComponent<SpriteRenderer>().DOFade(1, 1);
        StartCoroutine(WaitFor(3f, MessageBox));
    }

    void ChangeAntiMessageBox()//更改防毒提示框
    {
        AntiMessageBox = GameObject.Find("AntiMessageBox_Alert");
        GameObject.Find("AntiMessageBox_Sleep").GetComponent<SpriteRenderer>().DOFade(0, 1);
    }

    public void ShowAntiMessageBox()
    {
        AntiMessageBox.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
        MessageBox.transform.GetComponent<SpriteRenderer>().sortingOrder = 0;
        AntiMessageBox.GetComponent<SpriteRenderer>().DOFade(1, 1);
        StartCoroutine(WaitFor(3f, AntiMessageBox));
    }

    IEnumerator WaitFor(float Seconds, GameObject Messagebox)
    {
        yield return new WaitForSeconds(Seconds);
        Messagebox.GetComponent<SpriteRenderer>().DOFade(0, 1);
    }


    #endregion

    #region  Portal動畫控制

    public void PortalShining()
    {
        if (AniEvents.AniDone)
        {
            PortalAni.SetBool("Shining", true);
        }
    }
    public void PortalUnshine()
    {
        if (AniEvents.AniDone)
        {
            PortalAni.SetBool("Shining", false);
        }
    }


    #endregion

    #region 狗相關
    public void EnableDogClick()
    {
        Dog.GetComponent<BoxCollider2D>().enabled = true;
    }
    #endregion
}
