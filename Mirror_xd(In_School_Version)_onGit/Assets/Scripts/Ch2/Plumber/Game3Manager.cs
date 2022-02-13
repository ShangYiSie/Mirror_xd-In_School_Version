using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Game3Manager : MonoBehaviour
{
    #region Manager
    Ch2GameManager ch2GameManager;
    Ch2DialogueManager ch2DialogueManager;
    BGMManager bgmManager;
    Ch2VisualManager ch2VisualManager;
    Ch2CursorSetting cursorSetting;

    #endregion

    #region 樓梯&地板&牆
    public GameObject[] Floor = new GameObject[3];
    public GameObject[] Wall = new GameObject[3];
    public GameObject Ladder;
    public GameObject Ladder1;
    public GameObject Ladder3;
    [Header("第一層和第二層物件")]
    public SpriteRenderer BalckBg;
    public GameObject[] Level = new GameObject[2];
    public GameObject DialogCanvas;
    public GameObject BigDick;

    #endregion

    #region 角色物件
    [Header("空的角色物件")]
    public GameObject Character;

    [Header("浣熊")]
    public GameObject Araiguma;

    [Header("小明")]
    public GameObject XiaoMing;

    [Header("槓")]
    public GameObject BricksKiller;

    [Header("水電工")]
    public GameObject Plumber;

    [Header("垃圾桶Prefab")]
    public GameObject Trashcan;
    #endregion

    #region 角色動畫
    [Header("浣熊動畫")]
    public Animator AraigumaAni;
    #endregion

    #region 角色移動
    [Header("往上和往下的箭頭")]
    public GameObject UpArrow;
    public GameObject DownArrow;
    public Vector2 mouseWorldPosition;
    private Vector2 charPos;
    private float step;
    private float speed = 2f;
    public float xMin;
    public float xMax;
    public bool walk = true;
    #endregion

    #region 參考的布林變數 
    [Header("判斷是否撞到槓")]
    public bool isCollisionKiller = false;

    [Header("判斷水電工有沒有Trigger到樓梯")]
    public bool isTriggerLadder = false;//判斷有沒有碰到樓梯，再搬走樓梯

    [Header("判斷有沒有碰到樓梯")]
    public bool isTriggerLadder1 = false;
    public bool isTriggerLadder2 = false;

    [Header("判斷現在是不是在第二層")]
    public bool isinLevel2 = false;
    [Header("判斷是否再爬梯子")]
    public bool isClambLadder = false;
    [Header("判斷有沒有在播動畫")]
    public bool isPlayAni = false;

    public bool FirstOpenDialog = true;

    public bool isMoveLadder = false;

    public bool isDie = false;

    public static bool araigumaDie = false;
    #endregion

    #region 木馬過場動畫

    [Header("美式木馬")]
    public GameObject Horse_A;
    [Header("像素木馬")]
    public GameObject Horse_C;
    [Header("美式Myth")]
    public GameObject Myth_A;
    [Header("像素Myth")]
    public GameObject Myth_C;
    [Header("第二層木馬")]
    public GameObject Horse;
    public GameObject FramePartical;

    #endregion

    #region 滑鼠操作說明圖&文字
    [Header("磚塊殺手和大金剛技能說明圖和文字")]
    public Sprite[] SkillSprite = new Sprite[2];
    public string[] SkillTexts = { "移動    技能", "拖移", "移動" };
    public GameObject SkillImg;
    public Text SkillText;

    #endregion

    public Button CloseBtn;
    [Header("破鏡子BTN")]
    public Button ToBackBtn;
    public Vector2 CharacterPos;
    public Vector2 LadderPos;
    public int Stage = 1;
    public int WhitchFloor;

    AudioManager audioManager;

    [Header("第一次")]
    public static bool TrashKillerCharFirst = true;

    // Start is called before the first frame update

    private void Awake()
    {
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();
    }
    void Start()
    {
        //defaultset
        TrashKillerCharFirst = true;
        //defaultset

        ch2GameManager = GameObject.Find("GameManager").GetComponent<Ch2GameManager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        ch2VisualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();

        Initialize();
        // GoUpLevel2();
        // MythAppear();
    }

    // Update is called once per frame
    void Update()
    {
        if (araigumaDie)
        {
            AraigumaAni.SetTrigger("die2");
        }
        if (Character != null)
        {
            if (Character.transform.position.y < -30.8)
            {
                //暫時先這樣寫:)
                Character.transform.localPosition = new Vector2(-4.5f, -10.1f);
                Character.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                mouseWorldPosition.x = Character.transform.position.x;
                Character.GetComponent<Animator>().SetBool("walk", false);
                if (Character == Plumber)
                {
                    Character.GetComponent<Animator>().SetBool("ladderwalk", false);
                    Character.GetComponent<Animator>().SetBool("ladderidle", false);
                }
                Character.GetComponent<Animator>().SetBool("clamb", false);
                Character.GetComponent<Animator>().SetBool("die", false);
                UpArrow.SetActive(false);
                DownArrow.SetActive(false);
                isDie = false;
            }
            switch (WhitchFloor)//控制箭頭開關
            {
                case 1:
                    if (Mathf.Abs(Ladder1.transform.position.x - Character.transform.position.x) <= 0.1f)//第一層向上箭頭開關
                    {
                        UpArrow.SetActive(true);
                        DownArrow.SetActive(false);
                    }
                    else
                    {
                        UpArrow.SetActive(false);
                        DownArrow.SetActive(false);
                    }
                    break;
                case 2:
                    if (Mathf.Abs(Ladder1.transform.position.x - Character.transform.position.x) <= 0.1f && !isMoveLadder)
                    {
                        UpArrow.SetActive(false);
                        DownArrow.SetActive(true);
                    }
                    else if (Mathf.Abs(Ladder.transform.position.x - Character.transform.position.x) <= 0.1f && !isMoveLadder)
                    {
                        UpArrow.SetActive(true);
                        DownArrow.SetActive(false);
                    }
                    else
                    {
                        UpArrow.SetActive(false);
                        DownArrow.SetActive(false);
                    }
                    break;
                case 3:
                    if (Mathf.Abs(Ladder.transform.position.x - Character.transform.position.x) <= 0.1f)
                    {
                        DownArrow.SetActive(true);
                        UpArrow.SetActive(false);
                    }
                    else
                    {
                        UpArrow.SetActive(false);
                        DownArrow.SetActive(false);
                    }
                    break;
            }
        }

        if (DialogCanvas.activeSelf)
        {
            mouseWorldPosition = Character.transform.position;
        }
        if (isClambLadder || isPlayAni || Stage == 2 || isDie)
        {
            mouseWorldPosition.x = Character.transform.position.x;
        }
        else if (Mathf.Abs(charPos.x - mouseWorldPosition.x) > 0.01 && Character != BricksKiller && Character != null)//角色不是槓的移動
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
                Character.GetComponent<Animator>().SetBool("walk", true);
                Character.GetComponent<Animator>().SetBool("attack", false);
            }
            else if (Character == Plumber)
            {
                if (Ladder.transform.parent.name == "pb_Plumber")//水電工拿到樓梯移動
                {
                    Character.GetComponent<Animator>().SetBool("walk", false);
                    Character.GetComponent<Animator>().SetBool("ladderidle", false);
                    Character.GetComponent<Animator>().SetBool("ladderwalk", true);
                }
                else
                {
                    Character.GetComponent<Animator>().SetBool("walk", true);
                    Character.GetComponent<Animator>().SetBool("ladderidle", false);
                    Character.GetComponent<Animator>().SetBool("ladderwalk", false);
                }
            }
            charPos = Character.transform.position + Character.transform.forward;
            step = speed * Time.deltaTime;
            // move to mouse position
            Character.transform.position = Vector2.MoveTowards(charPos, new Vector2(mouseWorldPosition.x, charPos.y), step);
        }
        else if (Mathf.Abs(charPos.x - mouseWorldPosition.x) <= 0.01 && Character != BricksKiller)//角色不是槓的idle
        {
            if (Character == XiaoMing)
            {
                Character.GetComponent<Animator>().SetBool("walk", false);
                Character.GetComponent<Animator>().SetBool("attack", false);
            }
            else if (Character == Plumber)
            {
                if (Ladder.transform.parent.name == "pb_Plumber")
                {
                    Character.GetComponent<Animator>().SetBool("walk", false);
                    Character.GetComponent<Animator>().SetBool("ladderidle", true);
                    Character.GetComponent<Animator>().SetBool("ladderwalk", false);
                }
                else
                {
                    Character.GetComponent<Animator>().SetBool("walk", false);
                    Character.GetComponent<Animator>().SetBool("ladderidle", false);
                    Character.GetComponent<Animator>().SetBool("ladderwalk", false);
                }
            }

        }


        if (Input.GetMouseButtonDown(1) && isDie == false)
        {
            Attack();
        }
    }
    IEnumerator waitforsecondToOpenDialog()
    {
        yield return new WaitForSeconds(1.5f);
        //鼠標沙漏動畫
        Ch2AniEvents.AniDone = true;
        cursorSetting.StopCursorHourglassAni();
        //鼠標沙漏動畫
        Game2and3DialogTypingEffect.stage = 5;
        DialogCanvas.SetActive(true);
        DialogCanvas.GetComponent<Ch2PlumberDialog>().StartDialog();

    }
    public void Move()//水電工和小明的移動
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 tempWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (tempWorldPos.x < 5.06 && tempWorldPos.x > -4.96)
        {
            //Debug.Log(mouseWorldPosition.x);
            switch (WhitchFloor)
            {
                case 1:
                    mouseWorldPosition.x = Mathf.Clamp(tempWorldPos.x, -1.05f, 2.91f);
                    break;
                case 2:
                    mouseWorldPosition.x = Mathf.Clamp(tempWorldPos.x, -3.62f, 2.23f);
                    break;
                case 3:
                    mouseWorldPosition.x = Mathf.Clamp(tempWorldPos.x, -2.84f, 4f);
                    break;
            }
            //mouseWorldPosition.x = tempWorldPos.x;
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
    public void Attack()//水電工拿樓梯
    {
        if (Character == Plumber)
        {
            if (isTriggerLadder && WhitchFloor == 2 && !isClambLadder)
            {
                // 觸發拿到梯子的音效
                audioManager.PlumberUseLadder_Raccoon();
                // ===========================================
                Ladder.transform.SetParent(Plumber.transform);
                Ladder.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 90);
                isTriggerLadder = !isTriggerLadder;
                UpArrow.SetActive(false);
                isMoveLadder = true;
            }
            else if (!isTriggerLadder && WhitchFloor == 2 && !isClambLadder)
            {
                Ladder.transform.SetParent(GameObject.Find("ladder").transform);
                Ladder.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                isMoveLadder = false;
            }
        }
    }
    public void CheckCharactor()//判定場上是什麼角色，清除場面所有多餘的垃圾桶
    {
        GameObject[] Trashcan;
        Trashcan = GameObject.FindGameObjectsWithTag("Trashcan");
        isDie = false;
        if (XiaoMing.activeSelf)
        {
            Character = XiaoMing;
            UpArrow = XiaoMing.transform.GetChild(0).gameObject;
            DownArrow = XiaoMing.transform.GetChild(1).gameObject;
            SkillImg.GetComponent<SpriteRenderer>().sprite = SkillSprite[1];
            SkillText.text = SkillTexts[2];
            mouseWorldPosition.x = Character.transform.position.x;
        }
        else if (BricksKiller.activeSelf)
        {
            Character = BricksKiller;
            SkillImg.GetComponent<SpriteRenderer>().sprite = SkillSprite[1];
            SkillText.text = SkillTexts[1];
        }
        else if (Plumber.activeSelf)
        {
            Character = Plumber;
            UpArrow = Plumber.transform.GetChild(0).gameObject;
            DownArrow = Plumber.transform.GetChild(1).gameObject;
            SkillImg.GetComponent<SpriteRenderer>().sprite = SkillSprite[0];
            SkillText.text = SkillTexts[0];
            mouseWorldPosition.x = Character.transform.position.x;
        }
        else
        {
            Character = null;
        }
        foreach (GameObject trashcan in Trashcan)//清除垃圾桶
        {
            Destroy(trashcan);
        }
        if (isinLevel2)
        {
            Character.transform.localPosition = new Vector2(-14.7f, -5f);
        }
    }
    public void AraigumaThrowTrashcan()//浣熊丟垃圾桶動畫
    {
        AraigumaAni.SetBool("throw", true);
    }

    #region 初始化設定

    public void Initialize()
    {
        if (Character != null)
        {
            CharacterPos = Character.transform.position;
            LadderPos = Ladder.transform.position;
            Physics2D.IgnoreCollision(Ladder.GetComponent<Collider2D>(), Floor[1].GetComponent<Collider2D>(), true);
            Physics2D.IgnoreCollision(Ladder.GetComponent<Collider2D>(), Floor[2].GetComponent<Collider2D>(), true);
            Physics2D.IgnoreCollision(Ladder1.GetComponent<Collider2D>(), Floor[0].GetComponent<Collider2D>(), true);
            Physics2D.IgnoreCollision(Ladder1.GetComponent<Collider2D>(), Floor[1].GetComponent<Collider2D>(), true);
        }
    }

    #endregion

    public int OnWhitchFloor(string Floor)
    {
        if (Floor == "Floor1")
        {
            return 1;
        }
        else if (Floor == "Floor2")
        {
            return 2;
        }
        else if (Floor == "Floor3")
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    #region 木馬開場動畫
    public void Opening()
    {
        //鼠標沙漏動畫
        Ch2AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        //鼠標沙漏動畫

        ToBackBtn.enabled = false;

        CloseBtn.enabled = false;
        isPlayAni = true;
        //美式木馬從左邊走進來
        Horse_A.transform.DOLocalMoveX(-8.04f, 2f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            //美式木馬切換跳的動畫
            Horse_A.GetComponent<Animator>().SetTrigger("Jump");
            StartCoroutine(wait());
        });
    }

    public void HorseClamb()//木馬走過去樓梯+爬上樓梯
    {
        Horse_C.GetComponent<Animator>().SetTrigger("Walk");
        Horse_C.transform.DOLocalMoveY(2.78f, 3f, false).SetEase(Ease.Linear);
        Horse_C.transform.DOLocalMoveX(3f, 3f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            Horse_C.GetComponent<Animator>().SetTrigger("Clamb");
            Horse_C.transform.DOLocalMoveY(4.61f, 2f, false).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(Horse_C);
                InvokeRepeating("AraigumaThrowTrashcan", 0f, 6f);
                CloseBtn.enabled = true;
                isPlayAni = false;

                ToBackBtn.enabled = true;
                //鼠標沙漏動畫
                Ch2AniEvents.AniDone = true;
                cursorSetting.StopCursorHourglassAni();
                //鼠標沙漏動畫
            });
        });
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        // 觸發文本
        ch2DialogueManager.ShowNextDialogue("玩家第一次點開無情浣熊，木馬跳進來", true);
        //美式木馬跳進視窗
        audioManager.UsaHorseJump_BreakSafe();

        Horse_A.transform.DOLocalJump(new Vector3(-4.07f, 2.11f, 0), 1f, 1, 1f, false).SetEase(Ease.Linear);
        StartCoroutine(wait2());
    }
    IEnumerator wait2()
    {
        yield return new WaitForSeconds(0.55f);

        //像素木馬跳進視窗
        Instantiate(FramePartical, new Vector3(Horse_A.transform.position.x, Horse_A.transform.position.y + 0.7f, Horse_A.transform.position.z), Horse_A.transform.rotation);
        audioManager.UsaHorseTriggerEnter_BreakSafe();
        Horse_C.transform.DOLocalMove(new Vector3(-2.66f, 2.65f, 0), 1f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(Horse_A);
            // =============================
            // 切換BGM
            bgmManager.StopCH2BGM();
            bgmManager.PlayHorseCGBgm();
            // =============================
            //鼠標沙漏動畫
            Ch2AniEvents.AniDone = true;
            cursorSetting.StopCursorHourglassAni();
            //鼠標沙漏動畫

            DialogCanvas.SetActive(true);//打開對話視窗
            ToBackBtn.enabled = false;
            CloseBtn.enabled = false;
            //要記得打開
        });
    }

    public void GoUpLevel2()//跟浣熊對戰後自動走上第二層
    {
        //鼠標動畫
        isClambLadder = true;
        // Ch2AniEvents.AniDone = false;
        // cursorSetting.CursorHourglassAni();

        Character.GetComponent<Rigidbody2D>().isKinematic = true;
        Character.GetComponent<Animator>().SetBool("clamb", false);
        Character.GetComponent<Animator>().SetBool("walk", true);
        mouseWorldPosition.x = Ladder3.transform.position.x;
        Character.transform.DOLocalMove(new Vector3(12.1f, 7.6f, 0), 2.5f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            Character.GetComponent<Animator>().SetBool("walk", false);
            Character.GetComponent<Animator>().SetBool("clamb", true);
            // ========================
            Character.transform.DOLocalMove(new Vector3(12.1f, 18.5f, 0), 2.5f, false).SetEase(Ease.Linear).OnComplete(() =>
            {
                Character.GetComponent<Rigidbody2D>().isKinematic = false;
            });

        });
    }

    public IEnumerator TurnToRac()
    {
        // //鼠標沙漏動畫
        // Ch2AniEvents.AniDone = false;
        // cursorSetting.CursorHourglassAni();
        // //鼠標沙漏動畫

        ch2GameManager.DickimonToRac();
        //Destroy(Araiguma);
        Araiguma.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        BigDick.SetActive(true);
        BigDick.transform.DOScale(new Vector3(0f, 0f, 0), 3f).OnComplete(() =>
        {
            GoUpLevel2();
        });
    }

    public IEnumerator TurnToRacMyth()
    {
        //鼠標沙漏動畫
        Ch2AniEvents.AniDone = false;
        cursorSetting.CursorHourglassAni();
        //鼠標沙漏動畫

        ch2GameManager.DickimonToRac();
        yield return new WaitForSeconds(0.3f);
        BigDick.SetActive(true);
        BigDick.transform.DOScale(new Vector3(0f, 0f, 0), 3f).OnComplete(() =>
        {
            // =======================
            // 觸發文本
            ch2DialogueManager.ShowNextDialogue("MYTH出現", true);
            // =======================
            //DialogCanvas.SetActive(true);
            MythAppear();
        });
    }

    public void MythAppear()//Myth出場動畫
    {
        StartCoroutine(MythJumpAudio(3, 2.5f));
        Myth_A.transform.DOLocalJump(new Vector3(8.09f, -2.11f, 0), 1.5f, 3, 2.5f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            Myth_A.GetComponent<Animator>().enabled = false;
            audioManager.MythJump_Raccoon();
            Myth_A.transform.DOLocalJump(new Vector3(4.28f, 1.72f, 0), 1, 1, 1.5f, false).SetEase(Ease.Linear).OnComplete(() =>
            {
                audioManager.PixelMythJump_Raccoon();
                Instantiate(FramePartical, Myth_C.transform.position, Myth_C.transform.rotation);
                Myth_C.transform.DOLocalJump(new Vector3(2.98f, -0.05f, 0), 1, 1, 1f, false).SetEase(Ease.Linear).OnComplete(() =>
                {
                    //鼠標沙漏動畫
                    Ch2AniEvents.AniDone = true;
                    cursorSetting.StopCursorHourglassAni();
                    //鼠標沙漏動畫
                    Game2and3DialogTypingEffect.stage = 12;
                    DialogCanvas.SetActive(true);
                    ToBackBtn.enabled = false;
                    CloseBtn.enabled = false;
                    DialogCanvas.GetComponent<Ch2PlumberDialog>().StartDialog();
                    // =============================
                    // 切換BGM
                    bgmManager.StopCH2BGM();
                    bgmManager.PlayMythCGBgm();
                    // =============================
                });
            });
        });
    }
    IEnumerator MythJumpAudio(int num, float duration)
    {
        for (int i = 0; i < num; i++)
        {
            audioManager.MythJump_Raccoon();
            yield return new WaitForSeconds(duration / num);
        }
    }
    #endregion

    #region 點箭頭

    public void ClickUpArrow()
    {
        if (isTriggerLadder1)
        {
            Character.GetComponent<Animator>().SetBool("clamb", true);
            Character.GetComponent<Animator>().SetBool("walk", false);
            Physics2D.IgnoreCollision(Character.GetComponent<Collider2D>(), Floor[1].GetComponent<Collider2D>(), true);
            //Character.GetComponent<Collider2D>().isTrigger = true;
            Character.GetComponent<Rigidbody2D>().isKinematic = true;
            isClambLadder = true;
            //Character.transform.DOMoveX(Ladder1.transform.position.x, 0f, true);
            Character.transform.DOLocalMoveY(-2.6f, 2.5f, true).OnComplete(() =>
            {
                isClambLadder = false;
                Character.GetComponent<Animator>().SetBool("clamb", false);
                isTriggerLadder1 = false;
                //Character.GetComponent<Collider2D>().isTrigger = false;
                Character.GetComponent<Rigidbody2D>().isKinematic = false;
                Physics2D.IgnoreCollision(Character.GetComponent<Collider2D>(), Floor[1].GetComponent<Collider2D>(), false);
            });
        }
        else if (isTriggerLadder2)
        {
            Character.GetComponent<Animator>().SetBool("clamb", true);
            Character.GetComponent<Animator>().SetBool("walk", false);
            Physics2D.IgnoreCollision(Character.GetComponent<Collider2D>(), Floor[2].GetComponent<Collider2D>(), true);
            //Character.GetComponent<Collider2D>().isTrigger = true;
            Character.GetComponent<Rigidbody2D>().isKinematic = true;
            isClambLadder = true;
            if (Ladder.transform.position.x < -2.03f)
            {
                //Character.transform.DOMoveX(Ladder.transform.position.x, 0f, true);
                Character.transform.DOLocalMoveY(6.8f, 2.5f, true).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Physics2D.IgnoreCollision(Character.GetComponent<Collider2D>(), Floor[2].GetComponent<Collider2D>(), false);
                    //Character.GetComponent<Collider2D>().isTrigger = false;
                    Character.GetComponent<Rigidbody2D>().isKinematic = false;
                    Character.GetComponent<Animator>().SetBool("clamb", false);
                    isClambLadder = false;
                    isTriggerLadder2 = false;

                });
            }
            else
            {
                Character.transform.DOMoveX(Ladder.transform.position.x, 0f, true);
                Character.transform.DOLocalMoveY(5f, 2.5f, true).OnComplete(() =>
                {
                    isClambLadder = false;
                    Character.GetComponent<Animator>().SetBool("clamb", false);
                    Character.GetComponent<Rigidbody2D>().isKinematic = false;
                    isTriggerLadder2 = false;
                    Character.GetComponent<Collider2D>().isTrigger = false;
                });
            }
        }
    }
    public void ClickDownArrow()
    {
        Character.GetComponent<Animator>().SetBool("clamb", true);
        Character.GetComponent<Animator>().SetBool("walk", false);
        isClambLadder = true;
        if (WhitchFloor == 2)
        {
            Physics2D.IgnoreCollision(Character.GetComponent<Collider2D>(), Floor[1].GetComponent<Collider2D>(), true);
            Character.GetComponent<Collider2D>().isTrigger = true;
            Character.transform.DOLocalMoveY(-10.4f, 2.5f, true).OnComplete(() =>
            {
                isClambLadder = false;
                Character.GetComponent<Animator>().SetBool("clamb", false);
                Character.GetComponent<Collider2D>().isTrigger = false;
            });
        }
        else if (WhitchFloor == 3)
        {
            Physics2D.IgnoreCollision(Character.GetComponent<Collider2D>(), Floor[2].GetComponent<Collider2D>(), true);
            Character.GetComponent<Collider2D>().isTrigger = true;
            Character.transform.DOLocalMoveY(-0.6f, 2.5f, true).OnComplete(() =>
            {
                isClambLadder = false;
                Character.GetComponent<Animator>().SetBool("clamb", false);
                Character.GetComponent<Collider2D>().isTrigger = false;
            });
        }
    }

    #endregion
}
