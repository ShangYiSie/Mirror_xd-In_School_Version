using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float audioVolume = 0.5f;

    public bool MainChangeScreen = false;
    public AudioClip[] Labo;
    public AudioSource LoopAudio;
    public AudioSource BirdRunningAudio;

    public bool isChangeToFront = false;
    public bool callTVClose = false;

    public bool hanIsTalking = false;

    string[] HanAudio = new string[6] { "audio/amb_keepworkhard", "audio/amb_keepworkhard2", "audio/amb_keepworkhard3", "audio/amb_keepworkhard4", "audio/amb_keepworkhard5", "audio/amb_keepworkhard6" };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateIsChangeToFront(bool toOpen)
    {
        isChangeToFront = toOpen;
    }

    public void UpdateAudioVolume(float vol)
    {
        audioVolume = vol;
    }

    public float GetAudioVolume()
    {
        return audioVolume;
    }

    private void PlayAudio(AudioClip sound, float vol)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = vol;
        audioSource.clip = sound;
        audioSource.Play();
        Destroy(audioSource, sound.length + 0.1f);
    }

    private void PlayLoopAudio(AudioClip sound, float vol, float len)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = vol;
        audioSource.clip = sound;
        audioSource.loop = true;
        audioSource.Play();
        Destroy(audioSource, sound.length + len + 0.1f);
    }

    private void PlayLoopForeverAudio(AudioClip sound, float vol)
    {
        LoopAudio = gameObject.AddComponent<AudioSource>();
        LoopAudio.volume = vol;
        LoopAudio.clip = sound;
        LoopAudio.loop = true;
        LoopAudio.Play();
    }


    // =================================================

    public void DestroyLoopAudio()
    {
        Destroy(LoopAudio);
    }

    public void PlayMouseClickAudio()
    {
        PlayAudio((AudioClip)Resources.Load("audio/MouseClick"), audioVolume);
    }

    // ==============================================
    // Main

    public void MainPointerEnter()
    {
        if (MainChangeScreen)
        {
            MainChangeScreen = false;
        }
        else
        {
            PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Jump Tone"), audioVolume * 0.8f);
        }

    }

    public void MainButtonClick(bool changeScreen)
    {
        if (changeScreen)
        {
            // 讓PointerEnter暫停一下
            MainChangeScreen = true;
        }
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Open Menu 1"), audioVolume * 1.5f);
    }

    public void MainGoPrev()
    {
        // 讓PointerEnter暫停一下
        MainChangeScreen = true;
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Close Menu"), audioVolume * 1.5f);
    }

    public void MainLeaveGame()
    {
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Power Down 12"), audioVolume);
    }

    public void ClickPause()
    {
        // 開啟暫停
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Goofy Collect 1"), audioVolume);
    }

    public void ClosePause()
    {
        // 關閉暫停
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Close Menu"), audioVolume);
    }

    public void StartComputerAudio()
    {
        // 進入遊戲 （開機音效）
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Power On 12"), audioVolume * 1.2f);
    }
    public void ClickUserLoginAudio()
    {
        // 點擊開始畫面中的左下使用者(黃色浣熊)
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click 4"), audioVolume);
    }

    #region 第一關
    // ==============================================
    // Chapter 1

    public void CloseWindow()
    {
        // 關閉任何視窗
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Soft Button Close Menu"), audioVolume * 1.4f);
    }

    public void ClickWindowsIconOrDogIcon()
    {
        // 點擊桌面icon或狗說話
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click 3"), audioVolume * 2f);
    }

    public void ClickInternetIconOrShowDogSpeak()
    {
        // 點擊網路icon
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Click Confirm 3"), audioVolume);
    }

    public void ClickGoodMirror()
    {
        // 點擊好的鏡子
        PlayAudio((AudioClip)Resources.Load("audio/amb_knock"), audioVolume);
    }
    public void ClickBrokenMirror()
    {
        // 點擊破掉的鏡子
        PlayAudio((AudioClip)Resources.Load("audio/amb_switch1"), audioVolume);
    }
    public void HoverBrokenMirror()
    {
        // hover破掉的鏡子
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click 6"), audioVolume);
    }
    public void PropsTriggerSpace()
    {
        // 道具放到凹槽
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Deep Low Button 2"), audioVolume * 1.8f);
    }

    #region 小恐龍
    // 小恐龍
    public void ClickDinasor()
    {
        // 點擊小恐龍
        PlayAudio((AudioClip)Resources.Load("audio/amb_yee"), audioVolume);
    }
    public void DinasorEatX()
    {
        // 點擊x被小恐龍吃掉
        PlayAudio((AudioClip)Resources.Load("audio/amb_ Laser Shoot1"), audioVolume * 0.2f);
    }
    public void DinasorEatXReverse()
    {
        // 點擊x被小恐龍吃掉
        PlayAudio((AudioClip)Resources.Load("audio/amb_ Laser Shoot 2"), audioVolume * 0.2f);
    }
    public void DinasorSwallowX()
    {
        // 小恐龍吞下x
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Zip Rip 1"), audioVolume);
    }
    public void DinasorPutBricks()
    {
        // 放置磚塊
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Click Switch 1"), audioVolume * 1.3f);
    }
    public void DinasorJump()
    {
        // 小恐龍跳躍
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Jump 13"), audioVolume * 0.7f);
    }
    public void DinasorEnd()
    {
        // 小恐龍
        PlayAudio((AudioClip)Resources.Load("audio/amb_scratch"), audioVolume);
    }
    public void ClickMeteorite()
    {
        // 第一次點擊隕石音效
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Hit Impact 14"), audioVolume);
    }
    public void MetoriteHitGroundAudio()
    {
        // 隕石砸地板音效
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Hit Impact 7"), audioVolume);
    }
    public void MetoriteBroken()
    {
        // 隕石破掉音效
        PlayAudio((AudioClip)Resources.Load("audio/explosion_16"), audioVolume);
    }
    public void CityRising()
    {
        // 城市升起
        PlayAudio((AudioClip)Resources.Load("audio/amb_earthquake"), audioVolume * 1.7f);
    }
    public void MetoriteHitDinasor()
    {
        // 隕石砸小恐龍
        PlayAudio((AudioClip)Resources.Load("audio/amb_yeeeeeeeee"), audioVolume * 1.7f);
    }
    public void WifiConnect(float num)
    {
        // 網路連線
        switch (num)
        {
            case 0:
                PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Pluck Tonal Confirm Button 4"), audioVolume);
                break;
            case 1:
                PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Pluck Tonal Confirm Button 5"), audioVolume);
                break;
            case 2:
                PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Pluck Tonal Confirm Button 6"), audioVolume);
                break;
        }
    }
    public void MoveXToSpaceInBrowser()
    {
        // 在網路連線後把x放到空格
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Deep Low Button 2"), audioVolume);
    }
    #endregion

    // ========================
    #region CMail
    public void ShowAD()
    {
        // 顯示dickimon和釣魚信件 廣告
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Chirp Notification"), audioVolume);
    }
    public void ClickAD()
    {
        // 點擊Dickimon廣告
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Open Menu 2"), audioVolume);
    }
    public void ClickCMailDownload()
    {
        // 點擊cmail裡的dickimon下載
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Bubble 2"), audioVolume);
    }
    public void ClickBitcoin()
    {
        // 點擊bitcoin
        PlayAudio((AudioClip)Resources.Load("audio/amb_bitcoin"), audioVolume);
    }
    public void UTriggerBitcoin()
    {
        // 用U拖移比特幣
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Collect PickUp Coin"), audioVolume);
    }
    public void BirdSleep()
    {
        // 鳥睡覺
        PlayAudio((AudioClip)Resources.Load("audio/amb_gugusleep"), audioVolume);
    }
    public void ClickBird()
    {
        // 點擊鳥
        PlayAudio((AudioClip)Resources.Load("audio/amb_gugu"), audioVolume);
    }
    public void DogeCoinPointerRotate()
    {
        // cmail狗狗幣箭頭往下掉
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whistle Slide Down"), audioVolume);
    }
    public void DogeCry()
    {
        // 狗哭聲在吸狗狗幣那邊
        PlayAudio((AudioClip)Resources.Load("audio/amb_dogecry"), audioVolume * 1.5f);
    }
    #endregion
    // ========================
    #region Myth安裝
    public void ClickMythSetupAgreeOrDisagree()
    {
        // 點擊Myth安裝的同意或不同意
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Click Switch Swipe 1"), audioVolume);
    }
    public void ClickMythSetupDisagreeDotUp()
    {
        // 不同意黑點點上升音效
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Click Swipe 1"), audioVolume);
    }
    public void ClickMythSetupCancelOrX()
    {
        // 點擊Myth安裝會下墜的按鈕
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whistle Slide Down"), audioVolume);
    }
    public void ClickMythSetupNextOrFinish()
    {
        // 點擊安裝或結束
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Bubble 2"), audioVolume);
    }
    public void ShowMythSetupWarning()
    {
        // 警告視窗彈出
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Connect 2"), audioVolume * 1.5f);
    }
    public void MythSetupLoading()
    {
        // 播放myth setup loading音效
        PlayLoopForeverAudio((AudioClip)Resources.Load("audio/amb_laoding"), audioVolume);
    }
    #endregion
    // ========================
    #region 桌面（木馬病毒、防毒軟體）
    public void DesktopHorseLaugh()
    {
        // 桌面木馬病毒笑
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Lunar Speak"), audioVolume);
    }
    public void DesktopLinkElec()
    {
        // 桌面木馬接到電
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Collect Power"), audioVolume * 1.2f);
    }
    public void DesktopHorseLeave()
    {
        // 桌面木馬接完線離開
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Alert Notification Dive"), audioVolume * 1.6f);
    }
    public void DesktopTipsLight()
    {
        // 桌面提示亮
        PlayAudio((AudioClip)Resources.Load("audio/amb_ggggg"), audioVolume);
    }
    public void DesktopAntiSleep()
    {
        // 防毒軟體睡覺
        PlayAudio((AudioClip)Resources.Load("audio/amb_dogsleep"), audioVolume);
    }
    public void UTriggerPointer()
    {
        // 磁鐵吸引指針
        PlayAudio((AudioClip)Resources.Load("audio/amb_pointer1"), audioVolume);
    }
    #endregion
    // ========================
    #region 記事本（帳號密碼）
    public void ClickAandP()
    {
        // 點擊帳號和密碼
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Click Confirm 1"), audioVolume);
    }
    #endregion
    // ========================
    #region Myth
    public void ClickLogin()
    {
        // 點擊Login
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Bubble 2"), audioVolume);
    }
    // ====================================
    // 木馬動畫
    public void HorseShow()
    {
        // 木馬冒出來
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whoosh Whip 2"), audioVolume);
    }
    public void HorseTakeU()
    {
        // 木馬拔磁鐵
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Flip Clip Click"), audioVolume);
    }
    public void HorseLittleTearPass()
    {
        // 木馬小拉扯密碼牌
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whoosh Tight"), audioVolume);
    }
    public void HorseTearPass()
    {
        // 木馬大力拉扯密碼牌
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whoosh Muted Dull"), audioVolume);
    }
    public void HorseAbsorbPass()
    {
        // 木馬吸出密碼
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Bubble 3"), audioVolume);
    }
    public void PassFall()
    {
        // 密碼掉落
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whistle Slide Down"), audioVolume);
    }
    public void PassScreamHelp()
    {
        // 動畫_mtyh_密碼尖叫_叫救命
        PlayAudio((AudioClip)Resources.Load("audio/amb_help1"), audioVolume);
    }
    public void PassScreamGetPull()
    {
        // 動畫_mtyh_密碼尖叫_被拉走
        PlayAudio((AudioClip)Resources.Load("audio/amb_help2"), audioVolume);
    }
    public void PassScreamStuck()
    {
        // 動畫_mtyh_密碼尖叫_卡住
        PlayAudio((AudioClip)Resources.Load("audio/amb_help3"), audioVolume);
    }
    public void PassScreamGetInto()
    {
        // 動畫_mtyh_密碼尖叫_拉進去
        PlayAudio((AudioClip)Resources.Load("audio/amb_help4"), audioVolume);
    }
    public void PassJump()
    {
        // 密碼跳
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Jump Delay"), audioVolume);
    }
    public void HorseCatchPass()
    {
        // 怪手抓到密碼
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Machine"), audioVolume);
    }
    public void HorsePullbackPass()
    {
        // 拉回怪手
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whoosh Long"), audioVolume);
    }
    public void PassStuck()
    {
        // 密碼卡住
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click 33"), audioVolume);
    }
    public void PassGotFilled()
    {
        // 密碼被拉進去
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Zip Rip 2"), audioVolume);
    }
    public void HorseMoveLeft()
    {
        // 木馬往左進去
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Alert Notification Dive"), audioVolume);
    }
    public void HorseMoveUp()
    {
        // 木馬從上面出現
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whoosh Whip 2"), audioVolume);
    }
    public void HorseLaugh()
    {
        // 木馬笑
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Lunar Speak"), audioVolume);
    }
    public void HorseUseUToHitAcc()
    {
        // 木馬拿磁鐵碰帳號
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Whoosh Tight"), audioVolume);
    }
    public void ShowRedWarning()
    {
        // 紅燈閃
        PlayLoopAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Alert Alarm 1"), audioVolume, 1.8f);
    }
    public void HorseScared()
    {
        // 木馬驚嚇
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Machine"), audioVolume);
    }
    public void MythUHitWall()
    {
        // 磁鐵打牆壁
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click 1"), audioVolume * 1.7f);
    }
    public void MythUHitMirror()
    {
        // 磁鐵砸破玻璃
        PlayAudio((AudioClip)Resources.Load("audio/amb_broken"), audioVolume * 2f);
    }
    public void MythUAbsorbedByMirror()
    {
        // 磁鐵被吸進去
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Goofy Long Bendy Engage"), audioVolume);
    }
    // =============
    public void ClickGUESTandShowErr()
    {
        // 點擊Guest和產稱Err的音效
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Connect 2"), audioVolume);
    }
    public void OpenBlueLayerAudio()
    {
        // 打開藍骨頭音效
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Disconnect 10"), audioVolume * 1.3f);
    }
    #endregion
    // ========================
    #region Myth背面
    public void HangUpU()
    {
        // 舉起U
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Achievement Unlock 6"), audioVolume * 0.7f);
    }
    public void GUESTBroken()
    {
        // GUEST裂掉
        PlayAudio((AudioClip)Resources.Load("audio/amb_rock1"), audioVolume * 1.4f);
    }
    public void GUESTBrokenAndTear()
    {
        // GUEST裂掉並且破掉
        PlayAudio((AudioClip)Resources.Load("audio/amb_rock2"), audioVolume * 1.4f);
    }
    #endregion
    // ========================
    #region 工具列
    public void ClickProps()
    {
        // 點擊道具
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Click Switch Swipe 2"), audioVolume);
    }
    public void PropsTriggerBar()
    {
        if (!isChangeToFront)
        {
            // 物件掉進bar
            PlayAudio((AudioClip)Resources.Load("audio/amb_zip"), audioVolume);
        }
    }
    public void ClickConversionRecord()
    {
        // 點擊對話紀錄
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Bubble Pop"), audioVolume);
    }
    public void ShowBackBar()
    {
        // 顯示背面包包
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Deep Low Button 4"), audioVolume);
    }
    public void HideBackBar()
    {
        // 隱藏背面包包
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Deep Low Button 5"), audioVolume);
    }
    #endregion
    // ========================
    #region 瀏覽器
    public void GeegleLaugh()
    {
        // geelge笑
        PlayAudio((AudioClip)Resources.Load("audio/amb_lol"), audioVolume * 1.5f);
    }
    public void TurntableAudio()
    {
        // 轉轉盤音效
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Collect"), audioVolume);
    }
    public void TurntableFail()
    {
        // 轉盤失敗音效
        PlayAudio((AudioClip)Resources.Load("audio/amb_thecry"), audioVolume * 1.4f);
    }
    public void TurntableWin()
    {
        // 轉盤成功音效
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Achievement Basic 1"), audioVolume * 1.3f);
    }
    public void TurntableLight()
    {
        // 轉盤燈光音效
        PlayAudio((AudioClip)Resources.Load("audio/amb_dddd"), audioVolume);
    }
    public void FishingPointer()
    {
        // 釣魚網站指針
        PlayAudio((AudioClip)Resources.Load("audio/amb_pointer1"), audioVolume);
    }
    // ========================
    // 釣魚背面
    public void FisherSayYes()
    {
        // 釣魚妹說Yes
        PlayAudio((AudioClip)Resources.Load("audio/amb_yes"), audioVolume);
    }
    public void FisherSayNo()
    {
        // 釣魚妹說No
        PlayAudio((AudioClip)Resources.Load("audio/amb_no"), audioVolume);
    }
    public void FishingReceiveLine()
    {
        // 釣魚收線
        PlayAudio((AudioClip)Resources.Load("audio/amb_fishing1"), audioVolume);
    }
    public void PropsExitWater()
    {
        // 物品出水面
        PlayAudio((AudioClip)Resources.Load("audio/amb_exitwater"), audioVolume);
    }
    public void FisherThrow()
    {
        // 釣魚妹丟東西
        PlayAudio((AudioClip)Resources.Load("audio/amb_whoosh1"), audioVolume);
    }
    public void PropsTriggerWater()
    {
        // 釣魚妹丟東西落水
        PlayAudio((AudioClip)Resources.Load("audio/amb_bigwater"), audioVolume);
    }
    public void FisherPutLine()
    {
        // 釣魚妹甩魚鉤
        PlayAudio((AudioClip)Resources.Load("audio/amb_whoosh2"), audioVolume);
    }
    public void LineTriggerWater()
    {
        // 魚鉤碰到水
        PlayAudio((AudioClip)Resources.Load("audio/amb_littlewater"), audioVolume);
    }
    public void FisherThrowLine()
    {
        // 釣魚妹拋線
        PlayAudio((AudioClip)Resources.Load("audio/amb_fishing2"), audioVolume);
    }
    public void FisherReceiveGiftLine()
    {
        // 釣魚妹收禮物的線
        PlayAudio((AudioClip)Resources.Load("audio/amb_fishing3"), audioVolume);
    }
    public void FisherScared()
    {
        // 釣魚妹嚇到
        PlayAudio((AudioClip)Resources.Load("audio/amb_rrrr"), audioVolume);
    }
    public void FisherAbsorbedByGift()
    {
        // 釣魚妹被禮物怪吸進去
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Rip Zip Whoosh"), audioVolume);
    }
    public void GiftSpitG()
    {
        // 禮物怪吐G
        PlayAudio((AudioClip)Resources.Load("audio/amb_brurp"), audioVolume);
    }
    public void GiftSwim()
    {
        // 禮物怪游泳
        PlayAudio((AudioClip)Resources.Load("audio/amb_gift"), audioVolume);
    }
    #endregion
    // ========================
    #region 資料夾
    public void DoorKeeperWalk()
    {
        // 守門人走路
        PlayAudio((AudioClip)Resources.Load("audio/amb_peopleRunning"), audioVolume * 1.5f);
    }
    public void ClickBlackMan(GameObject obj)
    {
        if (!hanIsTalking)
        {
            // 關掉按鈕
            obj.SetActive(false);
            // 點擊韓導旁邊的黑人
            PlayAudio((AudioClip)Resources.Load("audio/amb_youknowwhattodonow"), audioVolume);
            hanIsTalking = true;
            StartCoroutine(WaitForHanTalking(obj));
        }
    }
    public void ClickHan(GameObject obj)
    {
        if (!hanIsTalking)
        {
            // 關掉按鈕
            obj.SetActive(false);
            // 觸發韓導的勉勵
            PlayAudio((AudioClip)Resources.Load(HanAudio[Random.Range(0, 6)]), audioVolume * 2f);
            hanIsTalking = true;
            StartCoroutine(WaitForHanTalking(obj));
        }
    }
    IEnumerator WaitForHanTalking(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        // 開啟按鈕
        obj.SetActive(true);
        // =============================
        hanIsTalking = false;
    }
    public void ClickShovel()
    {
        // 點擊鏟子
        PlayAudio((AudioClip)Resources.Load("audio/amb_shovel1"), audioVolume);
    }
    public void UTriggerShovel()
    {
        // U觸發鏟子
        PlayAudio((AudioClip)Resources.Load("audio/amb_shovel2"), audioVolume);
    }
    public void ShovelDigBomb()
    {
        // 鏟子挖炸彈
        PlayAudio((AudioClip)Resources.Load("audio/amb_shovel3"), audioVolume);
    }
    public void MoraLoseAudio()
    {
        // 播放 You lose qq 的音效
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Pluck Tonal Confirm Button 9"), audioVolume);
    }
    public void TheManFlick()
    {
        // 放t猛男彈手指
        PlayAudio((AudioClip)Resources.Load("audio/amb_flick"), audioVolume);
    }
    public void TheManFire()
    {
        // 放t猛男火焰
        PlayAudio((AudioClip)Resources.Load("audio/amb_milosfire"), audioVolume);
    }
    public void TheManBird()
    {
        // 放s猛男鴿子
        PlayAudio((AudioClip)Resources.Load("audio/amb_gugu"), audioVolume);
    }
    public void MoraWinAudio()
    {
        // 播放 You win 的音效
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Achievement Unlock 10"), audioVolume);
    }
    public void MoraBombExplodedAudio()
    {
        // 播放炸彈爆炸音效
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Explosion 20"), audioVolume);
    }
    public void TheManDizzy()
    {
        // 猛男被炸暈的聲音，循環到他倒下離開面畫
        PlayAudio((AudioClip)Resources.Load("audio/funny_fall_01"), audioVolume);
    }
    public void TheManEGetTrigger()
    {
        // 拿磁鐵吸milos的王冠的聲音
        PlayAudio((AudioClip)Resources.Load("audio/amb_milosuuuu"), audioVolume);
    }
    public void MoraTheManDown()
    {
        // 播放猛男倒地音效
        PlayAudio((AudioClip)Resources.Load("audio/amb_falldown"), audioVolume);
    }
    public void ClickDoorKeeper()
    {
        // 點擊看門人
        PlayAudio((AudioClip)Resources.Load("audio/amb_haha"), audioVolume);
    }
    public void DoorFatGuyClick()
    {
        // 胖子按遙控器
        PlayAudio((AudioClip)Resources.Load("audio/amb_beep"), audioVolume);
    }
    public void DoorShake()
    {
        // 門震動音效
        PlayAudio((AudioClip)Resources.Load("audio/amb_doorup1"), audioVolume * 1.3f);
    }
    public void DoorUp()
    {
        // 門開啟音效
        PlayAudio((AudioClip)Resources.Load("audio/amb_doorup2"), audioVolume * 1.4f);
    }
    public void ClickTheMan()
    {
        // 點擊那個男人音效
        PlayAudio((AudioClip)Resources.Load("audio/amb_milos_speaking"), audioVolume * 1.5f);
    }
    public void WormMoveAudio()
    {
        // 蚯蚓蠕動聲音
        PlayAudio((AudioClip)Resources.Load("audio/amb_pui"), audioVolume * 1.2f);
    }
    public void BirdRunning()
    {
        // 鳥走路
        BirdRunningAudio = gameObject.AddComponent<AudioSource>();
        BirdRunningAudio.volume = audioVolume;
        BirdRunningAudio.clip = (AudioClip)Resources.Load("audio/amb_bird_Running");
        BirdRunningAudio.Play();
        Destroy(BirdRunningAudio, BirdRunningAudio.clip.length + 0.1f);
    }
    public void BirdRunningStop()
    {
        Destroy(BirdRunningAudio);
    }
    public void BirdEatWorm()
    {
        // 鳥叼蚯蚓
        PlayAudio((AudioClip)Resources.Load("audio/amb_peck"), audioVolume);
    }
    public void UTriggerNumber()
    {
        // 吸數字跟旗子的音效(一個物件的一個音效)
        PlayAudio((AudioClip)Resources.Load("audio/amb_lilu"), audioVolume);
    }
    #endregion
    // ========================
    // 各種常用

    public void BtnClick()
    {
        // 點擊按鈕
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Bubble 1"), audioVolume);
    }
    public void BtnHover()
    {
        // hover按鈕
        PlayAudio((AudioClip)Resources.Load("audio/amb_hover"), audioVolume);
    }
    public void PickAnyProps()
    {
        // 撿起任何道具
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Collect PickUp 4"), audioVolume);
    }
    public void HoverAnyPropsAndZoomInInBack()
    {
        // 在背面hover各種道具
        PlayAudio((AudioClip)Resources.Load("audio/amb_hover"), audioVolume);
    }
    public void BackZoomInBtnClick()
    {
        // 在背面zoom in 按鈕點擊
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Bubble 2"), audioVolume);
    }
    public void HoverBlackhole()
    {
        // hover黑洞
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Dull Button Click"), audioVolume);
    }
    public void EnterBlackhole()
    {
        // 進入黑洞 回到正面
        PlayAudio((AudioClip)Resources.Load("audio/amb_switch2"), audioVolume * 0.5f);
    }
    public void DogSpeak()
    {
        // 狗說話
        PlayAudio(Labo[Random.Range(0, 3)], audioVolume * 1.8f);
    }
    public void AccWalk(string stage)
    {
        // 帳號走路
        switch (stage)
        {
            case "Desktop":
                PlayAudio((AudioClip)Resources.Load("audio/Footsteps/grass/Footsteps_Walk_Grass_Mono_" + Random.Range(1, 21).ToString()), audioVolume * 1.7f);
                break;
            case "CMail":
                PlayAudio((AudioClip)Resources.Load("audio/Footsteps/grass/Footsteps_Walk_Grass_Mono_" + Random.Range(1, 21).ToString()), audioVolume * 1.7f);
                break;
            case "Folder":
                PlayAudio((AudioClip)Resources.Load("audio/Footsteps/grass/Footsteps_Walk_Grass_Mono_" + Random.Range(1, 21).ToString()), audioVolume * 1.7f);
                break;
            case "Myth":
                PlayAudio((AudioClip)Resources.Load("audio/Footsteps/wood/Footsteps_Wood_Walk_" + Random.Range(1, 11).ToString()), audioVolume * 1.7f);
                break;
            case "Browser":
                PlayAudio((AudioClip)Resources.Load("audio/Footsteps/wood/Footsteps_Wood_Walk_" + Random.Range(1, 11).ToString()), audioVolume * 1.7f);
                break;
            case "Fishing":
                PlayAudio((AudioClip)Resources.Load("audio/Footsteps/snow/Footsteps_Snow_Walk_" + Random.Range(1, 8).ToString()), audioVolume * 1.7f);
                break;
            case "Level2":
                PlayAudio((AudioClip)Resources.Load("audio/Footsteps/wood/Footsteps_Wood_Walk_" + Random.Range(1, 11).ToString()), audioVolume * 1.7f);
                break;
        }
    }
    #endregion

    #region 第二關
    public void ClickDialogueNext()
    {
        // 點擊_突破保險箱_對話框
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Soft Button 1"), audioVolume);
    }
    public void OpenCantOpenedWindow()
    {
        // 點擊_myth_不可開啟遊戲的按鈕
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Button Click Morph Ring 7"), audioVolume);
    }
    #region 突破保險箱
    public void UsaHorseWalk_BreakSafe()
    {
        // 被動_突破保險箱_美式木馬走路
        PlayAudio((AudioClip)Resources.Load("audio/amb_pixelhorsewalkimg"), audioVolume);
    }
    public void UsaHorseJump_BreakSafe()
    {
        // 被動_突破保險箱_美式木馬跳
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Machine Bend Up"), audioVolume);
    }
    public void UsaHorseTriggerEnter_BreakSafe()
    {
        // 被動_突破保險箱_美式木馬進入遊戲
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Button Click Zip 2"), audioVolume);
    }
    public void UsaHorseTriggerExit_BreakSafe()
    {
        // 被動_突破保險箱_美式木馬跑出遊戲
        PlayAudio((AudioClip)Resources.Load("audio/amb_HorseExit"), audioVolume);
    }
    public void PassHeadShowOut()
    {
        // 被動_突破保險箱_密碼出來
        PlayAudio((AudioClip)Resources.Load("audio/amb_zip3"), audioVolume);
    }
    public void PassHeadHideIn()
    {
        // 被動_突破保險箱_密碼進去
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Zip Rip 2"), audioVolume);
    }
    public void PixedHorseGetBall_BreakSafe()
    {
        // 被動_突破保險箱_像素木馬抓到球
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click Machine"), audioVolume);
    }
    public void BallGetStock_BreakSafe()
    {
        // 被動_突破保險箱_像素木馬球卡住
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Button Click 33"), audioVolume);
    }
    public void PixelHorseJump_BreakSafe()
    {
        // 被動_突破保險箱_像素木馬球跳躍
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Jump 3"), audioVolume);
    }
    public void MingWalking_BreakSafe()
    {
        // 被動_突破保險箱_小明走路
        PlayAudio((AudioClip)Resources.Load("audio/amb_mingwalkimg"), audioVolume);
    }
    public void MingAttack_BreakSafe()
    {
        // 點擊_突破保險箱_小明丟球
        PlayAudio((AudioClip)Resources.Load("audio/cute_drop_01"), audioVolume);
    }
    public void MingBallBroken_BreakSafe()
    {
        // 被動_突破保險箱_小明球粉碎
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Hit Impact 5"), audioVolume);
    }
    public void PumberWalking_BreakSafe()
    {
        // 被動_突破保險箱_水電工走路
        PlayAudio((AudioClip)Resources.Load("audio/amb_plumberwalking"), audioVolume);
    }
    public void PumberJump_BreakSafe()
    {
        // 點擊_突破保險箱_水電工跳躍
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Jump 1"), audioVolume);
    }
    public void GreenBrickBroken_BreakSafe()
    {
        // 被動_突破保險箱_綠磚塊裂開
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Hit Impact 16"), audioVolume);
    }
    public void NormalBricksBroken_Break_Safe()
    {
        // 被動_突破保險箱_任何磚塊粉碎
        PlayAudio((AudioClip)Resources.Load("audio/explosion_12"), audioVolume * 1.5f);
    }
    public void SaferUnlock_BreakSafe()
    {
        // 被動_突破保險箱_保險箱開鎖
        PlayAudio((AudioClip)Resources.Load("audio/amb_unlocksafe"), audioVolume);
    }
    public void SaferOpen_BreakSafe()
    {
        // 被動_突破保險箱_保險箱打開
        PlayAudio((AudioClip)Resources.Load("audio/amb_opensafe"), audioVolume);
    }
    public void ClickCoin_BreakSafe()
    {
        // 點擊_突破保險箱_金幣
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Pickup Coin 19"), audioVolume);
    }
    #endregion

    #region 背面
    public void OpenTV_Back()
    {
        // 被動_myth_電視開啟
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Power Up Collect"), audioVolume * 0.7f);
    }
    public void CloseTV_Back()
    {
        if (!callTVClose)
        {
            // 被動_myth_電視關閉
            PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Collect Power"), audioVolume * 0.7f);
            callTVClose = true;
            StartCoroutine(WaitToSetTVClose());
        }
    }
    IEnumerator WaitToSetTVClose()
    {
        yield return new WaitForSeconds(2f);
        callTVClose = false;
    }
    public void AccGetBrand_Back()
    {
        // 被動_帳號_拿名牌
        PlayAudio((AudioClip)Resources.Load("audio/amb_TakeTheBrand"), audioVolume);
    }
    public void AccPutBrand_Back()
    {
        // 被動_帳號_放名牌
        PlayAudio((AudioClip)Resources.Load("audio/amb_PutTheBrand"), audioVolume);
    }
    public void AccUnlock_Back()
    {
        // 被動_帳號_開鎖
        PlayAudio((AudioClip)Resources.Load("audio/amb_unlock"), audioVolume * 2);
    }
    public void AccPutCoin_Back()
    {
        // 被動_帳號_投幣
        PlayAudio((AudioClip)Resources.Load("audio/amb_putcoin"), audioVolume * 2);
    }
    public void AccChainFall_Back()
    {
        // 被動_帳號_鎖鏈掉地上
        PlayAudio((AudioClip)Resources.Load("audio/amb_chainfall"), audioVolume * 2);
    }
    #endregion

    #region 無情浣熊
    public void PlumberUseLadder_Raccoon()
    {
        // 被動_無情浣熊_水電工拿放梯子
        PlayAudio((AudioClip)Resources.Load("audio/Open_01"), audioVolume);
    }
    public void HitByTrashCan_Raccoon()
    {
        // 被動_無情浣熊_小明、水電工、浣熊碰到垃圾桶
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Explosion 20"), audioVolume);
    }
    public void CharacterDead_Raccoon()
    {
        // 被動_無情浣熊_小明、水電工死亡
        PlayAudio((AudioClip)Resources.Load("audio/stepped_fall_01"), audioVolume);
    }
    // ==========
    public void RaccoonFallDown_Raccoon()
    {
        // 被動_無情浣熊_浣熊暈倒
        PlayAudio((AudioClip)Resources.Load("audio/funny_fall_01"), audioVolume);
    }
    public void MingGoTopLevel2_Raccoon()
    {
        // 被動_無情浣熊_小明通往第二層
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Pickup Coin 10"), audioVolume);
    }
    public void MythJump_Raccoon()
    {
        // 被動_無情浣熊_MYTH跳
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Stretch Bend Jump"), audioVolume);
    }
    public void MythJumpIntoGame_Raccoon()
    {
        // 被動_無情浣熊_MYTH進入遊戲
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Button Click Zip 2"), audioVolume);
    }
    public void PixelMythJump_Raccoon()
    {
        // 被動_無情浣熊_像素MYTH跳
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Jump 1"), audioVolume);
    }
    public void Noise_Raccoon()
    {
        // 被動_無情浣熊_雜訊
        // PlayAudio((AudioClip)Resources.Load("audio/"), audioVolume);
    }
    #endregion

    #region 迪可夢
    public void HoverOption_Dickimon()
    {
        // hover_迪可夢_任何選項
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Blip Select 18"), audioVolume * 0.6f);
    }
    public void ClickAnyBtnMain_Dickimon()
    {
        // 點擊_迪可夢_任何選項(除了否、返回、逃跑)
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Button Click Morph Ring 9"), audioVolume);
    }
    public void ClickBtnSecondary_Dickimon()
    {
        // 點擊_迪可夢_否、返回
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Button Click Morph Ring 1"), audioVolume);
    }
    public void UselessProps_Dickimon()
    {
        // 點擊_迪可夢_用剩的道具(蘑菇)
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Button Click Morph Ring 6"), audioVolume);
    }
    public void RunAway_Dickimon()
    {
        // 點擊_迪可夢_逃跑
        PlayAudio((AudioClip)Resources.Load("audio/Hit_00"), audioVolume);
    }
    public void ClickNextDialogue_Dickimon()
    {
        // 點擊_迪可夢_對話下一句(有紅色三角型的)
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Button Click Morph Ring 9"), audioVolume);
    }
    public void DickiShout_Dickimon()
    {
        // 被動_迪可夢_迪可丘叫聲(野生的迪可丘出現了)
        PlayAudio((AudioClip)Resources.Load("audio/amb_dickichu1"), audioVolume);
    }
    public void MingThrowBall_Dickimon()
    {
        // 被動_迪可夢_小明丟出愛心寶貝球
        PlayAudio((AudioClip)Resources.Load("audio/Zap_C_06_8-Bit_11025Hz"), audioVolume * 2f);
    }
    public void CallAniDickimon_Dickimon()
    {
        // 被動_迪可夢_招喚任何迪可夢
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Powerup 7"), audioVolume);
    }
    public void LongNeckDuckShoutAtBegin_Dickimon()
    {
        // 被動_迪可夢_長頸鴨出場叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_duck1"), audioVolume * 2f);
    }
    public void LongNeckDuckShoutAtBattle_Dickimon()
    {
        // 被動_迪可夢_長頸鴨出任何招叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_duck3"), audioVolume);
    }
    public void LongNeckDuckFall_Dickimon()
    {
        // 被動_迪可夢_長頸鴨跌倒
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Hit Impact 5"), audioVolume);
    }
    public void AnyDickimonHurt_Dickimon()
    {
        // 被動_迪可夢_任何角色受傷(閃爍)
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Attack"), audioVolume);
    }
    public void LongNeckDuckShoutWhenHurt_Dickimon()
    {
        // 被動_迪可夢_長頸鴨受傷叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_duck4"), audioVolume);
    }
    public void LongNeckDuckSprayWater_Dickimon()
    {
        // 被動_迪可夢_長頸鴨噴出水
        PlayAudio((AudioClip)Resources.Load("audio/amb_shootwater"), audioVolume);
    }
    public void DickimonHurtByWater_Dickimon()
    {
        // 被動_迪可夢_迪可丘被噴水打到
        PlayAudio((AudioClip)Resources.Load("audio/amb_water"), audioVolume);
    }
    public void DickimonBringBring_Dickimon()
    {
        // 被動_迪可夢_迪可丘閃亮閃亮
        PlayAudio((AudioClip)Resources.Load("audio/amb_twinkle1"), audioVolume);
    }
    public void LongNeckDuckSprayBubble_Dickimon()
    {
        // 被動_迪可夢_長頸鴨噴泡泡
        PlayAudio((AudioClip)Resources.Load("audio/amb_bubble"), audioVolume);
    }
    public void DickimonBubbleDisappear_Dickimon()
    {
        // 被動_迪可夢_迪可丘身上的泡泡消失
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Bubble Pop"), audioVolume);
    }
    public void MingThrowBallToGet_Dickimon()
    {
        // 被動_迪可夢_小明丟出愛心寶貝球(收服用)
        PlayAudio((AudioClip)Resources.Load("audio/Zap_C_06_8-Bit_11025Hz"), audioVolume);
    }
    public void DickimonFlapBall_Dickimon()
    {
        // 被動_迪可夢_迪可丘拍走愛心寶貝球
        PlayAudio((AudioClip)Resources.Load("audio/short_noise_zap_rnd_01"), audioVolume);
    }
    public void ReceiveReceive_Dickimon()
    {
        // 被動_迪可夢_長頸鴨收回
        // 被動_迪可夢_無情浣熊出被吸進球
        PlayAudio((AudioClip)Resources.Load("audio/amb_intoball"), audioVolume);
    }
    public void DickimonShoutAtBattle_Dickimon()
    {
        // 被動_迪可夢_迪可丘出招叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_dickichu2"), audioVolume * 2f);
    }
    public void DickimonElecBallGathering_Dickimon()
    {
        // 被動_迪可夢_迪可丘電球集氣
        PlayAudio((AudioClip)Resources.Load("audio/powerup_7"), audioVolume);
    }
    public void DickimonRun_Dickimon()
    {
        // 被動_迪可夢_迪可丘衝刺
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Whoosh Long Pew"), audioVolume);
    }
    public void DickimonHitTarget_Dickimon()
    {
        // 被動_迪可夢_迪可丘打中目標
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Hit Impact 13"), audioVolume);
    }
    public void LongNeckDuckShoutWhenDead_Dickimon()
    {
        // 被動_迪可夢_長頸鴨死去叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_duck2"), audioVolume);
    }
    public void LongNeckDuckDown_Dickimon()
    {
        // 被動_迪可夢_長頸鴨下去
        PlayAudio((AudioClip)Resources.Load("audio/amb_down"), audioVolume);
    }
    public void DickimonHurtByReverseAttack_Dickimon()
    {
        // 被動_迪可夢_巨大或小迪可丘被磚塊殺手反彈
        PlayAudio((AudioClip)Resources.Load("audio/clean_short_jump_01"), audioVolume);
    }
    public void BigDickmonFlyAway_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘飛走
        PlayAudio((AudioClip)Resources.Load("audio/amb_flyaway"), audioVolume);
    }
    public void AfterBigDickmonFlyAway_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘飛走後
        PlayAudio((AudioClip)Resources.Load("audio/amb_twinkle2"), audioVolume);
    }
    public void KeyFallDownTriggerFloor_Dickimon()
    {
        // 被動_迪可夢_鑰匙掉下碰到地板
        PlayAudio((AudioClip)Resources.Load("audio/coin_1"), audioVolume);
    }
    public void ClickDickimonKey_Dickimon()
    {
        // 點擊_迪可夢_鑰匙
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Pickup Coin 15"), audioVolume);
    }
    public void YouGetKey_Dickimon()
    {
        // 被動_迪可夢_你獲得了鑰匙!
        PlayAudio((AudioClip)Resources.Load("audio/Jingle_Win_00"), audioVolume);
    }
    public void PlumberShoutAtBegin_Dickimon()
    {
        // 被動_迪可夢_水電工出場叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_plumber"), audioVolume * 2f);
    }
    public void PlumberJump_Dickimon()
    {
        // 被動_迪可夢_水電工跳躍
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Jump 3"), audioVolume);
    }
    public void PlumberThrowMashroom_Dickimon()
    {
        // 被動_迪可夢_水電工扔蘑菇
        PlayAudio((AudioClip)Resources.Load("audio/Zap_C_06_8-Bit_11025Hz"), audioVolume);
    }
    public void DickimonEatMashroomAndGrowUp_Dickimon()
    {
        // 被動_迪可夢_迪可丘吃蘑菇變大
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Powerup 11"), audioVolume);
    }
    public void BigDickimonShout_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_bigdick1"), audioVolume);
    }
    public void BigDickimonShoutAtBattle_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘出招叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_bigdick2"), audioVolume);
    }
    public void BigDickimonShoutBeforeHit_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘撞到之前的叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_bigdick3"), audioVolume);
    }
    public void BigDickimonShoutWhenFlyAway_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘飛走叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_bigdick4"), audioVolume);
    }
    public void PlumberHitByDickimonAndFlyAway_Dickimon()
    {
        // 被動_迪可夢_水電工被巨大迪可丘撞飛
        PlayAudio((AudioClip)Resources.Load("audio/Hit_03"), audioVolume);
    }
    public void PlumberHitByDickimonAndFlyAwayAndShout_Dickimon()
    {
        // 被動_迪可夢_水電工被巨大迪可丘撞飛大叫
        PlayAudio((AudioClip)Resources.Load("audio/amb_WilhelmScream"), audioVolume);
    }
    public void RaccoonAppear_Dickimon()
    {
        // 被動_迪可夢_無情浣熊出場叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_raccoon"), audioVolume * 10f);
    }
    public void RaccoonFallDown_Dickimon()
    {
        // 被動_迪可夢_無情浣熊暈倒
        PlayAudio((AudioClip)Resources.Load("audio/funny_fall_01"), audioVolume);
    }
    public void BallTriggerFloor_Dickimon()
    {
        // 被動_迪可夢_愛心寶貝球彈地
        PlayAudio((AudioClip)Resources.Load("audio/amb_loveball1"), audioVolume);
    }
    public void BallTriggerDickimon()
    {
        // 被動_迪可夢_打到迪可夢或迪可丘拍球
        PlayAudio((AudioClip)Resources.Load("audio/amb_loveball2"), audioVolume);
    }
    public void BallShakeLeftAndRight_Dickimon()
    {
        // 被動_迪可夢_愛心寶貝球左右搖擺
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Soft Button 1"), audioVolume);
    }
    public void GetDickimonSuccess_Dickimon()
    {
        // 被動_迪可夢_愛心寶貝球收服成功
        PlayAudio((AudioClip)Resources.Load("audio/Jingle_Win_00"), audioVolume);
    }
    public void HorseShoutAtBegin_Dickimon()
    {
        // 被動_迪可夢_木馬病毒出場叫聲
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Lunar Speak"), audioVolume);
    }
    public void BigDickimonFall_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘落下聲
        PlayAudio((AudioClip)Resources.Load("audio/bomb_drop_01"), audioVolume);
    }
    public void BigDickimonHitHorse_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘砸到木馬病毒
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Explosion 16"), audioVolume);
    }
    public void BigDickimonRunAway_Dickimon()
    {
        // 被動_迪可夢_巨大迪可丘跑走
        PlayAudio((AudioClip)Resources.Load("audio/amb_bigbeep"), audioVolume);
    }
    public void HorseExplose_Dickimon()
    {
        // 被動_迪可夢_木馬病毒爆炸
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Explosion 16"), audioVolume);
    }
    public void HorseTransform_Dickimon()
    {
        // 被動_迪可夢_木馬病毒變身
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Explosion 18"), audioVolume);
    }
    public void SecondHorseShout_Dickimon()
    {
        // 被動_迪可夢_木馬終結者叫聲
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Machine Power Down"), audioVolume);
    }
    public void DeleteDeadLight_Dickimon()
    {
        // 被動_迪可夢_刪除死光
        PlayAudio((AudioClip)Resources.Load("audio/rocket_lift_off_rnd_06"), audioVolume);
    }
    public void DeleteDeadLightShotUp_Dickimon()
    {
        // 被動_迪可夢_刪除死光往上射的
        PlayAudio((AudioClip)Resources.Load("audio/amb_rocket"), audioVolume);
    }
    public void HitByDeleteDeadLight_Dickimon()
    {
        // 被動_迪可夢_任何角色被刪除死光打中
        PlayAudio((AudioClip)Resources.Load("audio/randomize_12"), audioVolume);
    }
    public void SetWoodBoard_Dickimon()
    {
        // 被動_迪可夢_木牌出現
        PlayAudio((AudioClip)Resources.Load("audio/amb_wood1"), audioVolume * 1.5f);
    }
    public void DestroyWoodBoard_Dickimon()
    {
        // 被動_迪可夢_木牌碎掉
        PlayAudio((AudioClip)Resources.Load("audio/amb_wood2"), audioVolume * 1.5f);
    }
    public void ClickRunAway_Dickimon()
    {
        // 被動_迪可夢_點逃跑變成煙
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Blip Select 18"), audioVolume);
    }
    public void DickimonShoutBeforeGetHit_Dickimon()
    {
        // 被動_迪可夢_迪可丘撞到前叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_dickichu3"), audioVolume);
    }
    public void DickimonShoutAfterBounce_Dickimon()
    {
        // 被動_迪可夢_迪可丘被彈回去叫聲
        PlayAudio((AudioClip)Resources.Load("audio/amb_dickichu4"), audioVolume);
    }
    public void MythErrorShow_Dickimon()
    {
        // 被動_myth_error彈出
        PlayAudio((AudioClip)Resources.Load("audio/Analog Synthesized UI - Connect 2"), audioVolume);
    }
    public void MythErrorCloseFire_Dickimon()
    {
        // 被動_myth_error關閉火焰
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Magic Fire Spell"), audioVolume);
    }
    public void SecondHorseHitByFire_Dickimon()
    {
        // 被動_迪可夢_木馬終結者被火焰打中
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Magic Attack"), audioVolume);
    }
    public void SecondHorseFireBurn_Dickimon()
    {
        // 被動_迪可夢_木馬終結者身上的火焰燃燒
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Magic Fire Spell"), audioVolume);
    }
    public void SecondHorseExplose_Dickimon()
    {
        // 被動_迪可夢_木馬終結者爆炸
        PlayAudio((AudioClip)Resources.Load("audio/explosion_21"), audioVolume);
    }
    public void YouDefeatTheSecondHorse_Dickimon()
    {
        // 被動_迪可夢_您打敗了木馬終結者!
        PlayAudio((AudioClip)Resources.Load("audio/Jingle_Win_00"), audioVolume);
    }
    public void MingIntoBattle_Raccoon()
    {
        // 被動_無情浣熊_小明進入對戰
        PlayAudio((AudioClip)Resources.Load("audio/The Chris Alan - 8-Bit SFX & UI - Powerup 2"), audioVolume);
    }
    public void MingBackFromBattle_Raccoon()
    {
        // 被動_無情浣熊_小明對戰回來
        PlayAudio((AudioClip)Resources.Load("audio/amb_powerup 2"), audioVolume);
    }
    public void TrashcanTriggerBricks_Raccoon()
    {
        // 被動_無情浣熊_磚塊殺手碰到圾垃桶
        PlayAudio((AudioClip)Resources.Load("audio/amb_ballhit2"), audioVolume);
    }
    public void TrashcanTriggerWall_Raccoon()
    {
        // 被動_無情浣熊_磚塊殺手碰到的圾垃桶碰到牆壁
        PlayAudio((AudioClip)Resources.Load("audio/amb_ballhit1"), audioVolume);
    }
    #endregion

    #endregion

}
