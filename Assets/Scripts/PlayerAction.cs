using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{

    public Slider slider;

    private HingeJoint2D hinge;
    [SerializeField] GameObject sphere;

    int lightMaxPower = 50;
    int lightCurrentPower;
    int lightAddPower = 40;
    int lightMainasuPower = 1;

    int finishTimer = 200;//終わるまでの時間
    int timer;//経過時間

    float lightTimer = 0f;//１秒ごとに光値を減らしたいのでそれ用のタイマー

    bool isLeave;//紐から外れたか falseでくっついてる

    void Start()
    {
        slider.value = 1;
        lightCurrentPower = lightMaxPower;
        sphere = GameObject.Find("Omori");
        hinge = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        Light();
        Finish();
    }

    void Light()
    {
        lightTimer += Time.deltaTime;
        // 1秒経過したら実行
        if (lightTimer >= 1f)
        {
            // 値を減らす
            lightCurrentPower -= lightMainasuPower;

            // ゲージ更新
            slider.value = (float)lightCurrentPower / (float)lightMaxPower;

            // タイマーリセット
            lightTimer = 0f;
        }
    }

    void Range()
    {
        //現在のポジションを保持する
        Vector3 currentPos = transform.position;

        //Mathf.ClampでX,Yの値それぞれが最小～最大の範囲内に収める。
        //物理挙動のあるisTriggerにしたいが、床は突き抜けてほしくないので無理やり範囲を決めて落ちないようにする
        currentPos.x = Mathf.Clamp(currentPos.x, -8.27f, 8.27f);
        currentPos.y = Mathf.Clamp(currentPos.y, -4.37f, 4.37f);

        //positionをcurrentPosにする
        transform.position = currentPos;

    }


    void Finish()
    {
        //lightPower--;
        //経過時間が終わる時間を過ぎたら
        if (lightCurrentPower <= 0)
        {
            //おもりが落ちる
            isLeave = true;
            hinge.connectedBody = null;
            sphere.GetComponent<RopeREnderer>().enabled = false;
            sphere.GetComponent<HingeJoint2D>().enabled = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Box")
        {
            //当たったら光が増える
            //lightPower += lightAddPower;
            GameObject box = collision.gameObject;
            //box.SetActive(false);

            //BoxManagerに頼んで再表示してもらう
            BoxManager.Instance.RespawnBox(box, 10f);
        }

        if (collision.collider.tag == "Goal" && isLeave)
        {
            Debug.Log("クリア！");
        }
    }
}
