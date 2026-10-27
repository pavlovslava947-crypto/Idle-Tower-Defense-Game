using UnityEngine;

public class TargetSearcher : MonoBehaviour
{
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject _particle;

    private readonly Collider[] _hitBuffer = new Collider[32]; // Предвыделенный буфер для WebGL

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

        _particle.SetActive(nearestEnemy != null);

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
}