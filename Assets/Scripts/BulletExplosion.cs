using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    private Rigidbody _bulletRigidbody;
    private Transform _targetBullet, _transform;
    [SerializeField] private float _speedBulletFalls;
    [SerializeField] private int _damage;

    public void ChangeBulletDamage(int multiply)
    {
        _damage *= multiply;
    }

    public void SetTartgetFromTower(Transform _target)
    {
        _targetBullet = _target;
        _transform = GetComponent<Transform>();
    }

    void Start()
    {
        Destroy(gameObject, 5f);
        _bulletRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_targetBullet != null)
        {
            transform.LookAt(_targetBullet);
            //if (_bulletRigidbody.velocity.y < 0)
                _transform.position = Vector3.MoveTowards(_transform.position, _targetBullet.position, _speedBulletFalls * Time.deltaTime);
            if (Vector3.Distance(_transform.position, _targetBullet.position) < 2f)
            {
                Debug.Log("Bullet Send Damage");
                _targetBullet.GetComponent<IEnemy>().GetDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Ground")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
