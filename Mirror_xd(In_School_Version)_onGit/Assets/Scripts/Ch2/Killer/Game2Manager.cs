using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Game2Manager : MonoBehaviour
{
    Ch2DialogueManager ch2DialogueManager;

    public GameObject Character;
    [Header("視窗關閉btn")]
    public Button CloseBtn;
    #region 角色物件
    public GameObject XiaoMing;
    public GameObject BricksKiller;
    public GameObject Plumber;
    public GameObject Ball;

    public GameObject Bricks1;
    public GameObject Bricks2;
    #endregion

    #region 角色動畫
    public Animator XiaoMingAni;
    public Animator PlumberAni;
    [Header("過場動畫物件")]
    public Animator BlackBallAni;
    public Animator Horse_InsideAni;
    public Animator Horse_OutSideAni;
    public Animator SafeAni;
    [Header("金幣")]
    public GameObject Coin;
    public GameObject DialogCanvas;
    public Sprite Bricks2Broken;
    #endregion

    #region 滑鼠操作說明圖&文字
    [Header("磚塊殺手和大金剛技能說明圖和文字")]
    public Sprite[] SkillSprite = new Sprite[2];
    public string[] SkillTexts = { "移動         技能", "拖移" };
    public GameObject SkillImg;
    public Text SkillText;

    #endregion
    public Vector2 mouseWorldPosition;
    public Vector2 charPos;
    private float step;
    public float speed = 3f;
    public float xMin;
    public float xMax;
    public bool walk = true;
    public bool isPlayAni = false;

    [Header("第一次")]
    public bool PlumberBreakFirst = true;
    public bool MingThrowBallFirst = true;
    public bool FirstOpenDialog = true;
    public bool UnlockSafeFirst = true;

    [Header("鎖攻擊")]
    public GameObject g2DiaCanvas;
    public GameObject horse;
    [Header("鎖水電工CD")]
    public bool isJump = false;

    // Start is called before the first frame update
    void Start()
    {
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        /*if(Character != null)
        {
            mouseWorldPosition = Character.transform.position;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayAni)
        {
            mouseWorldPosition = Character.transform.position;
        }
        if ((int)Ch2GameManager.nowGames == 1 && this.gameObject.activeSelf)
        {
            mouseWorldPosition.x = Mathf.Clamp(mouseWorldPosition.x, xMin, xMax);
            if (Mathf.Abs(charPos.x - mouseWorldPosition.x) > 0.01 && Character != BricksKiller)
            {
                switch (walk)
                {
                    case true:
                        Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                        break;
                    case false:
                        Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                        break;
                }
                if (Character == XiaoMing)
                {
                    XiaoMingAni.SetBool("walk", true);
                    XiaoMingAni.SetBool("attack", false);
                }
                else if (Character == Plumber)
                {
                    PlumberAni.SetBool("walk", true);
                    PlumberAni.SetBool("jump", false);
                }
                charPos = Character.transform.position + Character.transform.forward;
                step = speed * Time.deltaTime;
                // move to mouse position
                Character.transform.position = Vector2.MoveTowards(charPos, new Vector2(mouseWorldPosition.x, charPos.y), step);
            }
            else if (Mathf.Abs(charPos.x - mouseWorldPosition.x) <= 0.01 && Character != BricksKiller)
            {
                if (Character == XiaoMing)
                {
                    XiaoMingAni.SetBool("walk", false);
                    XiaoMingAni.SetBool("attack", false);
                }
                else if (Character == Plumber)
                {
                    PlumberAni.SetBool("walk", false);
                    PlumberAni.SetBool("jump", false);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                mouseWorldPosition.x = Character.transform.position.x;
                Attack();
            }
        }
        if (CheckBricks())
        {
            if (UnlockSafeFirst)
            {
                SafeAni.enabled = true;
                UnlockSafeFirst = false;
                StartCoroutine(WaitForSecend());
            }
            if (Coin != null && Coin.GetComponent<Ch2ItemEventManager>() == null)
            {
                Coin.AddComponent<Ch2ItemEventManager>();
            }
        }
    }
    public IEnumerator WaitForSecend()
    {
        yield return new WaitForSeconds(1f);

        GameObject[] Particals;
        Particals = GameObject.FindGameObjectsWithTag("Partical");
        foreach (GameObject Partical in Particals)
        {
            Destroy(Partical);
        }
    }
    public void Move()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 tempWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (tempWorldPos.x < 4.46 && tempWorldPos.x > -4.56)
        {
            mouseWorldPosition.x = tempWorldPos.x;
            if (charPos.x > mouseWorldPosition.x)
            {
                walk = false;//往左是false
            }
            else
            {
                walk = true;//往右是true
            }
        }

    }
    public void Attack()
    {
        if (Character == XiaoMing && !g2DiaCanvas.activeSelf && horse == null)
        {
            if (MingThrowBallFirst)
            {
                // 觸發文本
                ch2DialogueManager.ShowNextDialogue("用小明進入遊戲第一次扔球", true);
                MingThrowBallFirst = false;
            }
            Instantiate(Ball, Character.transform.position, Character.transform.rotation);
            mouseWorldPosition.x = Character.transform.position.x;
            XiaoMingAni.SetBool("walk", false);
            XiaoMingAni.SetBool("attack", true);
        }
        else if (Character == Plumber && !g2DiaCanvas.activeSelf && horse == null && isJump == false)
        {
            isJump = true;
            mouseWorldPosition.x = Character.transform.position.x;
            PlumberAni.SetBool("walk", false);
            PlumberAni.SetBool("jump", true);
            Plumber.transform.DOLocalMoveY(3.9f, 0.5f, false).OnComplete(() =>
            {
                if (PlumberBreakFirst)
                {
                    // 觸發文本
                    ch2DialogueManager.ShowNextDialogue("用水電工進入遊戲打破第一個磚塊", true);
                    PlumberBreakFirst = false;
                }
                Plumber.transform.DOLocalMoveY(-10.29f, 0.8f, false).OnComplete(() =>
                {
                    isJump = false;
                });
            });
        }
    }
    public void CheckCharactor()
    {
        if (XiaoMing.activeSelf)
        {
            Character = XiaoMing;
            xMin = -4.05f;
            xMax = 1.23f;
            SkillImg.GetComponent<SpriteRenderer>().sprite = SkillSprite[0];
            SkillText.text = SkillTexts[0];
            mouseWorldPosition.x = Character.transform.position.x;
        }
        else if (BricksKiller.activeSelf)
        {
            Character = BricksKiller;
            xMin = -3.76f;
            xMax = 1.04f;
            SkillImg.GetComponent<SpriteRenderer>().sprite = SkillSprite[1];
            SkillText.text = SkillTexts[1];
        }
        else if (Plumber.activeSelf)
        {
            Character = Plumber;
            xMin = -4.05f;
            xMax = 1.23f;
            SkillImg.GetComponent<SpriteRenderer>().sprite = SkillSprite[0];
            SkillText.text = SkillTexts[0];
            mouseWorldPosition.x = Character.transform.position.x;
        }
    }
    public bool CheckBricks()
    {
        if (Bricks1.transform.childCount == 0 && Bricks2.transform.childCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
