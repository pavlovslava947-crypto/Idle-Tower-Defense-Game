using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 3f;
    public float stoppingDistance = 2f; // Расстояние, на котором враг остановится для удара

    [Header("Атака")]
    public float damage = 10f;
    public float attackCooldown = 1f; // Как часто бьет (в секундах)

    private float nextAttackTime = 0f;

    private Transform towerTarget;
    private TowerHealth towerHealth;

    void Start()
    {
        // Ищем башню по тегу. Это простой и надежный способ для прототипа.
        GameObject towerObj = GameObject.FindGameObjectWithTag("Tower");

        if (towerObj != null)
        {
            towerTarget = towerObj.transform;
            towerHealth = towerObj.GetComponent<TowerHealth>();
        }
        else
        {
            Debug.LogError("Враг не нашел башню! Проверьте, есть ли у башни тег 'Tower'.");
        }
    }

    void Update()
    {
        // Если башни нет (или она уничтожена), враг ничего не делает
        if (towerTarget == null || towerHealth == null) return;

        // Вычисляем расстояние от врага до башни
        float distance = Vector3.Distance(transform.position, towerTarget.position);

        if (distance > stoppingDistance)
        {
            MoveTowardsTower();
        }
        else
        {
            AttackTower();
        }
    }

    private void MoveTowardsTower()
    {
        // Поворачиваем врага лицом к башне (опционально, но в 3D выглядит лучше)
        // Игнорируем разницу по высоте (Y), чтобы враг не наклонялся, если башня высокая
        Vector3 lookPosition = new Vector3(towerTarget.position.x, transform.position.y, towerTarget.position.z);
        transform.LookAt(lookPosition);

        // Двигаем врага вперед
        transform.position = Vector3.MoveTowards(transform.position, towerTarget.position, speed * Time.deltaTime);
    }

    private void AttackTower()
    {
        // Проверяем, прошло ли время перезарядки атаки
        if (Time.time >= nextAttackTime)
        {
            towerHealth.TakeDamage(damage);

            // Задаем время для следующего удара
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}