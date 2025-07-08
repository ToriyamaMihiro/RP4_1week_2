using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject arrow;

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

        //”’l‚ª3•ª‚Ì1ˆÈ‰º‚È‚ç–îˆó•\¦
        if (player.lightCurrentPower <= player.lightMaxPower / 3)
        {
            arrow.gameObject.SetActive(true);
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }
    }
}
