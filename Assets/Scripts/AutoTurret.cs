using UnityEngine;

[RequireComponent(typeof(TargetSearcher))]
public class AutoTurret : MonoBehaviour
{
    [Header("Зависимости")]
    [SerializeField] private PlayerStats playerStats; // Ссылка на систему прокачки

    [Header("Параметры стрельбы (Базовые/Фоллбэк)")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 2f; // Выстрелов в секунду
    [SerializeField] private LayerMask enemyLayer;

    [Header("Точки и визуал")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform rotatablePart; // Поворотная часть башни/юнита (опционально)
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Визуализация Gizmos")]
    [SerializeField] private float gizmoRayDuration = 0.1f; // Сколько секунд виден луч в эдиторе

    private TargetSearcher _targetSearcher;
    private float _fireCooldown;

    // Переменные для отрисовки последнего выстрела через Gizmos
    private Vector3 _lastRayStart;
    private Vector3 _lastRayEnd;
    private float _gizmoTimer;

    private void Awake()
    {
        _targetSearcher = GetComponent<TargetSearcher>();
    }

    private void Start()
    {
        // Подписываемся на события изменения характеристик
        if (playerStats != null)
        {
            playerStats.OnStatChanged += HandleStatChanged;
            UpdateStats(); // Инициализируем стартовые значения из PlayerStats
        }
    }

    private void OnDestroy()
    {
        // Обязательно отписываемся от события при уничтожении объекта
        if (playerStats != null)
        {
            playerStats.OnStatChanged -= HandleStatChanged;
        }
    }

    private void HandleStatChanged(StatType statType, float newValue)
    {
        // При любом изменении обновляем параметры турели
        UpdateStats();
    }

    private void UpdateStats()
    {
        if (playerStats == null) return;

        // Обновляем урон и скорострельность из единого источника данных
        damage = playerStats.GetStat(StatType.Damage);
        fireRate = playerStats.GetStat(StatType.FireRate);
    }

    private void Update()
    {
        _fireCooldown -= Time.deltaTime;

        Transform target = _targetSearcher.GetNearestTarget(transform.position);

        if (target == null) return;

        // Поворот в сторону цели
        RotateTowards(target.position);

        // Стрельба по готовности
        if (_fireCooldown <= 0f)
        {
            Shoot(target);
            // Избегаем деления на ноль, если fireRate вдруг станет 0
            _fireCooldown = fireRate > 0f ? 1f / fireRate : 0.5f;
        }

        // Таймер отрисовки Gizmos
        if (_gizmoTimer > 0)
        {
            _gizmoTimer -= Time.deltaTime;
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        if (rotatablePart == null) return;

        Vector3 direction = (targetPosition - rotatablePart.position).normalized;
        direction.y = 0;
          // Игнорируем наклон по Y (для Top-Down башен)

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rotatablePart.rotation = Quaternion.Slerp(rotatablePart.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void Shoot(Transform target)
    {
        Vector3 origin = firePoint != null ? firePoint.position : transform.position;

        // Определяем точку прицеливания (грудь/центр)
        Vector3 targetAimPosition = target.position;
        if (target.TryGetComponent<ITargetable>(out var targetable))
        {
            targetAimPosition = targetable.AimPosition;
        }

        Vector3 direction = (targetAimPosition - origin).normalized;

        _lastRayStart = origin;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, _targetSearcher.AttackRange, enemyLayer))
        {
            _lastRayEnd = hit.point;

            if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                // Наносится актуальный урон, пересчитанный из PlayerStats!
                damageable.TakeDamage(damage);
            }
        }
        else
        {
            _lastRayEnd = origin + direction * _targetSearcher.AttackRange;
        }

        _gizmoTimer = gizmoRayDuration;
    }

    private void OnDrawGizmos()
    {
        // Отрисовка траектории выстрела в Scene View
        if (_gizmoTimer > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_lastRayStart, _lastRayEnd);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_lastRayEnd, 0.2f);
        }
    }
}