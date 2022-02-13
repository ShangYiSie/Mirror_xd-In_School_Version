using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    UserManager userManager;
    GameManager gameManager;
    VisualManager visualManager;
    DialogueManager dialogueManager;
    AudioManager audioManager;
    public GameObject U;
    public GameObject E;
    public GameObject S;
    public GameObject G;
    GameObject nowTriggerTarget;

    // 確認是否得到
    //public bool haveGetS = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        visualManager = GameObject.Find("VisualManager").GetComponent<VisualManager>();
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("OnTrigger" + other.gameObject.name);

        nowTriggerTarget = other.gameObject;

        if (nowTriggerTarget.name == "Portal")
        {
            // 關閉目前的dialogue
            dialogueManager.StopNowDialogue();
            // =====================
            // ========================
            
            // ========================
            if (userManager.haveGetS && GameManager.stage == 24)
            {
                // 得到s，並且回到Cmail正面
                dialogueManager.ShowNextDialogue("after_get_s_and_toCmailfront", true);
                // 避免再次進入
                userManager.haveGetS = false;
            }
            gameManager.ChangeToFront();
        }
        /*else if (nowTriggerTarget.name == "U" && GameManager.stage == 10 && nowTriggerTarget.GetComponent<ItemEventManager>() == null)
        {
            // 撿起U，觸發文本
            dialogueManager.ShowNextDialogue("pickup_U", false);

            // this.transform.GetChild(0).transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nowTriggerTarget.GetComponent<SpriteRenderer>().sprite;
            //userManager.isTakeU = true;
            nowTriggerTarget.AddComponent<ItemEventManager>();
            GameManager.stage++;
            U.transform.SetParent(GameObject.Find("Tools").transform);
            // U.transform.DOLocalMoveY(-4.65f, 2f).SetEase(Ease.OutBounce);
            U.GetComponent<ItemEventManager>().PlayerGet = true;
            U.GetComponent<LayoutElement>().ignoreLayout = false;
            nowTriggerTarget.GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
        }
        else if (nowTriggerTarget.name == "E" && nowTriggerTarget.GetComponent<ItemEventManager>() == null)
        {
            // 撿起E，觸發文本
            dialogueManager.ShowNextDialogue("get_E", false);

            visualManager.isGetE = true;
            // this.transform.GetChild(0).transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nowTriggerTarget.GetComponent<SpriteRenderer>().sprite;
            if (GameManager.stage == 18)
            {
                GameManager.stage++;
            }
            if (VisualManager.GetS)
            {
                GameManager.stage = 23;
            }
            else
            {
                GameManager.stage = 22;
            }
            //userManager.isPickUp = true;
            nowTriggerTarget.AddComponent<ItemEventManager>();
            E.transform.SetParent(GameObject.Find("Tools").transform);
            E.GetComponent<ItemEventManager>().PlayerGet = true;
            E.GetComponent<LayoutElement>().ignoreLayout = false;
            nowTriggerTarget.GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
        }
        else if (nowTriggerTarget.name == "S" && nowTriggerTarget.GetComponent<ItemEventManager>() == null)
        {
            VisualManager.GetS = true;

            userManager.haveGetS = true;
            // 撿起S，觸發文本
            dialogueManager.ShowNextDialogue("bird_sleep_and_get_s", false);

            // this.transform.GetChild(0).transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nowTriggerTarget.GetComponent<SpriteRenderer>().sprite;
            if (GameManager.stage == 22 && visualManager.isGetE)
            {
                GameManager.stage++;
            }
            //visualManager.CMailIcon.GetComponent<SpriteRenderer>().sprite = visualManager.CmailIcomImg[0];
            visualManager.CmailIcon.SetActive(true);
            visualManager.CmailWormIcon.SetActive(false);
            visualManager.CmailWindowWorm.SetActive(false);
            //userManager.isPickUp = true;
            nowTriggerTarget.AddComponent<ItemEventManager>();
            S.transform.SetParent(GameObject.Find("Tools").transform);
            S.GetComponent<ItemEventManager>().PlayerGet = true;
            S.GetComponent<LayoutElement>().ignoreLayout = false;
            nowTriggerTarget.GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
        }
        else if (nowTriggerTarget.name == "G" && nowTriggerTarget.GetComponent<ItemEventManager>() == null)
        {
            // 撿起G，觸發文本
            dialogueManager.ShowNextDialogue("get_G", false);

            if (GameManager.stage == 28)
            {
                GameManager.stage++;
            }
            // this.transform.GetChild(0).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nowTriggerTarget.GetComponent<SpriteRenderer>().sprite;
            //userManager.isPickUp = true;
            nowTriggerTarget.AddComponent<ItemEventManager>();
            G.transform.SetParent(GameObject.Find("Tools").transform);
            G.GetComponent<ItemEventManager>().PlayerGet = true;
            G.GetComponent<LayoutElement>().ignoreLayout = false;
            nowTriggerTarget.GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
        }
        else if (nowTriggerTarget.name == "GUESTBtnProp" && nowTriggerTarget.GetComponent<ItemEventManager>() == null)
        {
            this.transform.GetChild(0).transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nowTriggerTarget.GetComponent<SpriteRenderer>().sprite;
            //userManager.isPickUp = true;
            nowTriggerTarget.AddComponent<ItemEventManager>();
            nowTriggerTarget.transform.SetParent(GameObject.Find("Tools").transform);
            nowTriggerTarget.GetComponent<ItemEventManager>().PlayerGet = true;
            nowTriggerTarget.GetComponent<LayoutElement>().ignoreLayout = false;
            nowTriggerTarget.GetComponent<SpriteRenderer>().sortingLayerName = "BackItem";
        }*/
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("ExitTrigger" + other.gameObject.name);
        nowTriggerTarget = null;
    }
}
