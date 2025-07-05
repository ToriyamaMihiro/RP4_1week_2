using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private HingeJoint2D hinge;
    [SerializeField] GameObject sphere;

    int lightPower = 100;
    int lightAddPower = 40;

    int finishTimer = 200;//終わるまでの時間
    int timer;//経過時間

    bool isLeave;//紐から外れたか falseでくっついてる

    void Start()
    {

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

    }

    void Finish()
    {
        //lightPower--;
        //経過時間が終わる時間を過ぎたら
        if (lightPower <= 0)
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
