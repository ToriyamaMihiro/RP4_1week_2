using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveWithStamina : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;                 // 通常時の移動速度
    public float exhaustedSpeed = 1f;            // スタミナ切れ時の移動速度
    private float currentSpeed;                  // 実際に使う移動速度

    [Header("スタミナ設定")]
    public float maxStamina = 100f;              // スタミナ最大値
    public float currentStamina;                 // 現在のスタミナ
    public float staminaPerUnit = 1f;            // 移動距離1あたりの消費量
    public float staminaRecoveryPerSecond = 10f; // 1秒あたりの回復量
    public float recoveryThreshold = 20f;        // 通常速度に戻るための必要スタミナ量

    private bool isExhausted = false;            // スタミナ切れ状態かどうか

    private Vector3 previousPosition;            // 前のフレームのプレイヤー位置
    private Vector3 previousMousePos;            // 前のフレームのマウス位置

    void Start()
    {
        currentStamina = maxStamina;
        previousPosition = transform.position;
        previousMousePos = Input.mousePosition;
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        // マウスが動いたかチェック（微小なブレは無視）
        Vector3 currentMousePos = Input.mousePosition;
        bool mouseMoved = (currentMousePos - previousMousePos).sqrMagnitude > 0.1f;
        previousMousePos = currentMousePos;

        // スタミナ切れ中の処理
        if (isExhausted)
        {
            // 回復処理だけ行う（移動は制限付きで行う）
            if (mouseMoved)
                MoveToMouse(); // ゆっくり動く

            // 回復してる間にスタミナが一定値を超えたら復帰
            currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRecoveryPerSecond * Time.deltaTime);

            if (currentStamina >= recoveryThreshold)
            {
                isExhausted = false;
                currentSpeed = moveSpeed; // 元の速度に戻す
            }

            previousPosition = transform.position;
            return;
        }

        // スタミナがあるときの通常処理
        if (mouseMoved)
        {
            MoveToMouse(); // 普通の速度で動く

            // 移動距離に応じてスタミナ消費
            float distanceMoved = Vector3.Distance(transform.position, previousPosition);
            float staminaToConsume = distanceMoved * staminaPerUnit;
            currentStamina = Mathf.Max(0f, currentStamina - staminaToConsume);
        }
        else
        {
            // マウスが動いてないなら回復
            currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRecoveryPerSecond * Time.deltaTime);
        }

        // スタミナ0になったらバテ状態へ
        if (currentStamina <= 0f)
        {
            isExhausted = true;
            currentSpeed = exhaustedSpeed;
        }

        previousPosition = transform.position;
    }

    // マウス位置へ向かって移動する処理
    void MoveToMouse()
    {
        // マウスのスクリーン座標をワールド座標に変換
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        // 方向ベクトルを計算
        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        // 実際に移動
        transform.position += direction * currentSpeed * Time.deltaTime;
    }
}
