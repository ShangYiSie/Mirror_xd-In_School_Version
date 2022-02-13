using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimImage : MonoBehaviour
{
    // public GameObject image;
    public int fps;
    public List<Sprite> sprites;


    int counter = 0;
    int index;


    void Start()
    {
        // if (image == null) image = GetComponent<Image>();



        float timer = 1.0f / fps;
        InvokeRepeating("Increment", 0, timer);

        // index = gameObject.GetComponent<Renderer>().materials.Length - 1;
    }
    void Increment()
    {
        counter += 1;

        if (counter >= sprites.Count)
        {
            counter = 0;
        }
    }
    void Update()
    {
        if (sprites.Count == 0) return;
        gameObject.GetComponent<Image>().sprite = sprites[counter];
        // gameObject.GetComponent<SpriteRenderer>().sprite = sprites[counter];
    }
}
