using UnityEngine;

public class CashMover : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _radius;
    private Transform _tower;
    private bool _movingToTower = true;
    
    void Awake()
    {
        TowerHealth tower = FindAnyObjectByType<TowerHealth>();

        if(tower != null)
        {
            _tower = tower.transform;
        }
    }

    
    void FixedUpdate()
    {
        if(_tower != null)
        {
            if (!_movingToTower) return;

            float distanceToTower = Vector3.Distance(transform.position, _tower.position);

            if (_movingToTower && distanceToTower <= _radius)
            {
                _movingToTower = false;
            }

            if (_movingToTower)
            {
                transform.position = Vector3.MoveTowards(transform.position, _tower.position, _speed * Time.fixedDeltaTime);
            }
        }

        else
        {
            return;
        }
    }
}
