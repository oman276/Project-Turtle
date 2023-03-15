using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject player;
    public Transform firepoint;
    public GameObject projectilePrefab;
    public float projectileForce = 3f;
    public float projectileDelay = 1.7f;
    public float projectileAppearAt = 0.7f;

    public GameObject explosionFX;

    GameManager gameManager;
    Rigidbody2D rb;
    public bool isFixed = true;

    bool isActive = false;

    public float rotateSpeed = 50f;

    public bool canRotate = true;

    public GameObject tempSpr;
    ObjectFade objectFade;
    float timer;
    bool fadeBegun = false;

    private void Start()
    {
        timer = Time.time;

        objectFade = FindObjectOfType<ObjectFade>();
        //tempSpr = Find

        //InvokeRepeating("FireProjectile", projectileDelay, projectileDelay);
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        if (isFixed) {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | 
                RigidbodyConstraints2D.FreezePosition;
        }

        player = GameObject.Find("Player Object");
        if (!player)
        {
            Debug.LogWarning("No Player Found by Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Fade in
        if (!fadeBegun && Time.time - timer >= projectileAppearAt) {
            fadeBegun = true;
            objectFade.FadeIn(projectileDelay - projectileAppearAt - 0.05f, tempSpr);
        }

        //Time Projectile Fire
        else if (Time.time - timer >= projectileDelay) {
            FireProjectile();
            SpriteRenderer i = tempSpr.GetComponent<SpriteRenderer>();
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
            timer = Time.time;
            fadeBegun = false;
        }

        //Rotate Player
        if (isActive && canRotate)
        {
            Vector3 targetPos = player.transform.position;
            targetPos.z = 0;
            Vector3 spritePos = this.transform.position;
            targetPos.x = targetPos.x - spritePos.x;
            targetPos.y = targetPos.y - spritePos.y;

            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            angle = angle + 90f;
            float step = rotateSpeed * Time.deltaTime;
            //this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,
                Quaternion.Euler(new Vector3(0, 0, angle)), step);

        }
    }

    void FireProjectile() {
        if (isActive) {
            GameObject currentProj = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
            //Vector3 direction = player.transform.position - this.transform.position;
            Vector3 direction = -this.transform.up;
            currentProj.GetComponent<Rigidbody2D>().velocity = projectileForce * direction.normalized;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ball") {
            EnemyDefeated();
        }
    }

    public void EnemyDefeated() {
        GameObject tempFX = Instantiate(explosionFX, this.transform.position,
                this.transform.rotation);
        Destroy(tempFX, 1f);
        gameManager.EnemyDefeated();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
