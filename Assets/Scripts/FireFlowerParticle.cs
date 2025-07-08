using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerParticle : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound);//音
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
