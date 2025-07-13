using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int direction;
    [SerializeField] float returnPos = 25;
    [SerializeField] bool isChangeDire = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    rb= GetComponent<Rigidbody2D>();    
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

        Move();
    }
}
