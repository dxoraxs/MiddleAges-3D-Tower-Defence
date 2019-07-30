using UnityEngine;

public class EnemyMoving : MonoBehaviour, IEnemy
{
    private Transform _thisTransform, _targetPoint;
    private int _numberPoint = 0, _healthEnemy;
    [SerializeField] private int _fullHealthEnemy;
    public FindTargetPoint FindTargetTransform;
    public float SpeedEnemy, SpeedRotationEnemy;
    private healthBarControlled _setHealthBarHealth;

    //public UnityEvent OnSetHealth;

    public int GetNumberPoint()
    {
        return _numberPoint;
    }

    void Start()
    {
        _thisTransform = GetComponent<Transform>();
        _setHealthBarHealth = GetComponent<healthBarControlled>();
        _targetPoint = FindTargetTransform.TargetEnemyPoint[_numberPoint];
        _healthEnemy = _fullHealthEnemy;
    }

    void Update()
    {
        //_canvasHealthBar.LookAt(Camera.main.transform);
        Vector3 direction = _targetPoint.position - _thisTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        _thisTransform.rotation = Quaternion.Lerp(_thisTransform.rotation, rotation, SpeedRotationEnemy * Time.deltaTime);

        if (Vector3.Distance(_thisTransform.position, _targetPoint.position) < 0.1f && _numberPoint < FindTargetTransform.TargetEnemyPoint.Count)
        {
            _numberPoint++;
            if (_numberPoint >= FindTargetTransform.TargetEnemyPoint.Count)
                Debug.Log("WAY ENEMY IS THE END");
            else
                _targetPoint = FindTargetTransform.TargetEnemyPoint[_numberPoint];
        }
        else
        {
            _thisTransform.position = Vector3.MoveTowards(_thisTransform.position, _targetPoint.position, SpeedEnemy * Time.deltaTime);
        }
    }

    public void GetDamage(int hit)
    {
        //Debug.Log("Enemy Get Damage\n"+ ((float)_healthEnemy / (float)_fullHealthEnemy));
        _healthEnemy -= hit;
        
        if (_healthEnemy <= 0)
        {
            Destroy(gameObject);
        }
        else _setHealthBarHealth.SetHealth(((float)_healthEnemy / _fullHealthEnemy));
        //OnSetHealth.Invoke();
    }
}
