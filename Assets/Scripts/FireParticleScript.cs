using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticleScript : MonoBehaviour
{
    [SerializeField] float timer = 0;
    [SerializeField] Vector2 randomPos = Vector2.zero;
    [SerializeField] float bornTime = 1;
    [SerializeField] bool isBorn = false;
    [SerializeField] Vector2 posXrange = Vector2.zero;
    [SerializeField] Vector2 posYrange = Vector2.zero;
    [SerializeField] Vector3 pos= Vector3.zero;//プレイヤーの位置

    public GameObject particlePosObj;//どのオブジェにつくか
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > bornTime && !isBorn)
        {
            randomPos.x = Random.Range(posXrange.x, posXrange.y);
            randomPos.y = Random.Range(posYrange.x, posYrange.y);
            isBorn = true;
        }

        if (isBorn)
        {
            //PlayerAction playerscript; //呼ぶスクリプトにあだなつける
            //GameObject obj = GameObject.Find("Omori"); //Playerっていうオブジェクトを探す
            //playerscript = obj.GetComponent<PlayerAction>(); //付いているスクリプトを取得
            pos = particlePosObj.transform.position;

            Instantiate(particle, new Vector3(pos.x+randomPos.x, pos.y+randomPos.y, 0.0f), Quaternion.identity);
            timer = 0;
            isBorn = false;

        }


        PlayerAction playerScript;
        GameObject obj = GameObject.FindWithTag("Player");
        playerScript = obj.GetComponent<PlayerAction>();
        if (playerScript.lightCurrentPower < 30)
        {
            bornTime = 0.2f;
        }
        
    }
}
