using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAction : MonoBehaviour
{

    int timer = 180;


    bool isHit;
    bool isExplosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Explosion();
    }


    void Explosion()
    {
        if (isHit)
        {
            timer--;
            
            if (timer <= 0)
            {
                isExplosion = true;
                gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player")
        {
            isHit = true;
        }
    }
}
