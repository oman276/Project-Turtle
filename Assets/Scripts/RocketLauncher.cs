using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RocketLauncher : MonoBehaviour
{
    public GameObject launcher;
    public GameObject rocket;
    public Transform firepoint;
    GameObject player;

    public float initialDelay = 3f;
    public float fireDelay = 3f;

    //public float lowBound = 0f;
    //public float highBound = 180f;

    GameManager gameManager;

    public GameObject explosionFX;

    //public float initOffset = 0f;
    //public float postOffset = 0f;

    public char position = 'N';

    bool isActive = false;

    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating("FireRocket", fireDelay, fireDelay);

        player = GameObject.Find("Player Object");
        if (!player) {
            Debug.LogError("NO PLAYER DETECTED");
        }

        if (position != 'N' && position != 'n' && position != 'E' &&
            position != 'e' && position != 'S' && position != 's' &&
            position != 'W' && position != 'w') {
            Debug.LogError("Rocket Launcher Error: Invalid position");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Vector3 targetPos = player.transform.position;
            targetPos.z = 0;
            Vector3 spritePos = this.transform.position;
            targetPos.x = targetPos.x - spritePos.x;
            targetPos.y = targetPos.y - spritePos.y;

            angle = (Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg);

            
            if (angle < 0) {
                angle += 360;
            }
            
            //Case N
            if(position == 'N' || position == 'n')
            {
                if (angle > 180 && angle <= 270) angle = 180;
                else if (angle > 270) angle = 0;
            }
            //Case E
            else if (position == 'E' || position == 'e')
            {

            }
            //Case S
            else if (position == 'S' || position == 's')
            {

            }
            //Case W
            else if (position == 'W' || position == 'w')
            {

            }


            launcher.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void FireRocket() {
        if (isActive) {
            Instantiate(rocket, firepoint.position, Quaternion.identity);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ball")
        {
            Explode();
        }
    }

    public void Explode()
    {
        GameObject tempFX = Instantiate(explosionFX, this.transform.position,
                this.transform.rotation);
        Destroy(tempFX, 1f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isActive = false;
        }
    }
}

