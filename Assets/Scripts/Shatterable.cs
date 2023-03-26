using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatterable : MonoBehaviour
{
    public GameObject debrisObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Projectile" || 
            collision.gameObject.tag == "Shockwave" || collision.gameObject.tag == "Ball"
            || collision.gameObject.tag == "Rolling Projectile")
        {
            Shatter(collision.transform.position, collision.gameObject);
        }
    }

    public void Shatter(Vector3 spawnpoint, GameObject collidingObj = null) {
        Instantiate(debrisObj, spawnpoint, Quaternion.identity);
        if (collidingObj) { 
            if (collidingObj.tag == "Player")
            {
                Rigidbody2D playerRB = collidingObj.GetComponent<Rigidbody2D>();
                playerRB.velocity /= 2;
            }
            else if (collidingObj.tag == "Projectile")
            {
                Destroy(collidingObj);
            }
        }
        Destroy(this.gameObject);
    }
}
