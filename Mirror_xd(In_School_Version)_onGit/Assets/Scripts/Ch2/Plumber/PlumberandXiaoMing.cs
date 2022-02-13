using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlumberandXiaoMing : MonoBehaviour
{
    Ch2GameManager ch2GameManager;
    Ch2DialogueManager ch2DialogueManager;
    BGMManager bgmManager;
    Game2Manager game2Manager;
    Game3Manager game3Manager;
    GameObject nowTriggerTarget;
    GameObject nowCollisionTarget;
    AudioManager audioManager;
    Ch2CursorSetting cursorSetting;
    public GameObject Game2;
    public GameObject Game3;
    public GameObject Partical;

    // Start is called before the first frame update
    void Start()
    {
        ch2GameManager = GameObject.Find("GameManager").GetComponent<Ch2GameManager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        game2Manager = Game2.GetComponent<Game2Manager>();
        game3Manager = Game3.GetComponent<Game3Manager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        nowTriggerTarget = other.gameObject;
        if (nowTriggerTarget.tag == "Bricks1" && this.name == "bk_Plumber")//第二關水電工撞到磚塊
        {
            audioManager.NormalBricksBroken_Break_Safe();
            nowTriggerTarget.transform.DOLocalMoveY(2.49f, 0.15f, false);
            Instantiate(Partical, nowTriggerTarget.transform.position, nowTriggerTarget.transform.rotation);
            StartCoroutine(wait(nowTriggerTarget));
        }


        //不用爬上第二層，要自己過去
        /*else if (nowTriggerTarget.name == "ladder_3" && game3Manager.isClickLadder)
        {
            game3Manager.Character.GetComponent<Animator>().SetBool("clamb", true);
            game3Manager.Character.GetComponent<Animator>().SetBool("walk", false);
            this.transform.DOLocalMoveY(16.8f, 2.5f, true);
            game3Manager.isTriggerLadder = false;
            game3Manager.isClickLadder = false;
        }*/
        IEnumerator wait(GameObject Target)
        {
            yield return new WaitForSeconds(0.15f);
            Destroy(Target);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        nowTriggerTarget = other.gameObject;
        /*if (game3Manager.isMoveLadder)
        {
            game3Manager.UpArrow.GetComponent<Collider2D>().enabled = false;
            game3Manager.UpArrow.SetActive(false);//把向上箭頭打開
            game3Manager.DownArrow.SetActive(false);//把向上箭頭打開
        }
        else
        {
            game3Manager.UpArrow.GetComponent<Collider2D>().enabled = true;
            game3Manager.UpArrow.SetActive(true);//把向上箭頭打開
        }*/
        if (nowTriggerTarget.name == "ladder_get")//如果碰到可以移動的樓梯
        {
            game3Manager.isTriggerLadder2 = true;
            if (nowTriggerTarget.transform.parent.name == "ladder" && this.name == "pb_Plumber")
            {
                game3Manager.isTriggerLadder = true;//如果是水電工又碰到可以動的樓梯
            }
        }
        else if (nowTriggerTarget.name == "ladder_1")
        {
            game3Manager.isTriggerLadder1 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        nowTriggerTarget = null;
        game3Manager.isTriggerLadder = false;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), game3Manager.Floor[1].GetComponent<Collider2D>(), false);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), game3Manager.Floor[2].GetComponent<Collider2D>(), false);
        game3Manager.UpArrow.SetActive(false);
        game3Manager.DownArrow.SetActive(false);
        game3Manager.isTriggerLadder1 = false;
        game3Manager.isTriggerLadder2 = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        nowCollisionTarget = other.gameObject;
        if (nowCollisionTarget.tag != "Trashcan" && nowCollisionTarget.name != "pb_araiguma")
        {
            game3Manager.WhitchFloor = game3Manager.OnWhitchFloor(nowCollisionTarget.name);
        }
        if (nowCollisionTarget.tag == "Trashcan" && nowCollisionTarget != null)
        {
            game3Manager.isDie = true;
            switch (this.name)
            {
                case "pb_Plumber":
                    audioManager.HitByTrashCan_Raccoon();
                    //如果水電工身上有樓梯
                    if (game3Manager.Ladder.transform.parent.name == this.gameObject.name && nowCollisionTarget.tag == "Trashcan")
                    {
                        CharacterDie();
                        this.transform.GetChild(2).gameObject.SetActive(true);//可以拿的樓梯
                        game3Manager.Ladder.transform.position = new Vector2(1.08f, 0.33f);
                        game3Manager.Ladder.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                        game3Manager.Ladder.transform.SetParent(GameObject.Find("ladder").transform);
                        game3Manager.isMoveLadder = false;
                    }
                    else if (nowCollisionTarget.tag == "Trashcan")
                    {
                        CharacterDie();
                    }
                    break;
                case "pb_XiaoMing":
                    audioManager.HitByTrashCan_Raccoon();
                    if (nowCollisionTarget.tag == "Trashcan")
                    {
                        CharacterDie();
                    }
                    break;
            }
        }
        else if (nowCollisionTarget.name == "Wall4")//回到原本的位置
        {
            if (this.name == "pb_Plumber")//如果水電工身上有梯子，把梯子送回原本位置
            {
                this.transform.GetChild(2).gameObject.SetActive(false);//粒子特效
            }
            foreach (GameObject floor in game3Manager.Floor)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), floor.GetComponent<Collider2D>(), false);
            }
            //game3Manager.InvisibleWall.SetActive(true);
            this.transform.localPosition = new Vector2(-4.5f, -11.1f);
            this.GetComponent<Animator>().SetBool("die", false);
            game3Manager.isClambLadder = false;
        }
        else if (nowCollisionTarget.name == "Wall3")//爬上第二層
        {
            game3Manager.Stage++;
            // 觸發文本
            ch2DialogueManager.ShowNextDialogue("上去且跟木馬病毒交談", true);
            game3Manager.SkillText.gameObject.SetActive(false);//關掉文字
            // =============================
            game3Manager.BalckBg.DOFade(1, 2f).OnComplete(() =>
             {
                 game3Manager.Level[1].SetActive(true);
                 game3Manager.Level[0].SetActive(false);
                 game3Manager.Ladder.SetActive(false);
                 game3Manager.Ladder1.SetActive(false);
                 this.transform.DOLocalMove(new Vector3(-14.7f, -16.6f, 0), 0f, false);
                 this.GetComponent<Rigidbody2D>().isKinematic = true;
                 this.GetComponent<Animator>().SetBool("clamb", true);
                 this.GetComponent<Animator>().speed = 0;
                 game3Manager.BalckBg.DOFade(0, 2f).OnComplete(() =>
                 {
                     this.GetComponent<Animator>().speed = 1;
                     this.transform.DOLocalMove(new Vector3(-14.7f, -5f, 0), 3f, true).OnComplete(() =>
                        {
                            this.GetComponent<Rigidbody2D>().isKinematic = false;
                            this.GetComponent<Animator>().SetBool("clamb", false);
                            //game3Manager.CharacterPos = this.transform.position;
                            game3Manager.isinLevel2 = true;
                            if (Game2and3DialogTypingEffect.stage == 5)
                            {
                                StartCoroutine(waitforsecondToOpenDialog());
                            }
                            // =============================
                            // 切換BGM
                            bgmManager.StopCH2BGM();
                            bgmManager.PlayHorseCGBgm();
                            // =============================

                        });
                 });
             }
            );
        }
        else if (nowCollisionTarget.name == "pb_araiguma" && this.name == "pb_XiaoMing")
        {
            nowCollisionTarget.GetComponent<Collider2D>().enabled = false;
            game3Manager.isClambLadder = true;
            game3Manager.Character.GetComponent<Animator>().SetBool("walk", false);//角色切回idle
            game3Manager.CloseBtn.enabled = false;
            StartCoroutine(wait());//等1.5秒後打開過場
        }
        IEnumerator waitforsecondToOpenDialog()
        {
            yield return new WaitForSeconds(1.5f);
            //鼠標沙漏動畫
            Ch2AniEvents.AniDone = true;
            cursorSetting.StopCursorHourglassAni();
            //鼠標沙漏動畫
            Game2and3DialogTypingEffect.stage = 6;
            game3Manager.DialogCanvas.SetActive(true);
            game3Manager.ToBackBtn.enabled = false;
            game3Manager.CloseBtn.enabled = false;
            game3Manager.DialogCanvas.GetComponent<Ch2PlumberDialog>().StartDialog();
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        game3Manager.BigDick.SetActive(true);
        // ====================
        // 過場音效
        audioManager.MingIntoBattle_Raccoon();
        // ====================
        game3Manager.BigDick.transform.DOScale(new Vector3(17f, 17f, 0), 3f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            ch2GameManager.RacToDickimon();
        });
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        nowCollisionTarget = null;
    }
    public void CharacterDie()//水電工或小明被垃圾桶砸到
    {
        DOTween.KillAll();
        this.GetComponent<Rigidbody2D>().isKinematic = false;//因為kill掉爬樓梯的動畫，所以要改回來
        game3Manager.isClambLadder = false;
        game3Manager.UpArrow.SetActive(false);
        game3Manager.DownArrow.SetActive(false);

        this.GetComponent<Animator>().SetBool("die", true);
        this.GetComponent<Animator>().SetBool("clamb", false);
        this.GetComponent<Animator>().SetBool("walk", false);

        foreach (GameObject floor in game3Manager.Floor)
        {
            //floor.GetComponent<Collider2D>().isTrigger = true;
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), floor.GetComponent<Collider2D>(), true);
        }
        GameObject[] Trashcan;
        Trashcan = GameObject.FindGameObjectsWithTag("Trashcan");
        GameObject[] Particals;
        Particals = GameObject.FindGameObjectsWithTag("Partical");
        foreach (GameObject trashcan in Trashcan)//Destory所有垃圾桶
        {
            Destroy(trashcan);
        }
        foreach (GameObject Partical in Particals)
        {
            Destroy(Partical);
        }
        //audioManager.CharacterDead_Raccoon();
        //這裡有問題
        Sequence seq = DOTween.Sequence();
        seq.Append(this.transform.DOLocalMoveY(-30f, 0.8f).SetEase(Ease.InBack));
        seq.Insert(0.5f, this.transform.DOShakeRotation(0.1f).OnComplete(() =>
        {
            // ========================================
            // 觸發角色死亡音效
            audioManager.CharacterDead_Raccoon();
            // ========================================
        }));
    }
}
