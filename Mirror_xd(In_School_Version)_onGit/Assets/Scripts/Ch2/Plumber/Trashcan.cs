using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trashcan : MonoBehaviour
{
    Game3Manager game3Manager;
    Ch2DialogueManager ch2DialogueManager;
    Rigidbody2D TrashcanRigbody;
    AudioManager audioManager;

    [Header("水平速度")]
    public float SpeedX;
    [Header("垂直速度")]
    public float SpeedY;
    public GameObject Partical;

    public bool isTriggeredBricks = false;

    // Start is called before the first frame update
    void Start()
    {
        game3Manager = GameObject.Find("PlumberGame").GetComponent<Game3Manager>();
        ch2DialogueManager = GameObject.Find("DialogueManager").GetComponent<Ch2DialogueManager>();
        TrashcanRigbody = this.gameObject.GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LockSpeed()
    {
        Vector2 LockSpeed = new Vector2(resetSpeedX(),resetSpeedY());
        TrashcanRigbody.velocity = LockSpeed;
    }
    float resetSpeedX()
    {
        float currentSpeedX = TrashcanRigbody.velocity.x;
        if(currentSpeedX<0)
        {
            return -SpeedX;
        }
        else
        {
            return SpeedX;
        }
    }
    float resetSpeedY()
    {
        float currentSpeedY = TrashcanRigbody.velocity.y;
        if(currentSpeedY<0)
        {
            return -SpeedY;
        }
        else
        {
            return SpeedY;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name == "Wall4")
        {
            Destroy(this.gameObject);
        }
        else if(other.gameObject.name == "pb_Killer")
        {
            isTriggeredBricks = true;
            // 觸發音效
            audioManager.TrashcanTriggerBricks_Raccoon();
            // ==================================================================
            game3Manager.isCollisionKiller = true;
            SpeedX = 7.5f;
            SpeedY = 5f;
            TrashcanRigbody.velocity = new Vector2(SpeedX,SpeedY);
            TrashcanRigbody.sharedMaterial.bounciness = 1f;
            TrashcanRigbody.sharedMaterial.friction = 0f;
        }
        else if(other.gameObject.name == "pb_araiguma")
        {
            // 觸發音效
            audioManager.HitByTrashCan_Raccoon();
            // 觸發文本
            ch2DialogueManager.ShowNextDialogue("磚塊殺手反彈火球打到浣熊", true);
            // ==================================================================
            other.gameObject.transform.DOLocalJump(new Vector3(0.14f,2.7f,0),1f,1,0.5f,false);
            Instantiate(Partical,this.transform.position,this.transform.rotation);
            //Destroy(GameObject.FindGameObjectWithTag("Partical"),3f);
            GameObject [] Trashcan;
            Trashcan = GameObject.FindGameObjectsWithTag("Trashcan");
            game3Manager.AraigumaAni.SetBool("die",true);
            game3Manager.AraigumaAni.SetBool("throw",false);
            foreach (GameObject trashcan in Trashcan)
            {
                Destroy(trashcan);
            }
            game3Manager.StopCoroutine("AraigumaThrowTrashcan");
        }
        else if(other.gameObject.name == "pb_XiaoMing" || other.gameObject.name == "pb_Plumber")
        {
            if (Game3Manager.TrashKillerCharFirst)
            {
                // 觸發文本
                ch2DialogueManager.ShowNextDialogue("任何角色被垃圾桶砸死一次", true);
                Game3Manager.TrashKillerCharFirst = false;
            }
            Instantiate(Partical,this.transform.position,this.transform.rotation);
            Destroy(GameObject.FindGameObjectWithTag("Partical"),3f);
        }
        if(other.gameObject.name == "Wall1" || other.gameObject.name == "Wall2" || other.gameObject.name == "Wall3")
        {
            // 觸發音效
            audioManager.TrashcanTriggerWall_Raccoon();
        }
        if (isTriggeredBricks)
        {
            if(other.gameObject.name == "Floor1" || other.gameObject.name == "Floor2" || other.gameObject.name == "Floor3")
            {
                // 觸發音效
                audioManager.TrashcanTriggerWall_Raccoon();
            }
        }
        LockSpeed();
    }
}
