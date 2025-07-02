using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurikoLine : MonoBehaviour
{
    [Header("スプリングの目標角度（度）")]
    public float targetAngle = 0f;

    [Header("スプリングの強さ")]
    public float springStrength = 10f;

    [Header("減衰（ダンパー）")]
    public float damping = 2f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 現在の角度（Z軸回転、−180〜180）
        float currentAngle = rb.rotation;

        // 角度差を求めて（−180〜180）に補正
        float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);

        // スプリングトルク計算（差 × 強さ − 角速度 × 減衰）
        float torque = angleDiff * springStrength - rb.angularVelocity * damping;

        // トルクを加える
        rb.AddTorque(torque);
    }
}
