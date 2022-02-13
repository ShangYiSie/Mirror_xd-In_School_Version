using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneManagement : MonoBehaviour
{
    AsyncOperation async;

    public static bool isCh1ToCh2 = false;

    public static bool isMainToCh2 = false;

    private void Start()
    {
        // StartCoroutine(PreLoadScene1()); //輸出時要打開
    }
    public void LoadScene()
    {
        // SceneManager.LoadScene("Chapter1");

        // StartCoroutine(PreLoadScene1());
        async.allowSceneActivation = true;//啟用async 轉跳至預載好的場景
        // StopCoroutine(PreLoadScene1());
        GameManager.nowChapter = GameManager.Chapter.CH1;
        GameManager.nowCH1State = GameManager.CH1State.DeskTopFront;
        // async.allowSceneActivation = true;
    }

    public void LoadingAsync(int ch)
    {
        DOTween.Clear(true);
        if (ch == 1)
        {
            // Loading 1
            StartCoroutine(PreLoadScene1());
        }
        else if (ch == 2)
        {
            // Loading 2
            StartCoroutine(PreLoadScene2());
        }
        else if (ch == 3)
        {
            // loading 3
        }
    }

    public void StartToPlay(int ch)
    {
        async.allowSceneActivation = true;
        switch (ch)
        {
            case 1:
                StopCoroutine(PreLoadScene1());
                GameManager.startFromMain = true;
                GameManager.nowChapter = GameManager.Chapter.CH1;
                GameManager.nowCH1State = GameManager.CH1State.DeskTopFront;
                break;
            case 2:
                StopCoroutine(PreLoadScene2());
                Ch2GameManager.startFromMain = true;
                break;
            case 3:
                // StopCoroutine(PreLoadScene3());
                break;
        }
    }

    IEnumerator PreLoadScene1()
    {
        // yield return null;
        // yield return new WaitForSeconds(1f);

        async = SceneManager.LoadSceneAsync("Chapter1");//預先載入CH1
        async.allowSceneActivation = false;//將async 避免一載入就轉場景
        yield return null;
    }

    public IEnumerator PreLoadScene2()
    {
        // yield return null;
        // yield return new WaitForSeconds(1f);

        async = SceneManager.LoadSceneAsync("Chapter2");//預先載入CH2
        async.allowSceneActivation = false;//將async 避免一載入就轉場景
        yield return null;
    }
    public IEnumerator LoadToOtherScene()
    {
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;

    }
    public void ToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
