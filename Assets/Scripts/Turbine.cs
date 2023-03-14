using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{
    public LayerMask initLayer;
    public LayerMask blockLayer;
    
    public BoxCollider2D triggerCollider;

    public float maxMag = 20f;

    AreaEffector2D ae;
    bool activeAE = false;

    GameObject player;

    SpriteRenderer sr;
    public Sprite onSpr;
    public Sprite offSpr;

    bool active = true;
    public bool willLoop = true;
    public float initDelay = 3f;
    public float loopDelay = 3f;

    public ParticleSystem effect;

    // Start is called before the first frame update
    void Start()
    {
        Transform dirPoint = transform.Find("Target Point");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (dirPoint.position - transform.position), 
            Mathf.Infinity, initLayer);
        if (hit) {
            triggerCollider.size = new Vector2(Vector2.Distance(transform.position, hit.point), 
                triggerCollider.size.y);
            triggerCollider.offset = new Vector3(triggerCollider.size.x / 2f, 0f);
        }
        else
        {
            Debug.LogError("No Collider found for this Turbine");
        }

        ae = GetComponent<AreaEffector2D>();
        ae.forceMagnitude = 0;

        player = GameObject.Find("Player Object");
        if (!player)
        {
            Debug.LogWarning("No Player Found by Turbine");
        }

        sr = GetComponentInChildren<SpriteRenderer>();
        effect.Play();

        if(willLoop) InvokeRepeating("changeOn", initDelay, loopDelay);
    }

    private void Update()
    {
        if (activeAE && active) {
            print(transform.right);
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, -transform.right,
                Mathf.Infinity, blockLayer);
            if (hit)
            {
                print(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Turbine")
                {
                    ae.forceMagnitude = maxMag;
                }
                else
                {
                    ae.forceMagnitude = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            activeAE = true;
            player.GetComponent<PlayerMovement>().onTreadmill++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activeAE = false;
            ae.forceMagnitude = 0;
            player.GetComponent<PlayerMovement>().onTreadmill--;
        }
    }

    void changeOn() {
        if (active)
        {
            effect.Stop();
            sr.sprite = offSpr;
            ae.forceMagnitude = 0;
            active = false;
        }
        else {
            effect.Play();
            sr.sprite = onSpr;
            active = true;
        }
    }
}
