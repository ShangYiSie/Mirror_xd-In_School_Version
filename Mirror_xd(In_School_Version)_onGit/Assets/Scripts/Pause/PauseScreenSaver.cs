using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreenSaver : MonoBehaviour
{
    public float velocityX;  // 速度多少
    public float velocityY;

    public GameObject PauseManager;

    [Header("主畫面Canvas")]
    public GameObject MainCanvas;
    public GameObject MainManager;

    [Header("螢幕保護程式")]
    public GameObject ScreenObj;

    bool startTrans = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CallScreenSaver()
    {
        // float[] ranVel = new float[2] { -velocity, velocity };
        // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(ranVel[Random.Range(0, 2)], ranVel[Random.Range(0, 2)]);
        startTrans = true;
        StartCoroutine(TransformLogo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            ToWakeUp();
        }

    }

    IEnumerator TransformLogo()
    {
        while (startTrans)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x + velocityX, gameObject.GetComponent<RectTransform>().localPosition.y + velocityY, 0);
            if(gameObject.GetComponent<RectTransform>().localPosition.x < -510f || gameObject.GetComponent<RectTransform>().localPosition.x > 510f)
            {
                // 到最左邊or最右邊
                gameObject.GetComponent<Image>().color = new Color32((byte)Random.Range(100f, 255f), (byte)Random.Range(100f, 255f), (byte)Random.Range(100f, 255f), 255);
                velocityX *= -1;
            }
            else if(gameObject.GetComponent<RectTransform>().localPosition.y < -427f || gameObject.GetComponent<RectTransform>().localPosition.y > 427f)
            {
                // 到最下面or最上面
                gameObject.GetComponent<Image>().color = new Color32((byte)Random.Range(100f, 255f), (byte)Random.Range(100f, 255f), (byte)Random.Range(100f, 255f), 255);
                velocityY *= -1;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (collision.name)
        {
            case "borderTop":
                velocityY *= -1;
                // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(prevX, prevY * -1);
                break;
            case "borderRight":
                velocityX *= -1;
                // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(prevX * -1, prevY);
                break;
            case "borderBottom":
                velocityY *= -1;
                // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(prevX, prevY * -1);
                break;
            case "borderLeft":
                velocityX *= -1;
                // gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(prevX * -1, prevY);
                break;
            default:
                break;
        }
    }

    public void ToWakeUp()
    {
        startTrans = false;
        PauseManager.GetComponent<PauseManager>().handleScreenSaver(false);
        PauseManager.GetComponent<PauseManager>().StartCountDown();
    }
}
