using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyHorizontal : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 0.5f;

    public Transform leftBound;
    public Transform rightBound;

    public float buffer = 0.1f;

    public bool movingRight;
    Vector3 targetVec;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        movingRight = true;
        transform.position = leftBound.position;
        targetVec = rightBound.position - transform.position;
        targetVec = targetVec.normalized;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingRight && Mathf.Abs(transform.position.x - rightBound.position.x) <= buffer
                && Mathf.Abs(transform.position.y - rightBound.position.y) <= buffer)
        {
            transform.position = rightBound.position;
            movingRight = false;
            targetVec = leftBound.position - transform.position;
            targetVec = targetVec.normalized;
        }
        else if (!movingRight && Mathf.Abs(transform.position.x - leftBound.position.x) <= buffer
            && Mathf.Abs(transform.position.y - leftBound.position.y) <= buffer)
        {
            movingRight = true;
            transform.position = leftBound.position;
            targetVec = rightBound.position - transform.position;
            targetVec = targetVec.normalized;

        }
        
        rb.MovePosition(transform.position + (targetVec * moveSpeed));
    }    
}

