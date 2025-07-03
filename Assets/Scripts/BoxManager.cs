using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public static BoxManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RespawnBox(GameObject box, float delay)
    {
        StartCoroutine(RespawnCoroutine(box, delay));
    }

    private IEnumerator RespawnCoroutine(GameObject box, float delay)
    {
        yield return new WaitForSeconds(delay);
        box.SetActive(true);
    }
}
