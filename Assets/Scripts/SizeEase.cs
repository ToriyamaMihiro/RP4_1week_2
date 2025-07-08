using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeEase : MonoBehaviour
{
    public Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(size, 0.5f).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
