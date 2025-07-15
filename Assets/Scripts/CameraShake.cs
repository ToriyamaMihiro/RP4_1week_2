using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private AudioSource ad;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        ad= GetComponent<AudioSource>();
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
            ad.PlayOneShot(sound);//音
            player.isDamege = false;
        }
    }
}
