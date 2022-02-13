using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{

    public float charsPerSecond = 0.01f;//打字時間間隔
    private string words;//保存需要顯示的文字
    private bool isActive = false;
    private float timer;//計時器
    private Text myText;
    private int currentPos = 0;//當前打字位置

    public GameObject MainManagement;
    MainManager mainManager;


    // Use this for initialization
    void Start()
    {
        mainManager = MainManagement.GetComponent<MainManager>();
        // ===================
        timer = 0;
        isActive = true;
        charsPerSecond = Mathf.Max(0.001f, charsPerSecond);
        myText = gameObject.GetComponent<Text>();
        words = myText.text;
        myText.text = "";
        // ====================
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
        Debug.Log(words);
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
        // ======================
        mainManager.FinishTyping();
    }

}