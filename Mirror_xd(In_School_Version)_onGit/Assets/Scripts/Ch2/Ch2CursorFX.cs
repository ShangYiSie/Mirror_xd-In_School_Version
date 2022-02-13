using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Ch2CursorFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    Ch2CursorSetting cursorSetting;
    Ch2VisualManager visualManager;
    public static bool lasttriggerProp = false;

    public static bool PlayerMovable = true;

    bool thistate = false;

    static bool intoshowbar = false;
    // Start is called before the first frame update
    void Start()
    {
        cursorSetting = GameObject.Find("GameManager").GetComponent<Ch2CursorSetting>();
        visualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Button>() != null)
        {

            // Debug.Log(this.name + ":" + 
        }

        if (!Ch2AniEvents.AniDone && this.name != "DialogueTextBack" && this.name != "Dialogue_front")
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
            if (this.GetComponent<Button>() != null)//&& this.GetComponent<Button>().interactable == true)
            {
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
            if ((int)Ch2GameManager.nowCH2State % 2 == 0)   // 代表在正面世界
            {
                cursorSetting.ChangeState("Front_Nograb");
            }
            else
            {

                Ch2CursorSetting.CursorNotTrigger = false;
                lasttriggerProp = true;
                cursorSetting.ChangeState("Back_Nograb");

            }
        }
        else//不是道具但可互動的物品
        {
            if ((int)Ch2GameManager.nowCH2State % 2 == 0)   // 代表在正面世界
            {
                cursorSetting.ChangeState("Front_Select");
            }
            else
            {
                if (!visualManager.ShowBag)
                {
                    Ch2CursorSetting.CursorNotTrigger = false;
                    cursorSetting.ChangeState("Back_Select");
                }
                else if (this.name == "BtnShowBag" || this.tag == "Donthide" || this.name == "Dog" || this.name == "Portal")
                {
                    Ch2CursorSetting.CursorNotTrigger = false;
                    cursorSetting.ChangeState("Back_Select");
                }

                if (this.name == "BtnShowBag")
                {
                    intoshowbar = true;
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
        if ((int)Ch2GameManager.nowCH2State % 2 == 0)   // 代表在正面世界
        {
            cursorSetting.ChangeState("Front_Normal");
        }
        else
        {

            lasttriggerProp = false;
            Ch2CursorSetting.CursorNotTrigger = true;
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
        if ((int)Ch2GameManager.nowCH2State % 2 == 0)   // 代表在正面世界
        {
            cursorSetting.ChangeState("Front_Normal");
        }
        else
        {

            if (this.name != "Portal" && this.name != "BtnShowBag" && this.name != "Dog")
            {
                Ch2CursorSetting.CursorNotTrigger = true;
                cursorSetting.ChangeState("Back_Normal");
                PlayerMovable = true;
            }
            else
            {
                cursorSetting.ChangeState("Back_Select");
            }


        }
    }


    public void OnMouseDown()
    {
        if (this.tag == "Prop" && this.name != "Portal" && this.name != "BtnShowBag" && this.name != "Dog")
        {
            if ((int)Ch2GameManager.nowCH2State % 2 == 0)   // 代表在正面世界
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
    }
    public void OnMouseDrag()
    {
        if (this.tag == "Prop" && this.name != "Portal" && this.name != "BtnShowBag")
        {
            if ((int)Ch2GameManager.nowCH2State % 2 == 0)   // 代表在正面世界
            {
                cursorSetting.ChangeState("Front_Grab");
            }
            else
            {
                cursorSetting.ChangeState("Back_Grab");
            }
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.GetComponent<Button>() != null && this.GetComponent<Button>().enabled == true)
        {
            if (this.name == "DialogueTextBack" || this.name == "Dialogue_front")
            {
                if ((int)Ch2GameManager.nowCH2State % 2 == 0)
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
            Ch2CursorSetting.CursorNotTrigger = false;
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
                Ch2CursorSetting.CursorNotTrigger = true;
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
