using UnityEngine;

public class PowerUpTurret : MonoBehaviour
{
    [Header("Настройки стрельбы пулемета")]
    [SerializeField] private float fireRate = 10f; // Очень высокая скорость (выстрелов в секунду)
    [SerializeField] private float damage = 25f;    // Урон каждого выстрела
    [SerializeField] private float range = 20f;    // Дальность поиска врагов
    [SerializeField] private LayerMask enemyLayer; // Слой, на котором находятся враги
    [SerializeField] private Transform firePoint;  // Откуда вылетает луч (если пусто, то от позиции турели)

    private float _nextFireTime;

    void Update()
    {
        // Стреляем по кулдауну
        if (Time.time >= _nextFireTime)
        {
            ShootClosestEnemy();
            _nextFireTime = Time.time + (1f / fireRate);
        }
    }

    private void ShootClosestEnemy()
    {
        // Ищем всех врагов в радиусе с помощью коллайдеров
        Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);

        if (hits.Length == 0) return;

        // Находим самого близкого врага
        Transform closestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = hit.transform;
            }
        }

        if (closestEnemy != null)
        {
            // Поворачиваемся лицом к врагу (опционально, для красоты)
            Vector3 direction = (closestEnemy.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            }

            // Наносим урон через интерфейс IDamageable (как в главной башне)
            if (closestEnemy.TryGetComponent<IDamageable>(out var enemyDamageable))
            {
                enemyDamageable.TakeDamage(damage);
                // Тут можно добавить визуальный эффект выстрела (например, луч или партикл)
            }
        }
    }
}