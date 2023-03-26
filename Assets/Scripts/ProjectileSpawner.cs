using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public float delay = 10f;
    public float initDelay = 10f;
    public Transform spawnpoint;

    public bool delayedDeath = false;
    public float newDelay = 10f;

    private void Start()
    {
        InvokeRepeating("Spawn", initDelay, delay);
    }

    void Spawn() {
        GameObject proj = Instantiate(projectile, spawnpoint.position, this.transform.rotation);
        if (delayedDeath) {
            RollingProjectile rp = proj.GetComponent<RollingProjectile>();
            rp.delay = 10f;
            rp.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}
   