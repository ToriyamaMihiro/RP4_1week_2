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
    [SerializeField] GameObject copy;

    [SerializeField]
    TextMeshProUGUI ComboCountText;

    private HingeJoint2D hinge;
    private SpriteRenderer SR;

    public int lightMaxPower = 10;//制限時間の最大
    public float lightCurrentPower;//今の時間
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
    public int life = 3;

    float lightTimer = 0f;//１秒ごとに光値を減らしたいのでそれ用のタイマー

    bool isLeave;//紐から外れたか falseでくっついてる
    bool isCombo;//現在コンボ中か
    public bool isClear;
    public bool isDeath;
    bool isCanSee = true;//
    public bool isOnemoreCheck;
    bool isRest;
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
        playerScore = 0;
    }

    void Update()
    {
        Light();
        Finish();
        Combo();
        ScoreUp();
        Onemore();
    }

    void Light()
    {
        if (Input.GetMouseButton(0))
        {
            lightTimer += 2;
        }
        else
        {
            lightTimer += 1;
        }
        // 1秒経過したら実行
        if (lightTimer >= 60f)
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
            comboMaxTime -= 25;
            isScoreUp = true;
        }
        if (comboNum % comboBounusNum == 1)
        {
            isScoreUp = false;
        }

        if (comboNum == 0)
        {
            scoreUp = 1000;
            comboMaxTime = 200;
        }
    }

    void Finish()
    {
        //lightPower--;
        //経過時間が終わる時間を過ぎたら
        if (lightCurrentPower <= 0 && !isLeave)
        {
            //おもりが落ちる
            Instantiate(copy, gameObject.transform.position, Quaternion.identity);
            isOnemoreCheck = true;
            // hinge.connectedBody = null;
            isCanSee = false;
            isLeave = true;
        }

        if (life <= 0)
        {
            isDeath = true;
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
            playerScore += scoreUp;
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

    void Onemore()
    {

        if (isOnemoreCheck && !isRest)
        {
            Invoke("Reset", 3f);
            isRest = true;
        }


        if (SR != null)
        {
            if (isCanSee)
            {
                SR.enabled = true;
            }
            else
            {
                SR.enabled = false;
            }
        }
    }

    void Reset()
    {
        isLeave = false;
        isCanSee = true;
        lightCurrentPower = lightMaxPower;
        isOnemoreCheck = false;
        isRest = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Box")
        {
            //当たったら光が増える
            //lightPower += lightAddPower;
            GameObject box = collision.gameObject;
            //box.SetActive(false);

            //BoxManagerに頼んで再表示してもらう
            BoxManager.Instance.RespawnBox(box, 15f);
        }

        if (collision.gameObject.tag == "Goal" && isLeave)
        {
            //isClear = true;
            //playerScore += 30000;
            //goalHeadObj.SetActive(true);
            //SR.enabled = false;
        }
    }
}
