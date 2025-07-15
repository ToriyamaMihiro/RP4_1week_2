using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int direction;
    [SerializeField] float returnPos = 25;
    [SerializeField] bool isChangeDire = false;
    [SerializeField] bool isSound=false;
    private Rigidbody2D rb;
    
    private AudioSource audioSource;
    public AudioClip sound;
    public GameObject particle;

    [SerializeField] GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Move()
    {

        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x > returnPos)
        {
            direction = -1;
        }
        else if (transform.position.x < -returnPos)
        {
            direction = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction player;
        GameObject obj = GameObject.FindWithTag("Player");
        player = obj.GetComponent<PlayerAction>();

        if (!player.isGoal)
        {
            Move();
        }

        if (isSound)
        {
            audioSource.PlayOneShot(sound);//音
            Instantiate(particle, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
            isSound = false;
        }
    }

    void Destroy()
    {

        Destroy(this.gameObject);

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Omori")
        {
            head.SetActive(true);
            isSound = true;
            Invoke("Destroy", 2f);
        }
    }
}
