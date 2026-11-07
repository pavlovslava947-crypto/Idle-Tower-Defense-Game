using UnityEngine;

public class CashMover : MonoBehaviour
{
    private Transform _tower;
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _radius;
    private bool _movingToTower = true;
    
    void Start()
    {
        var tower = FindAnyObjectByType<TowerHealth>();
        _tower = tower.transform;
    }

    
    void FixedUpdate()
    {
        if(!_movingToTower) return;

        float distanceToTower = Vector3.Distance(transform.position, _tower.position);

        if(_movingToTower && distanceToTower <= _radius)
        {
            _movingToTower = false;
        }

        if (_movingToTower)
        {
            transform.position = Vector3.MoveTowards(transform.position, _tower.position, _speed * Time.fixedDeltaTime);
        }
    }
}
