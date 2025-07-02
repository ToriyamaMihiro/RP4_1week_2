using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeREnderer : MonoBehaviour
{
    public Transform pivot;     // �R�̎x�_
    public Transform weight;    // �R�̐�i�d��j
    public float ropeLength = 5f; // �R�̒���
    private LineRenderer lineRenderer;

    public HingeJoint2D hinge;
    public float extendSpeed = 0.5f; // 1�b��0.5�P�ʐL�т�

    private float targetAnchorY = 8f;

    void FixedUpdate()
    {
        Vector2 dir = weight.position - pivot.position;
        float dist = dir.magnitude;

        if (dist > ropeLength)
        {
            Vector2 correctedPos = (Vector2)pivot.position + dir.normalized * ropeLength;
            weight.position = correctedPos;
        }

       // targetAnchorY += extendSpeed * Time.deltaTime;

        Vector2 anchor = hinge.anchor;
        anchor.y = targetAnchorY;
        hinge.anchor = anchor;
    }
    }
