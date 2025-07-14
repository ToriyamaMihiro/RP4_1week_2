using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicatorController : MonoBehaviour
{
    [Header("プレイヤーのみ指定すればOK")]
    public Transform player;
    public float edgeBuffer = 0.05f;

    private Transform goal;
    private Camera cam;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        cam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ゴールが見つからなければ探す
        if (goal == null)
        {
            GameObject found = GameObject.FindWithTag("Goal");
            if (found != null)
                goal = found.transform;
        }

        if (goal == null || player == null) return;

        Vector3 goalViewportPos = cam.WorldToViewportPoint(goal.position);
        bool isOnScreen = goalViewportPos.x >= 0 && goalViewportPos.x <= 1 &&
                          goalViewportPos.y >= 0 && goalViewportPos.y <= 1 &&
                          goalViewportPos.z >= 0;

        spriteRenderer.enabled = !isOnScreen;

        if (!isOnScreen)
        {
            UpdateArrowPositionAndRotation();
        }
    }

    void UpdateArrowPositionAndRotation()
    {
        Vector3 dir = (goal.position - player.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 playerViewport = cam.WorldToViewportPoint(player.position);
        Vector3 arrowViewport = playerViewport + dir * 0.5f;

        arrowViewport.x = Mathf.Clamp(arrowViewport.x, edgeBuffer, 1f - edgeBuffer);
        arrowViewport.y = Mathf.Clamp(arrowViewport.y, edgeBuffer, 1f - edgeBuffer);
        arrowViewport.z = cam.nearClipPlane + 1f;

        Vector3 worldPos = cam.ViewportToWorldPoint(arrowViewport);
        transform.position = worldPos;
    }
}