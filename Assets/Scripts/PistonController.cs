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

   // public float buffer = 0.05f;

    bool isExtending;
    public bool beginsExtended = true;

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
            targetVec = (targetPoint.position - transform.position).normalized;
            lastDist = dist + 5f;
        }
        else {
            transform.position = retractPoint.position;
            isExtending = true;
            moveSpeed = extendSpeed;
            targetPoint = extendPoint;
            dist = (transform.position - targetPoint.position).magnitude;
            targetVec = (targetPoint.position - transform.position).normalized;
            lastDist = dist + 5f;
        }

        rb.MovePosition(transform.position + (targetVec * moveSpeed));
    }

    private void FixedUpdate()
    {
        dist = (transform.position - targetPoint.position).magnitude;

        if (lastDist < dist || targetVec.normalized == new Vector3(0, 0, 0)) { //Swap
            if (isExtending)
            {
                isExtending = false;
                moveSpeed = retractSpeed;
                targetPoint = retractPoint;
                dist = (transform.position - targetPoint.position).magnitude;
                targetVec = (targetPoint.position - transform.position).normalized;
            }
            else {
                isExtending = true;
                moveSpeed = extendSpeed;
                targetPoint = extendPoint;
                dist = (transform.position - targetPoint.position).magnitude;
                targetVec = (targetPoint.position - transform.position).normalized;
            }
        }
        
        lastDist = dist;
        rb.MovePosition(transform.position + (targetVec * moveSpeed));
    }
}
