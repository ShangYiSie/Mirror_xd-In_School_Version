using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[System.Serializable]
public class Dialogues
{
    public Dialogue[] dialogues;
}

[System.Serializable]
public class Dialogue
{
    public string trigger;
    public Content[] txt;
}

[System.Serializable]
public class Content
{
    public string content;
    public string emotion;
}

// =====================================
// 狗說話的內容
[System.Serializable]
public class DogDialogues
{
    public DogDialogue[] dogDialogues;
}

[System.Serializable]
public class DogDialogue
{
    public string chapter;
    public Dialogue[] dia;
}

public class DialogueManager : MonoBehaviour
{

    UserManager userManager;
    CursorSetting cursorSetting;
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

    [Header("狗的Animator")]
    public Animator DogAnim;

    [Header("歷史紀錄Obj")]
    public GameObject PlaceText;
    public GameObject HistoryContent;   // content
    public GameObject ConPrefab;    // 文字物件
    public GameObject SplitLine;    // 分隔線
    public Sprite[] SplitSprite;

    public GameObject Dog;  // 狗物件

    // STOP Controller

    IEnumerator co;
    bool callStop = false;
    // ==============================================
    // =============================================
    // stage == 11 => key frame
    // ==============================================
    // ==============================================
    
    // ==============================================
    // 目前狗的階段
    public string nowDogTrigger = "enter_back";   // 目前狗的trigger (目前要講哪段話)
    public int nowDogIndex = 0;     // 那段話的第幾句

    // Start is called before the first frame update
    void Start()
    {
        userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<CursorSetting>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
        // test
        // ShowNextDialogue("backtofront_first", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //string[] txtContent()
    //{

    //}

    string ConvertPlaceText()
    {
        switch (GameManager.nowCH1State)
        {
            case GameManager.CH1State.AntivirusFront:
                return "（現實世界 - Watching dog）";
            case GameManager.CH1State.BrowserBack:
                return "（虛擬世界 - Internet Chrone）";
            case GameManager.CH1State.BrowserFront:
                return "（現實世界 - Internet Chrone）";
            case GameManager.CH1State.CMailBack:
                return "（虛擬世界 - CMail）";
            case GameManager.CH1State.CMailFront:
                return "（現實世界 - CMail）";
            case GameManager.CH1State.DeskTopBack:
                return "（虛擬世界 - 桌面）";
            case GameManager.CH1State.DeskTopFront:
                return "（現實世界 - 桌面）";
            case GameManager.CH1State.FolderBack:
                return "（虛擬世界 - 7414）";
            case GameManager.CH1State.FolderFront:
                return "（現實世界 - 7414）";
            case GameManager.CH1State.MythBack:
                return "（虛擬世界 - Myth）";
            case GameManager.CH1State.MythFront:
                return "（現實世界 - Myth）";
            case GameManager.CH1State.NoteBack:
                return "（虛擬世界 - 日記.txt）";
            case GameManager.CH1State.NoteFront:
                return "（現實世界 - 日記.txt）";
            default:
                return "";
        }
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
        DogDialogue dialogue = dialogueInJson.dogDialogues[0];   // 0 => 到時候要根據關卡改變index
        foreach (Dialogue dia in dialogue.dia)
        {
            if(dia.trigger == nowDogTrigger)
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
        // 特殊表情
        if(emotion == "sad_water_normal")
        {
            if(GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
            {
                emotion = "water_normal";
            }
            else
            {
                emotion = "sad";
            }
        }
        else if(emotion == "angry_water_angry")
        {
            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
            {
                emotion = "water_angry";
            }
            else
            {
                emotion = "angry";
            }
        }
        // ========================================
        yield return new WaitForSeconds(0.5f);
        // ========================================
        // 讓狗說話音效
        audioManager.DogSpeak();
        // ========================================
        yield return SelectDialogueBoxSize(box, innerTxt, con, false, emotion);

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
        if(nowDogIndex + 1 == arrLen)
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
        if (trigger == "back_first")
        {
            userManager.ChangeFirstToBackStatus();
            // ShowNextDialogue("after_back_first", false);
        }
        if (cursorSetting.isonText)
        {
            if ((int)GameManager.nowCH1State % 2 == 0)
            {
                cursorSetting.ChangeState("Front_Normal");
            }
            else
            {
                cursorSetting.ChangeState("Back_Normal");
            }

        }
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
            if (isFront)
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
        if (co != null)
        {
            StopCoroutine(co);
        }
        if ((int)GameManager.nowCH1State % 2 == 0)
        {
            DialogueFront.SetActive(false);
        }
        else
        {
            DialogueBack.SetActive(false);
        }
    }

    // 點擊跳過
    public void CloseNowDialogue()
    {
        // =============================
        // 觸發點擊略過音效
        audioManager.ClickConversionRecord();
        if((int)GameManager.nowCH1State %2 == 0)
        {
            DialogueFront.GetComponent<Button>().enabled = false;
        }
        // =============================
        // StopCoroutine(temp);
        callStop = true;
    }

    void WaitToFade(GameObject box, GameObject innerTxt)
    {

        dogIcon.GetComponent<Image>().DOFade(0, 1);
        Semi.GetComponent<Text>().DOFade(0, 1);
        box.GetComponent<Image>().DOFade(0, 1).OnComplete(() =>
        {
            box.SetActive(false);
        });
        innerTxt.GetComponent<Text>().DOFade(0, 1);
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

    // =====================================
    // 背面狗換動畫
    void CloseAllAnim()
    {
        if(GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
        {
            // 在釣魚網站背面
            DogAnim.SetBool("water_normal", false);
            DogAnim.SetBool("water_lol", false);
            DogAnim.SetBool("water_angry", false);
        }
        else
        {
            // 其他
            DogAnim.SetBool("normal", false);
            DogAnim.SetBool("lol", false);
            DogAnim.SetBool("angry", false);
            DogAnim.SetBool("sad", false);
        }
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
