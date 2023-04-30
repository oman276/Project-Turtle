using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameManager gm;
    public GameObject collectFX;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject tempFX = Instantiate(collectFX, this.transform.position,
                        this.transform.rotation);
            Destroy(tempFX, 0.5f);
            gm.CoinGet();
            Destroy(this.gameObject);
        }
    }

}
