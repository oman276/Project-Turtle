using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float startSize = 0.17f;
    public float endSize = 0.7f;

    public float cycleLength = 2f;

    float speed;
    float size;

    SpriteRenderer sr;

    public bool expand = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = Mathf.Abs(endSize - startSize) / cycleLength;
        size = startSize;

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (expand)
        {
            if (size <= endSize)
            {
                size += speed * Time.deltaTime;
                transform.localScale = new Vector3(size, size, size);
                sr.color = new Color(1.0f, 1.0f, 1.0f, 1 - (size / endSize));
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else {
            if (size >= endSize)
            {
                
                size -= speed * Time.deltaTime;
                transform.localScale = new Vector3(size, size, size);
                sr.color = new Color(1.0f, 1.0f, 1.0f, (endSize / size) -0.3f);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
