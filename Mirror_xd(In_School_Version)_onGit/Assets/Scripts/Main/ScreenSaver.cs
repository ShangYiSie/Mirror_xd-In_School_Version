using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSaver : MonoBehaviour
{
    public float velocity;  // 速度多少

    [Header("主畫面Canvas")]
    public GameObject MainCanvas;
    public GameObject MainManager;

    [Header("螢幕保護程式")]
    public GameObject ScreenObj;

    // Start is called before the first frame update
    void Start()
    {
        CallScreenSaver();
    }

    public void CallScreenSaver()
    {
        float[] ranVel = new float[2] { -velocity, velocity };
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(ranVel[Random.Range(0, 2)], ranVel[Random.Range(0, 2)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            ToWakeUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float prevX = gameObject.GetComponent<Rigidbody2D>().velocity.x;
        float prevY = gameObject.GetComponent<Rigidbody2D>().velocity.y;
        gameObject.GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(100f, 255f), (byte)Random.Range(100f, 255f), (byte)Random.Range(100f, 255f), 255);
        switch (collision.name)
        {
            case "borderTop":
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(prevX, prevY * -1);
                break;
            case "borderRight":
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(prevX * -1, prevY);
                break;
            case "borderBottom":
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(prevX, prevY * -1);
                break;
            case "borderLeft":
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(prevX * -1, prevY);
                break;
            default:
                break;
        }
    }

    public void ToWakeUp()
    {
        MainManager.GetComponent<MainManager>().timeRemain = 87f;
        ScreenObj.SetActive(false);
        MainCanvas.SetActive(true);
    }
}