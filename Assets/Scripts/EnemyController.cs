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

    public GameObject explosionFX;

    GameManager gameManager;
    Rigidbody2D rb;
    public bool isFixed = true;

    private void Start()
    {
        InvokeRepeating("FireProjectile", projectileDelay, projectileDelay);
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        if (isFixed) {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | 
                RigidbodyConstraints2D.FreezePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.z = 0;
        Vector3 spritePos = this.transform.position;
        targetPos.x = targetPos.x - spritePos.x;
        targetPos.y = targetPos.y - spritePos.y;

        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
    }

    void FireProjectile() {
        GameObject currentProj = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
        Vector3 direction = player.transform.position - this.transform.position;
        currentProj.GetComponent<Rigidbody2D>().velocity = projectileForce * direction.normalized;
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
}
