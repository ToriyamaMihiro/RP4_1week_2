using UnityEngine;

public class MouseMoveWithStamina : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float moveSpeed = 20f;
    public float exhaustedSpeed = 1f;
    private float currentSpeed;

    private Vector3 targetPos;                   // �ڕW�n�_
    private Vector3 previousMousePos;

    [Header("���x�ݒ�")]
    public float mouseUpdateThreshold = 2f;      // �ڕW�X�V�ɕK�v�ȃ}�E�X�ړ�����(px)

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
            // �}�E�X�����ȏ㓮������^�[�Q�b�g�X�V
            targetPos = Camera.main.ScreenToWorldPoint(currentMousePos);
            targetPos.z = transform.position.z;
            previousMousePos = currentMousePos;
        }

        MoveToTarget();
    }

    void MoveToTarget()
    {
        Vector3 direction = (targetPos - transform.position);
        if (direction.sqrMagnitude > 0.01f) // �����������������Ȃ疳��
        {
            transform.position += direction.normalized * currentSpeed * Time.deltaTime;
        }
    }
}