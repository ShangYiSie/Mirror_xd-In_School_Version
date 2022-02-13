using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ch2DickimonSkills : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    Ch2DickimonTypingEffect DickimonFullTyping;
    Ch2DickimonSetState DickimonSetState;
    Ch2DialogueManager ch2DialogueManager;
    AudioManager audioManager;
    public bool ThisisBag = false;

    public bool ThisisDic = false;

    public Animator dm_DuckAtor;

    public GameObject Undo;
    public GameObject Full_Dialog;

    public GameObject Two_Dialog;
    public int objindex;
    public static string[] SkillFeedbackstr;
    public static string[] BagFeedbackstr;

    public static string[] DickikmonFeedbackstr;

    [Header("第一次")]
    public bool UseCrashIntoFirst = true;   // 第一次使用捨身衝撞
    public bool UseSprayWaterFirst = true;  // 第一次使用噴水
    public bool UseBubbleAtkFirst = true;   // 第一次使用泡沫
    public bool ChangeLongNeckDuckFirst = true; // 第一次替換長頸鴨
    public bool UseBallFirst = true;    // 第一次使用道具愛心寶貝球
    public bool UseMushroomFirst = true;    // 第一次使用蘑菇


    private void Awake()
    {
        DickimonFullTyping = Full_Dialog.GetComponent<Ch2DickimonTypingEffect>();
        DickimonSetState = GameObject.Find("DickimonGame").GetComponent<Ch2DickimonSetState>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ImportFeedbackstr(string[] FeedBackStr)
    {
        SkillFeedbackstr = FeedBackStr;

    }

    public void ImporFullFeedBack(string[] bagfeedbackstr)
    {
        BagFeedbackstr = bagfeedbackstr;
    }

    public void ImporDickimonFeedback(string[] dickimonfeedbackstr)
    {
        DickikmonFeedbackstr = dickimonfeedbackstr;
    }

    public void ChangeDuckAmount()
    {
        if (DickimonSetState.nowEnemy != "木馬病毒")
        {
            int nowamount = DickimonSetState.NowDuckamount;
            DickikmonFeedbackstr[0] = string.Concat("回來吧!長頸鴨", DickimonSetState.NowDuckamount.ToString() + "!");
            DickikmonFeedbackstr[1] = string.Concat("就決定是你了!長頸鴨", (nowamount + 1).ToString() + "!");
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.GetComponent<Text>().text != "--")
        {
            for (int index = 0; index < this.gameObject.transform.childCount; index++)
            {
                // ========================
                // hover音效
                audioManager.HoverOption_Dickimon();
                // ========================

                this.transform.GetChild(index).gameObject.SetActive(true);

                GameObject Menuhover = this.transform.parent.transform.Find("Left_menu_hover").gameObject;
                float width = 0;
                width = (this.GetComponent<Text>().text.Length) * 66;
                Menuhover.transform.localPosition = new Vector3(this.transform.localPosition.x - 4.966f, this.transform.localPosition.y, 0);
                Menuhover.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 72.64622F);
                Menuhover.SetActive(true);
                this.GetComponent<Text>().color = new Color(1, 1, 1, 1);
                this.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.GetComponent<Text>().text != "--")
        {
            for (int index = 0; index < this.gameObject.transform.childCount; index++)
            {
                this.transform.GetChild(index).gameObject.SetActive(false);

                GameObject Menuhover = this.transform.parent.transform.Find("Left_menu_hover").gameObject;
                Menuhover.SetActive(false);
                this.GetComponent<Text>().color = new Color(0, 0, 0, 1);
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {

    }
    int PP;
    GameObject ppcounter;
    string Thistext;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Thistext = this.GetComponent<Text>().text;
        if (Thistext != "--")
        {
            if (!ThisisDic && Thistext != "反彈" && Thistext != "跳躍" && Thistext != "垃圾桶攻擊")
            {
                PP = int.Parse(GameObject.Find("PPcounter").GetComponent<Text>().text.ToString());
                ppcounter = GameObject.Find("PPcounter");
                if (Thistext != "愛心蘑菇")
                {
                    if (PP > 0)
                    {
                        PP -= 1;
                        if (Thistext == "愛心寶貝球")
                        {
                            DickimonSetState.NowBallamount = PP;
                        }
                        ppcounter.GetComponent<Text>().text = PP.ToString();
                    }
                }
            }

            string skillname = this.GetComponent<Text>().text;
            // Debug.Log(skillname);

            switch (skillname)
            {
                case "捨身衝撞":
                    dm_DuckAtor.SetBool("DuckAtkSlip", true);
                    if (UseCrashIntoFirst)
                    {
                        ch2DialogueManager.ShowNextDialogue("第一次使用長頸鴨技能:捨身衝撞", true);
                        UseCrashIntoFirst = false;
                    }
                    break;
                case "噴水":
                    dm_DuckAtor.SetBool("WaterGunAtk", true);
                    if (UseSprayWaterFirst)
                    {
                        ch2DialogueManager.ShowNextDialogue("第一次使用長頸鴨技能:噴水", true);
                        UseSprayWaterFirst = false;
                    }
                    break;
                case "泡沫":
                    dm_DuckAtor.SetBool("BubbleAtk", true);
                    if (UseBubbleAtkFirst)
                    {
                        ch2DialogueManager.ShowNextDialogue("第一次使用長頸鴨技能:泡沫", true);
                        UseBubbleAtkFirst = false;
                    }
                    break;
                case "跳躍":
                    if (DickimonSetState.nowEnemy == "木馬終結者")
                    {
                        DickimonSetState.AutoPlayingDia = true;
                        DickimonSetState.StartCoroutine(DickimonSetState.AutoPlayString());
                        //     DickimonSetState.HorseBeamAtor.SetTrigger("Plumber");
                    }
                    break;
                case "反彈":

                    if (DickimonSetState.nowEnemy == "木馬終結者")
                    {
                        DickimonSetState.AutoPlayingDia = true;
                        DickimonSetState.StartCoroutine(DickimonSetState.AutoPlayString());
                        //     DickimonSetState.HorseBeamAtor.SetTrigger("Plumber");
                    }
                    break;
            }

            showfeedbackdia();


            if (this.GetComponent<Text>().text != "--")
            {
                for (int index = 0; index < this.gameObject.transform.childCount; index++)
                {
                    if (this.GetComponent<Text>().text == "愛心蘑菇" || this.GetComponent<Text>().text == "愛心寶貝球")
                    {
                        // 點擊蘑菇音效
                        audioManager.UselessProps_Dickimon();
                    }
                    else
                    {
                        // 點擊音效
                        audioManager.ClickAnyBtnMain_Dickimon();
                    }
                    // ==========================
                    this.transform.GetChild(index).gameObject.SetActive(false);
                    GameObject Menuhover = this.transform.parent.transform.Find("Left_menu_hover").gameObject;
                    Menuhover.SetActive(false);
                    this.GetComponent<Text>().color = new Color(0, 0, 0, 1);
                    this.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

        }
    }

    public void showfeedbackdia()
    {
        Two_Dialog.transform.Find("Right_menu").gameObject.SetActive(true);
        Two_Dialog.transform.Find("Left_Dialog").gameObject.SetActive(true);
        this.gameObject.transform.parent.gameObject.SetActive(false);//將Skill物件關閉
        Undo.SetActive(false);
        Two_Dialog.SetActive(false);
        Full_Dialog.transform.GetChild(0).gameObject.SetActive(true);
        // Full_Dialog.GetComponent<Text>().text = SkillFeedbackstr[objindex];
        Full_Dialog.transform.parent.gameObject.SetActive(true);

        if (ThisisBag)
        {
            if (Thistext == "愛心寶貝球" && DickimonSetState.nowEnemy != "木馬病毒")
            {
                if (UseBallFirst && DickimonSetState.nowEnemy != "無情浣熊")
                {
                    // 觸發文本
                    ch2DialogueManager.ShowNextDialogue("第一次使用道具愛心寶貝球後", true);
                    UseBallFirst = false;
                }
                DickimonFullTyping.XiaoCatchballThrow();
            }
            if (Thistext == "愛心蘑菇" && PP == 1)
            {
                DickimonSetState.ChangeEnemy("巨大迪可丘");
                if (UseMushroomFirst)
                {
                    // 觸發文本
                    ch2DialogueManager.ShowNextDialogue("第一次使用水電工道具:蘑菇", true);
                    UseMushroomFirst = false;
                }
                DickimonFullTyping.Plumberthrow();
                DickimonFullTyping.TypingFeedback(BagFeedbackstr[0]);
                PP -= 1;
                ppcounter.GetComponent<Text>().text = PP.ToString();
            }
            else if (Thistext == "愛心蘑菇" && (DickimonSetState.nowEnemy == "迪可丘" || DickimonSetState.nowEnemy == "巨大迪可丘" || DickimonSetState.nowEnemy == "無情浣熊"))
            {
                DickimonFullTyping.TypingFeedback(BagFeedbackstr[1]);
            }
            else
            {
                DickimonFullTyping.ImportDialog(BagFeedbackstr, false);
            }
        }
        else if (ThisisDic)
        {
            switch (objindex)
            {
                case 0:
                    if (ChangeLongNeckDuckFirst)
                    {
                        // 觸發文本
                        ch2DialogueManager.ShowNextDialogue("第一次替換長頸鴨", true);
                        ChangeLongNeckDuckFirst = false;
                    }
                    ChangeDuckAmount();
                    DickimonFullTyping.ImportDialog(DickikmonFeedbackstr, false);
                    DickimonFullTyping.PlayDuckBackToBallAni();
                    break;
                default:
                    break;
            }

        }
        else
        {
            DickimonFullTyping.TypingFeedback(SkillFeedbackstr[objindex]);
        }
    }


}
