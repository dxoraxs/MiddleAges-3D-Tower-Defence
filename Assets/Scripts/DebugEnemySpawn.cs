using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform _prefabObject;
    public FindTargetPoint FindTargetTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            var prefab = Instantiate(_prefabObject);
            prefab.position = transform.position;
            prefab.GetComponent<EnemyMoving>().FindTargetTransform = FindTargetTransform;

        }
    }
}
