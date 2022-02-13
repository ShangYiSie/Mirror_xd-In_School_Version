using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

// 道具本身要Rigidbody+Collider(posZ:-2) 要觸發的道具Collider(isTrigger打勾)
public class ItemEventManager : MonoBehaviour
{
    VisualManager visualManager;
    GameManager gameManager;
    UserManager userManager;
    DialogueManager dialogueManager;
    AudioManager audioManager;

    CursorSetting cursorSetting;

    Vector3 screenSpace;
    Vector3 offset;
    Vector2 colliderSize;

    [Header("紀錄猜拳圖原始位置")]
    public Vector3 moraSprite;
    public bool PlayerGet = false;

    [Header("紀錄正反面scale")]
    public Vector3 tempScale;

    // 是否在bar上
    public bool isOnBar;

    GameObject nowTriggerTarget;

    public GameObject turntableStart;
    public GameObject turntablePointer;

    Collider2D turntableStartCollider;

    Collider2D turntablePointerCollider;

    bool isGetGift = false;

    bool TriggeredPointer = false;

    string nowState = "";
    //bool FishingOther;

    // 設定第一次的bool
    public bool changeTimeFirst = true; // 第一次改變時間
    public bool UTriggerHourhand = true;
    public bool UTriggerPointer = true;

    public bool UTriggerMoraE_First = true;     // 第一次在猛男野球拳拿磁鐵吸猛男的王冠

    Collider2D MyCollider;

