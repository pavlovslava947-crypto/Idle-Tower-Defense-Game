using UnityEngine;
using UnityEngine.AI; // Обязательно добавляем для работы с NavMesh

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Атака")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    

    private float _nextAttackTime;
    private Transform _towerTarget;
    private IDamageable _towerDamageable;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        GameObject towerObj = GameObject.FindGameObjectWithTag("Tower");

        if (towerObj != null)
        {
            _towerTarget = towerObj.transform;
            _towerDamageable = towerObj.GetComponent<IDamageable>();

            // Задаем конечную точку маршрута для NavMeshAgent
            _agent.SetDestination(_towerTarget.position);
        }
        else
        {
            Debug.LogError("[EnemyAI] Враг не нашел башню! Убедитесь, что у башни стоит Tag 'Tower'.");
        }
    }

    public void SetStats(float newDamage, float newSpeed)
    {
        damage = newDamage;

        // Синхронизируем скорость со скриптом NavMeshAgent
        if (_agent != null)
        {
            _agent.speed = newSpeed;
        }
    }

    private void Update()
    {
        if (_towerTarget == null || _towerDamageable == null) return;

        // Проверяем, построил ли агент валидный путь
        if (_agent.pathStatus == NavMeshPathStatus.PathPartial || _agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            // Если путь сломался, пробуем обновить пункт назначения
            _agent.SetDestination(_towerTarget.position);
        }

        float distance = Vector3.Distance(transform.position, _towerTarget.position);

        if (distance <= _agent.stoppingDistance || (_agent.hasPath && _agent.remainingDistance <= _agent.stoppingDistance))
        {
            _agent.isStopped = true;
            RotateTowardsOnly(_towerTarget.position);
            AttackTower();
        }
        else
        {
            _agent.isStopped = false;
        }
    }

    private void RotateTowardsOnly(Vector3 targetPosition)
    {
        Vector3 targetAtSameHeight = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        Vector3 direction = (targetAtSameHeight - transform.position).normalized;

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