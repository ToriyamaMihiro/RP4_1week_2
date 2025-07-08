using UnityEngine;

public class MouseMoveWithStamina : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 20f;
    public float exhaustedSpeed = 1f;
    private float currentSpeed;

    private Vector3 targetPos;                   // 目標地点
    private Vector3 previousMousePos;

    [Header("感度設定")]
    public float mouseUpdateThreshold = 2f;      // 目標更新に必要なマウス移動距離(px)

    void Start()
    {
        currentSpeed = moveSpeed;
        previousMousePos = Input.mousePosition;
        targetPos = transform.position;
    }

    void Update()
    {
        Vector3 currentMousePos = Input.mousePosition;
        float mouseDelta = (currentMousePos - previousMousePos).sqrMagnitude;

        if (mouseDelta > mouseUpdateThreshold)
        {
            // マウスが一定以上動いたらターゲット更新
            targetPos = Camera.main.ScreenToWorldPoint(currentMousePos);
            targetPos.z = transform.position.z;
            previousMousePos = currentMousePos;
        }

        MoveToTarget();
    }

    void MoveToTarget()
    {
        Vector3 direction = (targetPos - transform.position);
        if (direction.sqrMagnitude > 0.01f) // 距離がごく小さいなら無視
        {
            transform.position += direction.normalized * currentSpeed * Time.deltaTime;
        }
    }
}