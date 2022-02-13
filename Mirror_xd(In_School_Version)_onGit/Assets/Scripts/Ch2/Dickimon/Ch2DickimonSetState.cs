using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[System.Serializable]
public class nowDickmonStates
{
    public DickmonStatesc[] DickmonStates;
}
[System.Serializable]
public class DickmonStatesc
{
    public string NowEnemyName;
    public int EnemyLV;
    public NowStatec[] NowState;
}
[System.Serializable]
public class NowStatec
{
    public string NowCharatername;
    public int PlayerLV;

    public int PlayerHP;

    public string[] dialog;

    public Menuc Menu;

    public skillsc skills;

}
[System.Serializable]
public class Menuc
{
    public string[] MenuListstr;
    public string[] MenuTwofeedbackstr;
    public Bagc Bag;
    public Dickimonc Dickimon;
    public Menufullfeedbackc Menufullfeedback;
}
[System.Serializable]

public class Bagc
{
    public string[] Bag_L;
    public string[] Bag_R;
}
[System.Serializable]

public class Dickimonc
{
    public string[] Dickimon_L;
    public string[] Dickimon_R;
}
[System.Serializable]
public class Menufullfeedbackc
{
    public string[] Bagfeedbackstr;
    public string[] Dickimonfeedbackstr;

    public string[] DickimonAtkFeedbackstr;
}

[System.Serializable]
public class skillsc
{
    public string[] skill_name;
    public string[] skill_detail;

    public string[] skill_feedback;
}
public class Ch2DickimonSetState : MonoBehaviour
{
    //====ManagerMent====
    AudioManager audioManager;

    public GameObject PlumberGame;
    Game3Manager game3Manager;
    //===================

    public bool isPlayingAni = false;

    public bool AutoPlayingDia = false;

    public bool XiaoDone = false;

    public bool KillerDone = false;

    public bool PlumberDone = false;

    public bool ExitNone = false;

    public GameObject ExitBtn;
    public TextAsset file;

    public GameObject PlayerHPFrameObj;

    GameObject PlayerHPFrameRoot;

    public GameObject DmDickchu;
    public GameObject DmDuck;

    public GameObject DmXiaoMing;
    public GameObject DmPlumber;
    public GameObject DmBricksKiller;
    public GameObject DmGetchaRacObj;
    public GameObject EnemyAtkFx;
    public GameObject RaccoonObj;
    public GameObject Horse;
    public Animator HorseBeamAtor;
    public Animator GetchaedRac;
    public GameObject WoodBoard;
    public GameObject EnemyHp;

    public GameObject HorseBurnObj;
    public bool PlumberisAvailable = true;

    public bool GetKey = false;

    public bool KeyFell = false;

    int PlayerHPcom;
    int PlayerHPnow;
    public Image PlayerHPBar;
    Text[] PlayerName = new Text[2];

    Text[] PlayerLevel = new Text[2];

    Text[] PlayerHP = new Text[2];

    public GameObject EnemyHPFrameObj;

    public Image EnemyHPBar;
    Text[] EnemyName = new Text[2];

    Text[] EnemyLevel = new Text[2];

    public GameObject FulldialogObj;
    public GameObject TwodialogObj;

    GameObject Undo;

    GameObject Left_Dia;
    GameObject Right_Menu;
    GameObject Skill;

    GameObject Bag;

    GameObject Dickimon;


    public string nowCharater;

    public string nowEnemy;

    public int NowDuckamount = 1;

    public int NowBallamount = 999;

    Menuc nowMenu;

    skillsc nowskills;

    Bagc nowBag;

    Dickimonc nowDickimon;

    Ch2DickimonTypingEffect Dickimontpying;
    Ch2DickimonDianMenu DickimonDianMenu;

    Ch2DickimonSkills DickimonSkills;

    Ch2DickimonSkills DickmonBag;

    Ch2DickimonSkills DickimonDic;

    Ch2DickimonLeftdiatyping Leftdiatyping;


    // Start is called before the first frame update

