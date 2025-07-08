using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BoxAction : MonoBehaviour
{

    public int timer = 180;
    public int maxTime = 180;
    public float scorePoint = 1000;
    public Sprite hitSprite;
    public Sprite normalSprite;
    bool isHit;
    bool isExplosion;

    private BoxParticle particleScript;
    private SpriteRenderer mainSpriteRenderer;
    public GameObject bomParticle;

    // Start is called before the first frame update
    void Start()
    {
        mainSpriteRenderer = GetComponent<SpriteRenderer>();
        particleScript = GetComponent<BoxParticle>();
        timer = maxTime;
        
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
            mainSpriteRenderer.sprite = hitSprite;
            particleScript.enabled = true;

            if (timer <= 0)
            {
                isExplosion = true;
                isHit = false;
            }
        }

        if (isExplosion) {
            timer = maxTime;
            mainSpriteRenderer.sprite = normalSprite;
            particleScript.enabled = false;
            Instantiate(bomParticle, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);//爆発パーティクル
            //スコアアップ
            PlayerAction.playerScore += scorePoint;
            isExplosion = false;
            gameObject.SetActive(false);
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
