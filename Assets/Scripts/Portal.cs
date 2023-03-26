using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public List<GameObject> arriving;
    public GameObject otherPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Projectile"
            || collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Rocket"
            || collision.gameObject.tag == "Rolling Projectile" || collision.gameObject.tag == "Ball"
            || collision.gameObject.tag == "Debris") {
            if (!arriving.Contains(collision.gameObject))
            {
                otherPortal.GetComponent<Portal>().arriving.Add(collision.gameObject);
                collision.gameObject.transform.position = otherPortal.transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        arriving.Remove(collision.gameObject);
    }
}
