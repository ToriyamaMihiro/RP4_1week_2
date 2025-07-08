using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = 60;
        //Screen.SetResolution(1280, 720, false);
    }

    // Update is called once per frame
    void Update()
    {
        int nowSceneIndexNumber = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // pushKey.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(++nowSceneIndexNumber);

            }
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

                SceneManager.LoadScene(nowSceneIndexNumber);
            }

            PlayerAction player;
            GameObject obj = GameObject.FindWithTag("Player");
            player = obj.GetComponent<PlayerAction>();

            if (player.isClear)
            {
                SceneManager.LoadScene(++nowSceneIndexNumber);
            }

            if (player.isDeath)
            {
                SceneManager.LoadScene(++nowSceneIndexNumber);
            }


        }
        if (SceneManager.GetActiveScene().name == "Result")
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
