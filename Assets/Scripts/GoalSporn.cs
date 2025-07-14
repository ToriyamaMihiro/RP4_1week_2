using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.PlayerSettings;
using static UnityEngine.ParticleSystem;

public class GoalSporn : MonoBehaviour
{

    [SerializeField] public bool isBorn = false;
    [SerializeField] Vector2 bornPos= Vector2.zero;

    public GameObject goalObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBorn)
        {
            bornPos.x = Random.Range(-20, 20);
            Instantiate(goalObj, new Vector3(bornPos.x,bornPos.y, 0.0f), Quaternion.identity);
            isBorn = false;

        }
    }
}
