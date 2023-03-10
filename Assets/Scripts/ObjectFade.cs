using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFade : MonoBehaviour
{
    public void FadeOutText(float t, Text i)
    {
        StartCoroutine(FadeTextToZeroAlpha(t, i));
    }

    public void FadeInText(float t, Text i)
    {
        StartCoroutine(FadeTextToFullAlpha(t, i));
    }


    public void FadeOutImage(float t, Image i)
    {
        StartCoroutine(FadeImageToZeroAlpha(t, i));
    }

    public void FadeInImage(float t, Image i)
    {
        StartCoroutine(FadeImageToFullAlpha(t, i));
    }

    public void FadeInObject(float t, GameObject obj)
    {
        StartCoroutine(FadeObjectToFullAlpha(t, obj));
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


    IEnumerator FadeImageToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeImageToZeroAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeObjectToFullAlpha(float t, GameObject obj)
    {
        SpriteRenderer i = obj.GetComponent<SpriteRenderer>(); ;
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

}
