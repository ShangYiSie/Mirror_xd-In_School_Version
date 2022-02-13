using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class WarningFrameBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public Ch2ItemEventManager FrameItemEvents;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FrameItemEvents.isonUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FrameItemEvents.isonUI = false;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        FrameItemEvents.isonUI = false;
    }

}
