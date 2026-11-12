using System.Collections;
using UnityEngine;

public class TargetSearcher : MonoBehaviour
{
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject _particle;
    [SerializeField] private ShootingAnim _shootingAnim;
    [SerializeField] private float _maxTime = 0.7f;
    [SerializeField] private float _fireRateSoundSpeed = 0.07f;
    [SerializeField] private UpgradeButtonUI _upgradeButton;
    [SerializeField] private CurrencyManager _currencyManager;
    private float _timer;
   

    private readonly Collider[] _hitBuffer = new Collider[32]; // Предвыделенный буфер для WebGL

    private void Start()
    {
        _timer = _maxTime;
    }

    private void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }
    public Transform GetNearestTarget(Vector3 origin)
    {
        int count = Physics.OverlapSphereNonAlloc(origin, attackRange, _hitBuffer, enemyLayer);

        Transform nearestEnemy = null;
        float minDistanceSqr = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            var enemyCollider = _hitBuffer[i];

            // Проверяем, жива ли цель (если есть IDamageable)
            if (enemyCollider.TryGetComponent<IDamageable>(out var damageable) && damageable.CurrentHealth <= 0)
                continue;

           
            float distSqr = (enemyCollider.transform.position - origin).sqrMagnitude;
            if (distSqr < minDistanceSqr)
            {
                minDistanceSqr = distSqr;
                nearestEnemy = enemyCollider.transform;
            }


           
        }

        _shootingAnim.StartShootAnimation(nearestEnemy);
        _particle.SetActive(nearestEnemy != null);

        if (nearestEnemy != null && _timer <= 0)
        {
            SoundManager.Instance.PlaySound(0);

            _timer = _maxTime;
        }

        return nearestEnemy;
    }


    public float AttackRange => attackRange;

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса атаки башне/стрелка в эдиторе
        Gizmos.color = new Color(1f, 0.92f, 0.016f, 0.25f);
        Gizmos.DrawSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void FireRateGain()
    {
        if (_maxTime <= 0.1) return;

        if (_currencyManager.Coins >= _upgradeButton.CurrentCost)
        {
            _maxTime -= _fireRateSoundSpeed;
        }
    }
}