using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup text;
    public bool fadingOut;
    private int count = 0;

    void Start()
    {
        fadingOut = true;
    }


    void Update()
    {
        if (text.alpha > 0 && fadingOut == true)
        {
            text.alpha -= 0.01f;
        }
        else if (text.alpha >= 0 && fadingOut == false)
        {
            text.alpha += 0.01f;
        }
        if (text.alpha == 0)
        {
            fadingOut = false;
        }
        else if (text.alpha == 1)
        {
            fadingOut = true;
            count += 1;
        }
        if (count >= 2)
        {
            delete();
        }
    }
        public void delete()
    {
        Destroy(gameObject);
    }
}
