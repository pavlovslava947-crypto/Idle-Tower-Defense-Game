using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Движение")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float stoppingDistance = 2.5f; // Увеличьте, если башня широкая!

    [Header("Атака")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private float _nextAttackTime;
    private Transform _towerTarget;
    private IDamageable _towerDamageable;

    private void Start()
    {
        GameObject towerObj = GameObject.FindGameObjectWithTag("Tower");

        if (towerObj != null)
        {
            _towerTarget = towerObj.transform;
            _towerDamageable = towerObj.GetComponent<IDamageable>();
        }
        else
        {
            Debug.LogError("[EnemyAI] Враг не нашел башню! Убедитесь, что у башни стоит Tag 'Tower'.");
        }
    }

    public void SetStats(float newDamage, float newSpeed)
    {
        damage = newDamage;
        speed = newSpeed;
    }

    private void Update()
    {
        if (_towerTarget == null || _towerDamageable == null) return;

        // Выравниваем Y-координату целевой точки с высотой врага
        Vector3 targetAtSameHeight = new Vector3(_towerTarget.position.x, transform.position.y, _towerTarget.position.z);

        // Расчет дистанции по горизонтали
        float distance = Vector3.Distance(transform.position, targetAtSameHeight);

        // Если расстояние БОЛЬШЕ дистанции остановки — двигаемся
        if (distance > stoppingDistance)
        {
            MoveAndRotateTowards(targetAtSameHeight);
        }
        else
        {
            // Если подошли достаточно близко — останавливаемся и атакуем
            RotateTowardsOnly(targetAtSameHeight);
            AttackTower();
        }
    }

    private void MoveAndRotateTowards(Vector3 targetPosition)
    {
        RotateTowardsOnly(targetPosition);

        // Движение к башне
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void RotateTowardsOnly(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

    private void AttackTower()
    {
        if (Time.time >= _nextAttackTime)
        {
            _towerDamageable.TakeDamage(damage);
            _nextAttackTime = Time.time + attackCooldown;
        }
    }

}