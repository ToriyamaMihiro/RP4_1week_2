using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
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
        if (player.isDamege)
        {
            var impulseSource = GetComponent<CinemachineImpulseSource>();
            impulseSource.GenerateImpulse();
            player.isDamege = false;
        }
    }
}
