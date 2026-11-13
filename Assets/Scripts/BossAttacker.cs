using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _maxTime = 5f;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private LayerMask _towerLayer;
    [SerializeField] private float _damage = 30;
    private float _coldown;
   
    void Awake()
    {
        if( _firePoint == null)
        {
            Debug.LogError("точка стрельбы не найдена");
        }
    }

    private void Start()
    {
        _coldown = _maxTime;
    }


    void FixedUpdate()
    {
        if (_firePoint == null) return;

        if(_coldown > 0)
        {
            _coldown -= Time.fixedDeltaTime;
        }

        if(_coldown <= 0)
        {
            Ray ray = new Ray(_firePoint.position, _firePoint.forward); 

            Debug.DrawRay(ray.origin, ray.direction * _distance, Color.orangeRed);

            if(Physics.Raycast(ray, out RaycastHit hit, _distance, _towerLayer))
            {
                if(hit.collider.TryGetComponent<IDamageable>(out IDamageable tower))
                {
                    tower.TakeDamage(_damage);
                }
            }

            _coldown = _maxTime;
        }
    }
}
