using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.PlayerSettings;
using static UnityEngine.ParticleSystem;

public class GoalSporn : MonoBehaviour
{

    [SerializeField] public bool isBorn = false;
    [SerializeField] Vector2 bornPos = Vector2.zero;

    public GameObject goalObj;
    GameObject Goal;

    bool isSporn;
    bool isStart;//最初のゴールの呼び出し

    // Start is called before the first frame update
    void Start()
    {
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            bornPos.x = Random.Range(-20, 20);
            Goal = Instantiate(goalObj, new Vector3(bornPos.x, bornPos.y, 0.0f), Quaternion.identity);
            isStart = false;
        }
        //一回だけ呼び出すように
        if (isBorn && !isSporn)
        {
            GoalCamera.Instance.FocusOnGoal(Goal.transform); // カメラを切り替える処理
            isSporn = true;
            Invoke("Sporn", 2f);
        }

    }
    void Sporn()
    {
        bornPos.x = Random.Range(-20, 20);
        Goal = Instantiate(goalObj, new Vector3(bornPos.x, bornPos.y, 0.0f), Quaternion.identity);

        isBorn = false;
        isSporn = false;
    }
}

