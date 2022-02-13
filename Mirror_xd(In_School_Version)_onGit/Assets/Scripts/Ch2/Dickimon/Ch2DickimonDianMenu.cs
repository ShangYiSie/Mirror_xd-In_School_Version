using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ch2DickimonDianMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    Ch2DickimonLeftdiatyping Leftdiatyping;

    Ch2DickimonSetState DickimonSetState;

    Ch2GameManager ch2GameManager;
    AudioManager audioManager;
    Ch2DialogueManager ch2DialogueManager;

    public GameObject Undo;
    public GameObject Left_Dialog;

    public GameObject Two_Dialog;

    public GameObject DmDuck;

    GameObject Right_menu;

    GameObject Skill;

    GameObject Bag;

    GameObject Dickimon;

    public int objindex;

    public static string[] MenuTwofeedbackstr;

    [Header("第一次")]
    public bool RunAwayFirst = true;

    void Awake()
    {
        ch2GameManager = GameObject.Find("GameManager").GetComponent<Ch2GameManager>();
        DickimonSetState = GameObject.Find("DickimonGame").GetComponent<Ch2DickimonSetState>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();

        Undo.SetActive(false);
        Leftdiatyping = Left_Dialog.GetComponent<Ch2DickimonLeftdiatyping>();
        Right_menu = Two_Dialog.transform.Find("Right_menu").gameObject;
        Skill = Two_Dialog.transform.Find("Skill").gameObject;
        Bag = Two_Dialog.transform.Find("Bag").gameObject;
        Dickimon = Two_Dialog.transform.Find("Dickimon").gameObject;
    }
    public void ImportFeedbackstr(string[] TwoFeedBackStr)
    {
        MenuTwofeedbackstr = TwoFeedBackStr;

    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.GetComponent<Text>().text != "--" && this.GetComponent<Text>().text != "")
        {
            // ========================
            // hover音效
            audioManager.HoverOption_Dickimon();
            // ========================
            Leftdiatyping.TypingDialog(MenuTwofeedbackstr[objindex]);

            GameObject Menuhover = this.transform.parent.transform.Find("Right_menu_hover").gameObject;
            // Menuhover.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 0.13f, 0);
            // Menuhover.transform.localPosition = this.transform.localPosition;
            float width = 0;
            width = (this.GetComponent<Text>().text.Length) * 65;
            Menuhover.transform.localPosition = new Vector3(this.transform.localPosition.x - 3.01f, this.transform.localPosition.y, 0);
            Menuhover.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 71.34467F);
            Menuhover.SetActive(true);
            this.GetComponent<Text>().color = new Color(1, 1, 1, 1);
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.GetComponent<Text>().text != "--" && this.GetComponent<Text>().text != "")
        {
            Leftdiatyping.typDefault();
            GameObject Menuhover = this.transform.parent.transform.Find("Right_menu_hover").gameObject;
            Menuhover.SetActive(false);
            this.GetComponent<Text>().color = new Color(0, 0, 0, 1);
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        string ToDo = this.GetComponent<Text>().text;
        // Debug.Log(ToDo);

        switch (ToDo)
        {
            case "戰鬥":
                // 點擊音效
                audioManager.ClickAnyBtnMain_Dickimon();
                // ==========================
                Left_Dialog.SetActive(false);
                Right_menu.SetActive(false);
                Undo.SetActive(true);
                Skill.SetActive(true);
                break;
            case "口袋":
                // 點擊音效
                audioManager.ClickAnyBtnMain_Dickimon();
                // ==========================
                Left_Dialog.SetActive(false);
                Right_menu.SetActive(false);
                Undo.SetActive(true);
                Bag.SetActive(true);
                break;
            case "迪可夢":
                // 點擊音效
                audioManager.ClickAnyBtnMain_Dickimon();
                // ==========================
                Left_Dialog.SetActive(false);
                Right_menu.SetActive(false);
                Undo.SetActive(true);
                Dickimon.SetActive(true);
                break;
            case "逃跑":
                if (DickimonSetState.nowEnemy == "迪可丘" || DickimonSetState.nowEnemy == "巨大迪可丘")
                {
                    ch2GameManager.CloseGame1();
                }
                else
                {
                    // ========================
                    // 點擊討跑煙霧 音效
                    audioManager.RunAway_Dickimon();
                    audioManager.ClickRunAway_Dickimon();
                    // ========================
                    DickimonSetState.ExitNone = true;
                    this.GetComponent<Text>().text = "";
                    this.transform.Find("SmogFx").gameObject.SetActive(true);
                    StartCoroutine(WaitnDestroy());
                    this.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject Menuhover = this.transform.parent.transform.Find("Right_menu_hover").gameObject;
                    Menuhover.SetActive(false);
                }
                if (RunAwayFirst && DickimonSetState.nowEnemy != "無情浣熊" && DickimonSetState.nowEnemy != "木馬病毒")
                {
                    // 觸發文本
                    ch2DialogueManager.ShowNextDialogue("第一次逃跑後", true);
                    RunAwayFirst = false;
                }
                break;
            case "腰包":
                // 點擊音效
                audioManager.ClickAnyBtnMain_Dickimon();
                // ==========================
                Left_Dialog.SetActive(false);
                Right_menu.SetActive(false);
                Undo.SetActive(true);
                Bag.SetActive(true);
                break;
            default: break;
        }

        if (this.GetComponent<Text>().text != "")
        {
            Left_Dialog.GetComponent<Text>().text = "";
            // Left_Dialog.transform.GetChild(0).gameObject.SetActive(false);
            GameObject Menuhover = this.transform.parent.transform.Find("Right_menu_hover").gameObject;
            Menuhover.SetActive(false);
            this.GetComponent<Text>().color = new Color(0, 0, 0, 1);
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    IEnumerator WaitnDestroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.transform.Find("SmogFx").gameObject);
    }

}
