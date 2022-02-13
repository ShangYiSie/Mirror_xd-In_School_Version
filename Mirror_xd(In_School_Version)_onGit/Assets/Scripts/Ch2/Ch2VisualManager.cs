using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch2VisualManager : MonoBehaviour
{
    Ch2Usermanager ch2Usermanager;
    Ch2DickimonSetState DickimonSetState;
    AudioManager audioManager;
    Ch2DialogueManager ch2DialogueManager;
    Game2Manager game2Manager;
    Game3Manager game3Manager;
    BGMManager bgmManager;
    Ch2GameManager ch2GameManager;
    Ch2CursorSetting cursorSetting;

    #region  攝影機

    public GameObject Camera;

    #endregion

    #region  工具列換圖

    [Header("原始圖")]
    public Sprite[] source_sprite;

    [Header("正面bar的圖")]
    public Sprite[] bar_sprite;

    [Header("背面Bar的圖")]
    public Sprite[] backbar_sprite;

    public GameObject AntiMessageBox;
    public GameObject MessageBox;

    #endregion

    #region 鏡子與立繪的開關
    [Header("破鏡子按鈕")]
    public Button ToBackBtn;

    #endregion

    #region 正反面物件

    [Header("MythGame正面物件")]
    public GameObject MythGameFront;

    [Header("MythGame背面物件")]
    public GameObject MythGameBack;

    #endregion

    #region 背面背包
    public GameObject ShowBagBtn;
    public bool ShowBag;
    public GameObject Bar_Back;
    public Sprite[] UpandDown = new Sprite[2];
    #endregion

    #region 背面場景
    [Header("名牌框")]
    public Collider2D[] BrandFrame;
    [Header("名牌")]
    public GameObject XiaoMing;
    public GameObject BricksKiller;
    public GameObject Plumber;
    [Header("兩個鎖")]
    public GameObject BricksKiller_Locked;
    public GameObject Plumber_Locked;

    [Header("三個螢幕Ani")]
    public Animator Screen1;
    public Animator Screen2;
    public Animator Screen3;

    [Header("背面主角")]
    public GameObject Player;
    [Header("背面背包")]
    public GameObject ToolsBar_Back;
    [Header("背面通道")]
    public GameObject Portal;

    [Header("反面道具要忽略的Collider")]
    public Collider2D[] BackitemCollider = new Collider2D[6];
    [Header("正面道具要忽略的Collider")]
    public Collider2D[] FrontitemCollider = new Collider2D[3];
    #endregion

    public GameObject Tools;


    #region 正面遊戲場景

    [Header("DickimonGameRoot")]

    public GameObject DickimonGameRoot;

    bool needtoAutoOpen = false;

    [Header("正面的Bar")]
    public GameObject ToolsBar_Front;

    [Header("迪可夢遊戲角色")]
    public GameObject[] Game1Character = new GameObject[3];

    [Header("磚塊殺手遊戲角色")]
    public GameObject[] Game2Character = new GameObject[3];

    [Header("大金剛遊戲角色")]
    public GameObject[] Game3Character = new GameObject[3];
    [Header("正面警告視窗")]
    public GameObject WarningFrame;

    public GameObject WFrame;
    public GameObject PixelFire;

    public GameObject PixelFireRoot;

    [Header("磚塊殺手對話框")]
    public GameObject KillerDialog;
    [Header("迪可丘正面三個角色被木馬打死換圖")]
    public Sprite[] XiaoMingDie = new Sprite[4];
    public Sprite[] KillerDie = new Sprite[4];
    public Sprite[] PlumberDie = new Sprite[4];

    #endregion

    #region 正面遊戲開關

    [Header("三個Btn的Canvas")]
    public GameObject GameBtn;

    [Header("三個按鈕")]
    public GameObject Game1Btn;
    public GameObject Game2Btn;
    public GameObject Game3Btn;

    [Header("開始遊戲的圖")]
    public Sprite StartImg;

    [Header("正面遊戲視窗")]
    public GameObject Game1Frame;
    public GameObject Game2Frame;
    public GameObject Game3Frame;

    [Header("正面遊戲海報")]
    public BoxCollider2D[] GamePoster;

    [Header("防毒軟體視窗")]
    public GameObject AntiFront;
    public GameObject AntiBtn;
    public GameObject AnitvirusFrame;
    public GameObject ScrollCanvas;
    public GameObject AntivirusBtnImage;
    public Sprite[] AntivirusBtnSprite;
    public GameObject TutorialCanvas;

    [Header("暫停介面")]
    public GameObject PauseObj;
    public GameObject PauseMain;
    public GameObject PauseSetting;
    public GameObject PauseTitleText;
    public GameObject ScreenSaver;
    public GameObject CountDownText;

    [Header("目前各關的角色")]
    public string DickimonChar = "小明";
    public string BricksKillerChar = "磚塊殺手";
    public string PlumberChar = "水管工";

    public GameObject DialogueFront;

    [Header("第一次")]
    public bool OpenDickimonFirst_Ming = true;
    public bool OpenDickimonFirst_Plumber = true;
    public bool OpenDickimonFirst_Bricks = true;
    public bool NotPutBrandFirst = true;
    public bool OpenBreakSafeFirst_Bricks = true;
    public bool OpenBreakSafeFirst_Plumber = true;
    public bool OpenRaccoonFirst_Bricks = true;
    public bool OpenBattleHorseFirst_Bricks = true;
    public bool OpenBattleHorseFirst_Plumber = true;

    [Header("木馬死亡")]
    public bool HorseDie = false;

    public GameObject UsaHorse;
    public GameObject PlumberUsaHorse;
    public GameObject Dog;

    [Header("紀錄")]
    public int characterAlive = 3;  // 還存活的角色數量


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ch2Usermanager = GameObject.Find("UserManager").GetComponent<Ch2Usermanager>();
        DickimonSetState = GameObject.Find("Myth_Frontside").transform.Find("DickimonGame").gameObject.GetComponent<Ch2DickimonSetState>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        game2Manager = Game2Frame.GetComponent<Game2Manager>();
        game3Manager = Game3Frame.GetComponent<Game3Manager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        ch2GameManager = GameObject.Find("GameManager").GetComponent<Ch2GameManager>();

        //先關掉Game2和Game3的button
        /*Game2Btn.GetComponent<Button>().enabled = false;
        Game3Btn.GetComponent<Button>().enabled = false;*/


        // ScreenState();
        // GameFrontCharactorSwitch();
        if (SceneManagement.isCh1ToCh2)
        {
            SceneManagement.isCh1ToCh2 = false;
            Ch2TransitionAni();
        }
        else if (SceneManagement.isMainToCh2)
        {
            SceneManagement.isMainToCh2 = false;
            MainToCh2();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (MythGameBack.activeSelf && Input.GetMouseButtonDown(0))
        {
            ch2Usermanager.ToMoveCharacter();
        }
        #region 背面
        if ((int)Ch2GameManager.nowCH2State % 2 != 0)//如果在背面
        {
            if (ShowBag && Ch2CursorSetting.CursorNotTrigger && Ch2CursorFX.PlayerMovable)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Bag_Disappear();
                    ShowBag = !ShowBag;
                }
            }
        }
        #endregion

        // if (Input.GetKeyUp(KeyCode.A))
        // {
        //     openTransition();
        // }

    }

    #region 暫停
    public void OpenPause()
    {
        audioManager.ClickPause();
        PauseObj.SetActive(true);
        Time.timeScale = 0; // 暫停遊戲
    }
    public void ClosePause()
    {
        PauseObj.SetActive(false);
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

    #region 道具bar換圖
    int ConvertNameToNum(string name)
    {
        switch (name)
        {
            case "XiaoMing":
                return 0;
            case "BricksKiller":
                return 1;
            case "Plumber":
                return 2;
            case "Coin":
                return 3;
            case "Key":
                return 4;
                // case "GUESTBtnProp":
                //     return 5;
                // case "Meteorite":
                //     return 6;
                // case "Bomb":
                //     return 7;
                // case "note_password":
                //     return 8;
                // case "S":
                //     return 9;
                // case "Shovel":
                //     return 10;
                // case "T":
                //     return 11;
                // case "U":
                //     return 12;
                // case "ExitBTN":
                //     return 13;
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

    }
    public void ToolsExitBar(GameObject obj, Vector3 scale, Vector2 colliderSize)
    {
        int num = ConvertNameToNum(obj.name);
        obj.GetComponent<SpriteRenderer>().sprite = source_sprite[num];

        Vector2 v = new Vector2((float)source_sprite[num].texture.width / 100, (float)source_sprite[num].texture.height / 100);
        // 根據要轉換的圖片大小更改gameobject的width & height & scale
        obj.GetComponent<RectTransform>().sizeDelta = v;
        obj.GetComponent<RectTransform>().localScale = scale;
        // 根據gameobject的大小改變collider的大小
        obj.GetComponent<BoxCollider2D>().size = colliderSize;
        obj.GetComponent<Collider2D>().offset = new Vector2(0, 0);

    }
    #endregion

    #region 轉場

    #region 進場特效

    [Header("進場黑幕")]
    public GameObject BlackLayer;
    public void Ch2TransitionAni()
    {
        GameObject.Find("Transitions").transform.GetChild(1).gameObject.SetActive(false);
        BlackLayer.transform.parent.gameObject.SetActive(true);
        BlackLayer.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(() =>
        {
            // 開機音效
            audioManager.StartComputerAudio();
            // 文本
            ch2DialogueManager.ShowNextDialogue("第一次進入正面", true);
            // ========
            Destroy(BlackLayer.transform.parent.gameObject);
        });
    }
    #endregion
    public void MainToCh2()
    {
        GameObject.Find("Transitions").transform.GetChild(1).gameObject.SetActive(true);
        GameObject Black = GameObject.Find("Transitions").transform.GetChild(1).GetChild(0).gameObject;
        Black.GetComponent<Image>().DOFade(1, 0.3f).OnComplete(() =>
        {
            GameObject.Find("Transitions").transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        });
    }

    public void TurnToCh3FX()
    {
        Camera.GetComponent<ShaderEffect_Unsync>().enabled = true;
        Camera.GetComponent<ShaderEffect_CorruptedVram>().enabled = true;
    }

    public void openTransition()
    {
        //Demo
        GameObject.Find("Myth_Frontside").SetActive(false);
        GameObject.Find("ToolsBar").SetActive(false);
        //Demo


        GameObject.Find("Transitions").transform.GetChild(0).gameObject.SetActive(true);
        GameObject Black = GameObject.Find("Transitions").transform.GetChild(0).GetChild(0).gameObject;
        Black.GetComponent<Image>().DOFade(1, 1.2f).OnComplete(() =>
        {
            GameObject.Find("Transitions").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        });
    }
    #endregion

    #region 正反切換
    public void ChangeToBack()
    {
        if (Ch2AniEvents.AniDone)
        {
            //set playermovable
            Ch2CursorFX.PlayerMovable = true;

            ch2Usermanager.mouseWorldPosition = ch2Usermanager.Character.transform.position;
            ch2Usermanager.walk = true;

            cursorSetting.ChangeState("Back_Normal");
            if (Ch2GameManager.nowChapter == Ch2GameManager.Chapter.CH2)
            {
                switch (Ch2GameManager.nowCH2State)
                {
                    case Ch2GameManager.CH2State.MythGameFront:
                        Chapter2ChangeToBack(MythGameBack, MythGameFront);
                        Ch2GameManager.nowCH2State = Ch2GameManager.CH2State.MythGameBack;
                        ScreenState();
                        break;
                }

                //Dickimon場景設定
                if (!HorseDie)
                {
                    if (DickimonSetState.XiaoDone)
                    {
                        DickimonSetState.closeDickimonWindow();
                    }
                    else if (DickimonSetState.nowEnemy == "無情浣熊")
                    {
                        DickimonSetState.closeDickimonWindow();
                    }
                    else if (DickimonGameRoot.activeSelf)
                    {
                        DickimonSetState.closeDickimonWindow();
                        needtoAutoOpen = true;
                    }
                }
                //自動關閉WARNINGFRAME
                if (WarningFrame.activeSelf)
                {
                    WarningFrame.SetActive(false);
                }

            }
        }
    }
    public void Chapter2ChangeToBack(GameObject ToOpen, GameObject ToClose)
    {
        // 播放bgm
        bgmManager.StopCH2BGM();
        bgmManager.PlayCH2MythBackBgm();
        // 桌面正面關閉
        MythGameFront.SetActive(false);
        // 開啟反面
        ToOpen.SetActive(true);
        // 關閉正面
        ToClose.SetActive(false);
        // ToolBar 處理
        ToolsBar_Front.SetActive(false);
        ToolsBar_Back.SetActive(true);
        // 開啟角色
        Player.SetActive(true);
        //Player.transform.localPosition = new Vector3(-5.54f, -3.01f, -0.06f);
        // 開啟Dog
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
        Tools.transform.SetParent(GameObject.Find("Tools_Back").gameObject.transform);
        Tools.transform.localPosition = new Vector3(0.6f, -5.65f, 0);
        // 關閉正面的dialogue canvas
        DialogueFront.SetActive(false);
    }
    public void ScreenState()//背面螢幕狀態
    {
        XiaoMingScreen();
        KillerScreen();
        PlumberScreen();
    }
    void XiaoMingScreen()
    {
        switch (XiaoMing.transform.parent.name)//小明的螢幕
        {
            case "Game1_Frame":
                if (DickimonSetState.XiaoDone)
                {
                    Screen1.SetBool("Game1_Garbled", true);
                }
                else
                {
                    Screen1.SetBool("Game1_open", true);
                }
                break;
            case "Game2_Frame":
                if (DickimonSetState.XiaoDone)
                {
                    Screen2.SetBool("Game1_Garbled", true);
                }
                else
                {
                    Screen2.SetBool("Game1_open", true);
                }
                break;
            case "Game3_Frame":
                if (DickimonSetState.XiaoDone)
                {
                    Screen3.SetBool("Game1_Garbled", true);
                }
                else
                {
                    Screen3.SetBool("Game1_open", true);
                }
                break;
            default:
                audioManager.CloseTV_Back();
                Screen1.SetBool("Game1_Garbled", false);
                Screen2.SetBool("Game1_Garbled", false);
                Screen3.SetBool("Game1_Garbled", false);
                Screen1.SetBool("Game1_open", false);
                Screen2.SetBool("Game1_open", false);
                Screen3.SetBool("Game1_open", false);
                break;
        }
    }
    void KillerScreen()
    {
        switch (BricksKiller.transform.parent.name)//殺手的螢幕
        {
            case "Game1_Frame":
                if (DickimonSetState.KillerDone)
                {
                    Screen1.SetBool("Game2_Garbled", true);
                }
                else
                {
                    Screen1.SetBool("Game2_open", true);
                }
                break;
            case "Game2_Frame":
                if (DickimonSetState.KillerDone)
                {
                    Screen2.SetBool("Game2_Garbled", true);
                }
                else
                {
                    Screen2.SetBool("Game2_open", true);
                }
                break;
            case "Game3_Frame":
                if (DickimonSetState.KillerDone)
                {
                    Screen3.SetBool("Game2_Garbled", true);
                }
                else
                {
                    Screen3.SetBool("Game2_open", true);
                }
                break;
            default:
                audioManager.CloseTV_Back();
                Screen1.SetBool("Game2_Garbled", false);
                Screen2.SetBool("Game2_Garbled", false);
                Screen3.SetBool("Game2_Garbled", false);
                Screen1.SetBool("Game2_open", false);
                Screen2.SetBool("Game2_open", false);
                Screen3.SetBool("Game2_open", false);
                /*if (DickimonSetState.KillerDone)
                {
                    Screen1.SetBool("Game2_Garbled", false);
                    Screen2.SetBool("Game2_Garbled", false);
                    Screen3.SetBool("Game2_Garbled", false);
                }
                else
                {
                    Screen1.SetBool("Game2_open", false);
                    Screen2.SetBool("Game2_open", false);
                    Screen3.SetBool("Game2_open", false);
                }*/
                break;
        }
    }
    void PlumberScreen()
    {
        switch (Plumber.transform.parent.name)//水電工的螢幕
        {
            case "Game1_Frame":
                if (DickimonSetState.PlumberDone)
                {
                    Screen1.SetBool("Game3_Garbled", true);
                }
                else
                {
                    Screen1.SetBool("Game3_open", true);
                }
                break;
            case "Game2_Frame":
                if (DickimonSetState.PlumberDone)
                {
                    Screen2.SetBool("Game3_Garbled", true);
                }
                else
                {
                    Screen2.SetBool("Game3_open", true);
                }
                break;
            case "Game3_Frame":
                if (DickimonSetState.PlumberDone)
                {
                    Screen3.SetBool("Game3_Garbled", true);
                }
                else
                {
                    Screen3.SetBool("Game3_open", true);
                }
                break;
            default:
                audioManager.CloseTV_Back();
                Screen1.SetBool("Game3_Garbled", false);
                Screen2.SetBool("Game3_Garbled", false);
                Screen3.SetBool("Game3_Garbled", false);
                Screen1.SetBool("Game3_open", false);
                Screen2.SetBool("Game3_open", false);
                Screen3.SetBool("Game3_open", false);
                /*if (DickimonSetState.PlumberDone)
                {
                    Screen1.SetBool("Game3_Garbled", false);
                    Screen2.SetBool("Game3_Garbled", false);
                    Screen3.SetBool("Game3_Garbled", false);
                }
                else
                {
                    Screen1.SetBool("Game3_open", false);
                    Screen2.SetBool("Game3_open", false);
                    Screen3.SetBool("Game3_open", false);
                }*/
                break;
        }
    }
    // =============== 反到正 ======================

    public void ChangeToFront()
    {
        // 關閉目前的dialogue
        ch2DialogueManager.StopNowDialogue();
        // =======================
        audioManager.UpdateIsChangeToFront(true);
        if (Ch2AniEvents.AniDone)
        {
            // 停止bgm
            bgmManager.StopCH2BGM();
            // ================================
            cursorSetting.ChangeState("Front_Normal");
            ch2Usermanager.mouseWorldPosition.x = ch2Usermanager.Character.transform.position.x;
            ch2Usermanager.Character.transform.position = ch2Usermanager.Character.transform.position;
            if (Ch2GameManager.nowChapter == Ch2GameManager.Chapter.CH2)
            {

                switch (Ch2GameManager.nowCH2State)
                {
                    case Ch2GameManager.CH2State.MythGameBack:
                        Chapter2ChangeToFront(MythGameFront, MythGameBack);
                        Ch2GameManager.nowCH2State = Ch2GameManager.CH2State.MythGameFront;
                        GameFrontCharactorSwitch();
                        break;
                }

                int count = 0;
                if (!HorseDie)
                {
                    if (DickimonSetState.XiaoDone)
                    {
                        bgmManager.StopCH2BGM();
                        bgmManager.PlaySecondHorseBgm();
                        for (int i = 0; i < 3; i++)
                        {
                            if (Game1Character[i].activeSelf == false)
                            {
                                count++;
                            }
                        }
                        Game1Frame.SetActive(true);
                        if (count == 3 && !HorseDie)
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                GamePoster[i].enabled = false;
                            }
                            // =====================
                            // 觸發error彈出視窗 音效
                            audioManager.MythErrorShow_Dickimon();
                            // =====================
                            WarningFrame.SetActive(true);
                            DickimonSetState.nowCharater = "";
                            DickimonSetState.setNoCharater();
                        }
                        else
                        {
                            DickimonSetState.ToSetDickmonState(true);

                        }
                        // ========================
                        // 觸發文本
                        if (OpenBattleHorseFirst_Bricks && DickimonChar == "磚塊殺手")
                        {
                            ch2DialogueManager.ShowNextDialogue("第一次換磚塊殺手", true);
                            OpenBattleHorseFirst_Bricks = false;
                        }
                        else if (OpenBattleHorseFirst_Plumber && DickimonChar == "水電工")
                        {
                            ch2DialogueManager.ShowNextDialogue("第一次換水電工", true);
                            OpenBattleHorseFirst_Plumber = false;
                        }
                    }
                    else if (DickimonSetState.nowEnemy == "無情浣熊")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (Game1Character[i].activeSelf == false)
                            {
                                count++;
                            }
                        }
                        Game1Frame.SetActive(true);
                        if (count == 3 && !HorseDie)
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                GamePoster[i].enabled = false;
                            }
                            // =====================
                            // 觸發error彈出視窗 音效
                            audioManager.MythErrorShow_Dickimon();
                            // =====================
                            WarningFrame.SetActive(true);
                            DickimonSetState.nowCharater = "";
                            DickimonSetState.setNoCharater();
                        }
                        else
                        {
                            DickimonSetState.ToSetDickmonState(true);
                        }
                    }
                    else if (needtoAutoOpen)
                    {
                        needtoAutoOpen = false;
                        for (int i = 0; i < 3; i++)
                        {
                            if (Game1Character[i].activeSelf == false)
                            {
                                count++;
                            }
                        }
                        Game1Frame.SetActive(true);
                        if (count == 3 && !HorseDie)
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                GamePoster[i].enabled = false;
                            }
                            // =====================
                            // 觸發error彈出視窗 音效
                            audioManager.MythErrorShow_Dickimon();
                            // =====================
                            WarningFrame.SetActive(true);
                            DickimonSetState.nowCharater = "";
                            DickimonSetState.setNoCharater();
                        }
                        else
                        {
                            DickimonSetState.ToSetDickmonState(true);
                        }
                    }
                }
            }
            // Game2 Game3場景設定
            if (Ch2GameManager.nowGames == Ch2GameManager.Games.BreakoutSafe)
            {
                CloseGame();
                Ch2GameManager.nowGames = Ch2GameManager.Games.BreakoutSafe;
                OpenGame();
                game2Manager.CheckCharactor();
            }
            else if (Ch2GameManager.nowGames == Ch2GameManager.Games.RuthlessRaccoon)
            {
                CloseGame();
                Ch2GameManager.nowGames = Ch2GameManager.Games.RuthlessRaccoon;
                OpenGame();
                game3Manager.CheckCharactor();
            }
            else if (Ch2GameManager.nowGames == Ch2GameManager.Games.Dickimon)
            {
                if (DickimonSetState.nowEnemy == "迪可丘")
                {
                    bgmManager.PlayDickichuRaccoonBgm();
                }
                else
                {
                    if (DickimonSetState.nowEnemy != "木馬病毒" && DickimonSetState.nowEnemy != "木馬終結者")
                    {
                        bgmManager.PlayBigDickichuBgm();
                    }
                }
            }
        }
        StartCoroutine(WaitToReset());
    }
    IEnumerator WaitToReset()
    {
        yield return new WaitForSeconds(1f);
        audioManager.UpdateIsChangeToFront(false);
    }
    public void Chapter2ChangeToFront(GameObject ToOpen, GameObject ToClose)
    {
        // 如果背包是開的就關掉
        if (ShowBag)
        {
            // 關閉背包
            Bag_Appear();
        }
        // 桌面正面開啟
        MythGameFront.SetActive(true);
        // 開啟正面
        ToOpen.SetActive(true);
        // 關閉反面
        ToClose.SetActive(false);
        // ToolBar 處理
        ToolsBar_Front.SetActive(true);
        ToolsBar_Back.SetActive(false);
        // 關閉角色
        //Player.transform.localPosition = new Vector3(-5.54f, -3.518f, -0.06f);
        Player.SetActive(false);
        // 關閉Dog
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
        Tools.transform.SetParent(ToolsBar_Front.transform);
        Tools.transform.localPosition = new Vector3(-1f, -4.5f, 0);
        // ======================

    }
    public void GameFrontCharactorSwitch()
    {
        switch (XiaoMing.transform.parent.name)
        {
            case "Game1_Frame":
                if (DickimonSetState.nowEnemy == "" || DickimonSetState.nowEnemy == "迪可丘" || DickimonSetState.nowEnemy == "巨大迪可丘")
                {
                    DickimonChar = "小明";
                    Game1Character[0].SetActive(true);
                    Game2Character[0].SetActive(false);
                    Game3Character[0].SetActive(false);
                    DickimonSetState.nowCharater = "XiaoMing";
                }
                break;
            case "Game2_Frame":
                BricksKillerChar = "小明";
                Game1Character[0].SetActive(false);
                Game2Character[0].SetActive(true);
                Game3Character[0].SetActive(false);
                break;
            case "Game3_Frame":
                PlumberChar = "小明";

                Game1Character[0].SetActive(false);
                Game2Character[0].SetActive(false);
                Game3Character[0].SetActive(true);
                if (DickimonSetState.nowEnemy == "木馬病毒" || DickimonSetState.nowEnemy == "木馬終結者" || DickimonSetState.nowEnemy == "無情浣熊")
                {
                    DickimonChar = "小明";
                    Game1Character[0].SetActive(true);
                    Game2Character[0].SetActive(false);
                    Game3Character[0].SetActive(false);
                    DickimonSetState.nowCharater = "XiaoMing";
                }
                break;
            case "Tools":
                Game1Character[0].SetActive(false);
                Game2Character[0].SetActive(false);
                Game3Character[0].SetActive(false);
                break;
        }
        switch (BricksKiller.transform.parent.name)
        {
            case "Game1_Frame":
                if (DickimonSetState.nowEnemy == "" || DickimonSetState.nowEnemy == "迪可丘" || DickimonSetState.nowEnemy == "巨大迪可丘")
                {
                    DickimonChar = "磚塊殺手";
                    Game1Character[1].SetActive(true);
                    Game2Character[1].SetActive(false);
                    Game3Character[1].SetActive(false);
                    DickimonSetState.nowCharater = "BricksKiller";
                }
                break;
            case "Game2_Frame":
                BricksKillerChar = "磚塊殺手";
                Game1Character[1].SetActive(false);
                Game2Character[1].SetActive(true);
                Game3Character[1].SetActive(false);
                break;
            case "Game3_Frame":
                PlumberChar = "磚塊殺手";
                Game1Character[1].SetActive(false);
                Game2Character[1].SetActive(false);
                Game3Character[1].SetActive(true);
                if (DickimonSetState.nowEnemy == "木馬病毒" || DickimonSetState.nowEnemy == "木馬終結者" || DickimonSetState.nowEnemy == "無情浣熊")
                {
                    DickimonChar = "磚塊殺手";
                    Game1Character[1].SetActive(true);
                    Game2Character[1].SetActive(false);
                    Game3Character[1].SetActive(false);
                    DickimonSetState.nowCharater = "BricksKiller";
                }
                break;
            case "Tools":
                Game1Character[1].SetActive(false);
                Game2Character[1].SetActive(false);
                Game3Character[1].SetActive(false);
                break;
        }
        switch (Plumber.transform.parent.name)
        {
            case "Game1_Frame":
                if (DickimonSetState.nowEnemy == "" || DickimonSetState.nowEnemy == "迪可丘" || DickimonSetState.nowEnemy == "巨大迪可丘")
                {
                    DickimonChar = "水電工";
                    Game1Character[2].SetActive(true);
                    Game2Character[2].SetActive(false);
                    Game3Character[2].SetActive(false);
                    DickimonSetState.nowCharater = "Plumber";
                }
                break;
            case "Game2_Frame":
                BricksKillerChar = "水電工";
                Game1Character[2].SetActive(false);
                Game2Character[2].SetActive(true);
                Game3Character[2].SetActive(false);
                break;
            case "Game3_Frame":
                PlumberChar = "水電工";
                Game1Character[2].SetActive(false);
                Game2Character[2].SetActive(false);
                Game3Character[2].SetActive(true);
                if (DickimonSetState.nowEnemy == "木馬病毒" || DickimonSetState.nowEnemy == "木馬終結者" || DickimonSetState.nowEnemy == "無情浣熊")
                {
                    DickimonChar = "水電工";
                    Game1Character[2].SetActive(true);
                    Game2Character[2].SetActive(false);
                    Game2Character[2].SetActive(false);
                    DickimonSetState.nowCharater = "Plumber";
                }
                break;
            case "Tools":
                Game1Character[2].SetActive(false);
                Game2Character[2].SetActive(false);
                Game3Character[2].SetActive(false);
                break;
        }
    }


    #endregion

    #region 開關遊戲視窗
    public void OpenGame()
    {
        //====================
        //將Postercollider關閉
        for (int i = 0; i < 3; i++)
        {
            GamePoster[i].enabled = false;
        }
        //====================

        int count = 0;//角色有沒有開啟
        if (Ch2GameManager.nowCH2State == Ch2GameManager.CH2State.MythGameFront)
        {
            switch (Ch2GameManager.nowGames)
            {
                case Ch2GameManager.Games.Dickimon:

                    GameBtn.SetActive(false);
                    for (int i = 0; i < 3; i++)
                    {
                        if (Game1Character[i].activeSelf == false)
                        {
                            count++;
                        }
                    }
                    Game1Frame.SetActive(true);
                    if (count == 3 && !HorseDie)
                    {
                        // DickmonSetState.isPlayingAni = false;
                        for (int i = 0; i < 3; i++)
                        {
                            GamePoster[i].enabled = false;
                        }
                        // =====================
                        // 觸發error彈出視窗 音效
                        audioManager.MythErrorShow_Dickimon();
                        // =====================
                        WarningFrame.SetActive(true);
                        DickimonSetState.nowCharater = "";
                        DickimonChar = "";
                        DickimonSetState.setNoCharater();
                        if (NotPutBrandFirst)
                        {
                            // 觸發文本
                            ch2DialogueManager.ShowNextDialogue("第一次沒放名牌並開啟遊戲", true);
                            NotPutBrandFirst = false;
                        }
                    }
                    else
                    {
                        DickimonSetState.isPlayingAni = false;
                        DickimonSetState.ToSetDickmonState(true);
                    }
                    // ============================================
                    // 顯示對話
                    if (OpenDickimonFirst_Ming && DickimonChar == "小明" && DickimonSetState.nowEnemy != "無情浣熊")
                    {
                        ch2DialogueManager.ShowNextDialogue("第一次打開迪可夢，角色:小明", true);
                        OpenDickimonFirst_Ming = false;
                    }
                    else if (OpenDickimonFirst_Plumber && DickimonChar == "水電工" && DickimonSetState.nowEnemy != "無情浣熊")
                    {
                        ch2DialogueManager.ShowNextDialogue("第一次打開迪可夢，角色:水電工", true);
                        OpenDickimonFirst_Plumber = false;
                    }
                    else if (OpenDickimonFirst_Bricks && DickimonChar == "磚塊殺手" && DickimonSetState.nowEnemy != "無情浣熊")
                    {
                        ch2DialogueManager.ShowNextDialogue("第一次打開迪可夢，角色:磚塊殺手", true);
                        OpenDickimonFirst_Bricks = false;
                    }
                    // ============================================
                    // 播放BGM
                    if (DickimonSetState.nowEnemy == "迪可丘" || DickimonSetState.nowEnemy == "無情浣熊")
                    {
                        bgmManager.PlayDickichuRaccoonBgm();
                    }
                    else if (DickimonSetState.nowEnemy == "巨大迪可丘")
                    {
                        bgmManager.PlayBigDickichuBgm();
                    }
                    else if (DickimonSetState.nowEnemy == "木馬病毒")
                    {
                        bgmManager.PlayFirstHorseBgm();
                    }
                    else if (DickimonSetState.nowEnemy == "木馬終結者")
                    {
                        bgmManager.PlaySecondHorseBgm();
                    }
                    // ============================================
                    break;
                case Ch2GameManager.Games.BreakoutSafe:
                    Game2Frame.SetActive(true);
                    GameBtn.SetActive(false);
                    for (int i = 0; i < 3; i++)
                    {
                        if (Game2Character[i].activeSelf == false)
                        {
                            count++;
                        }
                    }
                    if (count == 3 && !HorseDie)
                    {

                        // 如果沒有角色，把木馬動畫關掉
                        if (game2Manager.Horse_InsideAni != null)
                        {
                            game2Manager.Horse_InsideAni.enabled = false;
                        }
                        else if (game2Manager.Horse_OutSideAni != null)
                        {
                            game2Manager.Horse_OutSideAni.enabled = false;
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            GamePoster[i].enabled = false;
                        }
                        // =====================
                        // 觸發error彈出視窗 音效
                        audioManager.MythErrorShow_Dickimon();
                        // =====================
                        WarningFrame.SetActive(true);
                        if (NotPutBrandFirst)
                        {
                            // 觸發文本
                            ch2DialogueManager.ShowNextDialogue("第一次沒放名牌並開啟遊戲", true);
                            NotPutBrandFirst = false;
                        }
                    }
                    else
                    {
                        if (game2Manager.FirstOpenDialog)
                        {
                            // 如果有角色，把木馬動畫打開
                            game2Manager.Horse_OutSideAni.enabled = true;
                            game2Manager.FirstOpenDialog = !game2Manager.FirstOpenDialog;
                        }
                    }
                    // ============================================
                    // 顯示對話
                    if (UsaHorse.gameObject == null)
                    {
                        if (OpenBreakSafeFirst_Bricks && BricksKillerChar == "磚塊殺手")
                        {
                            ch2DialogueManager.ShowNextDialogue("跑完過場動畫後，第一次用磚塊殺手進入遊戲", true);
                            OpenBreakSafeFirst_Bricks = false;
                        }
                        else if (OpenBreakSafeFirst_Plumber && BricksKillerChar == "水電工")
                        {
                            ch2DialogueManager.ShowNextDialogue("跑完過場動畫後，第一次用水電工進入遊戲", true);
                            OpenBreakSafeFirst_Plumber = false;
                        }
                    }
                    // ============================================
                    // 播放BGM
                    bgmManager.PlayBreakSafeBgm();
                    // ============================================
                    break;
                case Ch2GameManager.Games.RuthlessRaccoon:
                    Game3Frame.SetActive(true);
                    GameBtn.SetActive(false);
                    for (int i = 0; i < 3; i++)
                    {
                        if (Game3Character[i].activeSelf == false)
                        {
                            count++;
                        }
                    }
                    if (count == 3 && !HorseDie)
                    {
                        if (game3Manager.Horse_A != null)
                        {
                            // 如果沒有角色，把木馬動畫關掉
                            game3Manager.Horse_A.GetComponent<Animator>().enabled = false;
                        }
                        if (game3Manager.Horse_C != null)
                        {
                            // 如果沒有角色，把木馬動畫關掉
                            game3Manager.Horse_C.GetComponent<Animator>().enabled = false;
                        }

                        for (int i = 0; i < 3; i++)
                        {
                            GamePoster[i].enabled = false;
                        }
                        // =====================
                        // 觸發error彈出視窗 音效
                        audioManager.MythErrorShow_Dickimon();
                        // =====================

                        WarningFrame.SetActive(true);
                        if (NotPutBrandFirst)
                        {
                            // 觸發文本
                            ch2DialogueManager.ShowNextDialogue("第一次沒放名牌並開啟遊戲", true);
                            NotPutBrandFirst = false;
                        }
                    }
                    else
                    {
                        if (game3Manager.FirstOpenDialog)
                        {
                            // 如果有角色，把木馬動畫打開
                            game3Manager.Horse_A.GetComponent<Animator>().enabled = true;
                            game3Manager.Horse_C.GetComponent<Animator>().enabled = true;
                            game3Manager.Opening();
                            game3Manager.FirstOpenDialog = !game3Manager.FirstOpenDialog;
                        }
                    }
                    // ============================================
                    // 顯示對話
                    if (PlumberUsaHorse.gameObject == null)
                    {
                        if (OpenRaccoonFirst_Bricks && PlumberChar == "磚塊殺手")
                        {
                            ch2DialogueManager.ShowNextDialogue("磚塊殺手第一次在無情浣熊", true);
                            OpenRaccoonFirst_Bricks = false;
                        }
                    }
                    // ============================================
                    // 播放BGM
                    bgmManager.PlayRaccoonGameBgm();
                    // ============================================
                    break;
                case Ch2GameManager.Games.Antivirus:
                    AntiFront.SetActive(true);
                    AntiBtn.SetActive(false);
                    foreach (BoxCollider2D col in GamePoster)
                    {
                        col.enabled = false;
                    }
                    break;
            }
        }
    }
    public void CloseGame()
    {
        // audioManager.CloseWindow();
        // // =============================
        // // BGM
        // bgmManager.StopCH2BGM();
        // // =============================
        for (int i = 0; i < 3; i++)
        {
            GamePoster[i].enabled = true;
        }
        switch (Ch2GameManager.nowGames)
        {
            case Ch2GameManager.Games.Dickimon:
                if (DickimonSetState.nowEnemy != "木馬病毒" && DickimonSetState.nowEnemy != "木馬終結者" && DickimonSetState.nowEnemy != "無情浣熊")
                {
                    if (!DickimonSetState.isPlayingAni)
                    {
                        audioManager.CloseWindow();
                        // =============================
                        // BGM
                        bgmManager.StopCH2BGM();
                        // =============================
                        DickimonSetState.closeDickimonWindow();
                        Game1Frame.SetActive(false);
                        GameBtn.SetActive(true);
                        Ch2GameManager.nowGames = Ch2GameManager.Games.Myth;    // 關閉視窗讓狀態為Myth
                    }
                    else
                    {
                        Ch2GameManager.nowGames = Ch2GameManager.Games.Dickimon;
                        for (int i = 0; i < 3; i++)
                        {
                            GamePoster[i].enabled = false;
                        }
                    }
                }
                break;
            case Ch2GameManager.Games.BreakoutSafe:
                audioManager.CloseWindow();
                // =============================
                // BGM
                bgmManager.StopCH2BGM();
                // =============================
                Game2Frame.SetActive(false);
                GameBtn.SetActive(true);
                Ch2GameManager.nowGames = Ch2GameManager.Games.Myth;    // 關閉視窗讓狀態為Myth
                break;
            case Ch2GameManager.Games.RuthlessRaccoon:
                audioManager.CloseWindow();
                // =============================
                // BGM
                bgmManager.StopCH2BGM();
                // =============================
                Game3Frame.SetActive(false);
                GameBtn.SetActive(true);
                Ch2GameManager.nowGames = Ch2GameManager.Games.Myth;    // 關閉視窗讓狀態為Myth
                break;
            case Ch2GameManager.Games.Antivirus:
                audioManager.CloseWindow();
                // =============================
                // BGM
                bgmManager.StopCH2BGM();
                // =============================
                AntiFront.SetActive(false);
                AntiBtn.SetActive(true);
                Ch2GameManager.nowGames = Ch2GameManager.Games.Myth;    // 關閉視窗讓狀態為Myth
                break;
        }
    }

    public void ClickWarningFBtn()
    {
        // 觸發文本
        ch2DialogueManager.ShowNextDialogue("每次關閉error", true);
        // ===========================
        PixelFireRoot.transform.localPosition = WFrame.transform.localPosition;
        PixelFire.transform.parent = PixelFireRoot.transform;
        WarningFrame.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);
        WarningFrame.SetActive(false);
        if ((PixelFire.transform.position.x >= 1.1 && PixelFire.transform.position.x <= 4f) && (PixelFire.transform.position.y >= 1.6 && PixelFire.transform.position.y <= 3.4) && DickimonSetState.nowEnemy == "木馬終結者")
        {
            HorseDie = true;
            DickimonSetState.HorseDieDia();
            DickimonSetState.HorseBurnObj.SetActive(true);
            DickimonSetState.EnemyHp.GetComponent<Animator>().SetTrigger("Transreduce");
        }
        else
        {
            PixelFire.SetActive(true);
        }
    }
    public void PiexFiresetFalse()
    {
        PixelFire.SetActive(false);
        PixelFire.transform.parent = WFrame.transform;
        PixelFire.transform.localPosition = new Vector3(-5.42f, 1.02f, 0);
    }
    #endregion

    #region 背面背包
    public void Bag_Appear()
    {
        if (Ch2AniEvents.AniDone)
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
        }
    }
    public void Bag_Disappear()
    {
        Bar_Back.transform.DOLocalMoveY(-5.7f, 0.5f, false);
        GameObject.Find("BtnShowBag").GetComponent<SpriteRenderer>().sprite = UpandDown[0];
    }

    #endregion

    #region 防毒軟體
    public void ClickToViewStatus()
    {
        AnitvirusFrame.SetActive(true);
        ScrollCanvas.SetActive(false);
        TutorialCanvas.SetActive(false);
        AntivirusBtnImage.GetComponent<Image>().sprite = AntivirusBtnSprite[0];
    }
    public void ClickToViewHistory()
    {
        AnitvirusFrame.SetActive(false);
        ScrollCanvas.SetActive(true);
        TutorialCanvas.SetActive(false);
        AntivirusBtnImage.GetComponent<Image>().sprite = AntivirusBtnSprite[1];
    }
    public void ClickToViewTutorial()
    {
        AnitvirusFrame.SetActive(false);
        ScrollCanvas.SetActive(false);
        TutorialCanvas.SetActive(true);
        AntivirusBtnImage.GetComponent<Image>().sprite = AntivirusBtnSprite[2];
    }
    #endregion

    #region ToolBar
    public void ShowMessageBox()
    {
        AntiMessageBox.transform.GetComponent<SpriteRenderer>().sortingOrder = 0;
        MessageBox.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
        MessageBox.GetComponent<SpriteRenderer>().DOFade(1, 1);
        StartCoroutine(WaitFor(3f, MessageBox));
    }

    IEnumerator WaitFor(float Seconds, GameObject Messagebox)
    {
        yield return new WaitForSeconds(Seconds);
        Messagebox.GetComponent<SpriteRenderer>().DOFade(0, 1);
    }
    #endregion

    #region SetGetKey
    public void SetDickimonGetKey()
    {
        DickimonSetState.GetKey = true;
    }
    #endregion

    #region RaccoontoDickimon

    public void RacToDickimon(string nowEnemy)
    {
        DickimonSetState.ChangeEnemy(nowEnemy);
        Game1Character[0].SetActive(true);//將小明角色打開
        Game1Character[1].SetActive(false);//將磚塊殺手關掉
        Game1Character[2].SetActive(false);//將水電工關掉
        DickimonSetState.nowCharater = "XiaoMing";
        // DickimonSetState.ToSetDickmonState(true);
    }

    public void CloseDickimonWindow()
    {
        DickimonSetState.closeDickimonWindow();
        Game1Frame.SetActive(false);
        GameBtn.SetActive(true);
    }

    public IEnumerator ToDickimonHorse()
    {
        yield return new WaitForSeconds(0.5f);
        game3Manager.BigDick.SetActive(true);
        // ====================
        // 過場音效
        audioManager.MingIntoBattle_Raccoon();
        // ====================
        game3Manager.BigDick.transform.DOScale(new Vector3(17f, 17f, 0), 3f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            Destroy(game3Manager.Horse);
            ch2GameManager.RacToDickimon_Horse();
            game3Manager.ToBackBtn.enabled = true;
        });
    }

    #endregion

    #region 木馬破壞角色後換圖

    public void HorseAttackandCharacterDie(string name)
    {
        switch (name)
        {
            case "XiaoMing":
                // 存活角色-1
                characterAlive--;
                // 改變點擊狗會說的話
                ch2DialogueManager.changeDogStage("ming_dead");
                // ===================================
                //Visual換Bar圖
                source_sprite[0] = XiaoMingDie[0];
                bar_sprite[0] = XiaoMingDie[2];
                backbar_sprite[0] = XiaoMingDie[3];
                //背面框內換圖
                XiaoMing.GetComponent<SpriteRenderer>().sprite = XiaoMingDie[0];
                XiaoMing.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = XiaoMingDie[1];
                break;

            case "Killer":
                // 存活角色-1
                characterAlive--;
                // 改變點擊狗會說的話
                ch2DialogueManager.changeDogStage("bricks_dead");
                // ===================================
                //Visual換Bar圖
                source_sprite[1] = KillerDie[0];
                bar_sprite[1] = KillerDie[2];
                backbar_sprite[1] = KillerDie[3];
                //背面框內換圖
                BricksKiller.GetComponent<SpriteRenderer>().sprite = KillerDie[0];
                BricksKiller.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = KillerDie[1];
                break;

            case "Plumber":
                // 存活角色-1
                characterAlive--;
                // 改變點擊狗會說的話
                ch2DialogueManager.changeDogStage("plumber_dead");
                // ===================================
                //Visual換Bar圖
                source_sprite[2] = PlumberDie[0];
                bar_sprite[2] = PlumberDie[2];
                backbar_sprite[2] = PlumberDie[3];
                //背面框內換圖
                Plumber.GetComponent<SpriteRenderer>().sprite = PlumberDie[0];
                Plumber.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = PlumberDie[1];
                break;
        }

        if (characterAlive == 0)
        {
            // 改變點擊狗會說的話
            ch2DialogueManager.changeDogStage("all_dead");
            // ===================================
        }
    }

    #endregion

    #region  Portal動畫控制

    public Animator PortalAni;

    public void PortalShining()
    {
        if (Ch2AniEvents.AniDone)
        {
            PortalAni.SetBool("Shining", true);
        }
    }
    public void PortalUnshine()
    {
        if (Ch2AniEvents.AniDone)
        {
            PortalAni.SetBool("Shining", false);
        }
    }


    #endregion

    #region 背面名牌框Hover

    public void HoverGame1Frame()
    {
        if (BrandFrame[0].gameObject.transform.childCount == 3)//名牌在上面
        {
            BrandFrame[0].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            audioManager.HoverAnyPropsAndZoomInInBack();
        }
    }
    public void HoverGame2Frame()
    {
        if (BrandFrame[1].gameObject.transform.childCount == 3)//名牌在上面
        {
            BrandFrame[1].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            audioManager.HoverAnyPropsAndZoomInInBack();
        }
    }
    public void HoverGame3Frame()
    {
        if (BrandFrame[2].gameObject.transform.childCount == 3)//名牌在上面
        {
            BrandFrame[2].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            audioManager.HoverAnyPropsAndZoomInInBack();
        }
    }

    #endregion


    #region DemoTrans
    public Image QrcodeImg;

    public void ShowQrcode()
    {
        QrcodeImg.gameObject.SetActive(true);
        QrcodeImg.DOFade(1, 0.5f);
    }
    #endregion
}
