using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector3 curPosition;
    Vector3 offset = new Vector3(0, 0, 0);
    private void OnMouseDrag()
    {
        if (Ch2AniEvents.AniDone)
        {
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            if (this.name == "pb_Killer")
            {
                curPosition.x = Mathf.Clamp(curPosition.x, -5f, 5f);
            }
            else if (this.name == "bk_Killer")
            {
                curPosition.x = Mathf.Clamp(curPosition.x, -3.8f, 1.0f);
            }
            transform.position = new Vector3(curPosition.x, transform.position.y, transform.position.z);
        }
    }
}
