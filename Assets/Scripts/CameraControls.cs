using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public float speed = 2f;
    public GameObject player;
    public GameObject cameraObject;
    public float shakeCap = 2f;
    public float multiplier = 1f;

    public float zoomSpeed = 1f;
    public float zoomLowLimit = 3f;
    public float zoomUpLimit = 12f;

    public float dragZoomInFactor = 1.5f;
    public float speedZoomInFactor = 0.5f;
    float size = 7f;
    float sizeTarget = 7f;
    float sizeModified = 7f;
    float sizeBase = 7f;
    Camera cam;

    public bool shakeEnabled = true;


    //Map View Setup
    bool isMapScaleKey = false;
    public bool isMapScaleButton = false;
    bool isMapScaleStart = true;
    public float mapScale;
    public Transform mapTransform;
    float scaleZoomSpeed = 5f;

    PlayerMovement pm;
    Rigidbody2D rb;

    private void Start()
    {
        cam = cameraObject.GetComponent<Camera>();
        pm = FindObjectOfType<PlayerMovement>();
        rb = pm.gameObject.GetComponent<Rigidbody2D>();
        Invoke("startEnable", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapScaleKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.M)) {
            isMapScaleKey = false;
        }
        
        //Zoom in/out
        float scrollVal = Input.GetAxis("Mouse ScrollWheel");
        sizeBase -= scrollVal;
            
        if (sizeBase > zoomUpLimit)
        {
            sizeBase = zoomUpLimit;
        }        
        else if(sizeBase < zoomLowLimit)
        {
            sizeBase = zoomLowLimit;
        }

        //Modification Based on Dragging
        if (pm.dragging)
        {
            float dragPercent = pm.dirMag / pm.maxDistance;
            sizeModified = sizeBase - dragPercent * dragZoomInFactor;
        }
        //Modification Based on Speed
        else {
            float speedPercent = rb.velocity.magnitude / 18f;
            sizeModified = sizeBase + speedPercent * speedZoomInFactor;
        }

        if (!isMapScaleKey && !isMapScaleStart && !isMapScaleButton)
        {
            sizeTarget = Mathf.Lerp(sizeTarget, sizeModified, 0.025f);
        }
        else sizeTarget = mapScale;

        size = Mathf.Lerp(size, sizeTarget, zoomSpeed);

        //Map Scale
        if (isMapScaleKey || isMapScaleStart || isMapScaleButton) 
        {
            this.transform.position = mapTransform.position;
            //cam.orthographicSize = mapScale;
            //Vector3.Lerp(this.transform.position, mapTransform.position, speed * Time.deltaTime);
            cam.orthographicSize = size;    
        }
        //Normal camera Movement
        else {
            this.transform.position =
                Vector3.Lerp(this.transform.position, new Vector3(player.transform.position.x,
                player.transform.position.y, -10f), speed * Time.deltaTime);
            cam.orthographicSize = size;
        }
            
    }

    void startEnable() {
        isMapScaleStart = false;
    }

    public IEnumerator ShakeCurve(float duration, float velocity)
    {
        Vector3 originalPos = cameraObject.transform.localPosition;

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

            cameraObject.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        cameraObject.transform.localPosition = originalPos;
    }

    public IEnumerator ShakeFlat(float duration, float velocity)
    {
        Vector3 originalPos = cameraObject.transform.localPosition;

        float elapsed = 0;
        float midpoint = duration / 2;

        duration *= velocity;
        while (elapsed < duration && Time.timeScale != 0)
        {
            float timeStrength = duration - elapsed;

            float x = Random.Range(-1, 1) * velocity * timeStrength * multiplier;
            float y = Random.Range(-1, 1) * velocity * timeStrength * multiplier;

            cameraObject.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        cameraObject.transform.localPosition = originalPos;
    }

    public void Shake(float duration, float velocity, bool isFlat = true) {
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

    public void MapButtonOn() {
        isMapScaleButton = true;
    }

    public void MapButtonOff() {
        isMapScaleButton = false;
    }
}
