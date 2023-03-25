using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    public float delay = 10f;
    public float initDelay = 10f;
    public Transform spawnpoint;

    private void Start()
    {
        InvokeRepeating("Spawn", initDelay, delay);
    }

    void Spawn() {
        Instantiate(projectile, spawnpoint.position, this.transform.rotation);
    }
}
   