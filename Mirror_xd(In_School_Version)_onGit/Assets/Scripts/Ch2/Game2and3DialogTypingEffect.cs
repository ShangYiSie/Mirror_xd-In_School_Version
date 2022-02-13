using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2and3DialogTypingEffect : MonoBehaviour
{
    // backgroundText 的內容
    Ch2KillerDialog ch2KillerDialog;
    Ch2PlumberDialog ch2PlumberDialog;
    public GameObject KillerDialogCanvas;
    public GameObject PlumberDialogCanvas;
    string defaultstr = "要做些甚麼呢?";
    string[] Dialog;
    public static int stage = 1;

    public GameObject game2DiaBtn;
    public GameObject game3DiaBtn;
    // ============================================================
    public float charsPerSecond = 1f;//打字時間間隔
    private string words;//保存需要顯示的文字
    private bool isActive = false;
    private float timer;//計時器
    private Text myText;
    private int currentPos = 0;//當前打字位置

    private int currentSentence = 0;

    void Awake()
    {
        stage = 1;
        myText = GetComponent<Text>();
        isActive = false;
    }

    // Use this for initialization
    void Start()
    {
        ch2PlumberDialog = PlumberDialogCanvas.GetComponent<Ch2PlumberDialog>();
        ch2KillerDialog = KillerDialogCanvas.GetComponent<Ch2KillerDialog>();

        timer = 0;
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
            game2DiaBtn.SetActive(false);
            game3DiaBtn.SetActive(false);
            ch2KillerDialog.HorseshoeFlashing.SetActive(false);
            ch2PlumberDialog.HorseshoeFlashing.SetActive(false);
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//刷新文本顯示内容

                if (currentPos >= words.Length)
                {
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
        ch2KillerDialog.NextBtn.SetActive(true);
        ch2PlumberDialog.NextBtn.SetActive(true);
        game2DiaBtn.SetActive(true);
        game3DiaBtn.SetActive(true);
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.3f);
        ch2KillerDialog.HorseshoeFlashing.SetActive(true);
        ch2PlumberDialog.HorseshoeFlashing.SetActive(true);
    }

    public bool isFinished()
    {
        return !isActive;
    }

    public void NextString()
    {
        if (!istypingfeedback)
        {
            currentSentence++;
            if (currentSentence == Dialog.Length)
            {
                currentSentence = 0;//進入Twodialog
                stage++;
                if (KillerDialogCanvas.activeSelf)
                {
                    ch2KillerDialog.StartDialog();
                }
                else if (PlumberDialogCanvas.activeSelf)
                {
                    ch2PlumberDialog.StartDialog();
                }
            }
            else
            {
                words = Dialog[currentSentence];
                myText.text = "";
                StartEffect();
            }
        }
    }

    public void typingDefaultStr()
    {
        OnFinish();
        words = defaultstr;
        StartEffect();
    }

    public void ImportDialog(string[] dia)
    {
        Dialog = dia;
        currentSentence = 0;
        words = Dialog[currentSentence];
        myText.text = "";
        isActive = true;
        istypingfeedback = false;
    }
    bool istypingfeedback = false;
    public void TypingSkillFeedback(string SkillFeedbackstr)
    {
        timer = 0;
        currentPos = 0;
        words = SkillFeedbackstr;
        myText.text = "";
        StartEffect();
        istypingfeedback = true;
    }
}
