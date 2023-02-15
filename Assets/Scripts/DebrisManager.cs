using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisManager : MonoBehaviour
{
    public GameObject[] debris;
    public float minVelocity = 0;
    public float maxVelocity = 0;
    public float minRotation = 0;
    public float maxRotation = 0;
    public int minItems = 0;
    public int maxItems = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (debris.Length == 0) {
            Debug.LogError("No debris given to Debris Manager Object");
            return;
        }

        int numOfItems = Random.Range(minItems, maxItems + 1);
        for (int i = 0; i < numOfItems; ++i) {
            GameObject d = Instantiate(debris[Random.Range(0, debris.Length)], this.transform.position, Quaternion.identity);
            Rigidbody2D rb = d.GetComponent<Rigidbody2D>();
            
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 rotateDirection = Random.insideUnitCircle.normalized;
            float magnitude = Random.Range(minVelocity, maxVelocity);
            float rotationForce = Random.Range(minRotation, maxRotation);

            rb.AddForce(direction * magnitude);
            rb.AddTorque(rotationForce);
        }

        Destroy(this.gameObject);
    }

}
