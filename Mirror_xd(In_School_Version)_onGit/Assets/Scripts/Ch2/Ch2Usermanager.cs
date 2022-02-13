using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch2Usermanager : MonoBehaviour
{
    Ch2VisualManager ch2VisualManager;

    #region 主角

    [Header("移動速度")]
    public float speed = 3f;    // move speed

    [Header("主角的GameObject&動畫")]
    public GameObject Character;
    public Animator CharacterAni;

    public Vector2 charPos;
    public Vector2 mouseWorldPosition;
    private float step;
    public bool walk;
    public bool isClickXiaoMing;
    public bool isClickBricksKiller;
    public bool isClickPlumber;
    public bool isPutOnXiaoMing;
    public bool isPutOnBricksKiller;
    public bool isPutOnPlumber;
    public bool isUnLockKey;
    public bool isUnLockCoin;
    public string Brand;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ch2VisualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();
        //charPos = Character.transform.localPosition;

        walk = true;
        isClickXiaoMing = false;
        isClickBricksKiller = false;
        isClickPlumber = false;

        //CharacterAni = Character.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Ch2GameManager.nowCH2State % 2 != 0)
        {
            if (ch2VisualManager.ShowBag == true)
            {
                mouseWorldPosition.x = charPos.x;
                IdleAni();
            }
            if (charPos.x != mouseWorldPosition.x && !ch2VisualManager.ShowBag)
            {
                switch (walk)
                {
                    case true:
                        Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                        WalkAni();
                        break;
                    case false:
                        Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                        WalkAni();
                        break;
                }
                charPos = Character.transform.position;
                step = speed * Time.deltaTime;
                // move to mouse position
                Character.transform.position = Vector2.MoveTowards(charPos, new Vector2(mouseWorldPosition.x, charPos.y), step);
            }
            else
            {
                if (isClickXiaoMing || isClickBricksKiller || isClickPlumber)
                {
                    if (isClickXiaoMing)
                    {
                        PullBramdAni();
                        PlayerGetBrand();
                    }
                    else if (isClickBricksKiller)
                    {
                        PullBramdAni();
                        PlayerGetBrand();
                    }
                    else if (isClickPlumber)
                    {
                        PullBramdAni();
                        PlayerGetBrand();
                    }
                }
                else if (isPutOnXiaoMing || isPutOnBricksKiller || isPutOnPlumber)
                {
                    if (isPutOnXiaoMing)
                    {
                        PutBramdAni();
                        PlayerPutBrand();
                    }
                    else if (isPutOnBricksKiller)
                    {
                        PutBramdAni();
                        PlayerPutBrand();
                    }
                    else if (isPutOnPlumber)
                    {
                        PutBramdAni();
                        PlayerPutBrand();
                    }
                }
                else if (isUnLockKey || isUnLockCoin)
                {
                    UnLockedAni();
                    PlayerUnLock();
                }
                else
                {
                    //Debug.Log(Character.transform.position.x);
                    mouseWorldPosition.x = charPos.x;
                    Brand = null;
                    switch (walk)
                    {
                        case true:
                            Character.transform.rotation = new Quaternion(0, 0, 0, 0);
                            IdleAni();
                            break;
                        case false:
                            Character.transform.rotation = new Quaternion(0, 180, 0, 0);
                            IdleAni();
                            break;
                    }
                }
            }
        }

    }
    public void ToMoveCharacter()
    {
        //做動畫時不能觸發移動帳號
        if (Ch2AniEvents.AniDone && !ch2VisualManager.ShowBag && Ch2CursorFX.PlayerMovable)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 tempWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (tempWorldPos.x < 7.8 && tempWorldPos.x > -7.1 && tempWorldPos.y < -2.7 && tempWorldPos.y > -5 && !ch2VisualManager.ShowBag)
            {
                mouseWorldPosition = tempWorldPos;
                if (charPos.x > mouseWorldPosition.x)
                {
                    walk = false;//往左是false
                }
                else
                {
                    walk = true;//往右是true
                }
                isClickXiaoMing = false;
                isClickBricksKiller = false;
                isClickPlumber = false;
            }
        }
    }

    #region 主角動畫切換
    public void IdleAni()//主角站著
    {
        CharacterAni.SetBool("walk", false);
        CharacterAni.SetBool("idle", true);
        CharacterAni.SetBool("unLocked", false);
        CharacterAni.SetBool("pullBrand", false);
        CharacterAni.SetBool("putBrand", false);
    }
    public void WalkAni()//主角走路
    {
        CharacterAni.SetBool("walk", true);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("unLocked", false);
        CharacterAni.SetBool("pullBrand", false);
        CharacterAni.SetBool("putBrand", false);
    }
    public void UnLockedAni()//主角解鎖
    {
        CharacterAni.SetBool("walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("unLocked", true);
        CharacterAni.SetBool("pullBrand", false);
        CharacterAni.SetBool("putBrand", false);
    }
    public void PullBramdAni()//主角拔名牌
    {
        CharacterAni.SetBool("walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("unLocked", false);
        CharacterAni.SetBool("pullBrand", true);
        CharacterAni.SetBool("putBrand", false);
    }
    public void PutBramdAni()//主角放名牌
    {
        CharacterAni.SetBool("walk", false);
        CharacterAni.SetBool("idle", false);
        CharacterAni.SetBool("unLocked", false);
        CharacterAni.SetBool("pullBrand", false);
        CharacterAni.SetBool("putBrand", true);
    }
    #endregion

    #region 紀錄玩家點擊哪個名牌
    public void ClickXiaoMing()
    {
        isClickXiaoMing = true;
        isClickBricksKiller = false;
        isClickPlumber = false;
        ch2VisualManager.ShowBag = false;
        ch2VisualManager.Bag_Disappear();
    }
    public void ClickBricksKiller()
    {
        isClickXiaoMing = false;
        isClickBricksKiller = true;
        isClickPlumber = false;
        ch2VisualManager.ShowBag = false;
        ch2VisualManager.Bag_Disappear();
    }
    public void ClickPlumber()
    {
        isClickXiaoMing = false;
        isClickBricksKiller = false;
        isClickPlumber = true;
        ch2VisualManager.ShowBag = false;
        ch2VisualManager.Bag_Disappear();
    }
    #endregion

    #region 紀錄玩家放哪個名牌
    public void PutOnXiaoMing()
    {
        isPutOnXiaoMing = true;
        isPutOnBricksKiller = false;
        isPutOnPlumber = false;
        ch2VisualManager.ShowBag = false;
        ch2VisualManager.Bag_Disappear();
    }
    public void PutOnBricksKiller()
    {
        isPutOnXiaoMing = false;
        isPutOnBricksKiller = true;
        isPutOnPlumber = false;
        ch2VisualManager.ShowBag = false;
        ch2VisualManager.Bag_Disappear();
    }
    public void PutOnPlumber()
    {
        isPutOnXiaoMing = false;
        isPutOnBricksKiller = false;
        isPutOnPlumber = true;
        ch2VisualManager.ShowBag = false;
        ch2VisualManager.Bag_Disappear();
    }
    #endregion

    public void PlayerGetBrand()
    {
        if (isClickXiaoMing)
        {
            ch2VisualManager.XiaoMing.GetComponent<Ch2ItemEventManager>().PlayerGet = true;
            ch2VisualManager.XiaoMing.GetComponent<Ch2ItemEventManager>().isOnFrame = false;
            ch2VisualManager.XiaoMing.GetComponent<LayoutElement>().ignoreLayout = false;
        }
        else if (isClickBricksKiller)
        {
            ch2VisualManager.BricksKiller.GetComponent<Ch2ItemEventManager>().PlayerGet = true;
            ch2VisualManager.BricksKiller.GetComponent<Ch2ItemEventManager>().isOnFrame = false;
            ch2VisualManager.BricksKiller.GetComponent<LayoutElement>().ignoreLayout = false;
        }
        else if (isClickPlumber)
        {
            ch2VisualManager.Plumber.GetComponent<Ch2ItemEventManager>().PlayerGet = true;
            ch2VisualManager.Plumber.GetComponent<Ch2ItemEventManager>().isOnFrame = false;
            ch2VisualManager.Plumber.GetComponent<LayoutElement>().ignoreLayout = false;
        }
    }
    public void PlayerPutBrand()
    {
        if (isPutOnXiaoMing)
        {
            ch2VisualManager.XiaoMing.GetComponent<Ch2ItemEventManager>().PlayerGet = false;
            ch2VisualManager.XiaoMing.GetComponent<Ch2ItemEventManager>().isOnFrame = true;
            ch2VisualManager.XiaoMing.GetComponent<LayoutElement>().ignoreLayout = true;
            ch2VisualManager.XiaoMing.GetComponent<SpriteRenderer>().sortingLayerName = "Item";//測試測試
            ch2VisualManager.XiaoMing.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Item";
        }
        else if (isPutOnBricksKiller)
        {
            ch2VisualManager.BricksKiller.GetComponent<Ch2ItemEventManager>().PlayerGet = false;
            ch2VisualManager.BricksKiller.GetComponent<Ch2ItemEventManager>().isOnFrame = true;
            ch2VisualManager.BricksKiller.GetComponent<LayoutElement>().ignoreLayout = true;
            ch2VisualManager.BricksKiller.GetComponent<SpriteRenderer>().sortingLayerName = "Item";//測試測試
            ch2VisualManager.BricksKiller.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Item";
        }
        else if (isPutOnPlumber)
        {
            ch2VisualManager.Plumber.GetComponent<Ch2ItemEventManager>().PlayerGet = false;
            ch2VisualManager.Plumber.GetComponent<Ch2ItemEventManager>().isOnFrame = true;
            ch2VisualManager.Plumber.GetComponent<LayoutElement>().ignoreLayout = true;
            ch2VisualManager.Plumber.GetComponent<SpriteRenderer>().sortingLayerName = "Item";//測試測試
            ch2VisualManager.Plumber.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Item";
        }
    }

    public void PlayerUnLock()
    {
        if (isUnLockKey)
        {
            ch2VisualManager.BricksKiller_Locked.GetComponent<Animator>().enabled = true;
        }
        else if (isUnLockCoin)
        {
            ch2VisualManager.Plumber_Locked.GetComponent<Animator>().enabled = true;
        }
    }
}
