using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Landmineitems : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "U")
        {
            this.transform.DOLocalMoveY(-8f, 2f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }
    }
}
