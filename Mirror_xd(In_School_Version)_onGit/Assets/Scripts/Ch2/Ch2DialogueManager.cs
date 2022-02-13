using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Ch2DialogueManager : MonoBehaviour
{
    Ch2CursorSetting cursorSetting;
    AudioManager audioManager;

    public TextAsset jsonFile;
    public TextAsset dogJsonFile;

    [Header("每個字閱讀的時間長")]
    public float readSpeed = 0.2f;

    [Header("Dialogue Object")]
    public GameObject DialogueFront;
    public GameObject DialogueBack;

    [Header("文字object")]
    public GameObject DialogueTextFront;
    public GameObject DialogueTextBack;
    public GameObject DialogueTextBack_Outline;

    [Header("狗狗icon")]
    public GameObject dogIcon;
    public GameObject Semi;
    public Sprite[] dogIconImage = new Sprite[18];

    [Header("正面對話框的圖")]
    public Sprite front_small;
    public Sprite front_middle;
    public Sprite front_large;
    public Sprite front_superlarge;

    [Header("歷史紀錄Obj")]
    public GameObject PlaceText;
    public GameObject HistoryContent;   // content
    public GameObject ConPrefab;    // 文字物件
    public GameObject SplitLine;    // 分隔線
    public Sprite[] SplitSprite;

    [Header("狗的Animator")]
    public Animator DogAnim;

    public GameObject Dog;  // 狗物件

    // STOP Controller

    IEnumerator co;
    bool callStop = false;

    // 目前狗的階段
    public string nowDogTrigger = "chapter2";   // 目前狗的trigger (目前要講哪段話)
    public int nowDogIndex = 0;     // 那段話的第幾句

    void Start()
    {
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // test
        // ShowNextDialogue("第一次打開迪可夢，角色:磚塊殺手", true);
    }

    string ConvertPlaceText()
    {
        string returnText;
        switch (Ch2GameManager.nowCH2State)
        {
            case Ch2GameManager.CH2State.MythGameFront:
                returnText = "（現實世界 - Myth）";
                switch (Ch2GameManager.nowGames)
                {
                    case Ch2GameManager.Games.Antivirus:
                        returnText += "（防毒軟體）";
                        break;
                    case Ch2GameManager.Games.Dickimon:
                        returnText += "（迪可夢）";
                        break;
                    case Ch2GameManager.Games.BreakoutSafe:
                        returnText += "（突破保險箱）";
                        break;
                    case Ch2GameManager.Games.RuthlessRaccoon:
                        returnText += "（無情浣熊）";
                        break;
                    default:
                        break;
                }
                break;
            case Ch2GameManager.CH2State.MythGameBack:
                returnText = "（虛擬世界 - Myth）";
                break;
            default:
                returnText = "";
                break;
        }

        return returnText;
    }

    #region 點擊讓狗說話
    public void DogSpeak()
    {
        // 先讓前一個沒跑完的動畫結束
        if (co != null)
        {
            StopCoroutine(co);
        }

        // 取得狗講話的內容
        DogDialogues dialogueInJson = JsonUtility.FromJson<DogDialogues>(dogJsonFile.text);
        DogDialogue dialogue = dialogueInJson.dogDialogues[1];   // 0 => 到時候要根據關卡改變index
        foreach (Dialogue dia in dialogue.dia)
        {
            if (dia.trigger == nowDogTrigger)
            {
                // ==================
                co = DogSpeakWaitFor(DialogueBack, DialogueTextBack, dia.txt[nowDogIndex].content, dia.txt[nowDogIndex].emotion);
                StartCoroutine(co);

                // 確認狗下一句要說的話
                checkAndChangeToNextDiaInDog(dia.txt.Length);   // 傳array長度進去
            }
        }
    }

    IEnumerator DogSpeakWaitFor(GameObject box, GameObject innerTxt, string con, string emotion)
    {
        // ========================================
        yield return new WaitForSeconds(0.5f);
        // ========================================
        // 讓狗說話音效
        audioManager.DogSpeak();
        // ========================================
        yield return SelectDialogueBoxSize(box, innerTxt, con, false, emotion);

        // ===============================================
        CloseAllAnim();
        box.SetActive(false);
    }

    public void changeDogStage(string trigger)
    {
        nowDogTrigger = trigger;
        nowDogIndex = 0;
    }

    public void checkAndChangeToNextDiaInDog(int arrLen)
    {
        if (nowDogIndex + 1 == arrLen)
        {
            nowDogIndex = 0;
        }
        else
        {
            nowDogIndex++;
        }
    }

    #endregion

    public void ShowNextDialogue(string trigger, bool isFront)
    {
        // 先讓前一個沒跑完的動畫結束
        if (co != null)
        {
            DOTween.Kill("DogBox");
            StopCoroutine(co);
        }

        Dialogues dialogueInJson = JsonUtility.FromJson<Dialogues>(jsonFile.text);

        foreach (Dialogue dia in dialogueInJson.dialogues)
        {
            if (dia.trigger == trigger)
            {
                // 地點的文字
                GameObject placeObj;
                placeObj = Instantiate(PlaceText, HistoryContent.transform);
                placeObj.GetComponent<Text>().text = ConvertPlaceText();
                // ========================================
                // 先把所有文本存到歷史紀錄裡面
                foreach (Content con in dia.txt)
                {
                    GameObject obj;
                    obj = Instantiate(ConPrefab, HistoryContent.transform);
                    GameObject child = obj.transform.GetChild(0).gameObject;
                    if (isFront)
                    {
                        child.transform.GetChild(0).GetComponent<Image>().sprite = dogIconImage[ConvertIconToNum(con.emotion)];  // 換狗圖
                    }
                    else
                    {
                        child.transform.GetChild(0).GetComponent<Image>().sprite = dogIconImage[4];
                    }

                    obj.transform.GetChild(1).GetComponent<Text>().text = con.content;
                }
                // ========================================
                // 在歷史裡面加分隔線
                GameObject splitObj = Instantiate(SplitLine, HistoryContent.transform);
                splitObj.GetComponent<Image>().sprite = SplitSprite[Random.Range(0, 7)];

                // ========================================
                // using trigger to get specific json content (txt)
                if (isFront)
                {
                    // 在正面
                    co = WaitFor(DialogueFront, DialogueTextFront, dia.txt, true, trigger);

                }
                else
                {
                    co = WaitFor(DialogueBack, DialogueTextBack, dia.txt, false, trigger);
                    // 在背面
                }

                StartCoroutine(co);

                break;
            }

        }

    }

    void CheckContinue(string trigger)
    {
        // 接續動畫

    }

    IEnumerator WaitFor(GameObject box, GameObject innerTxt, Content[] con, bool isFront, string trigger)
    {
        CursorSet(trigger);
        for (int i = 0; i < con.Length; i++)
        {
            if (isFront)
            {
                box.GetComponent<Button>().enabled = true;
            }

            yield return new WaitForSeconds(0.5f);

            // ========================================
            if (isFront && (int)Ch2GameManager.nowCH2State % 2 == 0)
            {
                // =======================================
                // 觸發泡泡音效
                audioManager.ClickInternetIconOrShowDogSpeak();
            }
            else
            {
                // ========================================
                // 讓狗說話音效
                audioManager.DogSpeak();
            }

            yield return SelectDialogueBoxSize(box, innerTxt, con[i].content, isFront, con[i].emotion);

            // ========================================
            if (isFront)
            {
                box.GetComponent<Button>().enabled = false;
                WaitToFade(box, innerTxt);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                CloseAllAnim();
                box.SetActive(false);
            }

        }

        CheckContinue(trigger);
    }


    IEnumerator SelectDialogueBoxSize(GameObject box, GameObject innerTxt, string content, bool isFront, string emotion)
    {
        box.SetActive(true);
        innerTxt.GetComponent<Text>().text = content;

        if (isFront)
        {
            ChangeDogIcon(emotion);
            if (content.Length <= 10)
            {
                // small size
                dogIcon.GetComponent<RectTransform>().localPosition = new Vector3(-789.2306f, -194f, 0);
                Semi.GetComponent<RectTransform>().localPosition = new Vector3(-553.73f, -194f, 0);
                box.GetComponent<Image>().sprite = front_small;
                innerTxt.GetComponent<RectTransform>().localPosition = new Vector3(251.8f, -188.5f, 0);
                innerTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(1461.768f, 333.1239f);
            }
            else if (content.Length > 10 && content.Length <= 20)
            {
                // middle size
                dogIcon.GetComponent<RectTransform>().localPosition = new Vector3(-789.2306f, -35f, 0);
                Semi.GetComponent<RectTransform>().localPosition = new Vector3(-553.73f, -35f, 0);
                box.GetComponent<Image>().sprite = front_middle;
                innerTxt.GetComponent<RectTransform>().localPosition = new Vector3(251.8f, -114.61f, 0);
                innerTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(1461.768f, 480.8522f);
            }
            else if (content.Length > 20 && content.Length <= 30)
            {
                // large size
                dogIcon.GetComponent<RectTransform>().localPosition = new Vector3(-789.2306f, 159.9999f, 0);
                Semi.GetComponent<RectTransform>().localPosition = new Vector3(-553.73f, 160f, 0);
                box.GetComponent<Image>().sprite = front_large;
                innerTxt.GetComponent<RectTransform>().localPosition = new Vector3(251.8f, -19.3f, 0);
                innerTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(1461.768f, 671.5618f);
            }
            else
            {
                // super large size
                dogIcon.GetComponent<RectTransform>().localPosition = new Vector3(-789.2306f, 322f, 0);
                Semi.GetComponent<RectTransform>().localPosition = new Vector3(-553.73f, 322f, 0);
                box.GetComponent<Image>().sprite = front_superlarge;
                innerTxt.GetComponent<RectTransform>().localPosition = new Vector3(251.8f, 44.7f, 0);
                innerTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(1461.768f, 799.6846f);
            }
            dogIcon.GetComponent<Image>().DOFade(1, 1);
            Semi.GetComponent<Text>().DOFade(1, 1);
            box.GetComponent<Image>().DOFade(1, 1);
            innerTxt.GetComponent<Text>().DOFade(1, 1);

        }
        else
        {
            DialogueTextBack_Outline.GetComponent<Text>().text = content;
            CloseAllAnim();
            ChooseAnimToOpen(emotion);
        }

        for (float timer = content.Length * readSpeed; timer >= 0; timer -= Time.deltaTime)
        {
            if (callStop)
            {
                callStop = false;
                yield break;
            }
            yield return null;
        }
        // yield return new WaitForSeconds(content.Length * readSpeed);  
    }

    public void StopNowDialogue()
    {
        // 停止目前的dialogue
        if(co != null)
        {
            StopCoroutine(co);
        }
        if((int)Ch2GameManager.nowCH2State % 2 == 0)
        {
            DialogueFront.SetActive(false);
        }
        else
        {
            DialogueBack.SetActive(false);
        }
    }

    // =====================================
    // 正面dog icon換圖
    int ConvertIconToNum(string icon)
    {
        switch (icon)
        {
            case "whatpng":
                return 0;
            case "angry":
                return 1;
            case "cry":
                return 2;
            case "dead":
                return 3;
            case "dog":
                return 4;
            case "good":
                return 5;
            case "haha":
                return 6;
            case "hi":
                return 7;
            case "little_eye":
                return 8;
            case "lol":
                return 9;
            case "money":
                return 10;
            case "read":
                return 11;
            case "shock":
                return 12;
            case "slience":
                return 13;
            case "taunt":
                return 14;
            case "think":
                return 15;
            case "woof":
                return 16;
            case "wow":
                return 17;
            case "no":
                return 18;
            case "yes":
                return 19;
            case "u":
                return 20;
            case "fire":
                return 21;
            case "zzz":
                return 22;
            case "pk":
                return 23;
            case "www":
                return 24;
            default:
                return 0;
        }
    }

    void ChangeDogIcon(string icon)
    {
        int num = ConvertIconToNum(icon);
        dogIcon.GetComponent<Image>().sprite = dogIconImage[num];
    }

    // 點擊跳過
    public void CloseNowDialogue()
    {
        // =============================
        // 觸發點擊略過音效
        audioManager.ClickConversionRecord();
        if ((int)Ch2GameManager.nowCH2State % 2 == 0)
        {
            DialogueFront.GetComponent<Button>().enabled = false;
        }
        // =============================
        // StopCoroutine(temp);
        callStop = true;
    }

    void WaitToFade(GameObject box, GameObject innerTxt)
    {
        dogIcon.GetComponent<Image>().DOFade(0, 1).SetId<Tween>("DogBox");
        Semi.GetComponent<Text>().DOFade(0, 1).SetId<Tween>("DogBox");
        box.GetComponent<Image>().DOFade(0, 1).SetId<Tween>("DogBox").OnComplete(() =>
        {
            box.SetActive(false);
        });
        innerTxt.GetComponent<Text>().DOFade(0, 1).SetId<Tween>("DogBox");
    }

    // =====================================
    // 背面狗換動畫
    void CloseAllAnim()
    {
        DogAnim.SetBool("normal", false);
        DogAnim.SetBool("lol", false);
        DogAnim.SetBool("angry", false);
        DogAnim.SetBool("sad", false);
        DogAnim.SetBool("water_normal", false);
        DogAnim.SetBool("water_lol", false);
        DogAnim.SetBool("water_angry", false);
    }

    void ChooseAnimToOpen(string id)
    {
        DogAnim.SetBool(id, true);
    }

    void CursorSet(string trigger)
    {
        if (trigger == "back_first")
        {
            AniEvents.AniDone = false;
            cursorSetting.CursorHourglassAni();
        }
        // else if (trigger == "after_back_first")
        // {
        //     AniEvents.AniDone = true;
        //     cursorSetting.StopCursorHourglassAni();
        // }
    }

}
