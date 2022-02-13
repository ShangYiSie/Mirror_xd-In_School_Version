using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerTrigger : MonoBehaviour
{
    public GameObject MainManagement;
    MainManager mainManager;
    SceneManagement sceneManagement;
    AudioManager audioManager;

    MainCursor cursor;
    string[] modeContent = new string[] { "開始遊戲", "選擇關卡", "設定", "離開", "上一頁" };

    // ====================

    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        sceneManagement = GameObject.Find("SceneManager").GetComponent<SceneManagement>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cursor = GameObject.Find("MainManager").GetComponent<MainCursor>();
    }

    int DetermineIndex()
    {
        switch (this.name)
        {
            case "StartNew":
                return 0;
            case "StartOld":
                return 1;
            case "Setting":
                return 2;
            case "Leave":
                return 3;
            case "GoBack":
                return 4;
            default:
                return 5;
        }
    }

    public void OnPointerEnter()
    {
        // 打字動畫完成後才會出現
        if (mainManager.ReturnTypingStatus())
        {
            cursor.ChangeState("Select");
            // ===============================
            // 播放音效

            audioManager.MainPointerEnter();


            // ===============================
            if (!mainManager.ReturnSelectStatus() || this.name == "GoBack")
            {
                // 改文字
                this.transform.GetChild(0).GetComponent<Text>().text = modeContent[DetermineIndex()];
                // 改樣式
                gameObject.GetComponent<Button>().interactable = true;
                this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                this.transform.GetChild(0).GetComponent<Text>().color = new Color32(42, 42, 42, 255);
            }
            else
            {
                // 選關卡
                mainManager.ChangeWhenPointEnter(this.name);
            }
        }
    }

    public void OnPointerLeave()
    {
        // 打字動畫完成後才會能消失
        if (mainManager.ReturnTypingStatus())
        {
            cursor.ChangeState("Default");
            if (!mainManager.ReturnSelectStatus() || this.name == "GoBack")
            {
                this.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                this.transform.GetChild(0).GetComponent<Text>().color = new Color32(42, 42, 42, 0);
            }
            else
            {
                mainManager.ChangeWhenPointerExit(this.name);
            }

        }
    }

    public void OnSettingPointerEnter()
    {
        audioManager.MainPointerEnter();
        // 改樣式
        // this.GetComponent<Image>().color = new Color32(217, 217, 217, 255);
        this.transform.GetChild(0).GetComponent<Text>().color = new Color32(42, 42, 42, 255);
    }

    public void OnSettingPointerLeave()
    {
        // 改樣式
        // this.GetComponent<Image>().color = new Color32(42, 42, 42, 255);
        this.transform.GetChild(0).GetComponent<Text>().color = new Color32(217, 217, 217, 255);
    }

    // =======================================
    // 其他PointerEnter效果
    public void OnOtherPointerEnter()
    {
        cursor.ChangeState("Select");
        // ===============================
        // 播放音效
        audioManager.MainPointerEnter();
    }

    public void OnOtherPointerLeave()
    {
        cursor.ChangeState("Default");
    }

    public void OnClickBtn()
    {
        cursor.ChangeState("Default");
        switch (this.name)
        {
            case "StartNew":
                // 開始遊戲
                audioManager.MainButtonClick(false);
                mainManager.PlayLoadingAnim(1);
                // sceneManagement.LoadToOtherScene();
                break;
            case "StartOld":
                // 選擇關卡
                audioManager.MainButtonClick(true);
                mainManager.HandleSelectMode(true);
                break;
            case "Setting":
                // 選擇設定
                audioManager.MainButtonClick(false);
                mainManager.OpenSettingPage(true);
                break;
            case "Leave":
                // 結束遊戲
                audioManager.MainLeaveGame();
                sceneManagement.QuitGame();
                break;
            case "GoBack":
                // 上一頁
                audioManager.MainGoPrev();
                mainManager.HandleSelectMode(false);
                break;
            case "Level1":
                // 選擇第一關
                audioManager.MainButtonClick(false);
                mainManager.PlayLoadingAnim(1);
                break;
            case "Level2":
                // 選擇第二關
                audioManager.MainButtonClick(false);
                mainManager.PlayLoadingAnim(2);
                SceneManagement.isMainToCh2 = true;
                break;
            case "Level3":
                // 選擇第三關
                audioManager.MainButtonClick(false);
                // mainManager.PlayLoadingAnim(3);
                break;
            case "BackPrev":
                // 關閉設定
                audioManager.MainButtonClick(false);
                mainManager.OpenSettingPage(false);
                break;
            default:
                break;
        }
    }

}
