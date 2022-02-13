using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2CursorSetting : MonoBehaviour
{
    public Texture2D[] mouseTexture_front;//設置正面鼠標的圖片
    public Texture2D[] mouseTexture_back;//設置背面鼠標的圖片

    public Texture2D[] mouseHourglass_front;

    public Texture2D[] mouseHourglass_back;

    static public bool CursorNotTrigger = true;
    // public string cursorState = "";

    Texture2D nowCursorImg;
    Ch2VisualManager ch2visualManager;

    //============HourglassAni================
    public int fps;

    public bool isonText = false;
    int counter = 0;
    int index;

    void Start()
    {
        ch2visualManager = GameObject.Find("VisualManager").GetComponent<Ch2VisualManager>();

        Cursor.visible = false;
        nowCursorImg = mouseTexture_front[0];
    }
    //通過每幀實時渲染出鼠標的圖標
    private void OnGUI()
    {
        Vector3 mousePos = Input.mousePosition;
        Rect rect = new Rect(mousePos.x - 10, Screen.height - mousePos.y - 10, 54f, 55f);

        GUI.DrawTexture(rect, nowCursorImg);

        if ((int)Ch2GameManager.nowCH2State % 2 != 0 && CursorNotTrigger)//在背面
        {
            Vector2 tempWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (tempWorldPos.x < 7.8 && tempWorldPos.x > -8.1 && tempWorldPos.y < -2.7 && tempWorldPos.y > -5 && !ch2visualManager.ShowBag)
            {
                nowCursorImg = mouseTexture_back[4];
            }
            else
            {
                if (isonText)
                {
                    nowCursorImg = mouseTexture_back[5];
                }
                else
                {
                    nowCursorImg = mouseTexture_back[0];
                }
            }
        }


        if (!Ch2AniEvents.AniDone)
        {
            if ((int)Ch2GameManager.nowCH2State % 2 == 0)//正面
            {
                nowCursorImg = mouseHourglass_front[counter];
            }
            else
            {
                nowCursorImg = mouseHourglass_back[counter];
            }
        }
    }

    public void ChangeState(string cursorState)
    {

        if (Ch2AniEvents.AniDone)
        {
            switch (cursorState)
            {

                case "Front_Normal":
                    nowCursorImg = mouseTexture_front[0];
                    isonText = false;
                    break;
                case "Front_Nograb":
                    nowCursorImg = mouseTexture_front[1];
                    break;
                case "Front_Grab":
                    nowCursorImg = mouseTexture_front[2];
                    break;
                case "Front_Select":
                    nowCursorImg = mouseTexture_front[3];
                    break;
                case "Front_Play":
                    nowCursorImg = mouseTexture_front[4];
                    isonText = true;
                    break;
                case "Back_Normal":
                    nowCursorImg = mouseTexture_back[0];
                    isonText = false;
                    break;
                case "Back_Nograb":
                    nowCursorImg = mouseTexture_back[1];
                    break;
                case "Back_Grab":
                    nowCursorImg = mouseTexture_back[2];
                    break;
                case "Back_Select":
                    nowCursorImg = mouseTexture_back[3];
                    break;
                case "Back_Walk":
                    nowCursorImg = mouseTexture_back[4];
                    break;
                case "Back_Play":
                    nowCursorImg = mouseTexture_back[5];
                    isonText = true;
                    break;
                default:
                    break;
            }
        }
    }




    public void CursorHourglassAni()
    {
        if ((int)Ch2GameManager.nowCH2State % 2 != 0 && ch2visualManager.ShowBag)
        {
            ch2visualManager.Bag_Appear();
        }
        float timer = 1.0f / fps;
        InvokeRepeating("Increment", 0, timer);
        counter = 0;
    }

    public void StopCursorHourglassAni()
    {
        CancelInvoke("Increment");
        if ((int)Ch2GameManager.nowCH2State % 2 != 0)
        {
            ChangeState("Back_Normal");
        }
        else
        {
            ChangeState("Front_Normal");
        }
    }
    void Increment()
    {
        counter += 1;

        if (counter >= mouseHourglass_front.Length)
        {
            counter = 0;
        }
    }

}
