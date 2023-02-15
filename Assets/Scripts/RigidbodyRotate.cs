using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyRotate : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1f;
    //Vector3 eulerRot;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //eulerRot = new Vector3(0, 0, 10) * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Quaternion deltaRotation = Quaternion.Euler(eulerRot * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation + speed * Time.deltaTime);

    }
}
