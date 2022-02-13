using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CursorFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    CursorSetting cursorSetting;
    VisualManager visualManager;
    public static bool lasttriggerProp = false;

    public static bool PlayerMovable = true;

    static bool intoshowbar = false;
    // Start is called before the first frame update
    void Start()
    {
        cursorSetting = GameObject.Find("GameManager").GetComponent<CursorSetting>();
        visualManager = GameObject.Find("VisualManager").GetComponent<VisualManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)GameManager.nowCH1State % 2 != 0)//在反面
        {
            if (this.gameObject.GetComponent<Collider2D>() != null && this.name != "ZoominExitBtn" && GameManager.stage >= 10 && this.name != "U")
            {

                if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.name != "Tools")
                {
                    if (this.tag != "Donthide")
                    {
                        if (visualManager.ShowBag)
                        {

                            this.gameObject.GetComponent<Collider2D>().enabled = false;

                        }
                        else
                        {
                            this.gameObject.GetComponent<Collider2D>().enabled = true;
                        }
                    }
                }
            }
        }

        if (!AniEvents.AniDone && this.name != "DialogueTextBack" && this.name != "Dialogue_front")
        {
            if (this.GetComponent<Button>() != null)
            {
                // this.GetComponent<Button>().enabled = false;
                this.GetComponent<Button>().interactable = false;
            }

            if (this.GetComponent<Collider2D>() != null && this.name == "BtnShowBag")
            {
                this.GetComponent<Collider2D>().enabled = false;
            }

        }
        else
        {
            if (this.GetComponent<Button>() != null && this.name != "BtnGuest" && this.name != "CloseWindow_FX")//&& this.GetComponent<Button>().interactable == true)
            {
                // this.GetComponent<Button>().enabled = true;
                this.GetComponent<Button>().interactable = true;
            }

            if (this.GetComponent<Collider2D>() != null && this.name == "BtnShowBag")
            {
                this.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
    public void OnMouseEnter()
    {

        if (this.tag == "Prop" && this.name != "Portal" && this.name != "BtnShowBag") //是道具
        {
            if ((int)GameManager.nowCH1State % 2 == 0)   // 代表在正面世界
            {
                cursorSetting.ChangeState("Front_Nograb");
            }
            else
            {

                CursorSetting.CursorNotTrigger = false;
                lasttriggerProp = true;
                cursorSetting.ChangeState("Back_Nograb");

            }
        }
        else if (this.tag == "mora")//野球拳道具 只會在正面
        {
            cursorSetting.ChangeState("Front_Nograb");
        }
        else//不是道具但可互動的物品
        {
            if ((int)GameManager.nowCH1State % 2 == 0)   // 代表在正面世界
            {
                cursorSetting.ChangeState("Front_Select");
            }
            else
            {
                if (!visualManager.ShowBag && this.tag != "ZoominItem")
                {
                    CursorSetting.CursorNotTrigger = false;
                    cursorSetting.ChangeState("Back_Select");
                }
                else if (this.name == "BtnShowBag" || this.tag == "ZoominItem" || this.name == "ZoominExitBtn" || this.tag == "Donthide" || this.name == "Dog" || this.name == "Portal")
                {

                    CursorSetting.CursorNotTrigger = false;
                    cursorSetting.ChangeState("Back_Select");
                }

                if (this.name == "BtnShowBag")
                {
                    intoshowbar = true;
                }
                else if (this.name == "Btn_Clock")
                {
                    lasttriggerProp = true;
                }
            }
        }

        if (this.name == "Dog" || this.name == "Portal" || this.name == "BtnShowBag" || this.tag == "Prop")
        {
            PlayerMovable = false;
        }

    }

    public void OnMouseExit()
    {
        if ((int)GameManager.nowCH1State % 2 == 0)   // 代表在正面世界
        {
            cursorSetting.ChangeState("Front_Normal");
        }
        else
        {

            lasttriggerProp = false;
            CursorSetting.CursorNotTrigger = true;
            cursorSetting.ChangeState("Back_Normal");

            if (this.name == "BtnShowBag")
            {
                intoshowbar = false;
            }

            if (this.name == "Dog" || this.name == "Portal" || this.name == "BtnShowBag" || this.tag == "Prop")
            {
                PlayerMovable = true;
            }
        }



    }

    public void OnMouseUp()
    {
        if ((int)GameManager.nowCH1State % 2 == 0)   // 代表在正面世界
        {
            if (this.name != "geegle_geegle" && this.name != "Bitcoin")
            {
                cursorSetting.ChangeState("Front_Normal");
            }
        }
        else
        {

            if (this.name != "Portal" && this.name != "BtnShowBag" && this.name != "Dog")
            {
                CursorSetting.CursorNotTrigger = true;
                cursorSetting.ChangeState("Back_Normal");

            }
            else
            {
                cursorSetting.ChangeState("Back_Select");
            }

            if (this.tag == "Prop" && this.name != "Portal" && this.name != "BtnShowBag")
            {
                PlayerMovable = true;
            }
        }
    }


    public void OnMouseDown()
    {
        if (this.tag == "Prop" && this.name != "Portal" && this.name != "BtnShowBag" && this.tag != "ZoominItem" && this.name != "Dog")
        {
            if ((int)GameManager.nowCH1State % 2 == 0)   // 代表在正面世界
            {
                cursorSetting.ChangeState("Front_Grab");
            }
            else
            {
                PlayerMovable = false;
                if (!visualManager.ShowBag)
                {
                    cursorSetting.ChangeState("Back_Grab");
                }
            }

        }
        else if (this.tag == "mora")//野球拳道具 只會在正面
        {
            cursorSetting.ChangeState("Front_Grab");
        }
    }
    public void OnMouseDrag()
    {
        if (this.tag == "Prop" && this.name != "Portal" && this.name != "BtnShowBag")
        {
            if ((int)GameManager.nowCH1State % 2 == 0)   // 代表在正面世界
            {
                cursorSetting.ChangeState("Front_Grab");
            }
            else
            {
                cursorSetting.ChangeState("Back_Grab");
            }
        }
        else if (this.tag == "mora")//野球拳道具 只會在正面
        {
            cursorSetting.ChangeState("Front_Grab");
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.GetComponent<Button>() != null)
        {
            if (this.name == "DialogueTextBack" || this.name == "Dialogue_front")
            {
                if ((int)GameManager.nowCH1State % 2 == 0)
                {
                    cursorSetting.ChangeState("Front_Play");
                }
                else
                {
                    cursorSetting.ChangeState("Back_Play");
                }
            }
            else if (this.GetComponent<Button>().interactable == true)
            {
                OnMouseEnter();
            }
        }
        else if (this.name == "Back_Bar")
        {
            CursorSetting.CursorNotTrigger = false;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.name == "Back_Bar")
        {

            if (lasttriggerProp)
            {
            }
            else
            {
                CursorSetting.CursorNotTrigger = true;
            }
        }
        else if (this.name == "Btn_Clock")
        {
            if (lasttriggerProp && intoshowbar)
            {
                intoshowbar = false;
            }
            else
            {
                CursorSetting.CursorNotTrigger = true;
            }
        }
        else
        {
            OnMouseExit();
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (this.GetComponent<Button>() != null && this.tag == "BtnNeedsPressedFx" && this.GetComponent<Button>().interactable)
        {
            this.GetComponent<Image>().DOFade(1, 0);
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (this.GetComponent<Button>() != null && this.tag == "BtnNeedsPressedFx" && this.GetComponent<Button>().interactable)
        {
            this.GetComponent<Image>().DOFade(0, 0);
        }
    }
}
