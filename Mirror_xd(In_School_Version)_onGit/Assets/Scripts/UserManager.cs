using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    #region 主角移動
    [Header("移動速度")]
    public float speed = 3f;    // move speed
    public GameObject Character;
    public Vector2 charPos;
    public Vector2 mouseWorldPosition;
    private float step;

    [Header("主角動畫")]
    public Animator CharacterAni;
    public bool walk;
    public bool FirstToBack = true;
    public bool isZoomin;
    public bool isClickProps;
    public bool PayMoney;
    public bool GiveU;
    public bool PutFishing;
    public bool ClickGateKeeper;
    public bool ClickHunk;
    public bool ClickZoomin;
    public string Props;
    public string ZoominItem;
    #endregion
    public bool haveGetS = false;
    #region Managers
    VisualManager visualManager;
    GameManager gameManager;
    DialogueManager dialogueManager;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        visualManager = GameObject.Find("VisualManager").GetComponent<VisualManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        walk = true;
        isZoomin = false;
        PayMoney = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Determine whether to move character
        if ((int)GameManager.nowCH1State % 2 != 0)//(int)nowCH1State % 2 != 0 背後世界
        {
            if (FirstToBack && visualManager.ShowBag == false)
            {
                mouseWorldPosition.x = charPos.x;
                charPos.x = Character.transform.position.x;
            }
            else if (Mathf.Abs(charPos.x - mouseWorldPosition.x) > 0.01f && FirstToBack == false && visualManager.ShowBag == false && isZoomin == false)
            {
                switch (walk)
                {
                    case true:
                        Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                        WalkAni();
                        break;
                    case false:
                        Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                        Character.transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);
                        WalkAni();
                        break;
                }
                charPos = Character.transform.position;
                step = speed * Time.deltaTime;
                // move to mouse position
                Character.transform.position = Vector2.MoveTowards(charPos, new Vector2(mouseWorldPosition.x, charPos.y), step);
            }
            else if (visualManager.ShowBag == true || isZoomin == true || PayMoney || PutFishing || isClickProps || GiveU || ClickGateKeeper || ClickHunk || ClickZoomin)
            {
                mouseWorldPosition.x = charPos.x;
                if (PayMoney)
                {
                    PayMoneyAni();
                }
                else if (GiveU)
                {
                    GiveUAni();
                }
                else if (PutFishing)
                {
                    PutFishingAni();
                }
                else if (isClickProps)
                {
                    if (Props == "U")
                    {
                        TakeUAni();
                    }
                    else
                    {
                        PickupAni();
                        if (Props == "E")
                        {
                            // 改變點擊狗會說的話
                            dialogueManager.changeDogStage("get_e");
                            // =================================
                        }
                        else if (Props == "G")
                        {
                            // 改變點擊狗會說的話
                            dialogueManager.changeDogStage("get_g");
                            // =================================
                        }
                    }
                }
                else if (ClickGateKeeper)
                {
                    SayHelloAni();
                }
                else if (ClickHunk || ClickZoomin)
                {
                    TurnAroundAni();
                }
                else
                {
                    IdleAni();
                }
            }
            else
            {
                //Debug.Log(mouseWorldPosition.x);
                mouseWorldPosition.x = charPos.x;
                switch (walk)
                {
                    case true:
                        Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                        if (PayMoney)
                        {
                            PayMoneyAni();
                        }
                        else
                        {
                            IdleAni();
                        }
                        break;
                    case false:
                        Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                        Character.transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);
                        if (PayMoney)
                        {
                            PayMoneyAni();
                        }
                        else
                        {
                            IdleAni();
                        }
                        break;
                }
            }
        }
    }

    public void ChangeToFrontSetMousePos()
    {
        // 每次切到正面就讓記錄的點擊位置設到Portal前一步
        walk = true;
        mouseWorldPosition.x = -4f;
    }

    public void ChangeFirstToBackStatus()
    {
        CharacterAni.speed = 1;
    }

    public void ToMoveCharacter()
    {
        // Get mouse position and convert to World Space
        if (AniEvents.AniDone && !visualManager.ShowBag && CursorFX.PlayerMovable)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 tempWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (tempWorldPos.x < 7.8 && tempWorldPos.x > -8.1 && tempWorldPos.y < -2.7 && tempWorldPos.y > -5)
            {
                mouseWorldPosition = tempWorldPos;
                //CharacterAni.SetBool("Walk",true);
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
    }

    #region 控制主角動畫 

    public void PickupAni()
    {
        if (visualManager.Fishing_Back.activeSelf && GameManager.nowCH1State == GameManager.CH1State.BrowserBack)
        {
            CharacterAni.SetBool("Walk", false);
            CharacterAni.SetBool("idle", false);
            CharacterAni.SetBool("DownToidle", false);
            CharacterAni.SetBool("PickUp", false);
            CharacterAni.SetBool("TakeU", false);
            CharacterAni.SetBool("Water_idle", false);
            CharacterAni.SetBool("Water_walk", false);
            CharacterAni.SetBool("Water_pickup", true);
            CharacterAni.SetBool("PayMoney", false);
            CharacterAni.SetBool("PutFishing", false);
            CharacterAni.SetBool("GiveU", false);
            CharacterAni.SetBool("SayHello", false);
            CharacterAni.SetBool("TurnAround", false);
        }
        else
        {
            CharacterAni.SetBool("Walk", false);
            CharacterAni.SetBool("idle", false);
            CharacterAni.SetBool("DownToidle", false);
            CharacterAni.SetBool("PickUp", true);
            CharacterAni.SetBool("TakeU", false);
            CharacterAni.SetBool("Water_idle", false);
            CharacterAni.SetBool("Water_walk", false);
            CharacterAni.SetBool("Water_pickup", false);
            CharacterAni.SetBool("PayMoney", false);
            CharacterAni.SetBool("PutFishing", false);
            CharacterAni.SetBool("GiveU", false);
            CharacterAni.SetBool("SayHello", false);
            CharacterAni.SetBool("TurnAround", false);
        }
    }
    public void WalkAni()
    {
        if (visualManager.Fishing_Back.activeSelf && GameManager.nowCH1State == GameManager.CH1State.BrowserBack)
        {
            CharacterAni.SetBool("Walk", false);
            CharacterAni.SetBool("idle", false);
            CharacterAni.SetBool("DownToidle", false);
            CharacterAni.SetBool("PickUp", false);
            CharacterAni.SetBool("TakeU", false);
            CharacterAni.SetBool("Water_idle", false);
            CharacterAni.SetBool("Water_walk", true);
            CharacterAni.SetBool("Water_pickup", false);
            CharacterAni.SetBool("PayMoney", false);
            CharacterAni.SetBool("PutFishing", false);
            CharacterAni.SetBool("GiveU", false);
            CharacterAni.SetBool("SayHello", false);
            CharacterAni.SetBool("TurnAround", false);
        }
        else
        {
            CharacterAni.SetBool("Walk", true);
            CharacterAni.SetBool("idle", false);
            CharacterAni.SetBool("DownToidle", false);
            CharacterAni.SetBool("PickUp", false);
            CharacterAni.SetBool("TakeU", false);
            CharacterAni.SetBool("Water_idle", false);
            CharacterAni.SetBool("Water_walk", false);
            CharacterAni.SetBool("Water_pickup", false);
            CharacterAni.SetBool("PayMoney", false);
            CharacterAni.SetBool("PutFishing", false);
            CharacterAni.SetBool("GiveU", false);
            CharacterAni.SetBool("SayHello", false);
            CharacterAni.SetBool("TurnAround", false);
        }
    }
    public void DownAni()
    {
        CharacterAni.SetBool("DownToidle", true);
        CharacterAni.SetBool("Walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("PickUp", false);
        CharacterAni.SetBool("TakeU", false);
        CharacterAni.SetBool("Water_idle", false);
        CharacterAni.SetBool("Water_walk", false);
        CharacterAni.SetBool("Water_pickup", false);
        CharacterAni.SetBool("PayMoney", false);
        CharacterAni.SetBool("PutFishing", false);
        CharacterAni.SetBool("GiveU", false);
        CharacterAni.SetBool("SayHello", false);
        CharacterAni.SetBool("TurnAround", false);
    }
    public void IdleAni()
    {
        if (visualManager.Fishing_Back.activeSelf && GameManager.nowCH1State == GameManager.CH1State.BrowserBack)
        {
            CharacterAni.SetBool("Walk", false);
            CharacterAni.SetBool("idle", false);
            CharacterAni.SetBool("DownToidle", false);
            CharacterAni.SetBool("PickUp", false);
            CharacterAni.SetBool("TakeU", false);
            CharacterAni.SetBool("Water_idle", true);
            CharacterAni.SetBool("Water_walk", false);
            CharacterAni.SetBool("Water_pickup", false);
            CharacterAni.SetBool("PayMoney", false);
            CharacterAni.SetBool("PutFishing", false);
            CharacterAni.SetBool("GiveU", false);
            CharacterAni.SetBool("SayHello", false);
            CharacterAni.SetBool("TurnAround", false);
        }
        else
        {
            CharacterAni.SetBool("Walk", false);
            CharacterAni.SetBool("idle", true);
            CharacterAni.SetBool("DownToidle", false);
            CharacterAni.SetBool("PickUp", false);
            CharacterAni.SetBool("TakeU", false);
            CharacterAni.SetBool("Water_idle", false);
            CharacterAni.SetBool("Water_walk", false);
            CharacterAni.SetBool("Water_pickup", false);
            CharacterAni.SetBool("PayMoney", false);
            CharacterAni.SetBool("PutFishing", false);
            CharacterAni.SetBool("GiveU", false);
            CharacterAni.SetBool("SayHello", false);
            CharacterAni.SetBool("TurnAround", false);
        }
    }
    public void TakeUAni()
    {
        CharacterAni.SetBool("Walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("DownToidle", false);
        CharacterAni.SetBool("PickUp", false);
        CharacterAni.SetBool("TakeU", true);
        CharacterAni.SetBool("Water_idle", false);
        CharacterAni.SetBool("Water_walk", false);
        CharacterAni.SetBool("Water_pickup", false);
        CharacterAni.SetBool("PayMoney", false);
        CharacterAni.SetBool("PutFishing", false);
        CharacterAni.SetBool("GiveU", false);
        CharacterAni.SetBool("SayHello", false);
        CharacterAni.SetBool("TurnAround", false);
    }
    public void PayMoneyAni()
    {
        CharacterAni.SetBool("Walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("DownToidle", false);
        CharacterAni.SetBool("PickUp", false);
        CharacterAni.SetBool("TakeU", false);
        CharacterAni.SetBool("Water_idle", false);
        CharacterAni.SetBool("Water_walk", false);
        CharacterAni.SetBool("Water_pickup", false);
        CharacterAni.SetBool("PayMoney", true);
        CharacterAni.SetBool("PutFishing", false);
        CharacterAni.SetBool("GiveU", false);
        CharacterAni.SetBool("SayHello", false);
        CharacterAni.SetBool("TurnAround", false);
    }
    public void PutFishingAni()
    {
        CharacterAni.SetBool("Walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("DownToidle", false);
        CharacterAni.SetBool("PickUp", false);
        CharacterAni.SetBool("TakeU", false);
        CharacterAni.SetBool("Water_idle", false);
        CharacterAni.SetBool("Water_walk", false);
        CharacterAni.SetBool("Water_pickup", false);
        CharacterAni.SetBool("PayMoney", false);
        CharacterAni.SetBool("PutFishing", true);
        CharacterAni.SetBool("GiveU", false);
        CharacterAni.SetBool("SayHello", false);
        CharacterAni.SetBool("TurnAround", false);
    }
    public void GiveUAni()
    {
        CharacterAni.SetBool("Walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("DownToidle", false);
        CharacterAni.SetBool("PickUp", false);
        CharacterAni.SetBool("TakeU", false);
        CharacterAni.SetBool("Water_idle", false);
        CharacterAni.SetBool("Water_walk", false);
        CharacterAni.SetBool("Water_pickup", false);
        CharacterAni.SetBool("PayMoney", false);
        CharacterAni.SetBool("PutFishing", false);
        CharacterAni.SetBool("GiveU", true);
        CharacterAni.SetBool("SayHello", false);
        CharacterAni.SetBool("TurnAround", false);
    }
    public void SayHelloAni()
    {
        CharacterAni.SetBool("Walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("DownToidle", false);
        CharacterAni.SetBool("PickUp", false);
        CharacterAni.SetBool("TakeU", false);
        CharacterAni.SetBool("Water_idle", false);
        CharacterAni.SetBool("Water_walk", false);
        CharacterAni.SetBool("Water_pickup", false);
        CharacterAni.SetBool("PayMoney", false);
        CharacterAni.SetBool("PutFishing", false);
        CharacterAni.SetBool("GiveU", false);
        CharacterAni.SetBool("SayHello", true);
        CharacterAni.SetBool("TurnAround", false);
    }
    public void TurnAroundAni()
    {
        CharacterAni.SetBool("Walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("DownToidle", false);
        CharacterAni.SetBool("PickUp", false);
        CharacterAni.SetBool("TakeU", false);
        CharacterAni.SetBool("Water_idle", false);
        CharacterAni.SetBool("Water_walk", false);
        CharacterAni.SetBool("Water_pickup", false);
        CharacterAni.SetBool("PayMoney", false);
        CharacterAni.SetBool("PutFishing", false);
        CharacterAni.SetBool("GiveU", false);
        CharacterAni.SetBool("SayHello", false);
        CharacterAni.SetBool("TurnAround", true);
    }
    #endregion

    #region Myth
    public void PutOnPasswaed()
    {
        visualManager.Stole_Password();
    }
    #endregion
}
