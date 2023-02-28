using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    //public GameObject[] staticObjectArray;
    //public GameObject[] dynamicObjectArray;

    public List<GameObject> staticObjectArray;
    public List<GameObject> dynamicObjectArray;

    CircleCollider2D cc;
    float radius;
    public float powerMultiplier = 30f;

    public GameObject explosionEffect;

    bool destroyed = false;

    private void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        radius = cc.radius;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Projectile") {
            Explode();
        }
    }

    public void Explode() {
        if (destroyed) {
            return;
        }
        destroyed = true;

        //staticObjectArray.ForEach(ExplodeLoop);
        //dynamicObjectArray.ForEach(ExplodeLoop);
        foreach (GameObject g in staticObjectArray.ToArray()) {
            ExplodeLoop(g);
        }
        foreach (GameObject g in dynamicObjectArray.ToArray())
        {
            ExplodeLoop(g);
        }
        GameObject tempFX = Instantiate(explosionEffect, this.transform.position,
            this.transform.rotation);
        Destroy(tempFX, 1f);
        Destroy(this.gameObject);
    }

    public void ExplodeLoop(GameObject obj) {
        
        
        //If object no longer exists, return
        if (!obj) {
            return;
        }

        //if is enemy: destroy
        EnemyController ec = obj.GetComponent<EnemyController>();
        if (ec) {
            ec.EnemyDefeated();
            return;
        }

        //If is other explosive barrel: detonate
        ExplosiveBarrel eb = obj.GetComponent<ExplosiveBarrel>();
        if (eb) {
            eb.Explode();
            return;
        }

        //If is shatterable wall: break
        Shatterable s = obj.GetComponent<Shatterable>();
        if (s) {
            s.Shatter(obj.transform.position);
            return;
        }

        //Otherwise, apply RB force
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb) {
            ExplosionForce(rb);
        }
    }

    void ExplosionForce(Rigidbody2D target) {
        Vector2 distance = target.position - new Vector2(this.transform.position.x, this.transform.position.y);
        float distanceMultiplier = radius - distance.magnitude;
        target.AddForce(distance.normalized * distanceMultiplier * powerMultiplier,
            ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D enteredRB = collision.gameObject.GetComponent<Rigidbody2D>();

        if (enteredRB) {
            dynamicObjectArray.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D enteredRB = collision.gameObject.GetComponent<Rigidbody2D>();

        if (enteredRB)
        {
            dynamicObjectArray.Remove(collision.gameObject);
        }
    }

}
