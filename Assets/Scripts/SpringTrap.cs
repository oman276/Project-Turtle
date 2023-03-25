using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTrap : MonoBehaviour
{
    public GameObject arm;
    bool playerInRange = false;

    int state = 1;

    float timer;

    public float activationDelay = 1f;
    public float extendSpeed = 4f;
    public float retractSpeed = 0.5f;
    bool playerIn = false;

    //STATE MACHINE

    //State 1: completely retracted
    //  on player enter: transition to S2
    //State 2: triggered, about to extend
    //  on player exit: transition to S1
    //  by activationDelay: transition to S3
    //State 3: extending
    //  by arm in position: transition to S3
    //  on player exit: transition to S5
    //State 4: extended
    //  on player exit: transition to S5
    //State 5: retracting
    //  on player enter: transition to S3
    //  by arm in position: transition to S1

    private void Start()
    {
        arm.transform.localRotation = Quaternion.Euler(0, 0, 90);
        timer = Time.time;
    }

    private void Update()
    {
        if (state == 2)
        {
            if (Time.time - timer >= activationDelay)
            {
                state = 3;
            }
        }
        else if (state == 3)
        {
            if (arm.transform.localRotation != Quaternion.Euler(0, 0, 0))
            {
                float step = extendSpeed * Time.deltaTime;
                arm.transform.localRotation = Quaternion.RotateTowards(arm.transform.localRotation,
                    Quaternion.Euler(0, 0, 0), step);
            }
            else
            {
                arm.transform.localRotation = Quaternion.Euler(0, 0, 0);
                state = 4;
            }
        }
        else if (state == 4) {
            if (!playerIn) state = 5;
        }
        else if (state == 5)
        {
            if (arm.transform.localRotation != Quaternion.Euler(0, 0, 90))
            {
                float step = retractSpeed * Time.deltaTime;
                arm.transform.localRotation = Quaternion.RotateTowards(arm.transform.localRotation,
                    Quaternion.Euler(0, 0, 90), step);
            }
            else
            {
                arm.transform.localRotation = Quaternion.Euler(0, 0, 90);
                state = 1;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = true;
            if (state == 1)
            {
                timer = Time.time;
                state = 2;
            }
            else if (state == 5) {
                state = 3;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            playerIn = false;
            //state = 5;
            /*
            if (state == 2)
            {
                state = 1;
            }
            
            if (state == 3 || state == 4) {
                state = 5;
            }
            */
        }
    }
}
