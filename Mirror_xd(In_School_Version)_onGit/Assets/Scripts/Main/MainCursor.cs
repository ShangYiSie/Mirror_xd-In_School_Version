using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCursor : MonoBehaviour
{
    public Texture2D mouseTexture_Default;//設置鼠標的圖片

    public Texture2D mouseTexture_Select;//設置鼠標的圖片
    Texture2D nowCursorImg;



    void Start()
    {
        Cursor.visible = false;
        nowCursorImg = mouseTexture_Default;
    }
    //通過每幀實時渲染出鼠標的圖標
    private void OnGUI()
    {
        Vector3 mousePos = Input.mousePosition;
        Rect rect = new Rect(mousePos.x - 10, Screen.height - mousePos.y - 10, 54f, 55f);

        GUI.DrawTexture(rect, nowCursorImg);
    }

    public void ChangeState(string cursorState)
    {


        switch (cursorState)
        {

            case "Default":
                nowCursorImg = mouseTexture_Default;
                break;
            case "Select":
                nowCursorImg = mouseTexture_Select;
                break;

            default:
                break;
        }

    }





}

