using UnityEngine;
using System.Collections;
// using UnityEngine;
using UnityEngine.UI;

public class SceneChangeTypingFX : MonoBehaviour
{
    // backgroundText 的內容
    const string CH2Text = "第二章";
    const string MythText = "MYTH平台";
    // ============================================================
    public float charsPerSecond = 1f;//打字時間間隔
    private string words;//保存需要顯示的文字
    private bool isActive = false;
    private float timer;//計時器
    private Text myText;
    private int currentPos = 0;//當前打字位置

    SceneManagement sceneManagement;

    Ch2VisualManager ch2VisualManager;

    VisualManager ch1visualManager;

    // Use this for initialization
    void Start()
    {
        sceneManagement = GameObject.Find("SceneManager").GetComponent<SceneManagement>();

        if (this.gameObject.name == "Text_Myth_insecence2" || this.gameObject.name == "Text_demotittle")
        {
            ch2VisualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();
        }

        timer = 0;
        isActive = true;
        // charsPerSecond = Mathf.Max(0.001f, charsPerSecond);
        myText = GetComponent<Text>();
        words = myText.text;
        myText.text = "";


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
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
        if (this.gameObject.name == "Text_CH2")
        {
            SceneManagement.isCh1ToCh2 = true;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            sceneManagement.StartCoroutine(sceneManagement.PreLoadScene2());
        }
        else if (this.gameObject.name == "Text_Myth")
        {
            sceneManagement.StartCoroutine(sceneManagement.LoadToOtherScene());
        }
        else if (this.gameObject.name == "Text_CH3" || this.gameObject.name == "Text_CH2_insecence2" || this.gameObject.name == "Text_CH1" || this.gameObject.name == "Text_DemoVer")
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (this.gameObject.name == "Text_demotittle")
        {
            ch2VisualManager.ShowQrcode();
            this.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
        }
        else if (this.gameObject.name == "Text_Myth_insecence2")
        {
            StartCoroutine(WaitoCh2());
        }
    }

    public bool isFinished()
    {
        return !isActive;
    }

    IEnumerator WaitoCh2()
    {
        yield return new WaitForSeconds(1.2f);
        ch2VisualManager.Ch2TransitionAni();
    }
}
