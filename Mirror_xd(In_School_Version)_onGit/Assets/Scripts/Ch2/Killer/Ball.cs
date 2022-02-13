using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject nowTriggerTarget;
    public float speed;
    //bool isBroken;
    AudioManager audioManager;
    Game2Manager game2Manager;
    public GameObject Bricks2Partical;
    public GameObject BallPartical;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        game2Manager = GameObject.Find("BricksKillerGame").GetComponent<Game2Manager>();

        //isBroken = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        Destroy(gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        nowTriggerTarget = other.gameObject;
        switch (nowTriggerTarget.tag)
        {
            case "Bricks1":
                audioManager.MingBallBroken_BreakSafe();
                Instantiate(BallPartical, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
                break;
            case "Bricks2":
                if (nowTriggerTarget.GetComponent<SpriteRenderer>().sprite != game2Manager.Bricks2Broken)
                {
                    audioManager.GreenBrickBroken_BreakSafe();
                    nowTriggerTarget.GetComponent<SpriteRenderer>().sprite = game2Manager.Bricks2Broken;
                    Instantiate(BallPartical, this.transform.position, this.transform.rotation);
                    Destroy(this.gameObject);
                }
                else if (nowTriggerTarget.GetComponent<SpriteRenderer>().sprite == game2Manager.Bricks2Broken)
                {
                    audioManager.NormalBricksBroken_Break_Safe();
                    Instantiate(Bricks2Partical, this.transform.position, this.transform.rotation);
                    Instantiate(BallPartical, this.transform.position, this.transform.rotation);
                    Destroy(nowTriggerTarget.gameObject);
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}
