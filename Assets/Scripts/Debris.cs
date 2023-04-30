using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public float duration = 0.5f;

    public float lowRange = 1f;
    public float highRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartFade", Random.Range(lowRange, highRange));
    }

    void StartFade() {
        this.gameObject.GetComponent<Fade>().FadeNow(duration);
    }

}
