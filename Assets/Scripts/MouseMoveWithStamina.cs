using UnityEngine;

public class MouseMoveWithStamina : MonoBehaviour
{
    public float smoothSpeed = 5f;             // 補間の速さ（加速感）
    float maxMoveSpeed = 23f;           // 1秒あたりの最大移動速度
    public float edgeThreshold = 0.05f;
    public float mouseMoveThreshold = 0.1f;

    private Camera cam;
    private Vector3 targetPosition;
    private Vector3 lastMousePosition;

    void Start()
    {
        cam = Camera.main;
        targetPosition = transform.position;
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        Vector3 currentMousePos = Input.mousePosition;
        Vector3 mouseDelta = currentMousePos - lastMousePosition;
        lastMousePosition = currentMousePos;

        bool isMouseMoving = mouseDelta.sqrMagnitude > mouseMoveThreshold * mouseMoveThreshold;

        Vector3 mouseViewportPos = cam.ScreenToViewportPoint(currentMousePos);
        bool isMouseAtEdge =
            mouseViewportPos.x <= edgeThreshold || mouseViewportPos.x >= 1f - edgeThreshold ||
            mouseViewportPos.y <= edgeThreshold || mouseViewportPos.y >= 1f - edgeThreshold;

        if (isMouseMoving || isMouseAtEdge)
        {
            currentMousePos.z = Mathf.Abs(cam.transform.position.z);
            Vector3 worldMousePos = cam.ScreenToWorldPoint(currentMousePos);
            targetPosition = worldMousePos;
        }

        // 「カメラをターゲットに向けて最大速度で移動」
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            maxMoveSpeed * Time.deltaTime
        );
    }
}