    void Awake()
    {
        Loaddefaultobjs();
        nowEnemy = "迪可丘";
        // nowCharater = "BricksKiller";
        // nowEnemy = "無情浣熊";
        // nowEnemy = "木馬病毒";
        // nowCharater = "XiaoMing";
        PlayerHPFrameRoot = PlayerHPFrameObj.transform.parent.transform.parent.transform.gameObject;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        game3Manager = PlumberGame.GetComponent<Game3Manager>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (changeHPBar)
        {
            PlayerHPBar.fillAmount = Mathf.Lerp(PlayerHPBar.fillAmount, (float)PlayerHPnow / (float)PlayerHPcom, 0.05f);
            if (PlayerHPBar.fillAmount == (float)PlayerHPnow / (float)PlayerHPcom)
            {
                changeHPBar = false;
            }
        }
    }

    public void ChangeEnemy(string enemyname)
    {
        nowEnemy = enemyname;
    }

    public void ToSetDickmonState(bool isfirst)
    {

        if (nowCharater == "XiaoMing" && XiaoDone)
        {
            DmXiaoMing.SetActive(false);
            setNoCharater();
            return;
        }
        else if (nowCharater == "BricksKiller" && KillerDone)
        {
            DmBricksKiller.SetActive(false);
            setNoCharater();
            return;
        }
        else if (nowCharater == "Plumber" && PlumberDone)
        {
            DmPlumber.SetActive(false);
            setNoCharater();
            return;
        }

        bool isDickchu = false;
        nowDickmonStates DickmonStateJson = JsonUtility.FromJson<nowDickmonStates>(file.text);

        foreach (DickmonStatesc dicstate in DickmonStateJson.DickmonStates)
        {
            if (dicstate.NowEnemyName == nowEnemy)
            {
                SetdefaultTexts(EnemyName, nowEnemy, NowDuckamount);
                SetdefaultTexts(EnemyLevel, "Lv:" + dicstate.EnemyLV.ToString(), NowDuckamount);

                if (dicstate.NowEnemyName == "迪可丘" || dicstate.NowEnemyName == "巨大迪可丘")
                {
                    EnemyHPBar.sprite = Resources.Load<Sprite>("Textures/HP");
                    EnemyHPBar.fillAmount = 1;

                    ExitBtn.SetActive(true);
                    DmDickchu.SetActive(true);
                    if (!KeyFell)
                    {
                        DmDickchu.transform.DOLocalJump(new Vector3(2.976593f, 0.4399387f, 0), 1f, 1, 0.4f, false);
                    }
                    //========音效========
                    if (dicstate.NowEnemyName == "迪可丘" && !GetKey && !KeyFell)
                    {
                        audioManager.DickiShout_Dickimon();
                    }
                    else if (!GetKey && !KeyFell)
                    {
                        audioManager.BigDickimonShout_Dickimon();
                    }
                    //========音效========
                    isDickchu = true;
                }
                else if (dicstate.NowEnemyName == "無情浣熊")
                {
                    EnemyHPBar.sprite = Resources.Load<Sprite>("Textures/HP_red");
                    EnemyHPBar.fillAmount = 0.1f;
                    PlumberisAvailable = true;
                    DmPlumber.GetComponent<Animator>().enabled = true;
                    DmDickchu.SetActive(false);
                    audioManager.RaccoonAppear_Dickimon();
                    GetKey = false;
                    KeyFell = false;
                    EnemyAtkFx.transform.parent = RaccoonObj.transform;
                    EnemyAtkFx.transform.localScale = new Vector3(1, 1, 0);
                    RaccoonObj.SetActive(true);
                    this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/rr_fream_withoutExitBtn");
                    ExitBtn.SetActive(false);
                }
                else if (dicstate.NowEnemyName == "木馬病毒")
                {
                    EnemyHPBar.sprite = Resources.Load<Sprite>("Textures/HP");
                    EnemyHPBar.fillAmount = 1;
                    //===setDia===
                    FulldialogObj.transform.parent.gameObject.SetActive(true);
                    TwodialogObj.SetActive(false);
                    TwodialogObj.transform.Find("undo").gameObject.SetActive(false);
                    Skill.SetActive(false);
                    Bag.SetActive(false);
                    Dickimon.SetActive(false);
                    Left_Dia.SetActive(false);
                    Right_Menu.SetActive(false);
                    //============
                    DmDickchu.SetActive(false);
                    RaccoonObj.SetActive(false);
                    audioManager.HorseShoutAtBegin_Dickimon();
                    GetKey = false;
                    KeyFell = false;
                    Horse.SetActive(true);
                    // Horse.transform.Find("horsePivot").localPosition = new Vector3(-0.3199997f, -1f, 0);
                    // Horse.transform.Find("horsePivot").DOLocalJump(new Vector3(-0.3199997f, -1f, 0), 0.5f, 1, 0.4f, false).OnComplete(() =>
                    // {
                    //     Horse.transform.Find("horsePivot").DOLocalMoveY(-1f, 0.1f, true);
                    // });
                    this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/rr_fream_withoutExitBtn");
                    ExitBtn.SetActive(false);
                }
                foreach (NowStatec nowstate in dicstate.NowState)
                {
                    if (nowstate.NowCharatername == nowCharater)
                    {
                        SetdefaultTexts(PlayerName, nowCharater, NowDuckamount);
                        SetdefaultTexts(PlayerLevel, "Lv:" + nowstate.PlayerLV.ToString(), NowDuckamount);
                        SetdefaultTexts(PlayerHP, nowstate.PlayerHP.ToString() + " / " + nowstate.PlayerHP.ToString(), NowDuckamount);
                        if (nowCharater == "XiaoMing" && nowEnemy != "木馬病毒" && nowEnemy != "木馬終結者")
                        {
                            nowstate.dialog[1] += NowDuckamount.ToString() + "!";
                        }
                        PlayerHPcom = nowstate.PlayerHP;
                        PlayerHPnow = PlayerHPcom;
                        if (isfirst)
                        {
                            Dickimontpying.ImportDialog(nowstate.dialog, false);
                        }
                        nowMenu = nowstate.Menu;
                        nowskills = nowstate.skills;
                        nowBag = nowMenu.Bag;
                        nowDickimon = nowMenu.Dickimon;
                        break;
                    }
                }
                break;
            }
        }

        Dickimontpying.DickimonAtkFeedbackstr = nowMenu.Menufullfeedback.DickimonAtkFeedbackstr;
        DickimonDianMenu.ImportFeedbackstr(nowMenu.MenuTwofeedbackstr);
        DickimonSkills.ImportFeedbackstr(nowskills.skill_feedback);
        DickmonBag.ImporFullFeedBack(nowMenu.Menufullfeedback.Bagfeedbackstr);
        DickimonDic.ImporDickimonFeedback(nowMenu.Menufullfeedback.Dickimonfeedbackstr);

        for (int index = 1; index < 5; index++)
        {
            Right_Menu.transform.GetChild(index).gameObject.transform.GetComponent<Text>().text = nowMenu.MenuListstr[index - 1];
            Skill.transform.GetChild(index).gameObject.transform.GetComponent<Text>().text = nowskills.skill_name[index - 1];
            Skill.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = nowskills.skill_detail[index - 1];
            Bag.transform.GetChild(index).gameObject.transform.GetComponent<Text>().text = nowBag.Bag_L[index - 1];
            Bag.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = nowBag.Bag_R[index - 1];
            if (index == 1)
            {
                Dickimon.transform.GetChild(index).gameObject.transform.GetComponent<Text>().text = nowDickimon.Dickimon_L[index - 1] + (NowDuckamount + 1).ToString();
            }
            else
            {
                Dickimon.transform.GetChild(index).gameObject.transform.GetComponent<Text>().text = nowDickimon.Dickimon_L[index - 1];
            }

            Dickimon.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = nowDickimon.Dickimon_R[index - 1];
        }
        if (ExitNone)
        {
            Right_Menu.transform.GetChild(4).gameObject.transform.GetComponent<Text>().text = "";
        }
        if (nowskills.skill_name[0] == "反彈" || nowskills.skill_name[0] == "跳躍" || nowskills.skill_name[0] == "垃圾桶攻擊")
        {
            Skill.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Skill.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

        }
        if (nowBag.Bag_L[0] == "愛心蘑菇")
        {
            if (isDickchu)
            {
                Bag.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "1";
            }
            else
            {
                Bag.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "0";
            }
        }
        else if (nowBag.Bag_L[0] == "愛心寶貝球")
        {
            Bag.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = NowBallamount.ToString();
        }
        HPFrameSlideAni(nowCharater);

        if (!PlumberisAvailable && nowCharater == "Plumber")
        {
            setPlumberUnavailable();
        }

        if (KeyFell)
        {
            setkeyfell();
        }
        if (GetKey)
        {
            setGetkey();
        }



    }
    //=============PlumberDiaSet==============
    void setPlumberUnavailable()
    {
        string[] dia = { "水電工被巨大的迪可丘撞飛了!!" };
        Dickimontpying.ImportDialog(dia, false);
        PlayerHPFrameRoot.transform.DOLocalMoveX(12.04f, 0);
        DmPlumber.GetComponent<Animator>().enabled = false;
        DmPlumber.transform.localPosition = new Vector3(-8.4f, -1.72f, 0);
    }

