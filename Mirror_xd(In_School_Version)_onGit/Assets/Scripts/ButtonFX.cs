using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "PropsHighlight")
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = this.GetComponentInParent<SpriteRenderer>().sortingLayerName;
        }
    }

    public void ButtonPointerDown()
    {
        this.GetComponent<Image>().DOFade(1, 0);
    }
    public void ButtonPointerUP()
    {
        this.GetComponent<Image>().DOFade(0, 0);
    }

    public void PropsPointerEnter()
    {
        if (this.GetComponentInParent<ItemEventManager>() == null)
        {
            this.GetComponent<SpriteRenderer>().DOFade(1, 0);

        }
        else if (!this.GetComponentInParent<ItemEventManager>().PlayerGet)
        {
            this.GetComponent<SpriteRenderer>().DOFade(1, 0);

        }
    }
    public void PropsPointerExit()
    {


        this.GetComponent<SpriteRenderer>().DOFade(0, 0);
    }

    public void TriggerotherFX()
    {
        this.GetComponent<SpriteRenderer>().DOFade(1, 0);
    }

    public void ExitotherFX()
    {
        this.GetComponent<SpriteRenderer>().DOFade(0, 0);
    }

    public void MirrorPointEnterFx()
    {
        if (AniEvents.AniDone && GameManager.stage >= 10)
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 0);
        }
    }
    public void MirrorPointExitFx()
    {
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0);
    }

}
