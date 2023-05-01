using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float multiply = 1.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb) {
            float oldMag = rb.velocity.magnitude;
            Vector2 oldVelocity = rb.velocity;

            Vector2 thisPos = this.transform.position;
            Vector2 newVector = (collision.ClosestPoint(collision.gameObject.transform.position)
                - thisPos).normalized;

            rb.velocity = Vector2.zero;
            rb.velocity = newVector * multiply * oldMag;
        }
    }
}
