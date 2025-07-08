using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{

    public Slider slider;
    public Slider comboSlider;
    [SerializeField] public static float playerScore;
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject goalHeadObj;

    [SerializeField]
    TextMeshProUGUI ComboCountText;

    private HingeJoint2D hinge;
    private SpriteRenderer SR;

    public int lightMaxPower = 10;//制限時間の最大
    public int lightCurrentPower;//今の時間
    int lightAddPower = 40;
    int lightMainasuPower = 1;
    int comboMaxTime = 200;//コンボ受付時間
    int comboTime;
    int comboNum;
    int currentTime;
    int comboBounusNum = 5;

    int finishTimer = 100;//終わるまでの時間
    int timer;//経過時間
    int scoreUp = 1000;

    float lightTimer = 0f;//１秒ごとに光値を減らしたいのでそれ用のタイマー

    bool isLeave;//紐から外れたか falseでくっついてる
    bool isCombo;//現在コンボ中か
    public bool isClear;
    public bool isDeath;
    bool isScoreUp;


    [SerializeField] public static bool isExplosion;

    void Start()
    {
        slider.value = 1;
        currentTime = comboMaxTime;
        lightCurrentPower = lightMaxPower;
        sphere = GameObject.Find("Omori");
        hinge = GetComponent<HingeJoint2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Light();
        Finish();
        Combo();
        ScoreUp();
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

    //void Range()
    //{
    //    //現在のポジションを保持する
    //    Vector3 currentPos = transform.position;

    //    //Mathf.ClampでX,Yの値それぞれが最小～最大の範囲内に収める。
    //    //物理挙動のあるisTriggerにしたいが、床は突き抜けてほしくないので無理やり範囲を決めて落ちないようにする
    //    currentPos.x = Mathf.Clamp(currentPos.x, -8.27f, 8.27f);
    //    currentPos.y = Mathf.Clamp(currentPos.y, -4.37f, 4.37f);

    //    //positionをcurrentPosにする
    //    transform.position = currentPos;

    // }

    void ScoreUp()
    {
        if (comboNum % comboBounusNum == 0 && !isScoreUp)
        {
            scoreUp += 500;
            isScoreUp = true;
        }
        if (comboNum % comboBounusNum == 1)
        {
            isScoreUp = false;
        }

        if (comboNum == 0)
        {
            scoreUp = 1000;
        }
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
        if (isLeave)
        {
            if (gameObject.transform.position.y <= -22)
            {
                isDeath = true;
            }
        }
    }

    void Combo()
    {

        ComboCountText.SetText("{0}", comboNum);

        BoxAction box;
        GameObject obj = GameObject.FindWithTag("Box");
        box = obj.GetComponent<BoxAction>();


        if (isExplosion)
        {
            //コンボ回数を増やす
            comboNum++;
            isCombo = true;
            comboTime = comboMaxTime;
            currentTime = comboMaxTime;
            isExplosion = false;
        }
        if (isCombo)
        {
            comboTime--;
            currentTime = currentTime - 1;

            comboSlider.value = (float)currentTime / (float)comboMaxTime;

        }
        //もしコンボ受付時間が0になったらコンボをリセット
        if (comboTime <= 0)
        {
            comboNum = 0;
            isCombo = false;
            comboTime = comboMaxTime;
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
            BoxManager.Instance.RespawnBox(box, 15f);
        }

        if (collision.collider.tag == "Goal" && isLeave)
        {
            isClear = true;
            playerScore += 10000;
            goalHeadObj.SetActive(true);
            SR.enabled = false;
        }
    }
}
