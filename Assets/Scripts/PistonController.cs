using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 targetVec;

    public Transform extendPoint;
    public Transform retractPoint;
    Transform targetPoint;

    float moveSpeed;
    public float retractSpeed = 0.3f;
    public float extendSpeed = 2;

    public float buffer = 0.05f;

    bool isExtending = true;
    public bool beginsExtended = true;

    //public float time;
    //public float lastTime;

    float dist;
    float lastDist;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (beginsExtended)
        {
            transform.position = extendPoint.position;
            isExtending = false;
            moveSpeed = retractSpeed;
            targetPoint = retractPoint;
            dist = (transform.position - targetPoint.position).magnitude;
            lastDist = dist;
            //time = 1;
            //lastTime = 1;
        }
        else {
            transform.position = retractPoint.position;
            isExtending = true;
            moveSpeed = extendSpeed;
            targetPoint = retractPoint;
            dist = (transform.position - targetPoint.position).magnitude;
            lastDist = dist;
            //time = 0;
            //lastTime = 0;
        }
    }

    private void FixedUpdate()
    {
        dist = (transform.position - targetPoint.position).magnitude;

        if (lastDist < dist) { //Swap
            if (isExtending)
            {

            }
            else { 
            
            }
        }

        if (isExtending) //Is Extending
        {

        }
        else { //Is Not Extending
        
        }

        lastDist = dist;

        /*
        time = Mathf.PingPong(Time.time * moveSpeed, 1);
        if (beginsExtended) time = 1 - time;

        if (isExtending && lastTime > time) { //Start Retracting
            print(1);

            isExtending = false;
            moveSpeed = retractSpeed;
            time = Mathf.PingPong(Time.time * moveSpeed, 1);
            //if (beginsExtended) time = 1 - time;
        }
        else if (!isExtending && lastTime < time) //Start Extending
        {
            print(2);

            isExtending = true;
            moveSpeed = extendSpeed;
            time = Mathf.PingPong(Time.time * moveSpeed, 1);
            //if (beginsExtended) time = 1 - time;
        }
        
        Vector3 newPosition = Vector3.Lerp(retractPoint.position, extendPoint.position, time);
        rb.MovePosition(newPosition);

        lastTime = time;
        */
        /*
        //Extending -> Not Extending
        if (isExtending && Mathf.Abs(transform.position.x - extendPoint.position.x) <= buffer
        && Mathf.Abs(transform.position.y - extendPoint.position.y) <= buffer)
        {
            transform.position = extendPoint.position;
            isExtending = false;
            targetVec = retractPoint.position - transform.position;
            targetVec = targetVec.normalized;
            moveSpeed = retractSpeed;
        }

        //Not Extending -> Extending
        else if (!isExtending && Mathf.Abs(transform.position.x - retractPoint.position.x) <= buffer
            && Mathf.Abs(transform.position.y - retractPoint.position.y) <= buffer)
        {
            transform.position = retractPoint.position;
            isExtending = true;
            targetVec = extendPoint.position - transform.position;
            targetVec = targetVec.normalized;
            moveSpeed = extendSpeed;
        }
        
        rb.MovePosition(transform.position + (targetVec * moveSpeed));
        */
    }
}
