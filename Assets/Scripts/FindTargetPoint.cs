using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetPoint : MonoBehaviour
{
    public List<Transform> TargetEnemyPoint;

    void Awake()
    {
        foreach(Transform child in transform)
        {
            TargetEnemyPoint.Add(child);
        }
    }

}
