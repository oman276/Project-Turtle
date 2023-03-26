using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Rocket : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explosionForce = 5f;

    AIDestinationSetter setter;

    private void Start()
    {
        setter = GetComponent<AIDestinationSetter>();
        GameObject player = GameObject.Find("Player Object");
        if (player)
        {
            setter.target = player.transform;
        }
        else
        {
            Debug.LogError("Rocket Error: No Player Object Found!");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        //Triggers to not explode when in contact with
        if (collision.gameObject.tag == "Debris" || collision.gameObject.tag == "Water"
            || collision.gameObject.tag == "Treadmill" || collision.gameObject.tag == "Bridge"
            || collision.gameObject.tag == "Portal") {
            return;
        }

        //Rocket Launcher: Destroy
        RocketLauncher rl = collision.gameObject.GetComponent<RocketLauncher>();
        if (rl)
        {
            if (collision.isTrigger)
            {
                return;
            }
            else {
                rl.Explode();
            }
        }

        //Enemy: Destroy
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec) {
            if (collision.isTrigger) {
                return;
            }
            ec.EnemyDefeated();
        }

        ExplosiveBarrel eb = collision.GetComponent<ExplosiveBarrel>();
        if (eb)
        {
            eb.Explode();
        }

        //Shatterable: Destroy
        Shatterable s = collision.gameObject.GetComponent<Shatterable>();
        if (s) {
            s.Shatter(collision.transform.position, this.gameObject);
        }

        //Rocket: Destroy
        Rocket r = collision.gameObject.GetComponent<Rocket>();
        if (r) {
            r.Explode();
        }


        if (rb) {
            ExplosionForce(rb);
        }

        Explode(); 
    }

    public void Explode() {
        GameObject tempFX = Instantiate(explosionPrefab, this.transform.position,
                this.transform.rotation);
        Destroy(tempFX, 1f);
        Destroy(this.gameObject);
    }

    void ExplosionForce(Rigidbody2D target)
    {
        Vector2 distance = target.position - new Vector2(this.transform.position.x, this.transform.position.y);
        //float distanceMultiplier = radius - distance.magnitude;
        target.AddForce(distance.normalized * explosionForce,
            ForceMode2D.Impulse);
    }

}