    //=============BricksKillerGetKeySet==============
    public void setGetkey()
    {
        string[] dia = { "你獲得了鑰匙!" };
        Dickimontpying.ImportDialog(dia, false);
        PlayerHPFrameRoot.transform.DOLocalMoveX(12.04f, 0);
    }
    //=============BricksKillerKeyFellSet==============
    void setkeyfell()
    {
        string[] dia = { "迪可丘身上的鑰匙掉下來了!" };
        Dickimontpying.ImportDialog(dia, false);
        PlayerHPFrameRoot.transform.DOLocalMoveX(12.04f, 0);
    }
    //=========setNoCharater============

    public void setNoCharater()
    {
        string[] dia = { "錯誤:未讀取到角色" };
        Dickimontpying.ImportDialog(dia, false);
        PlayerHPFrameRoot.transform.DOLocalMoveX(12.04f, 0);
        ExitBtn.SetActive(false);
    }

    //=============Clickundo==============
    public void ClickUndo()
    {
        // 點擊音效
        audioManager.ClickBtnSecondary_Dickimon();
        // ===========================
        Leftdiatyping.typDefault();
        Right_Menu.SetActive(true);
        Left_Dia.SetActive(true);
        Undo.SetActive(false);
        Skill.SetActive(false);
        Bag.SetActive(false);
        Dickimon.SetActive(false);
    }
    //==============HPBar=================

