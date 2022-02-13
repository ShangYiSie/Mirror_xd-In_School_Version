using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class CMailTextReset : MonoBehaviour, IDeselectHandler //This Interface is required to receive OnDeselect callbacks.
{
    public void OnDeselect(BaseEventData data)
    {
        this.gameObject.GetComponentInChildren<Text>().color = new Color32(42, 42, 42, 255);
    }
}