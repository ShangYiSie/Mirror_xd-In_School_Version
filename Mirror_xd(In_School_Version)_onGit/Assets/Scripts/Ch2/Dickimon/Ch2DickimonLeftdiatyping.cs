using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch2DickimonLeftdiatyping : MonoBehaviour
{
    //==========================Typing=============================
    string defaultstr = "要做些甚麼呢?";
    public float charsPerSecond = 0.05f;//打字時間間隔
    private string words;//保存需要顯示的文字
    private bool isActive = false;
    private float timer;//計時器
    private Text myText;
    private int currentPos = 0;//當前打字位置

    private void Awake()
    {
        myText = this.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        isActive = false;
        // StartCoroutine(firstco);

        typDefault();

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
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//刷新文本顯示内容

                if (currentPos >= words.Length)
                {
                    // this.transform.GetChild(0).gameObject.SetActive(true);
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
    }

    public bool isFinished()
    {
        return !isActive;
    }

    public void TypingDialog(string feedbackstr)
    {
        // this.transform.GetChild(0).gameObject.SetActive(false);
        words = feedbackstr;
        myText.text = "";
        isActive = true;
    }
    public void typDefault()
    {
        // this.transform.GetChild(0).gameObject.SetActive(false);
        timer = 0;
        currentPos = 0;
        words = defaultstr;
        StartEffect();
    }
}
