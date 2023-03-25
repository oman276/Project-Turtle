using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public float force = 4f;
    public float delay = 2.5f;

    public GameObject sprite;
    public float spinForce = 5f;
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.down * force);

        rotationSpeed = Random.Range(-60, 60f);
        if (rotationSpeed < 20f && rotationSpeed > -20f)
        {
            rotationSpeed = 20f;
        }
    }

    private void Update()
    {
        sprite.transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * 
            spinForce * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity /= 2;
        spinForce /= 2;
        rotationSpeed = Random.Range(-60, 60f);
        if (rotationSpeed < 20f && rotationSpeed > -20f)
        {
            rotationSpeed = 20f;
        }

        Invoke("Delete", delay);
    }

    void Delete() {
        Destroy(this.gameObject);
    }
}
