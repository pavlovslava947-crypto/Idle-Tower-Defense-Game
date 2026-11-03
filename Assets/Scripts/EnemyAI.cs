using UnityEngine;
using UnityEngine.AI; // Обязательно добавляем для работы с NavMesh

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Атака")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackInterval = 1.0f;
    [SerializeField] private float attackRange = 2.0f;


    private float _nextAttackTime;
    private Transform _towerTarget;
    private IDamageable _towerDamageable;
    private NavMeshAgent _agent;
    private bool _isAttacking = false;
    private float _attackTimer;

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

        if (_isAttacking)
        {
            // Стоим на месте, смотрим на башню и бьем по кулдауну
            _agent.isStopped = true;
            RotateTowardsOnly(_towerTarget.position);

            // Тикаем таймер атаки
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= attackInterval)
            {
                _attackTimer = 0f;
                _towerDamageable.TakeDamage(damage);
            }
            return; // Выходим из Update, дальше код движения не выполняем!

        }

        float distance = Vector3.Distance(transform.position, _towerTarget.position);

        if (distance <= attackRange)
        {
            // Переключаем в режим атаки навсегда
            _isAttacking = true;
            _agent.isStopped = true;
        }
    }

    private void RotateTowardsOnly(Vector3 targetPosition)
    {
        transform.LookAt(_towerTarget);
        
    }
}