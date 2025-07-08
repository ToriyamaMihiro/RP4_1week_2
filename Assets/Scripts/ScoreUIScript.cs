using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIScript : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    float point;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        point = PlayerAction.playerScore;


        //表示
        scoreText.text = point.ToString("f0");

    }
}
