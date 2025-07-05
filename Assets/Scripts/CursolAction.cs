using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursolAction : MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 target;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 10));
        this.transform.position = target;
    }
}
