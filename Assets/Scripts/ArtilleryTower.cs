using UnityEngine;

public class ArtilleryTower : MonoBehaviour
{ 
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _transformSpawnBullet;
    private Transform _target, _transform;
    [SerializeField]private float _radiusTower, _timeToShotStandart;
    private float _timeToShot, _timeToRotationStandart = 1f;
    [SerializeField] public LayerMask _maskScanEnemy;
    private Quaternion _standartRotationTower;
    private Animation _animation;
    private int _levelTower=1;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _animation = GetComponent<Animation>();
        _standartRotationTower = _transform.rotation;
    }

    public void ChangeTowerLevel()
    {
        _levelTower++;
        _transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public string[] ReturnNameLevel()
    {
        string[] array = new string[2];
        array[0] = "Artillery";
        array[1] = _levelTower + " level";
        return array;
    }

    void FixedUpdate()
    {
        if (_timeToShot >= 0) _timeToShot -= Time.fixedDeltaTime;
        if (_target != null)
        {
            Vector3 direction = _target.position + _target.forward * 5f - _transform.position - _target.up * 10f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, 5 * Time.deltaTime);
            if (Vector3.Distance(_target.position, _transform.position) > _radiusTower)
            {
                _target = null;
                _timeToRotationStandart = 1f;
                return;
            }
            if (_timeToShot < 0)
            {
                _timeToShot = _timeToShotStandart;
                ShootBullet();
            }
        }
        if (_target == null)
        {
            SelectedTarget();
            if (_timeToRotationStandart < 0)
            _transform.rotation = Quaternion.Lerp(_transform.rotation, _standartRotationTower, 3 * Time.deltaTime);
            else _timeToRotationStandart -= Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (_transform != null) Gizmos.DrawWireSphere(_transform.position, _radiusTower);
        if (_target != null) Gizmos.DrawWireSphere(_target.position, 1f);
    }

    private void SelectedTarget()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _radiusTower, _maskScanEnemy);
        if (_colliders.Length > 0)
        {
            float _minDistance = float.MaxValue;
            foreach (Collider collider in _colliders)
            {
                Transform colliderTransform = collider.GetComponent<Transform>().parent;
                if (Vector3.Distance(colliderTransform.position, _transform.position) < _minDistance)
                {
                    _target = colliderTransform;
                    _minDistance = Vector3.Distance(_target.position, _transform.position);
                }
            }
        }
    }

    private void ShootBullet()
    {
        var _bulletClone = Instantiate(_bulletPrefab);
        _bulletClone.transform.position = _transformSpawnBullet.position;
        Vector3 rotationShot = -_transform.position + targetShot();// _target.forward * 10;
        Vector3 strengthShot = new Vector3(0, (Vector3.Distance(_target.position, _transform.position) / _radiusTower) * 15, 0);
        Vector3 directionFlyBullet = rotationShot  + strengthShot;
        _bulletClone.GetComponent<Rigidbody>().AddForce(directionFlyBullet, ForceMode.VelocityChange);
        _bulletClone.GetComponent<BulletExplosion>().SetTartgetFromTower(_target);
        _bulletClone.GetComponent<BulletExplosion>().ChangeBulletDamage(_levelTower);
        _animation.Play();
    }

    private Vector3 targetShot()
    {
        Vector3 rotationShot = new Vector3();
        EnemyMoving enemyScript = _target.GetComponent<EnemyMoving>();
        if (Vector3.Distance(enemyScript.FindTargetTransform.TargetEnemyPoint[enemyScript.GetNumberPoint()].position, _target.position) < 10
            && enemyScript.GetNumberPoint() + 1 < enemyScript.FindTargetTransform.TargetEnemyPoint.Count)
        {
            Vector3 vecotrNextPoint=
                enemyScript.FindTargetTransform.TargetEnemyPoint[enemyScript.GetNumberPoint() + 1].position
                - enemyScript.FindTargetTransform.TargetEnemyPoint[enemyScript.GetNumberPoint()].position;
            rotationShot = enemyScript.FindTargetTransform.TargetEnemyPoint[enemyScript.GetNumberPoint()].position+ vecotrNextPoint.normalized*(10 - Vector3.Distance(enemyScript.FindTargetTransform.TargetEnemyPoint[enemyScript.GetNumberPoint()].position, _target.position));
        }
        else
        {
            rotationShot = _target.position + _target.forward * 10;
        }
        return rotationShot;
    }
}