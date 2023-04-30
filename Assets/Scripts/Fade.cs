using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject fx;

    public void FadeNow(float duration) {
        StartCoroutine(FadeCoroutine(duration));
    }

    IEnumerator FadeCoroutine(float duration)
    {
        float scaleX = this.gameObject.transform.localScale.x;
        float scaleY = this.gameObject.transform.localScale.y;

        float rateX = (scaleX - 0.01f) / duration;
        float rateY = (scaleY - 0.01f) / duration;

        while (scaleX > 0.01f) {
            scaleX -= rateX * Time.deltaTime;
            scaleY -= rateY * Time.deltaTime;

            transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);
            yield return null;
        }

        GameObject tempFX = Instantiate(fx, this.transform.position,
                        this.transform.rotation);
        Destroy(tempFX, 0.5f);
        Destroy(this.gameObject);
    }
}
