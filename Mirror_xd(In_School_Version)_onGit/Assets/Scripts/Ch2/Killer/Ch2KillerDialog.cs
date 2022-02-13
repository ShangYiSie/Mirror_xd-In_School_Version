using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class KillerDialogs
{
    public KillerDialog[] Killer;
}
[System.Serializable]
public class KillerDialog
{
    public int stage;
    public string name;
    public string[] dialog;
}
public class Ch2KillerDialog : MonoBehaviour
{
    public GameObject Dialog;
    #region 名字和圖片和按鈕
    public Text Name;//名字
    public Image PasswardImg;
    public Image HorseImg;
    public GameObject NextBtn;
    public GameObject HorseshoeFlashing;
    #endregion
    public TextAsset file;
    Game2and3DialogTypingEffect TypingEffect;
    Game2Manager game2Manager;
    Ch2DialogueManager ch2DialogueManager;
    AudioManager audioManager;
    BGMManager bgmManager;

    Ch2CursorSetting cursorSetting;

    // Start is called before the first frame update
    void Start()
    {
        TypingEffect = Dialog.GetComponent<Game2and3DialogTypingEffect>();
        game2Manager = GameObject.Find("BricksKillerGame").GetComponent<Game2Manager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        StartDialog();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartDialog()
    {
        KillerDialogs killerDialogsJson = JsonUtility.FromJson<KillerDialogs>(file.text);
        foreach (KillerDialog dia in killerDialogsJson.Killer)
        {
            if (dia.stage == Game2and3DialogTypingEffect.stage)
            {
                TypingEffect.ImportDialog(dia.dialog);
                Name.text = dia.name;//改名字
                if (dia.name == "木馬")
                {
                    if (PasswardImg.gameObject.activeSelf)
                    {
                        PasswardImg.color = new Color32(128, 128, 128, 255);
                    }
                    HorseImg.color = new Color32(255, 255, 255, 255);
                }
                else if (dia.name == "密碼")
                {
                    if (!PasswardImg.gameObject.activeSelf)
                    {
                        PasswardImg.gameObject.SetActive(true);
                    }
                    PasswardImg.color = new Color32(255, 255, 255, 255);
                    HorseImg.color = new Color32(128, 128, 128, 255);
                }
                switch (Game2and3DialogTypingEffect.stage)//叫出右下角狗視窗
                {
                    case 2:
                        ch2DialogueManager.ShowNextDialogue("dog1", true);
                        break;
                    case 4:
                        ch2DialogueManager.ShowNextDialogue("dog2", true);
                        break;
                    case 6:
                        ch2DialogueManager.ShowNextDialogue("dog3", true);
                        break;
                    case 7:
                        ch2DialogueManager.ShowNextDialogue("dog4", true);
                        break;
                    case 9:
                        ch2DialogueManager.ShowNextDialogue("dog5", true);
                        break;
                    case 11:
                        ch2DialogueManager.ShowNextDialogue("dog6", true);
                        break;
                    case 13:
                        ch2DialogueManager.ShowNextDialogue("dog7", true);
                        break;
                    case 14:
                        ch2DialogueManager.ShowNextDialogue("dog8", true);
                        break;
                }
            }
            else if (Game2and3DialogTypingEffect.stage > killerDialogsJson.Killer.Length)
            {
                // =============================
                // 切換BGM
                bgmManager.StopCH2BGM();
                bgmManager.PlayBreakSafeBgm();
                // =============================
                //ch2DialogueManager.ShowNextDialogue("dog8", true);
                //Game2and3DialogTypingEffect.stage = killerDialogsJson.Killer.Length;
                //關閉對話框&繼續過場動畫
                TypingEffect.KillerDialogCanvas.SetActive(false);
                //鼠標沙漏動畫
                Ch2AniEvents.AniDone = false;
                cursorSetting.CursorHourglassAni();
                //鼠標沙漏動畫

                game2Manager.Horse_InsideAni.speed = 1;
                game2Manager.Horse_InsideAni.SetTrigger("Horse_walk");
                //game2Manager.BlackBallAni.speed = 1;
                Game2and3DialogTypingEffect.stage = 1;
            }
        }
    }
    public void NextBtnClick()
    {
        audioManager.ClickDialogueNext();
        TypingEffect.NextString();
        NextBtn.SetActive(false);
    }
}
