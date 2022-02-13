using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AniEvents : MonoBehaviour
{
    VisualManager visualManager;
    UserManager userManager;
    DialogueManager dialogueManager;
    AudioManager audioManager;
    BGMManager bgmManager;
    GameManager gameManager;

    CursorSetting cursorSetting;

    [Header("主角動畫")]
    public Animator CharacterAni;
    public Animation DownTolidle;

    [Header("WormAnimator")]
    public Animator WormAni;

    GameObject connect_interent_MessageBox;

    public static bool AniDone = true;

    GameObject Bar;

    [Header("工具列圖片")]

    public Sprite[] BarImages;

    Text TextStr;

    [Header("Myth最後一貞")]
    public GameObject Myth;
    public Sprite MythLast_Sprite;
    public GameObject ClockCanvas;
    [Header("關閉Myth的Btn")]
    public GameObject CloseMyth_Btn;
    [Header("MoraAni")]
    public Animator Hunk_Mora_Ani;

    [Header("GUEST BTN")]
    public GameObject GuestBtn;
    [Header("ShowBagBtn")]
    public GameObject ShowBagBtn;

    int TextCounter = 0;

    [Header("釣魚動畫")]
    public Animator Fishing_Other;
    public GameObject Obj;

    public SpriteRenderer BrowserLyaerSR;

    public bool uTriggerMora_First = true;  // 第一次在猛男野球拳放U
    public bool tTriggerMora_First = true;  // 第一次在猛男野球拳放T
    public bool shovelTriggerMora_First = true;  // 第一次在猛男野球拳放shover
    public bool sTriggerMora_First = true;  // 第一次在猛男野球拳放S
    public bool clickDoorKeeperFirst = true;    // 第一次點擊守門人
    public bool triggered_GUEST = false;    // 第一次trigger GUEST板子
    public bool clickDogPosterFirst = true;    // 第一次點擊狗窩上的海報

    void Start()
    {
        // if (this.name == "ExitBTN")
        // {
        //     ExitBtnAni();
        // }

        visualManager = GameObject.Find("VisualManager").GetComponent<VisualManager>();
        userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<CursorSetting>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //ch2Player = GameObject.Find("Player").GetComponent<Ch2Player>();

        connect_interent_MessageBox = GameObject.Find("connect_interent_MessageBox");
        Bar = GameObject.Find("Bar");
        if (this.tag == "Text")
        {
            TextStr = this.GetComponent<Text>();
        }
    }

    #region 設定AniDone參數

    public void AniDonefalse()
    {
        AniDone = false;
        cursorSetting.CursorHourglassAni();
    }

    public void AniDoneTrue()
    {
        AniDone = true;
        cursorSetting.StopCursorHourglassAni();
    }

    #endregion

    #region 桌面
    public void connect_interent_MessageBoxShowUp() //淡入連接上網路對話框 換工具列成連上網路
    {
        connect_interent_MessageBox.GetComponent<SpriteRenderer>().DOFade(1, 1);
        Bar.GetComponent<SpriteRenderer>().sprite = BarImages[0];
        AniDone = true;
        cursorSetting.StopCursorHourglassAni();
    }
    public void connect_interent_MessageBoxDisappear()//淡出連接上網路對話框
    {
        connect_interent_MessageBox.GetComponent<SpriteRenderer>().DOFade(0, 1);
    }

    public void changeText()//更改防毒軟體文字
    {
        TextCounter++;
        switch (this.name)
        {
            case "Sleep_Text":
                switch (TextCounter)
                {
                    case 1:
                        TextStr.text = "您的電腦正在受到保護中.";
                        break;
                    case 2:
                        TextStr.text = "您的電腦正在受到保護中..";
                        break;
                    case 3:
                        TextStr.text = "您的電腦正在受到保護中...";
                        break;
                    case 4:
                        TextStr.text = "您的電腦正在受到保護中";
                        TextCounter = 0;
                        break;
                    default:
                        break;
                }
                break;
            case "Alert_Text":
                switch (TextCounter)
                {
                    case 1:
                        TextStr.text = "您的電腦正在受到未知的威脅!";
                        break;
                    case 2:
                        TextStr.text = "您的電腦正在受到未知的威脅!!";
                        break;
                    case 3:
                        TextStr.text = "您的電腦正在受到未知的威脅!!!";
                        break;
                    case 4:
                        TextStr.text = "您的電腦正在受到未知的威脅";
                        TextCounter = 0;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }


    }
    #endregion

    #region 瀏覽器
    public void ChangeYee()
    {
        this.transform.parent.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        BrowserLyaerSR.sprite = Resources.Load<Sprite>("Textures/browser_stopdino_bottomlayer");
        Destroy(GameObject.Find("Btn_Close_PlayEatAni"));
        visualManager.BrowserFrontMouseClick();
        visualManager.DinoEatAniDone = true;
        Destroy(this.gameObject);
    }

    public void YeeEnd()
    {
        visualManager.PlayDinoYee();
    }

    public void BtnPlayerClickCursor()
    {
        GameObject.Find("BtnPlayerClick").GetComponent<EventTrigger>().enabled = true;
        GameObject.Find("BtnPlayerClick").GetComponent<CursorFX>().enabled = true;
    }
    //=====================反面========================

    public void EatGift()
    {
        visualManager.G.SetActive(true);
        Destroy(visualManager.Fishing_Eat);
    }
    public void FishingOther()
    {
        Fishing_Other.SetBool("Fishing_Other", false);
    }

    #endregion

    #region 資料夾
    public void PlayerToIdle()
    {
        userManager.mouseWorldPosition.x = userManager.Character.transform.position.x;
        userManager.IdleAni();
    }
    bool IsAniEnd = false;
    public void Lose_Appear()
    {
        visualManager.Lose.transform.DOScale(0.25f, 1f);
        IsAniEnd = true;
    }
    public void Lose_Disappear()
    {
        visualManager.Lose.transform.DOScale(0f, 1f).OnComplete(() =>
        {
            if (IsAniEnd)
            {
                AniDoneTrue();
                IsAniEnd = false;
            }
        });
    }
    public void MoraExplode()
    {
        visualManager.Hunk_Mora_App.GetComponent<SpriteRenderer>().sprite = visualManager.HunkExplodeSprite;
        for (int i = 0; i < 3; i++)
        {
            Destroy(visualManager.moraSprite[i]);
        }
    }
    public void MoraExploded()
    {
        Hunk_Mora_Ani.enabled = false;
        visualManager.Win.transform.DOScale(0.3f, 1f);
    }

    public void DestoryBombFX()
    {
        Destroy(GameObject.Find("Explosion"));
    }
    public void showupBomb()
    {
        GameObject.Find("Layer_Green_Flower").GetComponent<SpriteRenderer>().sprite = null;
        GameObject.Find("Flower").GetComponent<SpriteRenderer>().sprite = visualManager.BombBg2;
        visualManager.Bomb.SetActive(true);
        visualManager.Bomb.transform.SetParent(GameObject.Find("Tools").transform);
        visualManager.Bomb.AddComponent<ItemEventManager>().PlayerGet = true;
        visualManager.Bomb.tag = "Prop";
        visualManager.Bomb.GetComponent<SpriteRenderer>().sortingOrder = 2;


        visualManager.T.SetActive(true);
        visualManager.T.transform.SetParent(GameObject.Find("Tools").transform);
        visualManager.T.AddComponent<ItemEventManager>();//.PlayerGet = true;
        visualManager.T.tag = "Prop";
        visualManager.T.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
    public void Wormmissing()//蚯蚓不見
    {
        WormAni.SetTrigger("wormChange");
        GameObject.Find("PicFrame").gameObject.transform.GetChild(2).gameObject.SetActive(true);
        Destroy(GameObject.Find("earthworm"));
        visualManager.Bird[0].SetActive(false);//鴿子原始型態
        visualManager.Bird[1].SetActive(true);//鴿子叼蚯蚓型態
        visualManager.EarthwormMissed = true;
    }
    public void DestoryAniObj()
    {
        Destroy(this.gameObject);
    }

    public void GateKeeperReceiveU()
    {
        dialogueManager.ShowNextDialogue("give_doorkeeper_u_first", false);
    }
    public void PutUOnMora()
    {
        if (uTriggerMora_First)
        {
            dialogueManager.ShowNextDialogue("put_u_on_mora", true);
            uTriggerMora_First = false;
        }
    }
    public void PutTOnMora()
    {
        if (tTriggerMora_First)
        {
            dialogueManager.ShowNextDialogue("put_t_on_mora", true);
            tTriggerMora_First = false;
        }
    }
    public void PutShovelOnMora()
    {
        if (shovelTriggerMora_First)
        {
            dialogueManager.ShowNextDialogue("put_shovel_on_mora", true);
            shovelTriggerMora_First = false;
        }
    }
    public void PutSOnMora()
    {
        if (sTriggerMora_First)
        {
            dialogueManager.ShowNextDialogue("put_s_on_mora", true);
            sTriggerMora_First = false;
        }
    }

    #endregion

    #region Myth第一次背面
    public void MythFirstBack()
    {
        GameObject.Find("U").GetComponent<Collider2D>().enabled = false;
        CharacterAni.speed = 0;
    }
    public void OpenTheLock()
    {
        if (GameManager.stage == 10)
        {
            GameObject.Find("U").GetComponent<Collider2D>().enabled = true;
            GuestBtn.SetActive(true);
            ShowBagBtn.SetActive(true);
        }
    }
    #endregion

    #region Myth開場動畫
    public void StolenMyth()
    {
        Myth.GetComponent<SpriteRenderer>().sprite = MythLast_Sprite;
        Myth.GetComponent<Animator>().enabled = false;
        Bar.GetComponent<SpriteRenderer>().sprite = BarImages[0];//Bar條換成破掉的鏡子
        Destroy(this.transform.GetChild(0).gameObject);
        Destroy(this.transform.GetChild(1).gameObject);
        visualManager.ToBackBtn.SetActive(true);
        visualManager.MythLightFX();
        // 把數字時鐘的layer提高
        // ClockCanvas.GetComponent<Canvas>().sortingOrder = 2;
    }

    public void CloseBtn()
    {
        CloseMyth_Btn.GetComponent<Button>().enabled = false;
        CloseMyth_Btn.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        GameObject.Find("CloseWindow_FX").SetActive(false);
    }

    public void changebarlayer(int order)
    {
        Bar.GetComponent<SpriteRenderer>().sortingOrder = order;
    }
    #endregion

    #region 木馬提示動畫

    public void ChangeNotice()
    {
        GameObject.Find("NoticeAni").transform.Find("notice_off").gameObject.SetActive(true);
        GameObject.Find("notice").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/light_on");
        StartCoroutine(noticefx(false));
    }

    public IEnumerator noticefx(bool light)
    {
        if (light)
        {
            // =================================
            // 播放亮的音效
            audioManager.DesktopTipsLight();
            // =================================
            GameObject.Find("notice").GetComponent<SpriteRenderer>().DOFade(0, 0f);
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(noticefx(false));
        }
        else
        {
            GameObject.Find("notice").GetComponent<SpriteRenderer>().DOFade(1, 0f);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(noticefx(true));
        }
    }
    #endregion

    #region GUEST石板動畫

    public void PlayGuestAni()
    {
        Destroy(this.gameObject);
        visualManager.GUESTBurstAni();
    }

    #endregion

    #region GUESTLOGIN
    public void ErrorWindowAni()
    {
        // this.transform.parent.gameObject.GetComponent<ErrorWindowFX>().enabled = true;
        ErrorWindowFX.showerrorwin = true;
    }
    #endregion

    #region 主角
    public void PlayerDownToidle()
    {
        // 讓狗可以被點擊
        visualManager.EnableDogClick();
        // ================================
        userManager.IdleAni();
        userManager.FirstToBack = false;
    }
    public void PlayerPickUp()
    {
        switch (userManager.Props)
        {
            case "G":
                dialogueManager.ShowNextDialogue("get_G", false);

                if (GameManager.stage == 28)
                {
                    GameManager.stage++;
                }
                break;
            case "U":
                //要掛在AniEvent
                dialogueManager.ShowNextDialogue("pickup_U", false);
                GameManager.stage++;
                break;
            case "E":
                dialogueManager.ShowNextDialogue("get_E", false); userManager.Character.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameObject.Find(userManager.Props).GetComponent<SpriteRenderer>().sprite;
                visualManager.isGetE = true;

                if (GameManager.stage == 18)
                {
                    GameManager.stage++;
                }
                if (VisualManager.GetS)
                {
                    GameManager.stage = 23;
                }
                else
                {
                    GameManager.stage = 22;
                }
                break;
            case "S":
                VisualManager.GetS = true;
                userManager.haveGetS = true;
                // 撿起S，觸發文本
                dialogueManager.ShowNextDialogue("bird_sleep_and_get_s", false);
                if (GameManager.stage == 22 && visualManager.isGetE)
                {
                    GameManager.stage++;
                }

                visualManager.CmailIcon.SetActive(true);
                visualManager.CmailWormIcon.SetActive(false);
                visualManager.CmailWindowWorm.SetActive(false);
                break;
            case "GUESTBtnProp":
                // 改變點擊狗會說的話
                dialogueManager.changeDogStage("get_guest");
                // =================================
                userManager.Character.transform.GetChild(0).transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                break;

        }

        userManager.Character.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameObject.Find(userManager.Props).GetComponent<SpriteRenderer>().sprite;
        GameObject.Find(userManager.Props).transform.SetParent(GameObject.Find("Tools").transform);
        GameObject.Find(userManager.Props).tag = "Prop";
        GameObject.Find(userManager.Props).GetComponent<ItemEventManager>().PlayerGet = true;
        GameObject.Find(userManager.Props).GetComponent<ItemEventManager>().isOnBar = true;
        GameObject.Find(userManager.Props).GetComponent<Collider2D>().enabled = true;
        GameObject.Find(userManager.Props).GetComponent<LayoutElement>().ignoreLayout = false;
        GameObject.Find(userManager.Props).GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
        GameObject.Find(userManager.Props).transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
    }
    public void PlayerPickUp_Finish()
    {
        userManager.IdleAni();
        userManager.isClickProps = false;
        userManager.Props = null;
    }
    public void PlayerGiveU()
    {
        userManager.GiveU = false;
    }
    public void PlayerPayMoney()
    {
        userManager.PayMoney = false;
    }
    public void PlayerPause()
    {
        userManager.CharacterAni.speed = 0;
        if (userManager.PayMoney)
        {
            StartCoroutine(visualManager.OpenDoor());
        }
        else if (userManager.GiveU)
        {
            visualManager.GiveU();
        }
        else if (userManager.ClickHunk)
        {
            visualManager.ToShowETips();
        }
        else if (userManager.ClickZoomin)
        {
            StartCoroutine(wait());
            IEnumerator wait()
            {
                yield return new WaitForSeconds(0.5f);
                switch (userManager.ZoominItem)
                {
                    case "Clock":
                        gameManager.SetSceneWithZoomin(GameManager.Chapter.CH1, GameManager.Zoomin.Clock, true);
                        AniDoneTrue();
                        break;
                    case "Poster":
                        gameManager.SetSceneWithZoomin(GameManager.Chapter.CH1, GameManager.Zoomin.Poster, true);
                        if (clickDogPosterFirst)
                        {
                            // 第一次點擊狗窩上的海報，觸發文本
                            dialogueManager.ShowNextDialogue("click_dog_house_poster", false);
                            clickDogPosterFirst = false;
                        }
                        AniDoneTrue();
                        break;
                    case "Guest":
                        gameManager.SetSceneWithZoomin(GameManager.Chapter.CH1, GameManager.Zoomin.GUEST, true);
                        if (!triggered_GUEST)
                        {
                            // =================================
                            dialogueManager.ShowNextDialogue("click_GUEST_first", false);
                            triggered_GUEST = true;
                        }
                        AniDoneTrue();
                        break;
                    case "Picture":
                        gameManager.SetSceneWithZoomin(GameManager.Chapter.CH1, GameManager.Zoomin.PicFrame, true);
                        AniDoneTrue();
                        break;
                }
            }
        }
    }
    public void PlayerContinue()
    {
        userManager.CharacterAni.speed = 1;
        dialogueManager.ShowNextDialogue("give_bitcoin", false);
    }
    public void PlayerContinue2()
    {
        userManager.CharacterAni.speed = 1;
    }
    public void GateKeeperPause()
    {
        visualManager.ThinAni.speed = 0;
    }
    public void GateKeeperContinue()
    {
        visualManager.ThinAni.speed = 1;
    }
    public void GateKeeperToidle()
    {
        visualManager.ThinAni.SetBool("GiveU", false);
    }
    public void PlayerPutFishing()
    {
        if (visualManager.GiftorOther)
        {
            visualManager.Fishing_Eat_Ani.SetBool("Fishing_Gift", true);
        }
        else
        {
            visualManager.Fishing_Eat_Ani.SetBool("Fishing_Other", true);
        }
    }
    public void PlayerFishfinish()
    {
        userManager.PutFishing = false;
    }
    public void PlayerClickGateKeeper()
    {
        userManager.ClickGateKeeper = false;
        visualManager.ShowBitcoinTips();
        // 觸發文本
        // 第一次點擊守門人，觸發文本
        if (clickDoorKeeperFirst)
        {
            dialogueManager.ShowNextDialogue("click_door_keeper", false);
            clickDoorKeeperFirst = false;
        }
    }
    public void PlayerTurnBack()
    {
        userManager.ClickHunk = false;
    }
    #endregion

    #region 資料夾背面鳥動畫
    public void BirdWalkIn()
    {
        dialogueManager.ShowNextDialogue("bird_walk_in", false);
    }
    public void BirdEatS()
    {
        dialogueManager.ShowNextDialogue("bird_get_s", false);
    }
    #endregion

    #region 滑鼠移入會發亮

    public void OnPoniterEnter()
    {
        this.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
    public void OnPointerExit()
    {
        this.GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }

    #endregion

    #region  Geegle

    public void ClickGeegle()
    {
        // geelge笑音效
        audioManager.GeegleLaugh();
        // ===============================
        Animator GeegleAni = this.gameObject.GetComponent<Animator>();
        GeegleAni.SetBool("Clickgeegle", true);
    }

    public void EndGeegleAni()
    {
        Animator GeegleAni = this.gameObject.GetComponent<Animator>();
        GeegleAni.SetBool("Clickgeegle", false);
    }

    #endregion

    #region 動畫音效相關
    // 帳號走路
    private string DetermineNowStage()
    {
        if (GameManager.nowCH1State == GameManager.CH1State.DeskTopBack)
        {
            return "Desktop";
        }
        else if (GameManager.nowCH1State == GameManager.CH1State.CMailBack)
        {
            return "CMail";
        }
        else if (GameManager.nowCH1State == GameManager.CH1State.FolderBack)
        {
            return "Folder";
        }
        else if (GameManager.nowCH1State == GameManager.CH1State.MythBack)
        {
            return "Myth";
        }
        else if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && visualManager.Fishing_Back.activeSelf)
        {
            // 在釣魚背面
            return "Fishing";
        }
        else
        {
            return "Browser";
        }

    }
    public void AccWalk()
    {
        // ==========================================
        // 帳號走路
        audioManager.AccWalk(DetermineNowStage());
    }

    // 桌面防毒軟體睡覺
    public void DesktopAntiSleep()
    {
        audioManager.DesktopAntiSleep();
    }
    // 小恐龍
    public void PlayDinoYeeAudio()
    {
        audioManager.ClickDinasor();
    }
    public void DinoEatXAudio()
    {
        audioManager.DinasorEatX();
    }
    public void DinoEatXReverseAudio()
    {
        audioManager.DinasorEatXReverse();
    }
    public void DinoSwallowX()
    {
        audioManager.DinasorSwallowX();
    }
    public void DinasorCallBGM()
    {
        bgmManager.DinasorBGM();
    }
    public void WifiConnect1()
    {
        audioManager.WifiConnect(0);
    }
    public void WifiConnect2()
    {
        audioManager.WifiConnect(1);
    }
    public void WifiConnect3()
    {
        audioManager.WifiConnect(2);
    }
    public void ShowWifiConnectedConversion()
    {
        audioManager.ClickInternetIconOrShowDogSpeak();
    }
    // 桌面木馬病毒
    public void DesktopHorseLaugh()
    {
        audioManager.DesktopHorseLaugh();
    }
    public void DesktopLinkElec()
    {
        audioManager.DesktopLinkElec();
    }
    public void DesktopHorseLeave()
    {
        audioManager.DesktopHorseLeave();
    }
    // ====================================
    // 木馬動畫
    public void HorseShow()
    {
        // 木馬冒出來
        audioManager.HorseShow();
    }
    public void HorseTakeU()
    {
        // 木馬拔磁鐵
        audioManager.HorseTakeU();
    }
    public void HorseLittleTearPass()
    {
        // 木馬小拉扯密碼牌
        audioManager.HorseLittleTearPass();
    }
    public void HorseTearPass()
    {
        // 木馬大力拉扯密碼牌
        audioManager.HorseTearPass();
    }
    public void HorseAbsorbPass()
    {
        // 木馬吸出密碼
        audioManager.HorseAbsorbPass();
    }
    public void PassFall()
    {
        // 密碼掉落
        audioManager.PassFall();
    }
    public void PassScreamHelp()
    {
        // 動畫_mtyh_密碼尖叫_叫救命
        audioManager.PassScreamHelp();
    }
    public void PassScreamGetPull()
    {
        // 動畫_mtyh_密碼尖叫_被拉走
        audioManager.PassScreamGetPull();
    }
    public void PassScreamStuck()
    {
        // 動畫_mtyh_密碼尖叫_卡住
        audioManager.PassScreamStuck();
    }
    public void PassScreamGetInto()
    {
        // 動畫_mtyh_密碼尖叫_拉進去
        audioManager.PassScreamGetInto();
    }
    public void PassJump()
    {
        // 密碼跳
        audioManager.PassJump();
    }
    public void HorseCatchPass()
    {
        // 怪手抓到密碼
        audioManager.HorseCatchPass();
    }
    public void HorsePullbackPass()
    {
        // 拉回怪手
        audioManager.HorsePullbackPass();
    }
    public void PassStuck()
    {
        // 密碼卡住
        audioManager.PassStuck();
    }
    public void PassGotFilled()
    {
        // 密碼被拉進去
        audioManager.PassGotFilled();
    }
    public void HorseMoveLeft()
    {
        // 木馬往左進去
        audioManager.HorseMoveLeft();
    }
    public void HorseMoveUp()
    {
        // 木馬從上面出現
        audioManager.HorseMoveUp();
    }
    public void HorseLaugh()
    {
        // 木馬笑
        audioManager.HorseLaugh();
    }
    public void HorseUseUToHitAcc()
    {
        // 木馬拿磁鐵碰帳號
        audioManager.HorseUseUToHitAcc();
    }
    public void ShowRedWarning()
    {
        // 紅燈閃
        audioManager.ShowRedWarning();
    }
    public void HorseScared()
    {
        // 木馬驚嚇
        audioManager.HorseScared();
    }
    public void MythUHitWall()
    {
        // 磁鐵打牆壁
        audioManager.MythUHitWall();
    }
    public void MythUHitMirror()
    {
        // 磁鐵砸破玻璃
        audioManager.MythUHitMirror();
    }
    public void MythUAbsorbedByMirror()
    {
        // 磁鐵被吸進去
        audioManager.MythUAbsorbedByMirror();
    }
    public void OpenBlueLayer()
    {
        // 觸發打開藍骨頭音效
        audioManager.OpenBlueLayerAudio();
    }
    // =======================================
    // CMail
    public void BirdSleep()
    {
        // 鳥睡覺
        audioManager.BirdSleep();
    }
    public void DogeCry()
    {
        // 狗哭聲在吸狗狗幣那邊
        audioManager.DogeCry();
    }

    // =======================================
    // Myth背面
    public void AccountHangUpU()
    {
        // 舉起U
        audioManager.HangUpU();
    }

    // =======================================
    // 資料夾
    public void DoorFatGuyClick()
    {
        // 看門胖子案遙控器
        audioManager.DoorFatGuyClick();
    }
    public void DoorKeeperWalk()
    {
        // 看門胖子走路
        // Debug.Log("trigger walk audio");
        audioManager.DoorKeeperWalk();
    }
    public void MoraLoseAudio()
    {
        // 播放 You lose qq 的音效
        audioManager.MoraLoseAudio();
    }
    public void MoraWinAudio()
    {
        // 播放 You win 的音效
        audioManager.MoraWinAudio();
    }
    public void MoraBombExplodedAudio()
    {
        // 先停止BGM
        bgmManager.StopBGM();
        // 播放炸彈爆炸音效
        audioManager.MoraBombExplodedAudio();
    }
    public void MoraTheManDown()
    {
        // 播放猛男倒地音效
        audioManager.MoraTheManDown();
        // 停止bgm
        bgmManager.StopBGM();
    }
    public void TheManFlick()
    {
        // 放t猛男彈手指
        audioManager.TheManFlick();
    }
    public void TheManFire()
    {
        // 放t猛男火焰
        audioManager.TheManFire();
    }
    public void TheManBird()
    {
        // 放s猛男鴿子
        audioManager.TheManBird();
    }
    public void TheManDizzy()
    {
        // 猛男被炸暈的聲音，循環到他倒下離開面畫
        audioManager.TheManDizzy();
    }
    public void TheManEGetTrigger()
    {
        // 拿磁鐵吸milos的王冠的聲音
        audioManager.TheManEGetTrigger();
    }
    // ============================
    public void WormMoveAudio()
    {
        // 蚯蚓蠕動聲音
        audioManager.WormMoveAudio();
    }
    public void BirdRunning()
    {
        // 鳥走路
        audioManager.BirdRunning();
    }
    public void BirdRunningStop()
    {
        // 鳥停止走路
        audioManager.BirdRunningStop();
    }
    public void BirdEatWorm()
    {
        // 鳥叼蚯蚓
        audioManager.BirdEatWorm();
    }

    // 釣魚背面
    public void FisherSayYes()
    {
        // 釣魚妹說Yes
        audioManager.FisherSayYes();
    }
    public void FisherSayNo()
    {
        // 釣魚妹說No
        audioManager.FisherSayNo();
    }
    public void FishingReceiveLine()
    {
        // 釣魚收線
        audioManager.FishingReceiveLine();
    }
    public void PropsExitWater()
    {
        // 物品出水面
        audioManager.PropsExitWater();
    }
    public void FisherThrow()
    {
        // 釣魚妹丟東西
        audioManager.FisherThrow();
    }
    public void PropsTriggerWater()
    {
        // 釣魚妹丟東西落水
        audioManager.PropsTriggerWater();
    }
    public void FisherPutLine()
    {
        // 釣魚妹甩魚鉤
        audioManager.FisherPutLine();
    }
    public void LineTriggerWater()
    {
        // 魚鉤碰到水
        audioManager.LineTriggerWater();
    }
    public void FisherThrowLine()
    {
        // 釣魚妹拋線
        audioManager.FisherThrowLine();
    }
    public void FisherReceiveGiftLine()
    {
        // 釣魚妹收禮物的線
        audioManager.FisherReceiveGiftLine();
    }
    public void FisherScared()
    {
        // 釣魚妹嚇到
        audioManager.FisherScared();
    }
    public void FisherAbsorbedByGift()
    {
        // 釣魚妹被禮物怪吸進去
        audioManager.FisherAbsorbedByGift();
    }
    public void GiftSpitG()
    {
        // 禮物怪吐G
        audioManager.GiftSpitG();
    }
    public void GiftSwim()
    {
        // 禮物怪游泳
        audioManager.GiftSwim();
    }

    // =======================================
    // 各種常用
    public void PickAnyProps()
    {
        // 撿起各種道具
        audioManager.PickAnyProps();
        //背面背包鍵縮放
        GameObject.Find("BtnShowBag").transform.DOScale(new Vector3(1.1f, 1.2f, 0), 0.3f).OnComplete(() =>
        {
            GameObject.Find("BtnShowBag").transform.DOScale(new Vector3(1f, 1f, 0), 0.3f);
        });
    }

    #endregion

}
