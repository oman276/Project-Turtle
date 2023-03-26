using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwavePoint : MonoBehaviour
{

    public Transform originPoint;
    public GameObject shockwave;

    public float initialDelay = 3f;
    public float timeBetweenCycles = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnShockwave", initialDelay, timeBetweenCycles);    
    }

    void SpawnShockwave() {
        Instantiate(shockwave, originPoint.position, Quaternion.identity);
    }
}
