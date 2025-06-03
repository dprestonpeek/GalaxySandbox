using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    [SerializeField]
    Image imageToFade;

    [SerializeField]
    [Range(0, 1)]
    float fadeFrom = 1;

    [SerializeField]
    [Range(0, 1)]
    float fadeTo = 0;

    [SerializeField]
    [Range(0, 1)]
    float speed = .5f;

    bool fading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            Fade();
        }
    }

    public void StartFade()
    {
        fading = true;
    }

    public void FadeIn()
    {
        StartFade(1, 0, speed);
    }

    public void FadeOut()
    {
        StartFade(0, 1, speed);
    }

    public void StartFade(float fadeFrom, float fadeTo, float speed)
    {
        this.fadeFrom = fadeFrom;
        this.fadeTo = fadeTo;
        this.speed = speed;
        fading = true;
    }

    void Fade()
    {
        Color newColor = imageToFade.color;
        if (fadeTo > fadeFrom)
        {
            newColor.a += speed;
            if (newColor.a >= fadeTo)
            {
                newColor.a = fadeTo;
                fading = false;
            }
        }
        else if (fadeTo < fadeFrom)
        {
            newColor.a -= speed;
            if (newColor.a <= fadeTo)
            {
                newColor.a = fadeTo;
                fading = false;
            }
        }
        imageToFade.color = newColor;
    }
}
