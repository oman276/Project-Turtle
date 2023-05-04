using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFade : MonoBehaviour
{

    PlayerMovement pm;
    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    public void FadeOut(float t, Text i)
    {
        StartCoroutine(FadeTextToZeroAlpha(t, i));
    }

    public void FadeIn(float t, Text i)
    {
        StartCoroutine(FadeTextToFullAlpha(t, i));
    }


    public void FadeOut(float t, Image i, bool isPlayer = false)
    {
        StartCoroutine(FadeImageToZeroAlpha(t, i, isPlayer));
    }

    public void FadeIn(float t, Image i, bool isPlayer = false)
    {
        StartCoroutine(FadeImageToFullAlpha(t, i, isPlayer));
    }

    public void FadeIn(float t, GameObject obj)
    {
        StartCoroutine(FadeObjectToFullAlpha(t, obj));
    }

    public void FadeOut(float t, GameObject obj)
    {
        StartCoroutine(FadeObjectToZeroAlpha(t, obj));
    }

    IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }


    IEnumerator FadeImageToFullAlpha(float t, Image i, bool isPlayer)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeImageToZeroAlpha(float t, Image i, bool isPlayer)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i && i.color.a > 0.0f)
        {
            if (isPlayer) {
                if (pm.fadeState != 4) {
                    i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
                    break;
                }
            }
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        if (pm.fadeState == 4) {
            pm.fadeState = 0;
        }
    }

    IEnumerator FadeObjectToFullAlpha(float t, GameObject obj)
    {
        SpriteRenderer i = obj.GetComponent<SpriteRenderer>(); 
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i && i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeObjectToZeroAlpha(float t, GameObject obj)
    {
        SpriteRenderer i = obj.GetComponent<SpriteRenderer>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