    bool changeHPBar = false;
    public void PlayergetHurt(int hptoreduce)
    {
        PlayerHPnow -= hptoreduce;
        if (PlayerHPnow < 0)
        {
            PlayerHPnow = 0;
        }
        else if (PlayerHPnow > PlayerHPcom)
        {
            PlayerHPnow = PlayerHPcom;
        }
        changeHPBar = true;
        SetdefaultTexts(PlayerHP, PlayerHPnow + " / " + PlayerHPcom, NowDuckamount);
    }

    //========HPFrameAni===============
    public void HPFrameSlideAni(string nowcharater)
    {
        if (nowcharater != "XiaoMing")
        {
            if (nowcharater == "Plumber")
            {
                if (PlumberisAvailable) PlayerHPFrameRoot.transform.DOLocalMoveX(12.04f, 0.3f);
            }
            else if (GetKey)
            {
                PlayerHPFrameRoot.transform.DOLocalMoveX(12.04f, 0f);
            }
            else
            {
                PlayerHPFrameRoot.transform.DOLocalMoveX(12.04f, 0.3f);
            }
        }
    }


    //========loaddefaultobjs===========
    public void Loaddefaultobjs()
    {
        Left_Dia = TwodialogObj.transform.Find("Left_Dialog").gameObject;
        Leftdiatyping = Left_Dia.GetComponent<Ch2DickimonLeftdiatyping>();
        Undo = TwodialogObj.transform.Find("undo").gameObject;
        Dickimontpying = FulldialogObj.GetComponent<Ch2DickimonTypingEffect>();
        Right_Menu = TwodialogObj.transform.Find("Right_menu").gameObject;
        DickimonDianMenu = Right_Menu.transform.GetChild(1).GetComponent<Ch2DickimonDianMenu>();
        Skill = TwodialogObj.transform.Find("Skill").gameObject;
        DickimonSkills = Skill.transform.GetChild(1).GetComponent<Ch2DickimonSkills>();
        Bag = TwodialogObj.transform.Find("Bag").gameObject;
        DickmonBag = Bag.transform.GetChild(1).GetComponent<Ch2DickimonSkills>();
        Dickimon = TwodialogObj.transform.Find("Dickimon").gameObject;
        DickimonDic = Dickimon.transform.GetChild(1).GetComponent<Ch2DickimonSkills>();

        loadTextObjs(true, "PlayerName");
        loadTextObjs(true, "PlayerLevel");
        loadTextObjs(true, "PlayerHP");
        loadTextObjs(false, "EnemyLevel");
        loadTextObjs(false, "EnemyName");
    }

