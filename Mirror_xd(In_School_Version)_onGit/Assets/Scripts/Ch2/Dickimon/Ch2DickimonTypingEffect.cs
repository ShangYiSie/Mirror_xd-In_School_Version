using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ch2DickimonTypingEffect : MonoBehaviour
{
    Ch2DickimonSetState DickimonSetState;
    AudioManager audioManager;

    Ch2CursorSetting cursorSetting;

    public Animator XiaoAtor;

    public Animator DmduckAtor;

    public Animator DmDickchuAtor;

    public Animator DmPlumber;

    string[] Dialog;

    bool horsebeburned = false;

    public GameObject DiaButton;

    public Text[] skillPPcounter;
    // ============================================================
    public float charsPerSecond = 1f;//打字時間間隔
    private string words;//保存需要顯示的文字
    private bool isActive = false;
    private float timer;//計時器
    private Text myText;
    private int currentPos = 0;//當前打字位置

    private int currentSentence = 0;

    public GameObject Twodialogobj;

    public string[] DickimonAtkFeedbackstr;

    void Awake()
    {
        DickimonSetState = GameObject.Find("DickimonGame").GetComponent<Ch2DickimonSetState>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();

        myText = GetComponent<Text>();
        isActive = false;
    }

    // Use this for initialization
    void Start()
    {
        timer = 0;
        // isActive = true;
        // charsPerSecond = Mathf.Max(0.001f, charsPerSecond);
        // myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        OnStartWriter();
    }

    public void StartEffect()
    {
        isActive = true;
    }
    /// <summary>
    /// 打字
    /// </summary>
    void OnStartWriter()
    {
        if (isActive)
        {
            DiaButton.SetActive(false);
            // if (words == "迪可丘使用福特攻擊!")
            // {
            //     DickimonSetState.isPlayingAni = false;
            // }
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//刷新文本顯示内容

                if (currentPos >= words.Length)
                {
                    if (words == "迪可丘使用福特攻擊!")
                    {
                        if (DickimonSetState.nowEnemy == "迪可丘")
                        {
                            if (DickimonSetState.nowCharater == "Plumber")
                            {
                                DickimonSetState.EnemyAtkFx.GetComponent<Animator>().SetBool("AtkPlumber", true);
                            }
                            else if (DickimonSetState.nowCharater == "BricksKiller")
                            {
                                DickimonSetState.EnemyAtkFx.GetComponent<Animator>().SetBool("AtkBrick", true);
                            }
                            else
                            {
                                DickimonSetState.EnemyAtkFx.GetComponent<Animator>().SetBool("FlashAtk", true);
                            }

                        }
                        else
                        {
                            if (DickimonSetState.nowCharater == "BricksKiller")
                            {
                                DickimonSetState.EnemyAtkFx.GetComponent<Animator>().SetBool("LAtkBrick", true);
                            }
                            else
                            {
                                DickimonSetState.EnemyAtkFx.GetComponent<Animator>().SetBool("LFlashAtk", true);
                            }
                        }

                    }
                    else if (words == "效果群拔!" || words == "長頸鴨倒下了!" || words == "回來吧!長頸鴨" + DickimonSetState.NowDuckamount + "!")
                    {
                        OnFinish();
                        DickimonSetState.AutoPlayingDia = true;
                        StartCoroutine(WaitForNextstr(1.2f));
                        return;
                    }
                    else if (words == "無情浣熊暈倒了!")
                    {
                        DickimonSetState.isPlayingAni = false;
                        DickimonSetState.AutoPlayingDia = false;
                        Ch2AniEvents.AniDone = true;
                        cursorSetting.StopCursorHourglassAni();
                    }
                    else if (words == "哇!超有型與最厲害的木馬病毒出現了!")
                    {
                        StartCoroutine(autoHorseunablemirror());
                    }

                    if (DickimonSetState.AutoPlayingDia)
                    {
                        this.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (words == "小明使用愛心寶貝球!" && DickimonSetState.nowEnemy == "無情浣熊")
                    {
                        this.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (words == "迪可丘使用福特攻擊!" || words == "錯誤:未讀取到角色" || words == "迪可丘身上的鑰匙掉下來了!")
                    {
                        this.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (words != "水電工被巨大的迪可丘撞飛了!!" && words != "你獲得了鑰匙!")
                    {
                        this.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    OnFinish();
                }
            }

        }
    }
    /// <summary>
    /// 结束打字，初始化数据
    /// </summary>
    void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
        DiaButton.SetActive(true);
    }

    public bool isFinished()
    {
        return !isActive;
    }

    public void NextString()
    {

        if (!DickimonSetState.isPlayingAni && !DickimonSetState.AutoPlayingDia && !isActive)
        {

            if (DickimonSetState.nowCharater == "" && DickimonSetState.nowEnemy != "木馬終結者")
            {
                return;
            }
            else if (DickimonSetState.nowEnemy == "木馬終結者" && !horsebeburned && DickimonSetState.nowCharater == "")
            {
                return;
            }
            // ========================
            // 播放點擊音效
            audioManager.ClickNextDialogue_Dickimon();
            // ========================
            if (DickimonSetState.nowCharater == "XiaoMing" && DickimonSetState.XiaoDone)
            {
                return;
            }
            else if (DickimonSetState.nowCharater == "BricksKiller" && DickimonSetState.KillerDone)
            {
                return;
            }
            else if (DickimonSetState.nowCharater == "Plumber" && DickimonSetState.PlumberDone)
            {
                return;
            }
            this.transform.GetChild(0).gameObject.SetActive(false);

            if (istypingfeedback)
            {

                if (words == "已經沒有愛心蘑菇了!")
                {
                    Twodialogobj.SetActive(true);
                    this.gameObject.transform.parent.gameObject.SetActive(false);
                    Ch2DickimonLeftdiatyping leftdiatyping = Twodialogobj.transform.Find("Left_Dialog").GetComponent<Ch2DickimonLeftdiatyping>();
                    leftdiatyping.typDefault();
                }
                else if (words == "迪可丘變巨大了!")
                {
                    // DickimonSetState.ChangeEnemy("巨大迪可丘");
                    DickimonSetState.ToSetDickmonState(false);
                    string[] dia = { "迪可丘使用福特攻擊!", "水電工被巨大的迪可丘撞飛了!!" };
                    DickimonSetState.PlumberisAvailable = false;
                    ImportDialog(dia, true);
                }
                else if (words == "對迪可丘使用愛心蘑菇!")
                {
                    string dia = "迪可丘變巨大了!";
                    TypingFeedback(dia);
                }
                else if (words == "你獲得了鑰匙!" || words == "迪可丘身上的鑰匙掉下來了!")
                {
                    return;
                    // Debug.Log("第一階段結束");
                }
                else if (words == "長頸鴨對迪可丘使用捨身衝撞!")
                {
                    string dia = "呱!長頸鴨滑倒了!";
                    TypingFeedback(dia);
                }
                else if (words == "長頸鴨對迪可丘使用噴水!")
                {
                    string dia = "迪可丘變乾淨了!";
                    TypingFeedback(dia);
                }
                else if (words == "長頸鴨對迪可丘使用泡沫!")
                {
                    string dia = "迪可丘滿身都是泡泡!";
                    TypingFeedback(dia);
                }
                else
                {
                    ImportDialog(DickimonAtkFeedbackstr, true);
                }
            }
            else if (istypatkfebback)
            {
                currentSentence++;
                currentPos = 0;
                if (currentSentence == Dialog.Length)
                {
                    if (words == "長頸鴨倒下了!")
                    {
                        string[] dia = { "回來吧!長頸鴨" + DickimonSetState.NowDuckamount + "!", "就決定是你了!長頸鴨" + (DickimonSetState.NowDuckamount + 1).ToString() + "!" };
                        ImportDialog(dia, false);
                    }
                    else if (words == "你獲得了鑰匙!" || words == "迪可丘身上的鑰匙掉下來了!")
                    {
                        return;
                        // Debug.Log("第一階段結束");
                    }
                    else if (words == "水電工被巨大的迪可丘撞飛了!!")
                    {
                        return;
                        // Debug.Log("被撞飛了!");
                    }
                    else
                    {
                        istypatkfebback = false;
                        currentSentence = 0;//進入Twodialog
                        Twodialogobj.SetActive(true);
                        this.gameObject.transform.parent.gameObject.SetActive(false);
                        Ch2DickimonLeftdiatyping leftdiatyping = Twodialogobj.transform.Find("Left_Dialog").GetComponent<Ch2DickimonLeftdiatyping>();
                        leftdiatyping.typDefault();
                    }
                }
                else if (currentSentence <= Dialog.Length)
                {
                    if (words != "迪可丘身上的鑰匙掉下來了!")
                    {
                        words = Dialog[currentSentence];
                        myText.text = "";
                        StartEffect();
                    }
                    else
                    {
                        currentSentence--;
                    }// OnStartWriter();
                }
            }
            else
            {
                if (DickimonSetState.nowCharater == "XiaoMing")
                {
                    if (words == "哇!野生的無情浣熊出現了!" || words == "哇!野生的迪可丘出現了!" || words == "哇!野生的巨大迪可丘出現了!" || words == "把鏡子遮住了!")
                    {
                        XiaoAtor.SetBool("throwball", true);
                    }
                    else if (words == "回來吧!長頸鴨" + (DickimonSetState.NowDuckamount).ToString() + "!")
                    {
                        DickimonSetState.NowDuckamount++;
                        DickimonSetState.changeDuckName();
                        XiaoAtor.SetBool("onlythrowball", true);
                        DickimonSetState.changeDuckAmount();

                        //resetpp
                        for (int i = 0; i < skillPPcounter.Length; i++)
                        {
                            if (i == 0)
                            {
                                skillPPcounter[i].text = "4";
                            }
                            else
                            {
                                skillPPcounter[i].text = "7";
                            }
                        }

                    }
                }
                else if (DickimonSetState.nowCharater == "Plumber" && DickimonSetState.nowEnemy == "木馬終結者")
                {
                    if (words == "已經沒有愛心蘑菇了!")
                    {
                        DickimonSetState.AutoPlayingDia = true;
                        DickimonSetState.StartCoroutine(DickimonSetState.AutoPlayString());
                    }
                }

                currentSentence++;
                currentPos = 0;
                if (words == "水電工被巨大的迪可丘撞飛了!!")
                {
                    return;
                    // Debug.Log("被撞飛了!");
                }
                else if (words == "你獲得了鑰匙!" || words == "迪可丘身上的鑰匙掉下來了!")
                {
                    return;
                    // Debug.Log("第一階段結束");
                }
                else if (words == "您打敗了木馬終結者!")
                {
                    DickimonSetState.TurnToRacMyth();
                }
                else if (currentSentence == Dialog.Length)
                {
                    currentSentence = 0;//進入Twodialog
                    Twodialogobj.SetActive(true);
                    Twodialogobj.transform.Find("Left_Dialog").gameObject.SetActive(true);
                    Twodialogobj.transform.Find("Right_menu").gameObject.SetActive(true);
                    this.gameObject.transform.parent.gameObject.SetActive(false);
                    Ch2DickimonLeftdiatyping leftdiatyping = Twodialogobj.transform.Find("Left_Dialog").GetComponent<Ch2DickimonLeftdiatyping>();
                    leftdiatyping.typDefault();
                }
                else
                {
                    words = Dialog[currentSentence];
                    myText.text = "";
                    StartEffect();
                    // OnStartWriter();
                }
            }
        }
    }

    public void AutoNextString()
    {
        if (!isActive)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);

            if (istypingfeedback)
            {
                if (words == "已經沒有愛心蘑菇了!")
                {
                    Twodialogobj.SetActive(true);
                    this.gameObject.transform.parent.gameObject.SetActive(false);
                    Ch2DickimonLeftdiatyping leftdiatyping = Twodialogobj.transform.Find("Left_Dialog").GetComponent<Ch2DickimonLeftdiatyping>();
                    leftdiatyping.typDefault();
                }
                else if (words == "迪可丘變巨大了!")
                {
                    // DickimonSetState.ChangeEnemy("巨大迪可丘");
                    DickimonSetState.ToSetDickmonState(false);
                    string[] dia = { "迪可丘使用福特攻擊!", "水電工被巨大的迪可丘撞飛了!!" };
                    DickimonSetState.PlumberisAvailable = false;
                    ImportDialog(dia, true);
                }
                else if (words == "對迪可丘使用愛心蘑菇!")
                {
                    string dia = "迪可丘變巨大了!";
                    TypingFeedback(dia);
                }
                else if (words == "長頸鴨使用捨身衝撞!")
                {
                    string dia = "呱!長頸鴨滑倒了!";
                    TypingFeedback(dia);
                }
                else if (words == "長頸鴨使用噴水!")
                {
                    string dia = "";
                    if (DickimonSetState.nowEnemy == "無情浣熊")
                    {
                        dia = "無情浣熊變乾淨了!";
                    }
                    else
                    {
                        dia = "迪可丘變乾淨了!";
                    }
                    TypingFeedback(dia);
                }
                else if (words == "長頸鴨使用泡沫!")
                {
                    string dia = "";
                    if (DickimonSetState.nowEnemy == "無情浣熊")
                    {
                        dia = "無情浣熊滿身都是泡泡!";
                    }
                    else
                    {
                        dia = "迪可丘滿身都是泡泡!";
                    }
                    TypingFeedback(dia);
                }
                else
                {
                    ImportDialog(DickimonAtkFeedbackstr, true);
                }
            }
            else if (istypatkfebback)
            {
                currentSentence++;
                currentPos = 0;
                if (currentSentence == Dialog.Length)
                {
                    if (words == "長頸鴨倒下了!")
                    {
                        string[] dia = { "回來吧!長頸鴨" + DickimonSetState.NowDuckamount + "!", "就決定是你了!長頸鴨" + (DickimonSetState.NowDuckamount + 1).ToString() + "!" };
                        ImportDialog(dia, false);
                    }
                    else if (words == "你獲得了鑰匙!" || words == "迪可丘身上的鑰匙掉下來了!")
                    {
                        // Debug.Log("第一階段結束");
                    }
                    else if (words == "水電工被巨大的迪可丘撞飛了!!")
                    {
                        // Debug.Log("被撞飛了!");
                    }
                    else
                    {
                        istypatkfebback = false;
                        currentSentence = 0;//進入Twodialog
                        Twodialogobj.SetActive(true);
                        this.gameObject.transform.parent.gameObject.SetActive(false);
                        Ch2DickimonLeftdiatyping leftdiatyping = Twodialogobj.transform.Find("Left_Dialog").GetComponent<Ch2DickimonLeftdiatyping>();
                        leftdiatyping.typDefault();
                    }
                }
                else if (currentSentence <= Dialog.Length)
                {
                    words = Dialog[currentSentence];
                    myText.text = "";
                    StartEffect();
                    // OnStartWriter();
                }
            }
            else
            {
                if (DickimonSetState.nowCharater == "XiaoMing")
                {
                    if (words == "哇!野生的無情浣熊出現了!" || words == "哇!野生的迪可丘出現了!" || words == "哇!野生的巨大迪可丘出現了!")
                    {
                        XiaoAtor.SetBool("throwball", true);
                    }
                    else if (words == "回來吧!長頸鴨" + (DickimonSetState.NowDuckamount).ToString() + "!")
                    {
                        DickimonSetState.NowDuckamount++;
                        DickimonSetState.changeDuckName();
                        XiaoAtor.SetBool("onlythrowball", true);
                        DickimonSetState.changeDuckAmount();

                        //resetpp
                        for (int i = 0; i < skillPPcounter.Length; i++)
                        {
                            if (i == 0)
                            {
                                skillPPcounter[i].text = "4";
                            }
                            else
                            {
                                skillPPcounter[i].text = "7";
                            }
                        }
                    }
                }

                currentSentence++;
                currentPos = 0;
                if (words == "水電工被巨大的迪可丘撞飛了!!")
                {
                    return;
                    // Debug.Log("被撞飛了!");
                }
                else if (words == "你獲得了鑰匙!" || words == "迪可丘身上的鑰匙掉下來了!")
                {
                    return;
                    // Debug.Log("第一階段結束");
                }
                else if (words == "太棒了!成功收服了無情浣熊")
                {
                    DickimonSetState.TurnToRac();
                }
                else if (currentSentence == Dialog.Length)
                {
                    currentSentence = 0;//進入Twodialog
                    Twodialogobj.SetActive(true);
                    Twodialogobj.transform.Find("Left_Dialog").gameObject.SetActive(true);
                    Twodialogobj.transform.Find("Right_menu").gameObject.SetActive(true);
                    this.gameObject.transform.parent.gameObject.SetActive(false);
                    Ch2DickimonLeftdiatyping leftdiatyping = Twodialogobj.transform.Find("Left_Dialog").GetComponent<Ch2DickimonLeftdiatyping>();
                    leftdiatyping.typDefault();
                }
                else
                {
                    words = Dialog[currentSentence];
                    myText.text = "";
                    StartEffect();
                    // OnStartWriter();
                }
            }
        }
    }
    //====================ImportDialog==========================
    public void ImportDialog(string[] dia, bool isatk)
    {
        if (dia.Length > 2)
        {
            if (dia[1] == "木馬終結者:「好燙啊!一代!一代!一代!」")
            {
                horsebeburned = true;
            }
        }
        Dialog = dia;
        currentSentence = 0;
        currentPos = 0;
        words = Dialog[currentSentence];
        myText.text = "";
        // isActive = true;
        istypingfeedback = false;
        istypatkfebback = isatk;
        this.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(BigDickchuCrushintohorse());
    }
    bool istypingfeedback = false;
    bool istypatkfebback = false;
    public void TypingFeedback(string Feedbackstr)
    {
        timer = 0;
        currentPos = 0;
        words = Feedbackstr;
        myText.text = "";
        this.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(BigDickchuCrushintohorse());
        // StartEffect();
        istypingfeedback = true;

    }
    //===============ANIMATION================== 
    public void PlayDuckBackToBallAni()
    {
        if (DickimonSetState.nowEnemy != "木馬病毒")
        {
            DmduckAtor.SetBool("backtoball", true);
        }
    }

    public void XiaoCatchballThrow()
    {
        if (DickimonSetState.nowEnemy == "無情浣熊")
        {
            XiaoAtor.SetBool("Raccatch", true);
        }
        else
        {
            XiaoAtor.SetBool("catchthrow", true);
        }

    }

    public void Plumberthrow()
    {
        DmPlumber.SetBool("Plumberthrow", true);
    }

    public IEnumerator BigDickchuCrushintohorse()
    {
        if (words == "巨大的迪可丘砸中了木馬病毒!")
        {
            DickimonSetState.DickchucrushintoHorse();
            isActive = false;
            yield return new WaitForSeconds(2.5f);
            isActive = true;
        }
        else
        {
            isActive = true;
        }
    }

    IEnumerator WaitForNextstr(float second)
    {
        yield return new WaitForSeconds(second);
        AutoNextString();
    }

    //autostring

    IEnumerator autoHorseunablemirror()
    {
        GameObject.Find("ToBackBtn").SetActive(false);//先關閉去背後的按鈕
        DickimonSetState.AutoPlayingDia = true;
        DickimonSetState.isPlayingAni = true;
        yield return new WaitForSeconds(0.5f);
        AutoNextString();
        DickimonSetState.HorseJump();
        audioManager.SetWoodBoard_Dickimon();
        DickimonSetState.WoodBoard.SetActive(true);
        DickimonSetState.WoodBoard.transform.parent = GameObject.Find("ToolsBar").transform;
        DickimonSetState.AutoPlayingDia = false;
        DickimonSetState.isPlayingAni = false;
    }
}
