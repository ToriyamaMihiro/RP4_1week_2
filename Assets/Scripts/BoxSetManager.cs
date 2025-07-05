using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BombInfo
{
    public Vector3 position;
    public int delay;

}

public class BoxSetManager : MonoBehaviour
{
    public List<BombInfo> bombList;

    public GameObject bombPrefab;

    void Start()
    {
        foreach (var info in bombList)
        {
            PlaceBomb(info.position, info.delay);
        }
    }

    void PlaceBomb(Vector2 position, int delay)
    {
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bomb.GetComponent<BoxAction>().timer = delay;
    }
}
