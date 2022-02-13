using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch2ButtonFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerotherFX()
    {
        // this.GetComponent<SpriteRenderer>().sortingLayerName = this.GetComponentInParent<SpriteRenderer>().sortingLayerName;
        this.GetComponent<SpriteRenderer>().DOFade(1, 0);
    }

    public void ExitotherFX()
    {
        this.GetComponent<SpriteRenderer>().DOFade(0, 0);
    }

    public void PointEnterFx()
    {
        if (Ch2AniEvents.AniDone)
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 0);
        }
    }
    public void PointExitFx()
    {
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0);
    }
}