    public void loadTextObjs(bool isPlayer, string nowtoload)
    {
        Component[] comp;
        if (isPlayer)
        {
            comp = PlayerHPFrameObj.transform.Find(nowtoload).GetComponentsInChildren(typeof(Text));
        }
        else
        {
            comp = EnemyHPFrameObj.transform.Find(nowtoload).GetComponentsInChildren(typeof(Text));
        }
        int index = 0;
        Text[] TexttoLoad = null;
        switch (nowtoload)
        {
            case "PlayerName":
                TexttoLoad = PlayerName;
                break;
            case "PlayerLevel":
                TexttoLoad = PlayerLevel;
                break;
            case "PlayerHP":
                TexttoLoad = PlayerHP;
                break;
            case "EnemyName":
                TexttoLoad = EnemyName;
                break;
            case "EnemyLevel":
                TexttoLoad = EnemyLevel;
                break;
        }

        foreach (Text text in comp)
        {
            TexttoLoad[index] = text;
            index++;

        }
    }
    //========setdefaultobjs===========

    public void SetdefaultTexts(Text[] nowTexttoset, string strtoset, int DuckNum)
    {
        if (nowTexttoset == PlayerName)
        {
            switch (strtoset)
            {
                case "XiaoMing":
                    if (nowEnemy == "木馬病毒" || nowEnemy == "木馬終結者")
                    {
                        strtoset = "無情浣熊";
                    }
                    else
                    {
                        strtoset = "長頸鴨" + DuckNum.ToString();
                    }
                    break;
                case "BricksKiller":
                    strtoset = "磚塊殺手";
                    break;
                case "Plumber":
                    strtoset = "水電工";
                    break;
            }
        }
        foreach (Text text in nowTexttoset)
        {
            text.text = strtoset;
        }
    }
    //==========CloseWindow===========

    public void closeDickimonWindow()
    {
        // ====================
        // 過場音效
        audioManager.MingBackFromBattle_Raccoon();
        // ====================
        PlayerHPFrameRoot.transform.DOLocalMoveX(40f, 0);
        DmXiaoMing.transform.localPosition = new Vector3(-1.47f, -1.59f, 0);
        FulldialogObj.transform.parent.gameObject.SetActive(true);
        TwodialogObj.SetActive(false);
        TwodialogObj.transform.Find("undo").gameObject.SetActive(false);
        Skill.SetActive(false);
        Bag.SetActive(false);
        Dickimon.SetActive(false);
        Left_Dia.SetActive(false);
        Right_Menu.SetActive(false);
        if (DmDuck.activeSelf)
        {
            DmDuck.SetActive(false);
            DmDuck.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        }
        else if (DmGetchaRacObj.activeSelf)
        {
            DmGetchaRacObj.SetActive(false);
        }
        AutoPlayingDia = false;
    }

