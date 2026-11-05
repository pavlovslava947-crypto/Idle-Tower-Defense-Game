using UnityEngine;

public class CashMover : MonoBehaviour
{
    private Transform _tower;
    private Transform _cashPoint;
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _radius;
    private bool _isMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var tower = FindAnyObjectByType<TowerHealth>();
        _tower = tower.transform;
        var cashPoint = GameObject.FindWithTag("CashPoint");
        _cashPoint = cashPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(_tower != null)
        {
            if (Vector3.Distance(transform.position, _tower.position) >= 6f)
            {
                Vector3.MoveTowards(transform.position, _cashPoint.position, _speed * Time.deltaTime);
            }
            if (!_isMove) return;
            transform.position = Vector3.MoveTowards(transform.position, _tower.position, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _tower.position) <= _radius)
            {
                _isMove = false;
            }

            
        }
    }
}