    // Start is called before the first frame update
    void Start()
    {
        /*if (this.name != "Meteorite" && this.tag != "mora")
        {
            this.tag = "Prop";
            if ((int)GameManager.nowCH1State % 2 != 0 && PlayerGet)
            {
                this.GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
            }
        }*/
        visualManager = GameObject.Find("VisualManager").GetComponent<VisualManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<CursorSetting>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        moraSprite = gameObject.transform.position;
        UTriggerPointer = true;
        UTriggerHourhand = true;

        // 先取得預先的collider size
        if (this.name != "Meteorite" && this.name != "stone" && this.name != "paper" && this.name != "scissors")
        {
            colliderSize = gameObject.GetComponent<BoxCollider2D>().size;
        }
        else if (this.name == "Meteorite")
        {
            colliderSize = gameObject.GetComponent<BoxCollider2D>().size;
        }

        // 設定預設scale大小
        if (this.name == "S" || this.name == "Bitcoin" || this.name == "Gift" || this.name == "Meteorite")//
        {
            tempScale = new Vector3(1f, 1f, 0);
        }
        else if (this.name == "Shovel")
        {
            tempScale = new Vector3(0.66f, 0.66f, 0);
        }
        else
        {
            tempScale = gameObject.transform.localScale;
        }

        //FishingOther = false;

        if (this.name == "ExitBTN")
        {
            PlayerGet = true;
        }
        // ================================================================================================================
        // 針對Bitcoin ( 先暫時這樣:) )
        if (this.name == "Bitcoin")
        {
            // this.tag = "Prop";
            visualManager.StartCoroutine(visualManager.GetDogeCoinAni());
            PlayerGet = true;
            this.transform.parent = GameObject.Find("Tools").transform;
            dialogueManager.ShowNextDialogue("get_bitcoin", true);
            Destroy(this.GetComponent<EventTrigger>());
            // this.transform.DOLocalMoveY(-4.56f, 2f).SetEase(Ease.OutBounce);
        }
        else if (this.name == "Shovel")
        {
            // this.tag = "Prop";
            PlayerGet = true;
            this.transform.parent = GameObject.Find("Tools").transform;
            Destroy(this.GetComponent<EventTrigger>());
            // this.transform.DOLocalMoveY(-4.23f, 2f).SetEase(Ease.OutBounce);
            // this.transform.DOScale(0.1f, 0.5f);
        }
        else if (this.name == "Gift")
        {
            this.tag = "Prop";
            PlayerGet = true;
            this.transform.parent = GameObject.Find("Tools").transform;
            this.transform.DOLocalMoveY(-4.23f, 2f).SetEase(Ease.OutBounce);
            visualManager.delGiftMesBox();
        }
        else if (this.name == "T")
        {
            // this.transform.DOLocalMoveY(-4.65f, 2f).SetEase(Ease.InBack).SetId<Tween>(this.name);

            Vector3 ogPos = this.gameObject.transform.position;
            Sequence T = DOTween.Sequence();
            T.Append(this.gameObject.transform.DOMoveX(ogPos.x + 3f, 1f).SetEase(Ease.Linear));//ExitBTN.transform.position.x + 
            T.Insert(0, this.gameObject.transform.DOMoveY(ogPos.y + 0.5f, 0.5f).SetEase(Ease.OutCirc));//ExitBTN.transform.position.y +
            T.Insert(0.5f, this.gameObject.transform.DOMoveY(-4.65f, 0.5f).SetEase(Ease.InCirc)).OnComplete(() =>//下落
            {
                T.Kill();
                PlayerGet = true;
            });
        }
        else if (this.name == "Bomb")
        {
            this.transform.DOLocalMoveY(-4.65f, 2f).SetEase(Ease.InBack).SetId<Tween>(this.name);
        }


        for (int i = 0; i < Landmineitems.Length; i++)
        {
            Landmineitems[i] = null;
        }

        MyCollider = this.GetComponent<Collider2D>();

        // ================================================================================================================
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerGet && GameManager.nowCH1State == GameManager.CH1State.MythBack)
        {
            if (this.name == "G" || this.name == "U" || this.name == "E" || this.name == "S" || this.name == "T")
            {
                if (VisualManager.GuestAllPutted)
                {
                    Destroy(this.gameObject);
                }
                else if (VisualManager.GuestReset)
                {
                    // this.gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("BackItem");
                    this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
                }
            }
        }
    }


    public void OnMouseDown()
    {
        if (AniEvents.AniDone)
        {
            if (this.tag == "Prop" && this.name != "Meteorite")
            {
                // 圖層上調一層
                this.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
            if (this.name == "Meteorite" && !PlayerGet)
            {
                // 第一次點擊隕石
                audioManager.ClickMeteorite();
            }
            else
            {
                // 播放點擊道具聲音
                audioManager.ClickProps();
            }
            // =========================================================================================================
            nowState = "MouseDown";

            DOTween.Kill(this.name);
            if (gameObject.name != "Meteorite" && this.tag != "mora")
            {
                // 點下物件，將element的ignoreLayout設為true
                gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
            }
            else if (this.name == "Meteorite")
            {
                gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
            }

            // =========================================================================================================
            if (!PlayerGet && gameObject.tag != "mora" && (int)GameManager.nowCH1State % 2 == 0)   // 代表在正面世界
            {
                PlayerGet = true;
                this.GetComponent<SpriteRenderer>().sortingOrder = 5;
                this.transform.DOLocalMoveY(-3.4f, 2f).SetEase(Ease.OutBounce).SetId<Tween>(this.name);
                if (this.name == "Meteorite")
                {
                    this.GetComponent<SpriteRenderer>().sortingOrder = 11;
                }
            }
            else if (!PlayerGet && gameObject.tag != "mora" && (int)GameManager.nowCH1State % 2 != 0 && !userManager.ClickZoomin)    // 代表在背後世界
            {
                //鼠標動畫
                AniEvents.AniDone = false;
                cursorSetting.CursorHourglassAni();

                //PlayerGet = true;
                //userManager.mouseWorldPosition.x = this.transform.position.x;
                switch (this.name)
                {
                    case "G":
                        if (this.transform.position.x > userManager.Character.transform.position.x)
                        {
                            userManager.walk = true;
                            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                            userManager.mouseWorldPosition.x = -2.277f;
                        }
                        else
                        {
                            userManager.walk = false;
                            userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                            userManager.mouseWorldPosition.x = 1.629f;
                        }
                        break;
                    case "U":
                        if (this.transform.position.x > userManager.Character.transform.position.x)
                        {
                            userManager.walk = true;
                            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                            userManager.mouseWorldPosition.x = 2.485f;
                        }
                        else
                        {
                            userManager.walk = false;
                            userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                            userManager.mouseWorldPosition.x = 6.65f;
                        }
                        break;
                    case "E":
                        if (this.transform.position.x > userManager.Character.transform.position.x)
                        {
                            userManager.walk = true;
                            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                            userManager.mouseWorldPosition.x = -2.462f;
                        }
                        else
                        {
                            userManager.walk = false;
                            userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                            userManager.mouseWorldPosition.x = 1.97f;
                        }
                        break;
                    case "S":
                        if (this.transform.position.x > userManager.Character.transform.position.x)
                        {
                            userManager.walk = true;
                            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                            userManager.mouseWorldPosition.x = -2.439f;
                        }
                        else
                        {
                            userManager.walk = false;
                            userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                            userManager.mouseWorldPosition.x = 1.21f;
                        }
                        break;
                    case "GUESTBtnProp":
                        if (this.transform.position.x > userManager.Character.transform.position.x)
                        {
                            userManager.walk = true;
                            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                            userManager.mouseWorldPosition.x = -1.676f;
                        }
                        else
                        {
                            userManager.walk = false;
                            userManager.Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                            userManager.mouseWorldPosition.x = 4.288f;
                        }
                        break;
                }
                userManager.isClickProps = true;
                userManager.Props = this.name;
            }
            //this.transform.parent = GameObject.Find("Tools").transform;
            screenSpace = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -3));

            //如果在正面釣魚網站
            if (this.name == "U" && GameManager.stage == 25 && GameManager.nowCH1State.ToString() == "BrowserFront")//stage要改
            {
                turntableStart = GameObject.Find("startTrigger");
                turntablePointer = GameObject.Find("pointer");

                turntableStartCollider = turntableStart.GetComponent<Collider2D>();
                turntablePointerCollider = turntablePointer.GetComponent<Collider2D>();
            }

            //如果在釣魚網站背面
            if ((this.name == "U" || this.name == "E" || this.name == "S" || this.name == "T" || this.name == "Gift") && GameManager.nowCH1State.ToString() == "BrowserBack")
            {
                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingU.gameObject.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingE.gameObject.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingS.gameObject.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingT.gameObject.GetComponent<Collider2D>(), true);
                if (visualManager.FishingGift != null)
                {
                    Physics2D.IgnoreCollision(MyCollider, visualManager.FishingGift.gameObject.GetComponent<Collider2D>(), true);
                }
            }
            else if ((this.name == "U" || this.name == "S" || this.name == "Shovel" || this.name == "T" || this.name == "Bomb") && GameManager.nowApps.ToString() == "Hunk_Mora")
            {
                GameObject[] Mora = GameObject.FindGameObjectsWithTag("mora");
                foreach (GameObject item in Mora)
                {
                    Physics2D.IgnoreCollision(MyCollider, item.GetComponent<Collider2D>(), true);
                }
            }

            if (this.name == "U" && PlayerGet)
            {
                UTriggerPointer = true;
                UTriggerHourhand = true;
            }


        }
    }
    Vector3 curPosition;
    public void OnMouseDrag()
    {
        if (AniEvents.AniDone)
        {
            nowState = "MouseDrag";
            if ((int)GameManager.nowCH1State % 2 == 0)
            {
                Physics2D.IgnoreCollision(MyCollider, visualManager.FrontBar, true);
            }
            else
            {
                Physics2D.IgnoreCollision(MyCollider, visualManager.BackBar, true);
            }

            if ((int)GameManager.nowCH1State % 2 != 0 && this.tag != "mora")
            {
                // 如果在背面拖移物件的話
                gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
                visualManager.ToolsExitBar(this.gameObject, tempScale, colliderSize);
            }

            if (AniEvents.AniDone && Input.mousePosition.y > -0.5f)
            {
                Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -3);
                curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
                transform.position = new Vector3(Mathf.Clamp(curPosition.x, -9f, 9f), Mathf.Clamp(curPosition.y, -5f, 5f));
            }
            // ================================================================================================================
            // 動態新增腳本
            if (this.name == "U" && GameManager.stage == 25 && GameManager.nowCH1State.ToString() == "BrowserFront")
            {
                if (!isGetGift)
                {
                    turntablePointerCollider.enabled = true;
                    turntableStartCollider.enabled = false;
                }
            }

            if (this.name == "Gift")
            {
                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingU.gameObject.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingE.gameObject.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingS.gameObject.GetComponent<Collider2D>(), true);
                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingT.gameObject.GetComponent<Collider2D>(), true);
            }

            if (nowTriggerTarget != null)
            {
                if (nowTriggerTarget.tag != "Prop")
                {
                    this.GetComponentInChildren<ButtonFX>().TriggerotherFX();
                }

                switch (this.name)
                {
                    case "U":
                        if (nowTriggerTarget.name == "Bitcoin" && nowTriggerTarget.GetComponent<ItemEventManager>() == null)
                        {
                            if (DOTween.IsTweening("BitCoinAni"))
                            {
                                DOTween.Kill("BitCoinAni");
                            }
                            // ==========================
                            // U 碰到 比特幣
                            audioManager.UTriggerBitcoin();
                            // ==========================
                            nowTriggerTarget.AddComponent<ItemEventManager>();
                            nowTriggerTarget.tag = "Prop";
                            nowTriggerTarget.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
                            this.GetComponentInChildren<ButtonFX>().TriggerotherFX();
                        }
                        else if (nowTriggerTarget.name == "Bitcoin" && GameManager.stage == 12)
                        {
                            this.GetComponentInChildren<ButtonFX>().TriggerotherFX();
                            nowTriggerTarget.transform.DOMove(this.transform.position, 0.1f);
                        }
                        else if (nowTriggerTarget.tag == "Landmineitems" && Landmineitems[0] != null)
                        {
                            this.GetComponentInChildren<ButtonFX>().TriggerotherFX();
                            if (visualManager.SpriteLandmineEmoji.sprite.name == "emoji_1")
                            {
                                visualManager.SpriteLandmineEmoji.sprite = Resources.Load<Sprite>("Textures/emoji_2");
                            }
                            for (int i = 0; i < Landmineitems.Length; i++)
                            {
                                float random = Random.Range(0.5f, 0.8f);
                                if (Landmineitems[i] != null)
                                {
                                    Landmineitems[i].transform.DOMove(this.transform.position, random);
                                }
                            }
                        }
                        else if (nowTriggerTarget.name == "Shovel" && nowTriggerTarget.GetComponent<ItemEventManager>() == null)
                        {
                            if (DOTween.IsTweening("Shovel"))
                            {
                                DOTween.Kill("Shovel");
                            }
                            // ===========================
                            // 點擊鏟子的音效
                            audioManager.UTriggerShovel();
                            // ===========================
                            // 取得鏟子，觸發文本
                            dialogueManager.ShowNextDialogue("get_shovel", true);

                            this.GetComponentInChildren<ButtonFX>().TriggerotherFX();

                            nowTriggerTarget.AddComponent<ItemEventManager>();
                            nowTriggerTarget.GetComponent<ItemEventManager>().PlayerGet = true;
                            nowTriggerTarget.tag = "Prop";
                        }
                        else if (nowTriggerTarget.name == "Shovel" && GameManager.stage == 14)
                        {
                            // nowTriggerTarget.transform.position = curPosition;
                            this.GetComponentInChildren<ButtonFX>().TriggerotherFX();
                            nowTriggerTarget.transform.DOMove(this.transform.position, 0.1f);
                        }
                        else if (nowTriggerTarget.name == "Hour_hand")
                        {
                            // ===================================
                            // 觸發音效
                            if (UTriggerHourhand)
                            {
                                audioManager.UTriggerPointer();
                                UTriggerHourhand = false;
                            }
                            // ===================================
                            if (changeTimeFirst)
                            {
                                // 第一次改變時間，觸發文本
                                dialogueManager.ShowNextDialogue("change_time_first", false);
                                changeTimeFirst = false;
                            }
                            //nowTriggerTarget.transform.position = this.transform.position;
                            Vector3 UPos = this.transform.position;
                            Vector3 direction = UPos - nowTriggerTarget.transform.position;
                            /*direction.z = 0f;
                            direction.Normalize();*/
                            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            nowTriggerTarget.transform.rotation = Quaternion.Slerp(nowTriggerTarget.transform.rotation, Quaternion.Euler(0, 0, targetAngle), 3f * Time.deltaTime);
                        }
                        else if (nowTriggerTarget.name == "pointer")
                        {
                            // ===================================
                            // 觸發音效
                            if (UTriggerPointer)
                            {
                                audioManager.FishingPointer();
                                UTriggerPointer = false;
                            }
                            // ===================================
                            TriggeredPointer = true;
                            Vector3 relative = transform.InverseTransformPoint(0, -1.29f, 0);
                            float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
                            angle += 90;
                            nowTriggerTarget.transform.rotation = Quaternion.Slerp(nowTriggerTarget.transform.rotation, Quaternion.Euler(0, 0, angle), 3f * Time.deltaTime);
                        }
                        else
                        {
                            // this.transform.DOLocalMoveY(-4.65f, 2f).SetEase(Ease.OutBounce).SetId<Tween>(this.name);

                        }
                        break;

                    default:
                        break;
                }
            }

            else
            {
                this.GetComponentInChildren<ButtonFX>().ExitotherFX();
            }
        }
        // ================================================================================================================
    }

    public void OnMouseUp()
    {
        if (AniEvents.AniDone)
        {
            if (this.tag == "Prop" && this.name != "Meteorite")
            {
                // 圖層下調一層
                this.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }

            this.GetComponentInChildren<ButtonFX>().ExitotherFX();

            nowState = "MouseUp";

            if ((int)GameManager.nowCH1State % 2 == 0)
            {
                Physics2D.IgnoreCollision(MyCollider, visualManager.FrontBar, false);
            }
            else
            {
                Physics2D.IgnoreCollision(MyCollider, visualManager.BackBar, false);
            }

            if (this.name == "U" && GameManager.stage == 25 && GameManager.nowCH1State.ToString() == "BrowserFront")
            {
                if (!isGetGift)
                {
                    turntablePointerCollider.enabled = false;
                    turntableStartCollider.enabled = true;
                    if (TriggeredPointer)
                    {
                        int pointNum = visualManager.turntableAfterU();
                        if (pointNum == 0)
                        {
                            isGetGift = true;
                            if (GameManager.stage == 25)
                            {
                                GameManager.stage++;
                            }
                        }
                    }
                }
            }

            if (this.name == "U" && (GameManager.nowCH1State.ToString() == "DeskTopBack" || GameManager.nowCH1State.ToString() == "NoteBack" || GameManager.nowCH1State.ToString() == "AntivirusBack"))//吸完時針後放開
            {
                visualManager.CheckClockTime();
            }

            if (nowTriggerTarget != null)
            {
                // Debug.Log(this.name + " trigger " + nowTriggerTarget.name);
                switch (this.name)
                {
                    case "Meteorite":
                        if (nowTriggerTarget.name == "yee")
                        {
                            // 小恐龍叫起來
                            audioManager.MetoriteHitDinasor();
                            // ==================
                            this.gameObject.transform.parent = GameObject.Find("Dinosaur").transform;
                            AniEvents.AniDone = false;
                            cursorSetting.CursorHourglassAni();
                            visualManager.AniMeteorite(this.gameObject);
                            PlayerGet = false;
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-3.4f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    case "Bitcoin":
                        if (nowTriggerTarget.name == "Gatekeeper")
                        {
                            if (GameManager.stage == 13)
                            {
                                GameManager.stage++;
                            }
                            //dialogueManager.ShowNextDialogue("give_bitcoin", false);
                            this.GetComponent<LayoutElement>().ignoreLayout = false;
                            userManager.PayMoney = true;
                            //滑鼠鼠標
                            AniEvents.AniDone = false;
                            cursorSetting.CursorHourglassAni();

                            userManager.walk = true;
                            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                            userManager.mouseWorldPosition.x = 0.73f;
                            //userManager.GiveBitCoin();
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_dogecoin", false);
                            }
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-3.4f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;

                    #region 猛男野球拳
                    case "stone":
                        if (nowTriggerTarget.name == "target")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            transform.position = nowTriggerTarget.transform.position;
                            MyCollider.enabled = false;
                            visualManager.Mora(2);
                        }
                        else
                        {
                            this.transform.position = moraSprite;
                        }
                        /*else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-3.4f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }*/
                        break;
                    case "paper":
                        if (nowTriggerTarget.name == "target")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            transform.position = nowTriggerTarget.transform.position;
                            MyCollider.enabled = false;
                            visualManager.Mora(3);
                        }
                        else
                        {
                            this.transform.position = moraSprite;
                        }
                        /*else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-3.4f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }*/
                        break;
                    case "scissors":
                        if (nowTriggerTarget.name == "target")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            transform.position = nowTriggerTarget.transform.position;
                            MyCollider.enabled = false;
                            visualManager.Mora(1);
                        }
                        else
                        {
                            this.transform.position = moraSprite;
                        }
                        /*else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-3.4f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }*/
                        break;
                    case "Bomb":
                        if (nowTriggerTarget.name == "target")
                        {
                            if (GameManager.stage == 17)
                            {
                                GameManager.stage++;
                            }
                            transform.position = nowTriggerTarget.transform.position;
                            this.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                            visualManager.Mora(4);

                            if (VisualManager.PigeonAniPlayed && !VisualManager.GetS)
                            {
                                GameManager.stage = 21;
                            }
                            // 把背後那個男人提示的框disable
                            // visualManager.CloseETips();
                            //把炸彈的ItemEvent和cursor拿掉
                            Destroy(GameObject.Find("target").gameObject);
                            Destroy(this.gameObject.GetComponent<ItemEventManager>());
                            Destroy(this.gameObject.GetComponent<CursorFX>());
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_bomb", false);
                            }
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    #endregion

                    case "note_account":
                        if (nowTriggerTarget.name == "Acount")
                        {
                            // =========================
                            // 播放帳號密碼放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            //this.transform.position = nowTriggerTarget.transform.position;
                            nowTriggerTarget.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                            nowTriggerTarget.GetComponent<Collider2D>().enabled = false;
                            Destroy(this.gameObject);
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    case "note_password":
                        if (nowTriggerTarget.name == "Password")
                        {
                            // =========================
                            // 播放帳號密碼放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            //this.transform.position = nowTriggerTarget.transform.position;                       
                            nowTriggerTarget.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                            nowTriggerTarget.GetComponent<Collider2D>().enabled = false;
                            Destroy(this.gameObject);
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    // ================================================================================================================
                    case "Shovel":
                        if (nowTriggerTarget.name == "Flower")
                        {
                            visualManager.SpriteLandmineEmoji.sprite = Resources.Load<Sprite>("Textures/emoji_3");
                            visualManager.Flagsum--;
                            visualManager.TextFlagSum.text = visualManager.Flagsum.ToString();
                            //Destroy(nowTriggerTarget);
                            if (GameManager.stage == 15)
                            {
                                GameManager.stage++;
                            }
                            // ===========================
                            // 點擊鏟子的音效
                            audioManager.ShovelDigBomb();
                            // ===========================
                            GameObject ShovelBroken = this.gameObject.transform.GetChild(1).gameObject;
                            ShovelBroken.SetActive(true);
                            ShovelBroken.transform.parent = GameObject.Find("踩地雷").transform;
                            Sequence s = DOTween.Sequence();
                            Vector3 ogPos = ShovelBroken.transform.position;
                            s.Append(ShovelBroken.transform.GetChild(0).transform.DOMoveX(ogPos.x - 4f, 1f).SetEase(Ease.Linear));//ExitBTN.transform.position.x + 
                            s.Insert(0, ShovelBroken.transform.GetChild(0).transform.DOMoveY(ogPos.y + 1f, 0.5f).SetEase(Ease.OutCirc));//ExitBTN.transform.position.y +
                            s.Insert(0.5f, ShovelBroken.transform.GetChild(0).transform.DOMoveY(ogPos.y - 6.19f, 0.5f).SetEase(Ease.InCirc)).OnComplete(() =>//下落
                            {
                                s.Kill();
                                Destroy(ShovelBroken);
                            });
                            Destroy(this.gameObject);
                            visualManager.BombAni.SetActive(true);

                            // 取得炸彈和T，觸發文本
                            dialogueManager.ShowNextDialogue("pickup_bomb_and_T", true);

                            if (VisualManager.PigeonAniPlayed)
                            {
                                GameManager.stage = 21;
                            }
                        }
                        else if (nowTriggerTarget.name == "target")//猛男野球拳部分
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            transform.position = nowTriggerTarget.transform.position;
                            this.GetComponent<SpriteRenderer>().sprite = visualManager.Hunkmora[3];

                            Vector2 v = new Vector2((float)visualManager.Hunkmora[3].texture.width / 100, (float)visualManager.Hunkmora[3].texture.height / 100);
                            // 根據要轉換的圖片大小更改gameobject的width & height & scale
                            this.GetComponent<RectTransform>().sizeDelta = v;
                            this.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0);

                            MyCollider.enabled = false;
                            visualManager.Mora(8);
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_shovel", false);
                            }
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    // ================================================================================================================
                    case "G":
                        if (nowTriggerTarget.name == "big g")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            visualManager.PutGUEST(nowTriggerTarget, 0);
                            this.gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Zoomin");
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_g", false);
                            }
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    case "U":
                        if (nowTriggerTarget.name == "big u")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            visualManager.PutGUEST(nowTriggerTarget, 1);
                            this.gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Zoomin");

                        }
                        //釣魚other物件
                        else if (nowTriggerTarget.name == "Obj")
                        {
                            if (userManager.Character.transform.position.x <= -4.6f)
                            {
                                visualManager.PutFishingObj(1);
                            }
                            else if (userManager.Character.transform.position.x >= 0.13)
                            {
                                visualManager.PutFishingObj(2);
                            }
                            else
                            {
                                visualManager.PutFishingObj(3);
                            }
                            visualManager.GiftorOther = false;
                            nowTriggerTarget.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                            userManager.Character.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                        }
                        else if (nowTriggerTarget.name == "Bitcoin" && GameManager.stage == 12)
                        {
                            nowTriggerTarget.transform.DOLocalMoveY(-4.56f, 2f);
                            this.transform.DOLocalMoveY(-4.65f, 2f).SetId<Tween>(this.name);
                            if (GameManager.stage == 12)
                            {
                                GameManager.stage++;
                            }
                        }
                        else if (nowTriggerTarget.name == "Gatekeeper")
                        {
                            this.GetComponent<LayoutElement>().ignoreLayout = false;
                            userManager.GiveU = true;
                            //滑鼠鼠標
                            AniEvents.AniDone = false;
                            cursorSetting.CursorHourglassAni();

                            userManager.walk = true;
                            userManager.Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                            userManager.mouseWorldPosition.x = 0.73f;
                        }
                        else if (nowTriggerTarget.name == "Shovel" && GameManager.stage == 14)
                        {
                            nowTriggerTarget.transform.DOLocalMoveY(-4.56f, 2f);
                            this.transform.DOLocalMoveY(-4.65f, 2f).SetId<Tween>(this.name);
                            if (GameManager.stage == 14)
                            {
                                GameManager.stage++;
                            }
                            if (VisualManager.PigeonAniPlayed && !VisualManager.GetS)
                            {
                                GameManager.stage = 21;
                            }
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_u", false);
                            }
                        }
                        else if (GameManager.nowCH1State.ToString() == "BrowserBack")
                        {
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingE.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingS.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingT.gameObject.GetComponent<Collider2D>(), false);
                            if (visualManager.FishingGift != null)
                            {
                                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingGift.gameObject.GetComponent<Collider2D>(), false);
                            }
                        }
                        else if (nowTriggerTarget.name == "target")//猛男野球拳部分
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            transform.position = nowTriggerTarget.transform.position;
                            this.GetComponent<SpriteRenderer>().sprite = visualManager.Hunkmora[0];

                            Vector2 v = new Vector2((float)visualManager.Hunkmora[0].texture.width / 100, (float)visualManager.Hunkmora[0].texture.height / 100);
                            // 根據要轉換的圖片大小更改gameobject的width & height & scale
                            this.GetComponent<RectTransform>().sizeDelta = v;
                            this.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0);

                            MyCollider.enabled = false;
                            visualManager.Mora(5);
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    case "E":
                        if (nowTriggerTarget.name == "big e")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            visualManager.PutGUEST(nowTriggerTarget, 2);
                            this.gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Zoomin");
                        }
                        //釣魚other物件
                        else if (nowTriggerTarget.name == "Obj")
                        {
                            if (userManager.Character.transform.position.x <= -4.6f)
                            {
                                visualManager.PutFishingObj(1);
                            }
                            else if (userManager.Character.transform.position.x >= 0.13)
                            {
                                visualManager.PutFishingObj(2);
                            }
                            else
                            {
                                visualManager.PutFishingObj(3);
                            }
                            visualManager.GiftorOther = false;
                            //visualManager.Fishing_Eat_Ani.SetBool("Fishing_Other", true);
                            nowTriggerTarget.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                            userManager.Character.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                            //this.transform.parent = nowTriggerTarget.transform;
                            //FishingOther = true;
                            //this.transform.position = nowTriggerTarget.transform.position;

                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_e", false);
                            }
                        }
                        else if (GameManager.nowCH1State.ToString() == "BrowserBack")
                        {
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingU.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingS.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingT.gameObject.GetComponent<Collider2D>(), false);
                            if (visualManager.FishingGift != null)
                            {
                                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingGift.gameObject.GetComponent<Collider2D>(), false);
                            }
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    case "S":
                        if (nowTriggerTarget.name == "big s")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            visualManager.PutGUEST(nowTriggerTarget, 3);
                            this.gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Zoomin");
                        }
                        //釣魚other物件
                        else if (nowTriggerTarget.name == "Obj")
                        {
                            if (userManager.Character.transform.position.x <= -4.6f)
                            {
                                visualManager.PutFishingObj(1);
                            }
                            else if (userManager.Character.transform.position.x >= 0.13)
                            {
                                visualManager.PutFishingObj(2);
                            }
                            else
                            {
                                visualManager.PutFishingObj(3);
                            }
                            visualManager.GiftorOther = false;
                            //visualManager.Fishing_Eat_Ani.SetBool("Fishing_Other", true);
                            nowTriggerTarget.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                            userManager.Character.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                            //this.transform.parent = nowTriggerTarget.transform;
                            //FishingOther = true;
                            //this.transform.position = nowTriggerTarget.transform.position;
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_s", false);
                            }
                        }
                        else if (GameManager.nowCH1State.ToString() == "BrowserBack")
                        {
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingU.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingE.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingT.gameObject.GetComponent<Collider2D>(), false);
                            if (visualManager.FishingGift != null)
                            {
                                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingGift.gameObject.GetComponent<Collider2D>(), false);
                            }
                        }
                        else if (nowTriggerTarget.name == "target")//猛男野球拳部分
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            transform.position = nowTriggerTarget.transform.position;
                            this.GetComponent<SpriteRenderer>().sprite = visualManager.Hunkmora[1];

                            Vector2 v = new Vector2((float)visualManager.Hunkmora[1].texture.width / 100, (float)visualManager.Hunkmora[1].texture.height / 100);
                            // 根據要轉換的圖片大小更改gameobject的width & height & scale
                            this.GetComponent<RectTransform>().sizeDelta = v;
                            this.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0);

                            MyCollider.enabled = false;
                            visualManager.Mora(6);
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    case "T":
                        if (nowTriggerTarget.name == "big t")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            visualManager.PutGUEST(nowTriggerTarget, 4);
                            // this.gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Zoomin");
                            this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Zoomin";
                        }
                        //釣魚other物件
                        else if (nowTriggerTarget.name == "Obj")
                        {
                            if (userManager.Character.transform.position.x <= -4.6f)
                            {
                                visualManager.PutFishingObj(1);
                            }
                            else if (userManager.Character.transform.position.x >= 0.13)
                            {
                                visualManager.PutFishingObj(2);
                            }
                            else
                            {
                                visualManager.PutFishingObj(3);
                            }
                            visualManager.GiftorOther = false;
                            //visualManager.Fishing_Eat_Ani.SetBool("Fishing_Other", true);
                            nowTriggerTarget.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                            userManager.Character.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_t", false);
                            }
                        }
                        else if (GameManager.nowCH1State.ToString() == "BrowserBack")
                        {
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingU.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingE.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingS.gameObject.GetComponent<Collider2D>(), false);
                            if (visualManager.FishingGift != null)
                            {
                                Physics2D.IgnoreCollision(MyCollider, visualManager.FishingGift.gameObject.GetComponent<Collider2D>(), false);
                            }
                        }
                        else if (nowTriggerTarget.name == "target")//猛男野球拳部分
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            transform.position = nowTriggerTarget.transform.position;
                            this.GetComponent<SpriteRenderer>().sprite = visualManager.Hunkmora[2];

                            Vector2 v = new Vector2((float)visualManager.Hunkmora[2].texture.width / 100, (float)visualManager.Hunkmora[2].texture.height / 100);
                            // 根據要轉換的圖片大小更改gameobject的width & height & scale
                            this.GetComponent<RectTransform>().sizeDelta = v;
                            this.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0);

                            MyCollider.enabled = false;
                            visualManager.Mora(7);
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    // ================================================================================================================
                    case "ExitBTN":
                        if (nowTriggerTarget.name == "BtnBlack")
                        {
                            // ===============================
                            // 把x放到空格去 音效
                            audioManager.MoveXToSpaceInBrowser();
                            // ===============================
                            this.gameObject.transform.position = nowTriggerTarget.transform.position;
                            nowTriggerTarget.GetComponent<Collider2D>().enabled = false;
                            visualManager.CloseBrowserBtn.SetActive(true);
                            Destroy(this.gameObject);
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    case "Gift":
                        if (nowTriggerTarget.name == "Obj")
                        {
                            if (userManager.Character.transform.position.x <= -4.6f)
                            {
                                visualManager.PutFishingObj(1);
                            }
                            else if (userManager.Character.transform.position.x >= 0.13)
                            {
                                visualManager.PutFishingObj(2);
                            }
                            else
                            {
                                visualManager.PutFishingObj(3);
                            }
                            userManager.Character.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                            visualManager.GiftorOther = true;
                            Destroy(this.gameObject);
                            //visualManager.Fishing_Eat_Ani.SetBool("Fishing_Gift", true);
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_gift", false);
                            }
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        if (GameManager.nowCH1State.ToString() == "BrowserBack")
                        {
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingU.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(MyCollider, visualManager.FishingE.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), visualManager.FishingS.gameObject.GetComponent<Collider2D>(), false);
                            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), visualManager.FishingT.gameObject.GetComponent<Collider2D>(), false);
                        }
                        break;
                    case "GUESTBtnProp":    //放上GUEST按鈕
                        if (nowTriggerTarget.name == "GUESTBTNF")
                        {
                            // =========================
                            // 播放道具放到凹槽的音效
                            audioManager.PropsTriggerSpace();
                            // =========================
                            if (GameManager.stage == 30)
                            {
                                GameManager.stage++;
                            }
                            nowTriggerTarget.GetComponent<Collider2D>().enabled = false;
                            GameObject.Find("BtnGuest").GetComponent<Button>().interactable = true;
                            GameObject.Find("BtnGuest").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                            Destroy(this.gameObject);
                        }
                        else if (nowTriggerTarget.name == "Dog")
                        {
                            if (GameManager.nowCH1State == GameManager.CH1State.BrowserBack && GameManager.stage >= 25)
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_anything_in_water", false);
                            }
                            else
                            {
                                visualManager.Bag_Appear();
                                dialogueManager.ShowNextDialogue("give_dog_guest", false);
                            }
                        }
                        else if (!isOnBar)
                        {
                            this.transform.DOLocalMoveY(-4.65f, 0.8f).SetEase(Ease.InBack).SetId<Tween>(this.name);
                        }
                        break;
                    default:
                        break;

                }
                if (isOnBar && this.tag != "mora")
                {
                    gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                }
            }
            else if (nowTriggerTarget == null && this.tag == "mora")
            {
                this.transform.position = moraSprite;
            }
            else
            {
                // 玩家已取得該道具，並且道具不在道具欄上
                if (PlayerGet && !isOnBar && (int)GameManager.nowCH1State % 2 == 0)//正面
                {
                    if (this.name != "Meteorite") { this.transform.parent = GameObject.Find("Tools").transform; }
                    switch (this.name)
                    {
                        case "note_account":
                            this.transform.DOLocalMoveY(-4.65f, 2f).SetId<Tween>(this.name);
                            break;
                        case "note_password":
                            this.transform.DOLocalMoveY(-4.65f, 2f).SetId<Tween>(this.name);
                            break;
                        case "U":
                            this.transform.DOLocalMoveY(-4.65f, 2f).SetId<Tween>(this.name);
                            break;
                        case "Bitcoin":
                            this.transform.DOLocalMoveY(-4.65f, 2f).SetId<Tween>(this.name);
                            break;
                        case "T":
                            this.transform.DOLocalMoveY(-4.65f, 2f).SetId<Tween>(this.name);
                            break;
                        case "Bomb":
                            this.transform.DOLocalMoveY(-4.65f, 2f).SetId<Tween>(this.name);
                            break;
                        case "ExitBTN":
                            this.transform.DOLocalMoveY(-4.68f, 2f).SetEase(Ease.OutBounce).SetId<Tween>(this.name);
                            break;
                        case "Meteorite":
                            this.tag = "Prop";
                            this.transform.DOLocalMoveY(-3.48f, 2f).SetEase(Ease.OutBounce).SetId<Tween>(this.name);
                            break;
                        default:
                            this.transform.DOLocalMoveY(-5.67f, 2f).SetId<Tween>(this.name);
                            // this.transform.DOLocalMoveY(-5.67f, 2f).SetEase(Ease.OutBounce).SetId<Tween>(this.name);
                            break;
                    }

                }
                else if (isOnBar && this.tag != "mora")
                {
                    // 如果道具從底下bar點擊拖移但沒有離開bar的話
                    gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                }
                else
                {
                    if (this.tag != "mora")
                    {
                        gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                    }
                }
            }
            if ((int)GameManager.nowCH1State % 2 != 0 && this.tag != "mora" && PlayerGet)
            {
                // 如果在背面放開物件的話
                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
                visualManager.ToolsTriggerBar(this.gameObject, false);
            }
            if (this.PlayerGet)
            {
                userManager.isClickProps = false;
            }
        }


    }

    int counter = 0;

    GameObject[] Landmineitems = new GameObject[38];

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(this.name + " OnTrigger :" + other.gameObject.name);
        if (other.gameObject.name == "Bar")//&& this.name != "Meteorite")
        {
            // 碰觸到正面的Bar
            DOTween.Pause(this.transform);
            // ===========================
            // 道具進入到bar的音效
            audioManager.PropsTriggerBar();
            // ===========================
            if (this.transform.parent.name != "Tools")
            {
                // 道具碰觸到底下的Bar
                this.transform.SetParent(GameObject.Find("Tools").transform);
            }
            isOnBar = true;
            if (nowState != "MouseDrag" && this.tag != "mora")
            {
                gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
            }
            visualManager.ToolsTriggerBar(this.gameObject, true);
        }
        else if (other.gameObject.name == "BackBarTrigger" && this.tag != "mora")
        {
            // 碰觸到背面的Bar
            isOnBar = true;
            gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
            visualManager.ToolsTriggerBar(this.gameObject, false);
        }
        else if (this.name == "U" && other.gameObject.tag == "Landmineitems")
        {
            // u 碰到 數字發出聲音
            audioManager.UTriggerNumber();
            // Debug.Log(other.gameObject.name);
            Landmineitems[counter] = other.gameObject;
            nowTriggerTarget = other.gameObject;
            // Debug.Log(counter);
            counter++;
            if (other.name == "flag")
            {
                other.gameObject.name = "flagtriggered";
                visualManager.Flagsum--;
                visualManager.TextFlagSum.text = visualManager.Flagsum.ToString();
            }
        }
        else if (this.name == "Shovel" && other.tag == "Landmineitems")
        {

        }
        else
        {
            // 道具碰到道具
            nowTriggerTarget = other.gameObject;
        }

    }

    //專門給磁鐵吸猛男皇冠用的
    void OnTriggerStay2D(Collider2D other)
    {
        // nowTriggerTarget = other.gameObject;
        if (nowTriggerTarget != null)
        {
            if (this.name == "U" && nowTriggerTarget.name == "Crown")
            {
                if (UTriggerMoraE_First)
                {
                    dialogueManager.ShowNextDialogue("u_trigger_e", true);
                    UTriggerMoraE_First = false;
                }
                visualManager.HunkAni.SetBool("Hunk_no", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (this.name == "Shovel" && other.tag == "Landmineitems")
        {
            return;
        }
        // Debug.Log(this.name + " ExitTrigger :" + other.gameObject.name);
        if (other.gameObject.name == "Bar" && this.tag != "mora")// && this.name != "Meteorite")
        {
            // 離開正面的Bar
            isOnBar = false;
            gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
            visualManager.ToolsExitBar(this.gameObject, tempScale, colliderSize);
        }
        else if (other.gameObject.name == "Back_Bar")
        {
            // 離開背面的Bar
            isOnBar = false;
            gameObject.GetComponent<LayoutElement>().ignoreLayout = true;
        }
        else
        {
            // 道具離開道具

        }

        if (this.gameObject.name == "U" && other.gameObject.name == "Bitcoin" && GameManager.stage == 12)
        {
            other.transform.DOLocalMoveY(-4.56f, 2f);
            if (GameManager.stage == 12)
            {
                GameManager.stage++;
            }
        }
        else if (this.gameObject.name == "U" && other.gameObject.name == "Shovel" && GameManager.stage == 14)
        {
            other.transform.DOLocalMoveY(-4.56f, 2f);
            if (GameManager.stage == 14)
            {
                GameManager.stage++;
            }
            if (VisualManager.PigeonAniPlayed)
            {
                GameManager.stage = 21;
            }
        }
        if (this.name == "U" && other.name == "Crown")//nowTriggerTarget.name == "Crown"
        {
            visualManager.HunkAni.SetBool("Hunk_no", false);
        }



        if (Landmineitems[0] != null)
        {
            for (int i = 0; i < Landmineitems.Length - 1; i++)
            {
                Landmineitems[i] = null;
            }
            counter = 0;
        }


        nowTriggerTarget = null;
    }
}


