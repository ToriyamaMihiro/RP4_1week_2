using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxParticle : MonoBehaviour
{
    [SerializeField] float timer = 0;
    [SerializeField] Vector2 randomPos = Vector2.zero;
    [SerializeField] float bornTime = 1;
    [SerializeField] bool isBorn = false;
    [SerializeField] Vector2 posXrange = Vector2.zero;
    [SerializeField] Vector2 posYrange = Vector2.zero;
    [SerializeField] Vector3 pos = Vector3.zero;//プレイヤーの位置

   
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
           
            Instantiate(particle, new Vector3(transform.position.x + randomPos.x, transform.position.y + randomPos.y, 0.0f), Quaternion.identity);
            timer = 0;
            isBorn = false;

        }
    }
}
