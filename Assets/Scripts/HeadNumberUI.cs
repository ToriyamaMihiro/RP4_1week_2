using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadNumberUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    float headNum;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        headNum = PlayerAction.life;


        //表示
        scoreText.text = headNum.ToString("f0");

    }
}
