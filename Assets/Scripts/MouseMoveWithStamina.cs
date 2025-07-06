using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveWithStamina : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float moveSpeed = 5f;                 // �ʏ펞�̈ړ����x
    public float exhaustedSpeed = 1f;            // �X�^�~�i�؂ꎞ�̈ړ����x
    private float currentSpeed;                  // ���ۂɎg���ړ����x

    [Header("�X�^�~�i�ݒ�")]
    public float maxStamina = 100f;              // �X�^�~�i�ő�l
    public float currentStamina;                 // ���݂̃X�^�~�i
    public float staminaPerUnit = 1f;            // �ړ�����1������̏����
    public float staminaRecoveryPerSecond = 10f; // 1�b������̉񕜗�
    public float recoveryThreshold = 20f;        // �ʏ푬�x�ɖ߂邽�߂̕K�v�X�^�~�i��

    private bool isExhausted = false;            // �X�^�~�i�؂��Ԃ��ǂ���

    private Vector3 previousPosition;            // �O�̃t���[���̃v���C���[�ʒu
    private Vector3 previousMousePos;            // �O�̃t���[���̃}�E�X�ʒu

    void Start()
    {
        currentStamina = maxStamina;
        previousPosition = transform.position;
        previousMousePos = Input.mousePosition;
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        // �}�E�X�����������`�F�b�N�i�����ȃu���͖����j
        Vector3 currentMousePos = Input.mousePosition;
        bool mouseMoved = (currentMousePos - previousMousePos).sqrMagnitude > 0.1f;
        previousMousePos = currentMousePos;

        // �X�^�~�i�؂ꒆ�̏���
        if (isExhausted)
        {
            // �񕜏��������s���i�ړ��͐����t���ōs���j
            if (mouseMoved)
                MoveToMouse(); // ������蓮��

            // �񕜂��Ă�ԂɃX�^�~�i�����l�𒴂����畜�A
            currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRecoveryPerSecond * Time.deltaTime);

            if (currentStamina >= recoveryThreshold)
            {
                isExhausted = false;
                currentSpeed = moveSpeed; // ���̑��x�ɖ߂�
            }

            previousPosition = transform.position;
            return;
        }

        // �X�^�~�i������Ƃ��̒ʏ폈��
        if (mouseMoved)
        {
            MoveToMouse(); // ���ʂ̑��x�œ���

            // �ړ������ɉ����ăX�^�~�i����
            float distanceMoved = Vector3.Distance(transform.position, previousPosition);
            float staminaToConsume = distanceMoved * staminaPerUnit;
            currentStamina = Mathf.Max(0f, currentStamina - staminaToConsume);
        }
        else
        {
            // �}�E�X�������ĂȂ��Ȃ��
            currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRecoveryPerSecond * Time.deltaTime);
        }

        // �X�^�~�i0�ɂȂ�����o�e��Ԃ�
        if (currentStamina <= 0f)
        {
            isExhausted = true;
            currentSpeed = exhaustedSpeed;
        }

        previousPosition = transform.position;
    }

    // �}�E�X�ʒu�֌������Ĉړ����鏈��
    void MoveToMouse()
    {
        // �}�E�X�̃X�N���[�����W�����[���h���W�ɕϊ�
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        // �����x�N�g�����v�Z
        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        // ���ۂɈړ�
        transform.position += direction * currentSpeed * Time.deltaTime;
    }
}
