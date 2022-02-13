using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ch2AniEvents : MonoBehaviour
{
    Ch2Usermanager ch2Usermanager;
    Ch2VisualManager ch2VisualManager;
    Ch2DialogueManager ch2DialogueManager;
    Game2Manager game2Manager;
    Game3Manager game3Manager;
    AudioManager audioManager;
    BGMManager bgmManager;

    Ch2DickimonSetState dickimonSetState;

    Ch2CursorSetting cursorSetting;
    public static bool AniDone = true;

    #region  Dickimon
    public GameObject BtnToBack;
    public GameObject dm_Duck;

    public GameObject dm_Rac;

    Animator dm_DuckAtor;

    public GameObject PlayerHPFrame;
    public Animator DmXiaoMingAni;

    public Animator DmDickchuAtor;

    public Animator DmPlumberAtor;

    public Animator DmRaccoonAtor;

    public GameObject DickchuAtkFX;

    public GameObject Key;

    public GameObject Myth_Frontside;

    public GameObject Horse;
    #endregion

    #region 打磚塊動畫

    public Animator Horse_Outside;
    public Animator Horse_Inside;
    public Animator BlackBall;
    public GameObject FramePartical;

    #endregion

    #region 大金剛

    public bool isUnlock = true;   // 先解鎖再投幣，解鎖後設為false

    #endregion
    public Sprite UnlockSafeSprite;

    [Header("第一次")]
    public bool UseJumpFirst = true;    // 第一次使用跳躍
    public bool ReverseDickchuFirst = true; // 第一次反彈迪可丘
    public static bool isTVOpen = false;    // 短時間內電視是否開啟

    // Start is called before the first frame update
    void Start()
    {
        ch2Usermanager = GameObject.Find("UserManager").GetComponent<Ch2Usermanager>();
        ch2VisualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();

        if ((int)Ch2GameManager.nowCH2State % 2 != 0)
        {
            game2Manager = Myth_Frontside.transform.Find("BricksKillerGame").GetComponent<Game2Manager>();
            game3Manager = Myth_Frontside.transform.Find("PlumberGame").GetComponent<Game3Manager>();
            dickimonSetState = Myth_Frontside.transform.Find("DickimonGame").GetComponent<Ch2DickimonSetState>();
        }
        else
        {
            game2Manager = GameObject.Find("Myth_Frontside").transform.Find("BricksKillerGame").GetComponent<Game2Manager>();
            game3Manager = GameObject.Find("Myth_Frontside").transform.Find("PlumberGame").GetComponent<Game3Manager>();
            dickimonSetState = GameObject.Find("Myth_Frontside").transform.Find("DickimonGame").GetComponent<Ch2DickimonSetState>();
        }
        if (this.name == "dm_duck" || this.name == "AtkAni" || this.name == "dm_XiaoMing")
        {
            dm_DuckAtor = dm_Duck.GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {

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

    #region 第二關反面動畫

    public void Destroythis()
    {
        Destroy(this.gameObject);
    }
    public void PlayerPullBrand()
    {
        GameObject.Find(ch2Usermanager.Brand).transform.SetParent(GameObject.Find("Tools").transform);
        ch2VisualManager.ScreenState();
        GameObject.Find(ch2Usermanager.Brand).GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
        GameObject.Find(ch2Usermanager.Brand).transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";

        if (Ch2GameManager.PullBrand)
        {
            ch2DialogueManager.ShowNextDialogue("第一次碰任何名牌", false);
            Ch2GameManager.PullBrand = false;
        }
    }
    public void PlayerPutBrand()
    {
        GameObject.Find(ch2Usermanager.Brand).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        ch2VisualManager.ScreenState();
    }
    public void PlayerUnLock()
    {
        if (ch2Usermanager.isUnLockKey)
        {
            // 觸發解鎖文本
            ch2DialogueManager.ShowNextDialogue("開啟磚塊殺手鎖", false);
        }
        else if (ch2Usermanager.isUnLockCoin)
        {
            // 觸發解鎖文本
            ch2DialogueManager.ShowNextDialogue("玩家解開無情浣熊的鎖", false);
        }
    }

    #endregion

    #region PiexFire

    public void PiexFiresetF()
    {
        ch2VisualManager.PiexFiresetFalse();
    }

    #endregion

    #region DickimonAni

    public void PlayerHPFrameSlide()
    {
        PlayerHPFrame.transform.DOLocalMoveX(12.04f, 0.3f);
    }

    public void KeyFall()
    {
        // ==========================
        // 被動_迪可夢_鑰匙掉下碰到地板
        StartCoroutine(PlayKeyFallAudio());
        // ==========================
        dickimonSetState.KeyFell = true;
        Key.transform.DOLocalMoveY(-0.3f, 2.5f).SetEase(Ease.OutBounce).SetId<Tween>("Key");
    }

    IEnumerator PlayKeyFallAudio()
    {
        // ==========================
        // 被動_迪可夢_鑰匙掉下碰到地板
        yield return new WaitForSeconds(1f);
        audioManager.KeyFallDownTriggerFloor_Dickimon();
    }

    public void NextString()
    {
        dickimonSetState.AutoNextString();
    }
    //=====================Duck=====================
    public void dm_MyDickimon_Appear()
    {
        if (dickimonSetState.nowEnemy == "木馬病毒")
        {
            dm_Rac.SetActive(true);
        }
        else
        {
            dm_Duck.SetActive(true);
        }
        dmXiaoMingAnidle();
    }
    public void dm_Duck_setfalse()
    {
        PlayerHPFrame.transform.DOLocalMoveX(40f, 0);
        dickimonSetState.PlayergetHurt(-100);
        dm_Duck.SetActive(false);
        DucksetAnidle();
    }
    public void DuckslipHurt()
    {
        dickimonSetState.PlayergetHurt(7);
    }
    public void DuckBubbleAtk()
    {
        dm_Duck.transform.Find("dm_Bubble_effect").gameObject.SetActive(true);
    }

    public void DuckWaterGun()
    {
        DmDickchuAtor.SetBool("WaterGun", true);
    }
    public void DucksetAnidle()
    {
        dm_DuckAtor.SetBool("Appear", false);
        dm_DuckAtor.SetBool("backtoball", false);
        dm_DuckAtor.SetBool("DuckAtkSlip", false);
        dm_DuckAtor.SetBool("BubbleAtk", false);
        dm_DuckAtor.SetBool("WaterGunAtk", false);
        dm_DuckAtor.SetBool("DuckHurt", false);

        if (dm_Duck.transform.Find("dm_Bubble_effect").gameObject.activeSelf)
        {
            dm_Duck.transform.Find("dm_Bubble_effect").gameObject.SetActive(false);
        }
    }

    //=====================XiaoMing=====================
    public void dmXiaoMingAnidle()
    {
        DmXiaoMingAni.SetBool("throwball", false);
        DmXiaoMingAni.SetBool("onlythrowball", false);
        DmXiaoMingAni.SetBool("catchthrow", false);
        DmXiaoMingAni.SetBool("Raccatch", false);
    }

    //=====================Dickchu=====================
    public void DickchuBeBubbleAtked()
    {
        DmDickchuAtor.SetBool("BubbleAtked", true);
    }
    public void DickchuAttack()
    {
        DmDickchuAtor.gameObject.transform.parent.transform.Find("dm_dicky").gameObject.transform.parent = DmDickchuAtor.gameObject.transform;
        DmDickchuAtor.gameObject.transform.Find("dm_dicky").gameObject.transform.localPosition = new Vector3(0, 0, 0);
        if (dickimonSetState.nowEnemy == "巨大迪可丘")
        {
            DmDickchuAtor.gameObject.transform.Find("dm_dicky").gameObject.transform.localScale = new Vector3(1f, 1f, 0);
            DmDickchuAtor.gameObject.transform.Find("dm_dicky").gameObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    int indexdickchu = 0;
    public void DickchuEatMushroomAni()
    {
        // dickimonSetState.isPlayingAni = true;
        indexdickchu = 0;
        Dickchudtwinkle();
    }

    void Dickchudtwinkle()
    {
        GameObject Dickchu = DickchuAtkFX.transform.parent.gameObject;
        if (indexdickchu < 3)
        {
            Dickchu.GetComponent<SpriteRenderer>().DOFade(0, 0.1f).OnComplete(() =>
                        {
                            Dickchu.GetComponent<SpriteRenderer>().DOFade(1, 0.1f).OnComplete(() =>
                            {
                                indexdickchu++;
                                if (indexdickchu == 2)
                                {
                                    // =========================
                                    // 被動_迪可夢_迪可丘吃蘑菇變大 音效
                                    audioManager.DickimonEatMashroomAndGrowUp_Dickimon();
                                    // =========================
                                    // dickimonSetState.isPlayingAni = false;
                                    Dickchu.transform.DOScale(new Vector3(4, 4, 0), 0.3f);
                                    Dickchu.transform.DOLocalMoveY(3, 0.3f).OnComplete(() =>
                                    {
                                        // 更改bgm
                                        bgmManager.StopCH2BGM();
                                        bgmManager.PlayBigDickichuBgm();
                                        // =================== 
                                        StartCoroutine(waitforautostr(1f));
                                    });
                                }
                                Dickchudtwinkle();
                            });

                        });

        }
    }
    IEnumerator waitforautostr(float sec)
    {
        yield return new WaitForSeconds(sec);

        NextString();
    }
    public void DickchuAtkRotate(int Angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Angle);
        DmDickchuAtor.gameObject.transform.Find("dm_dicky").gameObject.transform.rotation = rotation;
    }
    public void DickchuAtkDone()
    {
        NextString();
        // DiaAutoPlayecontrol(0);
        if (dm_Duck.activeSelf)
        {
            dm_Duck.transform.Find("duckelectric").gameObject.SetActive(true);
            dickimonSetState.PlayergetHurt(100);
            dm_DuckAtor.SetBool("DuckHurt", true);
        }

        DickchuAtkFX.SetActive(true);
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        DmDickchuAtor.gameObject.transform.Find("dm_dicky").gameObject.transform.parent = DmDickchuAtor.gameObject.transform.parent;
        DmDickchuAtor.gameObject.transform.parent.transform.Find("dm_dicky").gameObject.transform.localRotation = rotation;
        if (dickimonSetState.nowEnemy == "迪可丘")
        {
            DmDickchuAtor.gameObject.transform.parent.transform.Find("dm_dicky").gameObject.transform.localPosition = new Vector3(0, 0, 0);
            DmDickchuAtor.gameObject.transform.parent.transform.Find("dm_dicky").gameObject.transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            if (dickimonSetState.nowCharater != "BricksKiller")
            {
                DmDickchuAtor.gameObject.transform.parent.transform.Find("dm_dicky").gameObject.transform.localPosition = new Vector3(0, 3, 0);
                DmDickchuAtor.gameObject.transform.parent.transform.Find("dm_dicky").gameObject.transform.localScale = new Vector3(4, 4, 0);
            }
        }

    }

    public void DickchuEletricsetFalse()
    {
        dm_Duck.transform.Find("duckelectric").gameObject.SetActive(false);
    }

    public void DickchuAtksetfalse()
    {
        DickchuAtkFX.SetActive(false);
    }

    public void DickchuHitBall()
    {
        GameObject Dickchu = DickchuAtkFX.transform.parent.gameObject;
        Sequence s = DOTween.Sequence();
        s.Append(Dickchu.transform.DOPunchPosition(new Vector3(-1, 0, 0), 0.5f, 3, 0.5f));
        s.Insert(0.125f, Dickchu.transform.GetComponent<SpriteRenderer>().DOFade(1, 0).OnComplete(() =>
        {
            Dickchu.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/dm_dicky_hitball");
        }));

        s.Insert(0.5f, Dickchu.transform.GetComponent<SpriteRenderer>().DOFade(1, 0).OnComplete(() =>
        {
            Dickchu.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/dm_dicky");
            StartCoroutine(autoplaystr(1.2f));
        }));
    }

    IEnumerator autoplaystr(float second)
    {
        yield return new WaitForSeconds(second);
        NextString();
        Anicontrol(0);//鼠標沙漏結束
    }
    public void DickchubeatkedSetidle()
    {
        DmDickchuAtor.SetBool("BubbleAtked", false);
        DmDickchuAtor.SetBool("WaterGun", false);
        DmDickchuAtor.SetBool("FlashAtk", false);
        DmDickchuAtor.SetBool("LFlashAtk", false);
        DmDickchuAtor.SetBool("AtkPlumber", false);
        DmDickchuAtor.SetBool("AtkBrick", false);
        DmDickchuAtor.SetBool("LAtkBrick", false);
        // Anicontrol(0);
    }

    public void DestoryDickchu()
    {
        Destroy(dickimonSetState.DmDickchu);
    }
    //=====================RaccoonGetcha=====================
    public void RaccoonGetchaAni()
    {
        DmRaccoonAtor.SetTrigger("Rac_Getcha");
    }

    public void RacGetchaFx(int open)
    {
        GameObject BallFx = DmRaccoonAtor.gameObject.transform.parent.Find("BallOpFx").gameObject;
        if (open == 1)
        {
            BallFx.SetActive(true);
        }
        else
        {
            BallFx.SetActive(false);
        }
    }

    //=====================Plumber=====================
    public void PlayDmPlumberJumpAni()
    {
        DmPlumberAtor.SetBool("Plumberjump", true);
    }
    public void PlayerDmPlumberHyperJump()
    {
        DmPlumberAtor.SetBool("PlumberHyperJump", true);
    }
    public void PlayDmPlumberStrikeAni()
    {
        DmPlumberAtor.SetBool("Plumberstrike", true);
    }
    public void DmPlumberidle()
    {
        DmPlumberAtor.SetBool("Plumberthrow", false);
        DmPlumberAtor.SetBool("Plumberstrike", false);
        DmPlumberAtor.SetBool("Plumberjump", false);
    }
    public void DmPlumberJumpCallDia()
    {
        if (UseJumpFirst)
        {
            ch2DialogueManager.ShowNextDialogue("第一次使用水電工技能:跳躍", true);
            UseJumpFirst = false;
        }
    }
    //=====================Horse=====================
    static float waitsec = 0.5f;
    public IEnumerator HorseExplosionFxControl()
    {
        GameObject HorseFx = Horse.transform.GetChild(0).gameObject;
        if (this.name != "horseFx_30")
        {
            yield return new WaitForSeconds(waitsec);
            if (waitsec > 0)
            {
                waitsec -= 0.1f;
                if (waitsec < 0) waitsec = 0;
            }

            HorseFx.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            waitsec = 0.5f;
        }
        Destroy(this.gameObject);
    }
    public void HorseDieFireFxControl()
    {
        GameObject HorseObj = Horse.transform.GetChild(1).Find("dm_horse").gameObject;
        HorseObj.transform.GetChild(0).gameObject.SetActive(false);
        HorseObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/dm_horsekiller5");
    }

    public void BDcurshintoHorse()
    {
        // 觸發文本
        ch2DialogueManager.ShowNextDialogue("當木馬被巨大化迪可丘砸死", true);
        // ==========================
        GameObject horse = Horse.transform.GetChild(1).gameObject;
        horse.transform.DOScaleY(0.09f, 0.1f).SetEase(Ease.Linear);
        dickimonSetState.DickimonFrameShake(1);
    }

    public IEnumerator HorseBounceAni()
    {
        GameObject horse = Horse.transform.GetChild(1).gameObject;
        horse.transform.DOScaleY(0.2f, 0f);
        yield return new WaitForSeconds(0.1f);
        horse.transform.DOScaleY(0.15f, 0);
        yield return new WaitForSeconds(0.1f);
        horse.transform.DOScaleY(0.4f, 0);
        yield return new WaitForSeconds(0.1f);
        horse.transform.DOScaleY(0.2f, 0);
        yield return new WaitForSeconds(0.1f);
        horse.transform.DOScaleY(0.6f, 0);
        yield return new WaitForSeconds(0.1f);
        horse.transform.DOScaleY(0.3f, 0);
        yield return new WaitForSeconds(0.1f);
        horse.transform.DOScaleY(0.8f, 0);
        yield return new WaitForSeconds(0.1f);
        horse.transform.DOScaleY(0.4f, 0);
        yield return new WaitForSeconds(0.1f);
        horse.transform.DOScaleY(1f, 0);
        yield return new WaitForSeconds(0.5f);
        NextString();
        Horse.transform.GetChild(0).gameObject.SetActive(true);
        dickimonSetState.DickimonFrameShake(2);
        dickimonSetState.EnemyHp.GetComponent<Animator>().SetTrigger("Reduce");
        dickimonSetState.EnemyHp.GetComponent<Animator>().enabled = true;

        float sec = 0.3f;
        for (int i = 0; i <= 50; i++)
        {
            if (i % 2 == 0)
            {
                horse.transform.localPosition = new Vector3(-0.67f, -1f, 0);
            }
            else
            {
                horse.transform.localPosition = new Vector3(0.03f, -1f, 0);
            }
            if (i == 50)
            {
                horse.transform.localPosition = new Vector3(-0.3199997f, -1f, 0);
            }
            yield return new WaitForSeconds(sec);
            sec -= 0.015f;
            if (sec <= 0)
            {
                sec = 0.05f;
            }
        }
        yield return new WaitForSeconds(0.3f);
        horse.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);//white
        horse.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() =>
        {
            dickimonSetState.EnemyHp.GetComponent<Animator>().SetTrigger("Transform");
        });
        horse.transform.GetChild(1).gameObject.SetActive(true);//ExplosionFx
        // ======================
        // 變身音效
        // 被動_迪可夢_木馬病毒變身
        audioManager.HorseTransform_Dickimon();
        // 觸發文本
        ch2DialogueManager.ShowNextDialogue("木馬變身成木馬終結者", true);
        // 變更bgm
        bgmManager.StopCH2BGM();
        bgmManager.PlaySecondHorseBgm();
        // 被動_迪可夢_木馬終結者叫聲
        audioManager.SecondHorseShout_Dickimon();
        // ======================
        dickimonSetState.WoodBoard.transform.Find("ExplosionFx").gameObject.SetActive(true);
        Destroy(dickimonSetState.WoodBoard.transform.Find("wood_1").gameObject);
        horse.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/dm_horsekiller1");
        GameObject weapon = horse.transform.GetChild(0).GetChild(0).gameObject;
        weapon.SetActive(true);
        yield return new WaitForSeconds(1f);
        Sequence s = DOTween.Sequence();
        s.Append(weapon.transform.DOLocalMoveY(0.122f, 3f));
        s.Insert(0.6f, weapon.transform.GetComponent<SpriteRenderer>().DOFade(1, 0).OnComplete(() =>
        {
            weapon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/dm_horse_weapon_mid");
        }));

        s.Insert(1.2f, weapon.transform.GetComponent<SpriteRenderer>().DOFade(1, 0).OnComplete(() =>
        {
            weapon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/dm_horse_weapon_long");
            dickimonSetState.ChangeEnemy("木馬終結者");
            dickimonSetState.ToSetDickmonState(true);
        }));
        yield return new WaitForSeconds(4f);
        NextString();
        yield return new WaitForSeconds(1.5f);
        NextString();
        GameObject HypeBean = horse.transform.GetChild(0).GetChild(2).gameObject;
        HypeBean.SetActive(true);
        HypeBean.GetComponent<Animator>().SetTrigger("Normal");
        yield return new WaitForSeconds(1.75f);
        NextString();
        dickimonSetState.XiaoDone = true;//設定讓dia無法nextstring
        ch2VisualManager.HorseAttackandCharacterDie("XiaoMing");
        Anicontrol(0);
        Destroy(horse.transform.GetChild(1).gameObject);//destroyExplosionFx
        // ================================
        // 木板碎掉音效
        audioManager.DestroyWoodBoard_Dickimon();
        // ================================
        // ================================
        // 觸發文本
        ch2DialogueManager.ShowNextDialogue("木馬打死浣熊與小明", true);
        // ================================
        Destroy(dickimonSetState.WoodBoard);//destroyWoodBoard
        BtnToBack.SetActive(true);//打開去背後的按鈕
        DestoryDickchu();

    }

    public void HorseBeamFShake()
    {
        dickimonSetState.DickimonFrameShake(3);
    }

    public void MyDickmonExplosion()
    {
        switch (dickimonSetState.nowCharater)
        {
            case "XiaoMing":
                dickimonSetState.GetchaedRac.SetTrigger("Die");
                break;
            case "BricksKiller":
                // 觸發文本
                ch2DialogueManager.ShowNextDialogue("木馬打死磚塊殺手", true);
                // =======================
                dickimonSetState.KillerDone = true;
                ch2VisualManager.HorseAttackandCharacterDie("Killer");
                dickimonSetState.DmBricksKiller.GetComponent<SpriteRenderer>().sprite = null;
                dickimonSetState.DmBricksKiller.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Plumber":
                // 觸發文本
                ch2DialogueManager.ShowNextDialogue("木馬打死水電工", true);
                // =======================
                dickimonSetState.PlumberDone = true;
                ch2VisualManager.HorseAttackandCharacterDie("Plumber");
                dickimonSetState.DmPlumber.GetComponent<SpriteRenderer>().sprite = null;
                dickimonSetState.DmPlumber.transform.GetChild(1).gameObject.SetActive(true);
                break;
        }
        // =================================
        // 檢查死亡狀態並觸發文本
        if (dickimonSetState.CheckCharDeadNum() == 3)
        {
            // 全部死亡
            ch2DialogueManager.ShowNextDialogue("木馬打死三位角色", true);
        }
        // =================================
        dickimonSetState.PlayergetHurt(31);
        dickimonSetState.changePlayerInformation();
        if (dickimonSetState.isPlayKillExplosion)
        {
            NextString();
            dickimonSetState.isPlayKillExplosion = false;
        }
    }
    //=====================Anicontorl=====================
    public void Anicontrol(int anistate)
    {
        if (anistate == 1)
        {
            if (dickimonSetState.isPlayingAni == false)
            {
                dickimonSetState.isPlayingAni = true;
                AniDonefalse();
            }
        }
        else
        {
            dickimonSetState.isPlayingAni = false;
            AniDoneTrue();
        }
    }
    //=====================DiaAutoPlaycontorl=====================
    public void DiaAutoPlayecontrol(int anistate)
    {
        if (anistate == 1)
        {
            dickimonSetState.AutoPlayingDia = true;
        }
        else
        {
            dickimonSetState.AutoPlayingDia = false;
        }
    }
    #endregion
    #region 打磚塊木馬過場動畫

    //控制視窗開關鈕
    public void DisableCloseBtn()
    {
        game2Manager.CloseBtn.enabled = false;
        game2Manager.isPlayAni = true;
        ch2VisualManager.ToBackBtn.enabled = false;
        AniDonefalse();
    }
    public void EnableCloseBtn()
    {
        game2Manager.CloseBtn.enabled = true;
        game2Manager.isPlayAni = false;
        ch2VisualManager.ToBackBtn.enabled = true;
        AniDoneTrue();
    }
    //==========================================
    public void GeneratePartical()//產生粒子特效
    {
        Instantiate(FramePartical, this.transform.position, this.transform.rotation);
    }
    public void HorseIntoBricksKiller()
    {
        Horse_Inside.enabled = true;
    }
    public void BlackBallTaken()
    {
        BlackBall.enabled = true;
    }
    public void HorseInsideWalk()
    {
        Horse_Inside.speed = 1;
    }
    public void HorseInsideStop()
    {
        Horse_Inside.speed = 0;
        game2Manager.BlackBallAni.speed = 1;
    }
    public void HorseOutsideGetOut()
    {
        Horse_Outside.SetTrigger("GetOut");
    }
    public void stopInsideAni()
    {
        BlackBall.speed = 0;
        Horse_Inside.speed = 0;
        // 打開對話視窗
        ch2VisualManager.KillerDialog.SetActive(true);
        // =============================
        // 切換BGM
        bgmManager.StopCH2BGM();
        bgmManager.PlayHorseCGBgm();
        // =============================
        AniDoneTrue();
    }
    /*public void Horse_Inside_Stop()
    {
        Horse_Inside.speed = 0;
    }*/
    public void CoinAppear()
    {
        // 觸發文本
        ch2DialogueManager.ShowNextDialogue("打開保險箱", true);
        // ==========================
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5;
    }
    public void SafeAnimPause()
    {
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteRenderer>().sprite = UnlockSafeSprite;
    }
    #endregion

    #region 文本相關
    public void DickichuReverseFirstCallDia()
    {
        if (ReverseDickchuFirst)
        {
            ch2DialogueManager.ShowNextDialogue("磚塊殺手第一次反彈一般迪可丘", true);
            ReverseDickchuFirst = false;
        }
    }
    public void BigDickichuReverseFirstCallDia()
    {
        ch2DialogueManager.ShowNextDialogue("磚塊殺手反彈巨大化迪可丘", true);
    }
    public void GetInToBreakSafeFirst_Dialogue()
    {
        ch2DialogueManager.ShowNextDialogue("第一次進入突破保險箱，木馬出現", true);
    }
    public void UsaHorseToPixel_Dialogue()
    {
        ch2DialogueManager.ShowNextDialogue("木馬變成像素風", true);
    }
    public void HorseTakeAwayBlackBall_Dialogue()
    {
        ch2DialogueManager.ShowNextDialogue("木馬把球拿走", true);
    }
    public void MingGetRaccoon_Dialogue()
    {
        ch2DialogueManager.ShowNextDialogue("小明扔出迪可球，收服浣熊", true);
    }
    public void HorseGetFired_Dialogue()
    {
        ch2DialogueManager.ShowNextDialogue("木馬被警視窗的火打中", true);
    }
    #endregion

    #region 主角動畫控制
    public void PlayerPullBrandFinish()
    {
        ch2Usermanager.isClickXiaoMing = false;
        ch2Usermanager.isClickBricksKiller = false;
        ch2Usermanager.isClickPlumber = false;
    }
    public void PlayerPutonBrandFinish()
    {
        ch2Usermanager.isPutOnXiaoMing = false;
        ch2Usermanager.isPutOnBricksKiller = false;
        ch2Usermanager.isPutOnPlumber = false;
        Ch2CursorFX.PlayerMovable = true;
    }
    public void PlayerUnLockFinish()
    {
        ch2Usermanager.isUnLockKey = false;
        ch2Usermanager.isUnLockCoin = false;
    }
    #endregion
    #region 水電工內的動畫

    public void GenerateTrashcan()
    {
        Vector3 TrashcanPos = new Vector3(this.transform.position.x - 1f, this.transform.position.y - 0.5f, this.transform.position.z);
        Instantiate(game3Manager.Trashcan, TrashcanPos, this.transform.rotation);
        //this.GetComponent<Animator>().SetBool("throw", false);
    }
    public void AraigumaToidle()
    {
        this.GetComponent<Animator>().SetBool("throw", false);
    }

    public void StopAraigumaAni()
    {
        game3Manager.AraigumaAni.SetBool("die", false);
        game3Manager.AraigumaAni.SetTrigger("die2");
        Game3Manager.araigumaDie = true;
    }

    #endregion

    #region 動畫音效
    public void AccWalk()
    {
        // 帳號_myth走路
        audioManager.AccWalk("Level2");
    }
    public void UsaHorseWalk_BreakSafe_Audio()
    {
        // 被動_突破保險箱_美式木馬走路
        audioManager.UsaHorseWalk_BreakSafe();
    }
    public void UsaHorseJump_BreakSafe_Audio()
    {
        // 被動_突破保險箱_美式木馬跳
        audioManager.UsaHorseJump_BreakSafe();
    }
    public void UsaHorseTriggerEnter_BreakSafe_Audio()
    {
        // 被動_突破保險箱_美式木馬進入遊戲
        audioManager.UsaHorseTriggerEnter_BreakSafe();
    }
    public void UsaHorseTriggerExit_BreakSafe()
    {
        // 被動_突破保險箱_美式木馬跑出遊戲
        audioManager.UsaHorseTriggerExit_BreakSafe();
    }
    public void PassHeadShowOut_Audio()
    {
        // 被動_突破保險箱_密碼出來
        audioManager.PassHeadShowOut();
    }
    public void PassHeadHideIn_Audio()
    {
        // 被動_突破保險箱_密碼進去
        audioManager.PassHeadHideIn();
    }
    public void PixedHorseGetBall_BreakSafe_Audio()
    {
        // 被動_突破保險箱_像素木馬抓到球
        audioManager.PixedHorseGetBall_BreakSafe();
    }
    public void BallGetStock_BreakSafe_Audio()
    {
        // 被動_突破保險箱_像素木馬球卡住
        audioManager.BallGetStock_BreakSafe();
    }
    public void PixelHorseJump_BreakSafe_Audio()
    {
        // 被動_突破保險箱_像素木馬球跳躍
        audioManager.PixelHorseJump_BreakSafe();
    }
    public void MingWalking_BreakSafe_Audio()
    {
        // 被動_突破保險箱_小明走路
        audioManager.MingWalking_BreakSafe();
    }
    public void MingAttack_BreakSafe_Audio()
    {
        // 點擊_突破保險箱_小明丟球
        audioManager.MingAttack_BreakSafe();
    }
    public void PumberWalking_BreakSafe_Audio()
    {
        // 被動_突破保險箱_水電工走路
        audioManager.PumberWalking_BreakSafe();
    }
    public void PumberJump_BreakSafe_Audio()
    {
        // 點擊_突破保險箱_水電工跳躍
        audioManager.PumberJump_BreakSafe();
    }
    public void SaferUnlock_BreakSafe_Audio()
    {
        // 被動_突破保險箱_保險箱開鎖
        audioManager.SaferUnlock_BreakSafe();
    }
    public void SaferOpen_BreakSafe_Audio()
    {
        // 被動_突破保險箱_保險箱打開
        audioManager.SaferOpen_BreakSafe();
    }
    public void OpenTV_Back_Audio()
    {
        if (!isTVOpen)
        {
            // 被動_myth_電視開啟
            audioManager.OpenTV_Back();
            isTVOpen = true;
            StartCoroutine(CloseTVBool());
        }
    }
    IEnumerator CloseTVBool()
    {
        yield return new WaitForSeconds(2f);
        isTVOpen = false;
    }
    public void AccGetBrand_Audio()
    {
        // 被動_帳號_拿名牌
        audioManager.AccGetBrand_Back();
    }
    public void AccPutBrand_Audio()
    {
        // 被動_帳號_放名牌
        audioManager.AccPutBrand_Back();
    }
    public void AccUnlockOrPutCoin_Back_Audio()
    {
        if (isUnlock)
        {
            // 被動_帳號_開鎖
            audioManager.AccUnlock_Back();
            isUnlock = false;
        }
        else
        {
            // 被動_帳號_投幣
            audioManager.AccPutCoin_Back();
        }
    }
    public void AccChainFall_Back_Audio()
    {
        // 被動_帳號_鎖鏈掉地上
        audioManager.AccChainFall_Back();
    }

    #region 迪可夢
    public void DickiShout_Dickimon()
    {
        // 被動_迪可夢_迪可丘叫聲(野生的迪可丘出現了)
        audioManager.DickiShout_Dickimon();
    }
    public void MingThrowBall_Dickimon()
    {
        // 被動_迪可夢_小明丟出愛心寶貝球(收服用)
        audioManager.MingThrowBall_Dickimon();
    }
    public void CallAniDickimon_Dickimon()
    {
        // 被動_迪可夢_招喚任何迪可夢
        audioManager.CallAniDickimon_Dickimon();
    }
    public void LongNeckDuckShoutAtBegin_Dickimon()
    {
        // 被動_迪可夢_長頸鴨出場叫聲
        audioManager.LongNeckDuckShoutAtBegin_Dickimon();
    }
    public void LongNeckDuckShoutAtBattle_Dickimon()
    {
        // 被動_迪可夢_長頸鴨出任何招叫聲
        audioManager.LongNeckDuckShoutAtBattle_Dickimon();
    }
    public void LongNeckDuckFall_Dickimon()
    {
        // 被動_迪可夢_長頸鴨跌倒
        audioManager.LongNeckDuckFall_Dickimon();
    }
    public void AnyDickimonHurt_Dickimon()
    {
        // 被動_迪可夢_任何角色受傷(閃爍)
        audioManager.AnyDickimonHurt_Dickimon();
    }
    public void LongNeckDuckShoutWhenHurt_Dickimon()
    {
        // 被動_迪可夢_長頸鴨受傷叫聲
        audioManager.LongNeckDuckShoutWhenHurt_Dickimon();
    }
    public void LongNeckDuckSprayWater_Dickimon()
    {
        // 被動_迪可夢_長頸鴨噴出水
        audioManager.LongNeckDuckSprayWater_Dickimon();
    }
    public void DickimonHurtByWater_Dickimon()
    {
        // 被動_迪可夢_迪可丘被噴水打到
        audioManager.DickimonHurtByWater_Dickimon();
    }
    public void DickimonBringBring_Dickimon()
    {
        // 被動_迪可夢_迪可丘閃亮閃亮
        audioManager.DickimonBringBring_Dickimon();
    }
    public void LongNeckDuckSprayBubble_Dickimon()
    {
        // 被動_迪可夢_長頸鴨噴泡泡
        audioManager.LongNeckDuckSprayBubble_Dickimon();
    }
    public void DickimonBubbleDisappear_Dickimon()
    {
        // 被動_迪可夢_迪可丘身上的泡泡消失
        audioManager.DickimonBubbleDisappear_Dickimon();
    }
    public void MingThrowBallToGet_Dickimon()
    {
        // 被動_迪可夢_小明丟出愛心寶貝球(收服用)
        audioManager.MingThrowBallToGet_Dickimon();
    }
    public void DickimonFlapBall_Dickimon()
    {
        // 被動_迪可夢_迪可丘拍走愛心寶貝球
        audioManager.DickimonFlapBall_Dickimon();
    }
    public void ReceiveReceive_Dickimon()
    {
        // 被動_迪可夢_長頸鴨收回
        // 被動_迪可夢_無情浣熊出被吸進球
        audioManager.ReceiveReceive_Dickimon();
    }
    public void DickimonShoutAtBattle_Dickimon()
    {
        // 被動_迪可夢_迪可丘出招叫聲
        audioManager.DickimonShoutAtBattle_Dickimon();
    }
    public void DickimonElecBallGathering_Dickimon()
    {
        // 被動_迪可夢_迪可丘電球集氣
        audioManager.DickimonElecBallGathering_Dickimon();
    }
    public void DickimonRun_Dickimon()
    {
        // 被動_迪可夢_迪可丘衝刺
        audioManager.DickimonRun_Dickimon();
    }
    public void DickimonHitTarget_Dickimon()
    {
        // 被動_迪可夢_迪可丘打中目標
        audioManager.DickimonHitTarget_Dickimon();
    }
    public void LongNeckDuckShoutWhenDead_Dickimon()
    {
        // 被動_迪可夢_長頸鴨死去叫聲
        audioManager.LongNeckDuckShoutWhenDead_Dickimon();
    }
    public void LongNeckDuckDown_Dickimon()
    {
        // 被動_迪可夢_長頸鴨下去
        audioManager.LongNeckDuckDown_Dickimon();
    }
    public void DickimonHurtByReverseAttack_Dickimon()
    {
        // 被動_迪可夢_巨大或小迪可丘被磚塊殺手反彈
        audioManager.DickimonHurtByReverseAttack_Dickimon();
    }
    public void BigDickmonFlyAway_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘飛走
        audioManager.BigDickmonFlyAway_Dickimon();
    }
    public void AfterBigDickmonFlyAway_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘飛走後
        audioManager.AfterBigDickmonFlyAway_Dickimon();
    }
    public void YouGetKey_Dickimon()
    {
        // 被動_迪可夢_你獲得了鑰匙!
        audioManager.YouGetKey_Dickimon();
    }
    public void PlumberJump_Dickimon()
    {
        // 被動_迪可夢_水電工跳躍
        audioManager.PlumberJump_Dickimon();
    }
    public void PlumberJumpAndShout_Dickimon()
    {
        // 被動_迪可夢_水電工跳起來叫聲
        audioManager.PlumberShoutAtBegin_Dickimon();
    }
    public void PlumberThrowMashroom_Dickimon()
    {
        // 被動_迪可夢_水電工扔蘑菇
        audioManager.PlumberThrowMashroom_Dickimon();
    }
    public void BigDickimonShout_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘叫聲
        audioManager.BigDickimonShout_Dickimon();
    }
    public void BigDickimonShoutAtBattle_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘出招叫聲
        audioManager.BigDickimonShoutAtBattle_Dickimon();
    }
    public void BigDickimonShoutBeforeHit_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘撞到之前的叫聲
        audioManager.BigDickimonShoutBeforeHit_Dickimon();
    }
    public void BigDickimonShoutWhenFlyAway_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘飛走叫聲
        audioManager.BigDickimonShoutWhenFlyAway_Dickimon();
    }
    public void PlumberHitByDickimonAndFlyAway_Dickimon()
    {
        // 被動_迪可夢_水電工被巨大迪可丘撞飛
        audioManager.PlumberHitByDickimonAndFlyAway_Dickimon();
    }
    public void PlumberHitByDickimonAndFlyAwayAndShout_Dickimon()
    {
        // 被動_迪可夢_水電工被巨大迪可丘撞飛大叫
        audioManager.PlumberHitByDickimonAndFlyAwayAndShout_Dickimon();
    }
    public void RaccoonAppear_Dickimon()
    {
        // 被動_迪可夢_無情浣熊出場叫聲
        audioManager.RaccoonAppear_Dickimon();
    }
    public void RaccoonFallDown_Dickimon()
    {
        // 被動_迪可夢_無情浣熊暈倒
        audioManager.RaccoonFallDown_Dickimon();
    }
    public void BallTriggerDickimon()
    {
        // 被動_迪可夢_打到迪可夢或迪可丘拍球
        audioManager.BallTriggerDickimon();
    }
    public void BallTriggerFloor_Dickimon()
    {
        // 被動_迪可夢_愛心寶貝球彈地
        audioManager.BallTriggerFloor_Dickimon();
    }
    public void BallShakeLeftAndRight_Dickimon()
    {
        // 被動_迪可夢_愛心寶貝球左右搖擺
        audioManager.BallShakeLeftAndRight_Dickimon();
    }
    public void GetDickimonSuccess_Dickimon()
    {
        // 被動_迪可夢_愛心寶貝球收服成功
        audioManager.GetDickimonSuccess_Dickimon();
    }
    public void BigDickimonFall_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘落下聲
        audioManager.BigDickimonFall_Dickimon();
    }
    public void BigDickimonHitHorse_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘砸到木馬病毒
        audioManager.BigDickimonHitHorse_Dickimon();
    }
    public void BigDickimonRunAway_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘跑走
        audioManager.BigDickimonRunAway_Dickimon();
    }
    public void HorseExplose_Dickimon()
    {
        // 被動_迪可夢_木馬病毒爆炸
        audioManager.HorseExplose_Dickimon();
    }
    public void DeleteDeadLight_Dickimon()
    {
        // 被動_迪可夢_刪除死光
        audioManager.DeleteDeadLight_Dickimon();
    }
    public void DeleteDeadLightShotUp_Dickimon()
    {
        // 被動_迪可夢_刪除死光往上射的
        audioManager.DeleteDeadLightShotUp_Dickimon();
    }
    public void HitByDeleteDeadLight_Dickimon()
    {
        // 被動_迪可夢_任何角色被刪除死光打中
        audioManager.HitByDeleteDeadLight_Dickimon();
    }
    public void DickimonShoutBeforeGetHit_Dickimon()
    {
        // 被動_迪可夢_迪可丘撞到前叫聲
        audioManager.DickimonShoutBeforeGetHit_Dickimon();
    }
    public void DickimonShoutAfterBounce_Dickimon()
    {
        // 被動_迪可夢_迪可丘被彈回去叫聲
        audioManager.DickimonShoutAfterBounce_Dickimon();
    }
    public void MythErrorShow_Dickimon()
    {
        // 被動_myth_error彈出
        audioManager.MythErrorShow_Dickimon();
    }
    public void MythErrorCloseFire_Dickimon()
    {
        // 被動_myth_error關閉火焰
        audioManager.MythErrorCloseFire_Dickimon();
    }
    public void SecondHorseHitByFire_Dickimon()
    {
        // 被動_迪可夢_木馬終結者被火焰打中
        audioManager.SecondHorseHitByFire_Dickimon();
    }
    public void SecondHorseFireBurn_Dickimon()
    {
        // 被動_迪可夢_木馬終結者身上的火焰燃燒
        audioManager.SecondHorseFireBurn_Dickimon();
    }
    public void SecondHorseExplose_Dickimon()
    {
        // 被動_迪可夢_木馬終結者爆炸
        audioManager.SecondHorseExplose_Dickimon();
    }
    public void YouDefeatTheSecondHorse_Dickimon()
    {
        // 被動_迪可夢_您打敗了木馬終結者!
        audioManager.YouDefeatTheSecondHorse_Dickimon();
    }
    #endregion
    #endregion
}
