using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool shakeEnabled = true;

    //Change if this ever changes for the player
    public float velocityLimit = 1.8f;
    public float distanceLimit = 3f;

    public float maxShake = 1f;
    public float maxTime = 2.5f;
    public float minTime = 0.5f;

    float duration = 0;
    GameObject camObj;

    //public float multiplier = 1f;
    Vector3 originalPos;

    private void Start()
    {
        camObj = transform.Find("Main Camera").gameObject;
        originalPos = camObj.transform.localPosition;
    }


    public void Shake(float intensity, float distance)
    {
        float velPerc = intensity / velocityLimit;
        if (velPerc > velocityLimit)
        {
            velPerc = velocityLimit;
        }

        float disPerc = (distanceLimit - distance) / distanceLimit;
        if (disPerc < 0)
        {
            return;
        }

        float power = velPerc * disPerc;
        duration += (maxTime * power);
        if (duration > maxTime)
        {
            duration = maxTime;
        }
        else if (duration <= minTime) {
            duration = minTime;
        }
    }

    public void Shake(Vector2 velocity, float distance)
    {
        Shake(velocity.magnitude, distance);
    }


    private void Update()
    {
        if (duration != 0) {
            if (duration > 0)
            {
                float curPower = duration / maxTime;
                Vector2 direction = Random.insideUnitCircle * curPower * Time.timeScale;
                camObj.transform.localPosition = new Vector3(direction.x, direction.y, originalPos.z);
                duration -= Time.deltaTime;
            }
            else {
                duration = 0;
                camObj.transform.localPosition = originalPos;
            }
        }
    }

    /*
    public IEnumerator ShakeCurve(float duration, float velocity)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;
        float midpoint = duration / 2;

        duration *= velocity;
        while (elapsed < duration && Time.timeScale != 0)
        {
            float timeStrength = midpoint - Mathf.Abs(midpoint - elapsed);

            float x = Random.Range(-1, 1) * velocity * timeStrength * multiplier;
            if (x > shakeCap)
            {
                x = shakeCap;
            }
            else if (x < -shakeCap)
            {
                x = -shakeCap;
            }

            float y = Random.Range(-1, 1) * velocity * timeStrength * multiplier;
            if (y > shakeCap)
            {
                y = shakeCap;
            }
            else if (y < -shakeCap)
            {
                y = -shakeCap;
            }

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public IEnumerator ShakeFlat(float duration, float velocity)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;
        float midpoint = duration / 2;

        duration *= velocity;
        while (elapsed < duration && Time.timeScale != 0)
        {
            float timeStrength = duration - elapsed;

            float x = Random.Range(-1, 1) * velocity * timeStrength * multiplier;
            float y = Random.Range(-1, 1) * velocity * timeStrength * multiplier;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public void Shake(float duration, float velocity, bool isFlat = true)
    {
        if (shakeEnabled)
        {
            if (isFlat)
            {
                StartCoroutine(ShakeFlat(duration, velocity));
            }
            else
            {
                StartCoroutine(ShakeCurve(duration, velocity));
            }
        }
    }
    */


}
