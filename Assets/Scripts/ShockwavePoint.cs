using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwavePoint : MonoBehaviour
{

    public Transform originPoint;
    public GameObject shockwave;
    public GameObject preShockwave;

    public float initialDelay = 3f;
    public float timeBetweenCycles = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnShockwave", initialDelay, timeBetweenCycles + 0.3f);    
    }

    void SpawnShockwave() {
        StartCoroutine(ShockRoutine());
    }

    IEnumerator ShockRoutine() {
        Instantiate(preShockwave, originPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(shockwave, originPoint.position, Quaternion.identity);
    }


}
