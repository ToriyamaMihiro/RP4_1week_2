using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private HingeJoint2D hinge;
    [SerializeField] GameObject sphere;

    void Start()
    {
        sphere = GameObject.Find("Omori");
        hinge = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hinge.connectedBody = null;
            sphere.GetComponent<RopeREnderer>().enabled = false;
            sphere.GetComponent<HingeJoint2D>().enabled = false;
        }
    }
}
