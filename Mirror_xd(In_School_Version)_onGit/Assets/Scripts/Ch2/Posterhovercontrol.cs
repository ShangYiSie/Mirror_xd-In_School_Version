using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posterhovercontrol : MonoBehaviour
{
    public GameObject Poster1hover;
    public GameObject Poster2hover;
    public GameObject Poster3hover;

    public void hidePoster()
    {
        switch (this.name)
        {
            case "Game1Btn":
                Poster1hover.SetActive(false);
                break;
            case "Game2Btn":
                if (Ch2GameManager.nowstage >= 2) Poster2hover.SetActive(false);
                break;
            case "Game3Btn":
                if (Ch2GameManager.nowstage == 3) Poster3hover.SetActive(false);
                break;
        }
    }
}
