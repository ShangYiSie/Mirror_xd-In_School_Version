using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class Ch2ItemEventManager : MonoBehaviour
{
    Ch2VisualManager ch2VisualManager;
    Ch2Usermanager ch2Usermanager;
    Ch2DialogueManager ch2DialogueManager;
    Ch2GameManager ch2GameManager;
    AudioManager audioManager;

    public GameObject DickimonGamtRoot;
    Ch2DickimonSetState DickimonSetState;

    Ch2CursorSetting cursorSetting;

    [Header("紀錄正反面scale")]
    public Vector3 tempScale;
    public Vector2 colliderSize;
    public bool isOnBar;
    public bool PlayerGet = false;
    GameObject nowTriggerTarget = null;

    [Header("海報Hover物件")]

    public GameObject Poster2;

    public GameObject Poster3;

    [Header("名牌是否在框內")]
    public bool isOnFrame = true;

    Vector3 screenSpace;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        ch2Usermanager = GameObject.Find("UserManager").GetComponent<Ch2Usermanager>();
        ch2VisualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        ch2GameManager = GameObject.Find("GameManager").GetComponent<Ch2GameManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();

        if (this.name == "Key")
        {
            DickimonSetState = DickimonGamtRoot.GetComponent<Ch2DickimonSetState>();
        }
        tempScale = new Vector3(1f, 1f, 0);
        if (this.name == "XiaoMing" || this.name == "BricksKiller" || this.name == "Plumber")
        {
            colliderSize = new Vector2(1.3f, 0.8f);

            Physics2D.IgnoreCollision(ch2VisualManager.BricksKiller_Locked.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(ch2VisualManager.Plumber_Locked.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            //Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), ch2Usermanager.Character.GetComponent<Collider2D>());
        }
        else if (this.name == "Coin")
        {
            Poster3 = ch2VisualManager.Game3Btn;
            colliderSize = new Vector2(1f, 1f);
        }
        else
        {
            colliderSize = gameObject.GetComponent<BoxCollider2D>().size;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isonUI = false;
    public void OnMouseDown()
    {
        if (Ch2AniEvents.AniDone)
        {
            DOTween.Kill(this.name);
            audioManager.ClickProps();
            if (this.name == "XiaoMing" || this.name == "BricksKiller" || this.name == "Plumber")
            {
                if ((int)Ch2GameManager.nowCH2State % 2 != 0)//反面
                {
                    foreach (Collider2D collider in ch2VisualManager.BackitemCollider)
                    {
                        if (collider != this.GetComponent<Collider2D>() && collider != null)
                        {
                            Physics2D.IgnoreCollision(collider, this.GetComponent<Collider2D>());
                        }
                    }
                }
                else
                {
                    foreach (Collider2D collider in ch2VisualManager.FrontitemCollider)
                    {
                        if (collider != this.GetComponent<Collider2D>() && collider != null)
                        {
                            Physics2D.IgnoreCollision(collider, this.GetComponent<Collider2D>());
                        }
                    }
                }
            }
            else if (this.name == "Key" || this.name == "Coin")
            {
                if ((int)Ch2GameManager.nowCH2State % 2 != 0)//反面
                {
                    foreach (Collider2D collider in ch2VisualManager.BackitemCollider)
                    {
                        if (this.name == "Key" && collider != this.GetComponent<Collider2D>() && collider.name != "BricksKiller_Locked" && collider != null)
                        {
                            Physics2D.IgnoreCollision(collider, this.GetComponent<Collider2D>());
                        }
                        else if (this.name == "Coin" && collider != this.GetComponent<Collider2D>() && collider.name != "Plumber_Locked" && collider != null)
                        {
                            Physics2D.IgnoreCollision(collider, this.GetComponent<Collider2D>());
                        }
                    }
                    foreach (GameObject collider in GameObject.FindGameObjectsWithTag("Frame"))
                    {
                        Physics2D.IgnoreCollision(collider.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                    }
                }
                else
                {
                    foreach (Collider2D collider in ch2VisualManager.FrontitemCollider)
                    {
                        if (collider != this.GetComponent<Collider2D>() && collider != null)
                        {
                            Physics2D.IgnoreCollision(collider, this.GetComponent<Collider2D>());
                        }
                    }
                }
            }
            if (this.name == "Key" && !PlayerGet)
            {
                // DickimonSetState.AutoNextString();
                DickimonSetState.setGetkey();
                // ==========================
                // 觸發得到鑰匙文本
                ch2DialogueManager.ShowNextDialogue("得到鑰匙", true);
                // 播放取得鑰匙音效
                audioManager.ClickDickimonKey_Dickimon();
                audioManager.YouGetKey_Dickimon();
                // ==========================
                this.tag = "Prop";
                this.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
                ch2VisualManager.SetDickimonGetKey();
                PlayerGet = true;
                this.transform.parent = GameObject.Find("Tools").transform;
                this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                this.transform.DOLocalMoveY(-3.4f, 2f).SetId<Tween>(this.name);
            }
            if ((this.name == "XiaoMing" || this.name == "BricksKiller" || this.name == "Plumber") && this.tag != "Prop")//之後相同的東西要獨立拉出來做Function
            {
                //Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), ch2Usermanager.Character.GetComponent<BoxCollider2D>(), true);
                switch (this.name)
                {
                    case "XiaoMing":
                        if (!PlayerGet)
                        {
                            ch2Usermanager.Brand = this.name;
                            switch (this.transform.parent.name)
                            {
                                case "Game1_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < -4.3 || ch2Usermanager.Character.transform.position.x > -1.1))//在範圍外面
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickXiaoMing();

                                        if (ch2Usermanager.Character.transform.position.x < -4.3)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickXiaoMing();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = -1.1f; ;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[0].enabled = true;
                                    break;
                                case "Game2_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < 1.07f || ch2Usermanager.Character.transform.position.x > 4.18f) && Ch2AniEvents.AniDone == true)
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickXiaoMing();
                                        if (ch2Usermanager.Character.transform.position.x < 1.07f)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickXiaoMing();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[1].enabled = true;
                                    break;
                                case "Game3_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < 6.27f || ch2Usermanager.Character.transform.position.x > 9.4f) && Ch2AniEvents.AniDone == true)
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickXiaoMing();

                                        if (ch2Usermanager.Character.transform.position.x < 6.27f)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickXiaoMing();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[2].enabled = true;
                                    break;
                            }
                        }
                        break;
                    case "BricksKiller":
                        if (!PlayerGet)
                        {
                            ch2Usermanager.Brand = this.name;
                            switch (this.transform.parent.name)
                            {
                                case "Game1_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < -4.3 || ch2Usermanager.Character.transform.position.x > -1.1) && Ch2AniEvents.AniDone == true)
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickBricksKiller();

                                        if (ch2Usermanager.Character.transform.position.x < -4.3f)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickBricksKiller();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[0].enabled = true;
                                    break;
                                case "Game2_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < 1.07 || ch2Usermanager.Character.transform.position.x > 4.18) && Ch2AniEvents.AniDone == true)
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickBricksKiller();

                                        if (ch2Usermanager.Character.transform.position.x < 1.07f)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickBricksKiller();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[1].enabled = true;
                                    break;
                                case "Game3_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < 6.27 || ch2Usermanager.Character.transform.position.x > 9.4) && Ch2AniEvents.AniDone == true)
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickBricksKiller();

                                        if (ch2Usermanager.Character.transform.position.x < 6.27f)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickBricksKiller();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[2].enabled = true;
                                    break;
                            }
                        }
                        break;
                    case "Plumber":
                        if (!PlayerGet)
                        {
                            ch2Usermanager.Brand = this.name;
                            switch (this.transform.parent.name)
                            {
                                case "Game1_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < -4.3 || ch2Usermanager.Character.transform.position.x > -1.1) && Ch2AniEvents.AniDone == true)
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickPlumber();

                                        if (ch2Usermanager.Character.transform.position.x < -4.3f)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickPlumber();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[0].enabled = true;
                                    break;
                                case "Game2_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < 1.07 || ch2Usermanager.Character.transform.position.x > 4.18) && Ch2AniEvents.AniDone == true)
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickPlumber();

                                        if (ch2Usermanager.Character.transform.position.x < 1.07f)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickPlumber();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[1].enabled = true;
                                    break;
                                case "Game3_Frame":
                                    if ((ch2Usermanager.Character.transform.position.x < 6.27 || ch2Usermanager.Character.transform.position.x > 9.4) && Ch2AniEvents.AniDone == true)
                                    {
                                        //鼠標沙漏動畫
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickPlumber();

                                        if (ch2Usermanager.Character.transform.position.x < 6.27f)//轉向後走過去
                                        {
                                            ch2Usermanager.walk = true;
                                            ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                        }
                                        else//轉向後走過去
                                        {
                                            ch2Usermanager.walk = false;
                                            ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                        }
                                    }
                                    else//在名牌範圍內
                                    {
                                        Ch2AniEvents.AniDone = false;
                                        cursorSetting.CursorHourglassAni();

                                        this.tag = "Prop";
                                        ch2Usermanager.ClickPlumber();

                                        if (ch2Usermanager.walk)
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                        }
                                        else
                                        {
                                            ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                        }
                                    }
                                    ch2VisualManager.BrandFrame[2].enabled = true;
                                    break;
                            }
                        }
                        break;
                }
            }
            if (this.name == "Coin" && !PlayerGet)
            {
                // ========================
                audioManager.ClickCoin_BreakSafe();
                // ========================
                PlayerGet = true;
                this.tag = "Prop";
                this.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
                this.transform.parent = GameObject.Find("Tools").transform;
                this.transform.DOLocalMoveY(-2.5f, 2f).SetId<Tween>(this.name);
            }
        }
    }
    Vector3 curPosition;
    public void OnMouseDrag()
    {
        if (Ch2AniEvents.AniDone)
        {
            if ((int)Ch2GameManager.nowCH2State % 2 != 0)
            {
                // 離開背面的Bar
                gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
                ch2VisualManager.ToolsExitBar(this.gameObject, tempScale, colliderSize);
                isOnBar = false;
            }
            if (PlayerGet)
            {
                Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -3);
                curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
                // transform.position = curPosition;
                transform.position = new Vector3(Mathf.Clamp(curPosition.x, -9f, 9f), Mathf.Clamp(curPosition.y, -5f, 5f));
                gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
            }

            if (this.name == "Frame" && !isonUI)
            {
                Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -3);
                curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
                // transform.position = curPosition;
                transform.position = new Vector3(Mathf.Clamp(curPosition.x, -9f, 9f), Mathf.Clamp(curPosition.y, -5f, 5f));
            }

            if (this.name != "Frame")
            {
                if (nowTriggerTarget != null)
                {
                    switch (this.name)
                    {
                        case "Key":
                            if (nowTriggerTarget.name == "BricksKiller_Locked" || nowTriggerTarget.name == "Dog")
                            {
                                this.GetComponentInChildren<Ch2ButtonFX>().TriggerotherFX();
                            }
                            break;
                        case "Coin":
                            if (nowTriggerTarget.name == "Plumber_Locked" || nowTriggerTarget.name == "Dog")
                            {
                                this.GetComponentInChildren<Ch2ButtonFX>().TriggerotherFX();
                            }
                            break;
                        case "XiaoMing":
                            if (nowTriggerTarget.tag == "Frame" || nowTriggerTarget.name == "Dog")
                            {
                                this.GetComponentInChildren<Ch2ButtonFX>().TriggerotherFX();
                            }
                            break;
                        case "BricksKiller":
                            if (nowTriggerTarget.tag == "Frame" || nowTriggerTarget.name == "Dog")
                            {
                                this.GetComponentInChildren<Ch2ButtonFX>().TriggerotherFX();
                            }
                            break;
                        case "Plumber":
                            if (nowTriggerTarget.tag == "Frame" || nowTriggerTarget.name == "Dog")
                            {
                                this.GetComponentInChildren<Ch2ButtonFX>().TriggerotherFX();
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.GetComponentInChildren<Ch2ButtonFX>().ExitotherFX();
                }
            }
        }
    }
    public void OnMouseUp()
    {
        if (Ch2AniEvents.AniDone)
        {
            if (this.name != "Frame")
            {
                this.GetComponentInChildren<Ch2ButtonFX>().ExitotherFX();
            }
            if (this.name != "Frame")
            {
                if (nowTriggerTarget != null && nowTriggerTarget.tag == "Frame" && (int)Ch2GameManager.nowCH2State % 2 != 0)
                {
                    switch (this.name)
                    {
                        case "XiaoMing":
                            if (nowTriggerTarget.name == "Game1_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < -4.3f || ch2Usermanager.Character.transform.position.x > -1.1f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnXiaoMing();

                                    if (ch2Usermanager.Character.transform.position.x < -4.3f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnXiaoMing();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                    }
                                }

                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game1_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.XiaoMing.transform.SetParent(GameObject.Find("Game1_Frame").gameObject.transform);
                                ch2VisualManager.XiaoMing.transform.position = ch2VisualManager.XiaoMing.transform.parent.transform.position;
                                ch2VisualManager.XiaoMing.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[0].enabled = false;

                            }
                            else if (nowTriggerTarget.name == "Game2_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < 1.07f || ch2Usermanager.Character.transform.position.x > 4.18f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnXiaoMing();

                                    if (ch2Usermanager.Character.transform.position.x < 1.07f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnXiaoMing();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                    }
                                }

                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game1_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.XiaoMing.transform.SetParent(GameObject.Find("Game2_Frame").gameObject.transform);
                                ch2VisualManager.XiaoMing.transform.position = ch2VisualManager.XiaoMing.transform.parent.transform.position;
                                ch2VisualManager.XiaoMing.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[1].enabled = false;
                            }
                            else if (nowTriggerTarget.name == "Game3_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < 6.27f || ch2Usermanager.Character.transform.position.x > 9.4f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnXiaoMing();

                                    if (ch2Usermanager.Character.transform.position.x < 6.27f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnXiaoMing();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                    }
                                }


                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game1_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.XiaoMing.transform.SetParent(GameObject.Find("Game3_Frame").gameObject.transform);
                                ch2VisualManager.XiaoMing.transform.position = ch2VisualManager.XiaoMing.transform.parent.transform.position;
                                ch2VisualManager.XiaoMing.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[2].enabled = false;
                            }
                            else
                            {
                                this.GetComponent<LayoutElement>().ignoreLayout = false;
                                ch2VisualManager.ToolsTriggerBar(this.gameObject, false);
                            }
                            break;
                        case "BricksKiller":
                            if (nowTriggerTarget.name == "Game1_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < -4.3f || ch2Usermanager.Character.transform.position.x > -1.1f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnBricksKiller();

                                    if (ch2Usermanager.Character.transform.position.x < -4.3f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnBricksKiller();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                    }
                                }


                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game2_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.BricksKiller.transform.SetParent(GameObject.Find("Game1_Frame").gameObject.transform);
                                ch2VisualManager.BricksKiller.transform.position = ch2VisualManager.BricksKiller.transform.parent.transform.position;
                                ch2VisualManager.BricksKiller.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[0].enabled = false;
                            }
                            else if (nowTriggerTarget.name == "Game2_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < 1.07f || ch2Usermanager.Character.transform.position.x > 4.18f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnBricksKiller();

                                    if (ch2Usermanager.Character.transform.position.x < 1.07f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnBricksKiller();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                    }
                                }

                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game2_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.BricksKiller.transform.SetParent(GameObject.Find("Game2_Frame").gameObject.transform);
                                ch2VisualManager.BricksKiller.transform.position = ch2VisualManager.BricksKiller.transform.parent.transform.position;
                                ch2VisualManager.BricksKiller.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[1].enabled = false;
                            }
                            else if (nowTriggerTarget.name == "Game3_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < 6.27f || ch2Usermanager.Character.transform.position.x > 9.4f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnBricksKiller();

                                    if (ch2Usermanager.Character.transform.position.x < 6.27f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnBricksKiller();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                    }
                                }


                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game3_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.BricksKiller.transform.SetParent(GameObject.Find("Game3_Frame").gameObject.transform);
                                ch2VisualManager.BricksKiller.transform.position = ch2VisualManager.BricksKiller.transform.parent.transform.position;
                                ch2VisualManager.BricksKiller.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[2].enabled = false;
                            }
                            else
                            {
                                this.GetComponent<LayoutElement>().ignoreLayout = false;
                                ch2VisualManager.ToolsTriggerBar(this.gameObject, false);
                            }
                            break;
                        case "Plumber":
                            if (nowTriggerTarget.name == "Game1_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < -4.3f || ch2Usermanager.Character.transform.position.x > -1.1f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnPlumber();

                                    if (ch2Usermanager.Character.transform.position.x < -4.3f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnPlumber();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -4.3f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.1f;
                                    }
                                }


                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game3_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.Plumber.transform.SetParent(GameObject.Find("Game1_Frame").gameObject.transform);
                                ch2VisualManager.Plumber.transform.position = ch2VisualManager.Plumber.transform.parent.transform.position;
                                ch2VisualManager.Plumber.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[0].enabled = false;
                            }
                            else if (nowTriggerTarget.name == "Game2_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < 1.07f || ch2Usermanager.Character.transform.position.x > 4.18f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnPlumber();

                                    if (ch2Usermanager.Character.transform.position.x < 1.07f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnPlumber();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 1.07f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 4.18f;
                                    }
                                }


                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game3_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.Plumber.transform.SetParent(GameObject.Find("Game2_Frame").gameObject.transform);
                                ch2VisualManager.Plumber.transform.position = ch2VisualManager.Plumber.transform.parent.transform.position;
                                ch2VisualManager.Plumber.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[1].enabled = false;
                            }
                            else if (nowTriggerTarget.name == "Game3_Frame" && nowTriggerTarget.transform.childCount == 2)
                            {
                                ch2Usermanager.Brand = this.name;
                                if (ch2Usermanager.Character.transform.position.x < 6.27f || ch2Usermanager.Character.transform.position.x > 9.4f)
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnPlumber();

                                    if (ch2Usermanager.Character.transform.position.x < 6.27f)//轉向後走過去
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                    }
                                    else//轉向後走過去
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                    }
                                }
                                else
                                {
                                    //鼠標沙漏動畫
                                    Ch2AniEvents.AniDone = false;
                                    cursorSetting.CursorHourglassAni();

                                    this.tag = "Untagged";
                                    ch2Usermanager.PutOnPlumber();

                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 6.27f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 9.4f;
                                    }
                                }

                                if (nowTriggerTarget.transform.childCount == 2 && !isOnFrame)//只有shining的物件
                                {
                                    if (nowTriggerTarget.name != "Game3_Frame" && Ch2GameManager.PutBrandToDiffFrameFirst)
                                    {
                                        // 第一次把任何名牌裝在不是他的遊戲上 觸發文本
                                        ch2DialogueManager.ShowNextDialogue("第一次把任何名牌裝在不是他的遊戲上", false);
                                        Ch2GameManager.PutBrandToDiffFrameFirst = false;
                                    }
                                }
                                //先放上去然後透明度調為0
                                ch2VisualManager.Plumber.transform.SetParent(GameObject.Find("Game3_Frame").gameObject.transform);
                                ch2VisualManager.Plumber.transform.position = ch2VisualManager.Plumber.transform.parent.transform.position;
                                ch2VisualManager.Plumber.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
                                ch2VisualManager.BrandFrame[2].enabled = false;
                            }
                            else
                            {
                                this.GetComponent<LayoutElement>().ignoreLayout = false;
                                ch2VisualManager.ToolsTriggerBar(this.gameObject, false);
                            }
                            break;
                        default:

                            break;
                    }
                    if (isOnBar)
                    {
                        gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                    }
                }
                else if (nowTriggerTarget != null && PlayerGet)
                {
                    switch (this.name)
                    {
                        case "XiaoMing":
                            if (nowTriggerTarget.name == "Dog")
                            {
                                if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "小明")
                                {
                                    ch2DialogueManager.ShowNextDialogue("給狗狗小明", false);
                                }
                                else
                                {
                                    ch2DialogueManager.ShowNextDialogue("給狗狗任何亂碼的名牌", false);
                                }
                                ch2VisualManager.Bag_Appear();
                                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                            }
                            break;
                        case "BricksKiller":
                            if (nowTriggerTarget.name == "Dog")
                            {
                                if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "磚塊殺手")
                                {
                                    ch2DialogueManager.ShowNextDialogue("給狗狗磚塊殺手", false);
                                }
                                else
                                {
                                    ch2DialogueManager.ShowNextDialogue("給狗狗任何亂碼的名牌", false);
                                }
                                ch2VisualManager.Bag_Appear();
                                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                            }
                            break;
                        case "Plumber":
                            if (nowTriggerTarget.name == "Dog")
                            {
                                if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "水電工")
                                {
                                    ch2DialogueManager.ShowNextDialogue("給狗狗水電工", false);
                                }
                                else
                                {
                                    ch2DialogueManager.ShowNextDialogue("給狗狗任何亂碼的名牌", false);
                                }
                                ch2VisualManager.Bag_Appear();
                                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                            }
                            break;
                        case "Key":
                            if (nowTriggerTarget.name == "BricksKiller_Locked")
                            {
                                Ch2CursorFX.PlayerMovable = true;
                                // 改變點擊狗會說的話
                                ch2DialogueManager.changeDogStage("open_safe");
                                // ===================================
                                ch2GameManager.stage++;

                                //要先收背包再進動畫
                                ch2VisualManager.Bag_Appear();
                                //鼠標沙漏動畫
                                Ch2AniEvents.AniDone = false;
                                cursorSetting.CursorHourglassAni();

                                if (ch2Usermanager.Character.transform.position.x < -1.33f || ch2Usermanager.Character.transform.position.x > 1.71)//在範圍外
                                {
                                    if (ch2Usermanager.Character.transform.position.x < -1.33f)
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.33f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 1.71f;
                                    }
                                }
                                else//在範圍內
                                {
                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.33f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = -1.71f;
                                    }
                                }
                                ch2Usermanager.isUnLockKey = true;

                                //正面換圖
                                Poster2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/game_hover_start");
                                Destroy(gameObject);
                            }
                            else if (nowTriggerTarget.name == "Dog")
                            {
                                ch2VisualManager.Bag_Appear();
                                ch2DialogueManager.ShowNextDialogue("給狗狗鑰匙", false);
                                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                            }
                            else if (!isOnBar)
                            {
                                gameObject.transform.DOLocalMoveY(-0.7f, 0.8f).SetId<Tween>(this.name);
                            }
                            break;
                        case "Coin":
                            if (nowTriggerTarget.name == "Plumber_Locked")
                            {

                                // 改變點擊狗會說的話
                                ch2DialogueManager.changeDogStage("open_raccoon");
                                // ===================================
                                ch2GameManager.stage++;

                                //要先收背包再進動畫
                                ch2VisualManager.Bag_Appear();
                                //鼠標沙漏動畫
                                Ch2AniEvents.AniDone = false;
                                cursorSetting.CursorHourglassAni();

                                if (ch2Usermanager.Character.transform.position.x < 3.59f || ch2Usermanager.Character.transform.position.x > 7.54)//在範圍外
                                {
                                    if (ch2Usermanager.Character.transform.position.x < 3.59f)
                                    {
                                        ch2Usermanager.walk = true;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 3.59f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.walk = false;
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 7.54f;
                                    }
                                }
                                else//在範圍內
                                {
                                    if (ch2Usermanager.walk)
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 3.59f;
                                    }
                                    else
                                    {
                                        ch2Usermanager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                                        ch2Usermanager.mouseWorldPosition.x = 7.54f;
                                    }
                                }
                                ch2Usermanager.isUnLockCoin = true;
                                //正面換圖
                                Poster3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/game_hover_start");
                                Destroy(gameObject);
                            }
                            else if (nowTriggerTarget.name == "Dog")
                            {
                                ch2VisualManager.Bag_Appear();
                                ch2DialogueManager.ShowNextDialogue("給狗狗100塊", false);
                                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                            }
                            else if (!isOnBar)
                            {
                                gameObject.transform.DOLocalMoveY(-0.5f, 0.8f).SetId<Tween>(this.name);
                            }
                            break;
                        default:
                            //if (isOnBar)
                            //{
                            //    gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                            //    ch2VisualManager.ToolsTriggerBar(this.gameObject, true);
                            //}
                            //else
                            //{
                            //    gameObject.transform.DOLocalMoveY(-0.5f, 0.8f).SetId<Tween>(this.name);
                            //}
                            if ((int)Ch2GameManager.nowCH2State % 2 != 0)
                            {
                                // 如果在背面放開物件的話
                                if (isOnBar)
                                {
                                    gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                                }
                                else
                                {
                                    gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                                    ch2VisualManager.ToolsTriggerBar(this.gameObject, false);
                                }

                            }
                            else if ((int)Ch2GameManager.nowCH2State % 2 == 0)
                            {
                                gameObject.transform.DOLocalMoveY(-0.5f, 0.8f).SetId<Tween>(this.name);
                            }
                            break;
                    }
                }
                else if (nowTriggerTarget == null && this.PlayerGet)
                {
                    if ((int)Ch2GameManager.nowCH2State % 2 != 0)
                    {
                        // 如果在背面放開物件的話
                        if (isOnBar)
                        {
                            gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                        }
                        else
                        {
                            gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                            ch2VisualManager.ToolsTriggerBar(this.gameObject, false);
                            isOnBar = true;
                        }

                    }
                    else if ((int)Ch2GameManager.nowCH2State % 2 == 0)
                    {
                        gameObject.transform.DOLocalMoveY(-0.5f, 0.8f).SetId<Tween>(this.name);
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        nowTriggerTarget = other.gameObject;
        if (this.name != "Frame")
        {
            if (nowTriggerTarget.name == "Bar")
            {
                // 道具進入到bar的音效
                audioManager.PropsTriggerBar();
                // ===========================
                isOnBar = true;
                DOTween.Kill(this.name);
                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                ch2VisualManager.ToolsTriggerBar(this.gameObject, true);
            }
            else if (nowTriggerTarget.name == "BackBarTrigger")
            {
                isOnBar = true;
                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                ch2VisualManager.ToolsTriggerBar(this.gameObject, false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (this.name != "Frame")
        {
            if (other.gameObject.name == "Bar")
            {
                // 離開正面的Bar
                isOnBar = false;
                gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
                ch2VisualManager.ToolsExitBar(this.gameObject, tempScale, colliderSize);
            }
            else if (other.gameObject.name == "Back_Bar")
            {
                // 離開背面的Bar
                isOnBar = false;
                gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
            }
        }
        nowTriggerTarget = null;
    }

}
