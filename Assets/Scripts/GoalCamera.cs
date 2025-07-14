using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCamera : MonoBehaviour
{
    public static GoalCamera Instance;

    [Header("Virtual Cameras")]
    public CinemachineVirtualCamera mainVCam;
    public CinemachineVirtualCamera goalVCam;

    [Header("演出設定")]
    public float focusDuration = 2f; // 演出時間

    private void Awake()
    {
        Instance = this;
    }

    // プレイヤー操作を止める場合
    //public void FocusOnGoal()
    //{
    //    StartCoroutine(FocusCoroutine(true));
    //}

    // ゲーム全体は止めずに演出だけする場合
    public void FocusOnGoal(Transform newGoal)
    {
        StartCoroutine(FocusCoroutine(newGoal, false));
    }

    private IEnumerator FocusCoroutine(Transform target, bool stopGameplay)
    {
        if (stopGameplay)
        {
            Time.timeScale = 0f;
        }

        goalVCam.Follow = target;
        //goalVCam.LookAt = target;

        goalVCam.Priority = 20;
        mainVCam.Priority = 10;

        float timer = 0;
        while (timer < focusDuration)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        goalVCam.Priority = 10;
        mainVCam.Priority = 20;

        if (stopGameplay)
        {
            Time.timeScale = 1f;
        }
    }
}
