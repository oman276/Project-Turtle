using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 targetVec;

    public Transform extendPoint;
    public Transform retractPoint;

    float moveSpeed;
    public float retractSpeed = 0.3f;
    public float extendSpeed = 2;

    public float buffer = 0.05f;

    bool isExtending = true;
    public bool beginsExtended = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (beginsExtended)
        {
            transform.position = extendPoint.position;
            isExtending = false;
            targetVec = retractPoint.position - transform.position;
            targetVec = targetVec.normalized;
            moveSpeed = retractSpeed;
        }
        else {
            transform.position = retractPoint.position;
            isExtending = true;
            targetVec = extendPoint.position - transform.position;
            targetVec = targetVec.normalized;
            moveSpeed = extendSpeed;
        }
    }

    private void FixedUpdate()
    {
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
    }
}
