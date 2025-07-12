using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        PlayerAction player;
        GameObject obj = GameObject.FindWithTag("Player");
        player = obj.GetComponent<PlayerAction>();
        if (gameObject.transform.position.y <= -22 )
        {
            player.life--;
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            PlayerAction player;
            GameObject obj = GameObject.FindWithTag("Player");
            player = obj.GetComponent<PlayerAction>();
            player.isOnemoreCheck = true;

            Destroy(this.gameObject);

        }
    }
}
