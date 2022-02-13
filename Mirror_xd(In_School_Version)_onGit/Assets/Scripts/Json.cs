using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Json : MonoBehaviour
{
    [Serializable]
    public class MyData
    {
        public playerState[] items;
    }
    [Serializable]
    public class playerState
    {
        public string name;
        public int level;
        public Vector3 pos;
    }
    public void save()
    {
        // Debug.Log("Write!");

        //填寫jplayerState格式的資料..
        // playerState myPlayer = new playerState();
        // myPlayer.name = "sammaru";
        // myPlayer.level = 87;
        // myPlayer.pos = GameObject./Find("Player").transform.position;

        playerState[] PlayerData = new playerState[2];
        for (int i = 0; i < PlayerData.Length; i++)
        {
            PlayerData[i] = new playerState();
            PlayerData[i].name = "Test" + (i + 1);
            PlayerData[i].level = 100;
            PlayerData[i].pos = GameObject.Find("Player").transform.position;
        }

        //將myPlayer轉換成json格式的字串
        // string saveString = JsonUtility.ToJson(myPlayer, true);

        string saveString = JsonHelper.ToJson(PlayerData, true);

        // Debug.Log(saveString);
        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, "myPlayer.json"));
        file.Write(saveString);
        file.Close();
    }
    public void load()
    {
        // Debug.Log("Load!");
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, "myPlayer.json"));
        string loadJson = file.ReadToEnd();
        file.Close();

        //新增一個物件類型為playerState的變數 loadData
        playerState[] loadData = new playerState[2];

        // MyData loadData = new MyData();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        // loadData = JsonUtility.FromJson<playerState>(loadJson);

        loadData = JsonHelper.FromJson<playerState>(loadJson);

        //驗證用，將Player的位置變更為json內紀錄的位置
        GameObject.Find("Player").transform.position = loadData[0].pos;
        // GameObject.Find("Player").transform.position = loadData.items[0].pos;

    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            save();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            load();
        }

    }


    // public PlayerInputActions controls;
    // void Awake()
    // {
    //     controls = new PlayerInputActions();
    //     controls.Player.JsonWrite.performed += cxt => { save(); };
    //     controls.Player.JsonRead.performed += cxt => { load(); };
    // }

    // private void OnEnable()
    // {
    //     controls.Enable();
    // }
    // private void OnDisable()
    // {
    //     controls.Disable();
    // }
}

//For Json Array
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

