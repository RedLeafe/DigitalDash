using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    [SerializeField] private CanvasGroup text;
    public bool fadingOut;
    public bool clicked;

    void Start()
    {
        fadingOut = true;
        clicked = false;
    }

    void Update()
    {
        if (text.alpha > 0 && fadingOut == true)
        {
            text.alpha -= 0.002f;
        }
        else if (text.alpha >= 0 && fadingOut == false)
        {
            text.alpha += 0.002f;
        }
        if (text.alpha == 0)
        {
            fadingOut = false;
        }
        else if (text.alpha == 1)
        {
            fadingOut = true;
        }
        if (MainMenu.Clicked == true)
        {
            delete();
        }
    }

    public void delete()
    {
        Destroy(gameObject);
    }
}