    //=========ChangeDuckAmount=========

    public void changeDuckAmount()
    {
        Dickimon.transform.GetChild(1).gameObject.transform.GetComponent<Text>().text = nowDickimon.Dickimon_L[0] + (NowDuckamount + 1).ToString();
    }

    //=========ChangeDuckName=========
    public void changeDuckName()
    {
        SetdefaultTexts(PlayerName, "XiaoMing", NowDuckamount);
    }

    public void changePlayerInformation()
    {
        SetdefaultTexts(PlayerName, "&#^+#*#*$", NowDuckamount);
        SetdefaultTexts(PlayerLevel, "@%*$&(@#*", NowDuckamount);
        SetdefaultTexts(PlayerHP, "(*#&&^#@)$", NowDuckamount);
    }
    //=========RaccoonAutoPlayStr=========
    public void AutoNextString()
    {
        Dickimontpying.AutoNextString();
    }
    //=========DickchucurshIntoHorse=========
    public void DickchucrushintoHorse()
    {
        DmDickchu.transform.Find("dm_dicky").GetComponent<Animator>().enabled = true;
        DmDickchu.SetActive(true);
    }
    //=========DickimonFrameShake=========
    public void DickimonFrameShake(int stage)
    {
        switch (stage)
        {
            case 1:
                this.transform.DOShakePosition(0.3f, new Vector3(0.75f, 0.75f, 0)).SetId<Tween>("Fshake");
                break;
            case 2:
                DOTween.Kill("Fshake");
                this.transform.DOShakePosition(3f, new Vector3(0.25f, 0.25f, 0), 10, 45, false, false).SetEase(Ease.Linear).OnComplete(() =>
               {
                   this.transform.DOShakePosition(2f, new Vector3(0.25f, 0.25f, 0), 10, 45, false, false).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            this.transform.DOShakePosition(0.5f, new Vector3(0.3f, 0.3f, 0)).SetId<Tween>("Fshake");
                        });
               });
                break;
            case 3:
                DOTween.Kill("Fshake");
                this.transform.DOShakePosition(0.8f, new Vector3(0.75f, 0.75f, 0)).SetId<Tween>("Fshake");
                break;
        }

    }
    //=========AutoPlayString=========
    public bool isPlayKillExplosion = false;
    public IEnumerator AutoPlayString()
    {
        isPlayingAni = true;
        yield return new WaitForSeconds(1.2f);
        AutoNextString();
        if (nowCharater == "Plumber")
        {
            HorseBeamAtor.SetTrigger("Plumber");
        }
        else
        {
            isPlayKillExplosion = true;
            HorseBeamAtor.SetTrigger("Normal");
        }
    }

    //=========DickimonToRaccoon==========

    public void TurnToRac()
    {
        PlumberGame.SetActive(true);
        game3Manager.StartCoroutine(game3Manager.TurnToRac());
    }

    public void TurnToRacMyth()
    {
        PlumberGame.SetActive(true);
        game3Manager.StartCoroutine(game3Manager.TurnToRacMyth());
    }
    //=========Horse==========
    public void HorseJump()
    {
        Horse.transform.Find("horsePivot").DOLocalJump(new Vector3(-0.3199997f, -1f, 0), 0.5f, 1, 0.4f, false);
    }

    public void HorseDieDia()
    {
        string[] dia = { "效果群拔!", "木馬終結者:「好燙啊!一代!一代!一代!」", "木馬終結者被燒毀了!", "您打敗了木馬終結者!" };
        Dickimontpying.ImportDialog(dia, false);
    }
    // =============================
    // 檢查三個角色是否死亡
    public int CheckCharDeadNum()
    {
        int count = 0;
        if (XiaoDone)
        {
            count++;
        }
        if (KillerDone)
        {
            count++;
        }
        if (PlumberDone)
        {
            count++;
        }
        return count;
    }
}
