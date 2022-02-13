using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogState : Selectable
{
    // Update is called once per frame
    void Update()
    {
        if (IsHighlighted() == true)
        {
            //Output that the GameObject was highlighted, or do something else
            //Debug.Log(this.name + "ishighlight");
        }

    }
}
