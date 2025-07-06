using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private HingeJoint2D hinge;
    [SerializeField] GameObject sphere;

    int lightPower = 100;
    int lightAddPower = 40;

    int finishTimer = 200;//�I���܂ł̎���
    int timer;//�o�ߎ���

    bool isLeave;//�R����O�ꂽ�� false�ł������Ă�

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

    void Range()
    {
        //���݂̃|�W�V������ێ�����
        Vector3 currentPos = transform.position;

        //Mathf.Clamp��X,Y�̒l���ꂼ�ꂪ�ŏ��`�ő�͈͓̔��Ɏ��߂�B
        //���������̂���isTrigger�ɂ��������A���͓˂������Ăق����Ȃ��̂Ŗ������͈͂����߂ė����Ȃ��悤�ɂ���
        currentPos.x = Mathf.Clamp(currentPos.x, -8.27f, 8.27f);
        currentPos.y = Mathf.Clamp(currentPos.y, -4.37f, 4.37f);

        //position��currentPos�ɂ���
        transform.position = currentPos;

    }


    void Finish()
    {
        //lightPower--;
        //�o�ߎ��Ԃ��I��鎞�Ԃ��߂�����
        if (lightPower <= 0)
        {
            //�����肪������
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
            //�������������������
            //lightPower += lightAddPower;
            GameObject box = collision.gameObject;
            //box.SetActive(false);

            //BoxManager�ɗ���ōĕ\�����Ă��炤
            BoxManager.Instance.RespawnBox(box, 10f);
        }

        if (collision.collider.tag == "Goal" && isLeave)
        {
            Debug.Log("�N���A�I");
        }
    }
}
