using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public float speed = 2f;
    public GameObject player;
    public GameObject head;
    public GameObject rightarm;
    public GameObject leftarm;
    public GameObject leftleg;
    public GameObject rightleg;
    public GameObject tail;
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

    public void MapButtonOn() {
        isMapScaleButton = true;
    }

    public void MapButtonOff() {
        isMapScaleButton = false;
    }
}
