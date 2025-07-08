using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartEffect : MonoBehaviour
{
    public Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(1, 1, 1), 1f).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
