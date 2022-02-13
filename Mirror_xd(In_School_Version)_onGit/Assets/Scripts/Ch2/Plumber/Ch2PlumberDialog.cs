using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class PlumberrDialogs
{
    public PlumberDialog[] Plumber;
}
[System.Serializable]
public class PlumberDialog
{
    public int stage;
    public string name;
    public string[] dialog;
}
public class Ch2PlumberDialog : MonoBehaviour
{
    public GameObject Dialog;
    #region 名字和圖片和按鈕
    public Text Name;//名字
    public Image MythImg;
    public Image HorseImg;
    public GameObject NextBtn;
    public GameObject HorseshoeFlashing;
    #endregion
    public TextAsset file;
    Game2and3DialogTypingEffect TypingEffect;
    Ch2DialogueManager ch2DialogueManager;
    Game3Manager game3Manager;
    AudioManager audioManager;
    BGMManager bgmManager;
    Ch2VisualManager ch2visualManager;
    Ch2GameManager ch2GameManager;
    Ch2CursorSetting cursorSetting;

    // 第一次
    public bool GotoHorseFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        TypingEffect = Dialog.GetComponent<Game2and3DialogTypingEffect>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        game3Manager = GameObject.Find("PlumberGame").GetComponent<Game3Manager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        ch2visualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();
        ch2GameManager = GameObject.Find("GameManager").GetComponent<Ch2GameManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();

        StartDialog();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartDialog()
    {
        PlumberrDialogs PlumberDialogsJson = JsonUtility.FromJson<PlumberrDialogs>(file.text);
        foreach (PlumberDialog dia in PlumberDialogsJson.Plumber)
        {
            if (dia.stage == Game2and3DialogTypingEffect.stage)
            {
                TypingEffect.ImportDialog(dia.dialog);
                Name.text = dia.name;//改名字
                if (dia.name == "木馬")
                {
                    if (MythImg.gameObject.activeSelf)
                    {
                        MythImg.color = new Color32(128, 128, 128, 255);
                    }
                    HorseImg.color = new Color32(255, 255, 255, 255);
                }
                else if (dia.name == "Myth")
                {
                    MythImg.gameObject.SetActive(true);
                    MythImg.color = new Color32(255, 255, 255, 255);
                    HorseImg.color = new Color32(128, 128, 128, 255);
                }
                switch (Game2and3DialogTypingEffect.stage)//叫出右下角狗視窗
                {
                    case 1:
                        ch2DialogueManager.ShowNextDialogue("dog9", true);
                        break;
                    case 4:
                        ch2DialogueManager.ShowNextDialogue("dog10", true);
                        break;
                    case 6:
                        ch2DialogueManager.ShowNextDialogue("dog11", true);
                        break;
                    case 8:
                        ch2DialogueManager.ShowNextDialogue("dog12", true);
                        break;
                    case 9:
                        ch2DialogueManager.ShowNextDialogue("dog13", true);
                        break;
                    case 10:
                        ch2DialogueManager.ShowNextDialogue("dog14", true);
                        break;
                    case 12:
                        ch2DialogueManager.ShowNextDialogue("dog15", true);
                        break;
                    case 14:
                        ch2DialogueManager.ShowNextDialogue("dog16", true);
                        break;
                    case 15:
                        ch2DialogueManager.ShowNextDialogue("dog17", true);
                        break;
                    case 17:
                        ch2DialogueManager.ShowNextDialogue("dog18", true);
                        break;
                }
            }
            else if (Game2and3DialogTypingEffect.stage == 5)
            {
                // =============================
                // 切換BGM
                bgmManager.StopCH2BGM();
                bgmManager.PlayRaccoonGameBgm();
                // =============================
                //TypingEffect.PlumberDialogCanvas.SetActive(false);
                //鼠標沙漏動畫
                Ch2AniEvents.AniDone = false;
                cursorSetting.CursorHourglassAni();
                //StartCoroutine(wait());
                //鼠標沙漏動畫
                TypingEffect.PlumberDialogCanvas.SetActive(false);
                game3Manager.ToBackBtn.enabled = true;
                game3Manager.CloseBtn.enabled = true;
                game3Manager.HorseClamb();
            }
            else if (Game2and3DialogTypingEffect.stage == 11 && GotoHorseFirst)
            {
                GotoHorseFirst = false;
                // =============================
                // 切換BGM
                bgmManager.StopCH2BGM();
                bgmManager.PlayRaccoonGameBgm();
                // =============================
                Ch2AniEvents.AniDone = false;
                cursorSetting.CursorHourglassAni();
                //StartCoroutine(wait());
                HorseImg.gameObject.SetActive(false);
                TypingEffect.PlumberDialogCanvas.SetActive(false);
                //進入木馬對戰畫面
                ch2visualManager.StartCoroutine(ch2visualManager.ToDickimonHorse());
                Ch2AniEvents.AniDone = true;
                cursorSetting.StopCursorHourglassAni();
            }
            else if (Game2and3DialogTypingEffect.stage > 17)
            {
                //Game2and3DialogTypingEffect.stage = PlumberDialogsJson.Plumber.Length;
                // =============================
                // 切換BGM
                bgmManager.StopCH2BGM();
                bgmManager.PlayGLITCHBgm();
                // =============================
                Game2and3DialogTypingEffect.stage = 1;
                TypingEffect.PlumberDialogCanvas.SetActive(false);
                /*game3Manager.ToBackBtn.enabled = true;
                game3Manager.CloseBtn.enabled = true;*/
                // ===============================
                ch2visualManager.TurnToCh3FX();
            }

        }
    }

    /*IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        if (Game2and3DialogTypingEffect.stage == 4)
        {
            TypingEffect.PlumberDialogCanvas.SetActive(false);
            game3Manager.HorseClamb();
        }
        else if (Game2and3DialogTypingEffect.stage == 9)
        {
            HorseImg.gameObject.SetActive(false);
            TypingEffect.PlumberDialogCanvas.SetActive(false);

            //進入木馬對戰畫面
            ch2visualManager.StartCoroutine(ch2visualManager.ToDickimonHorse());
            Ch2AniEvents.AniDone = true;
            cursorSetting.StopCursorHourglassAni();
        }
    }*/

    public void NextBtnClick()
    {
        audioManager.ClickDialogueNext();
        TypingEffect.NextString();
        NextBtn.SetActive(false);
    }
}
