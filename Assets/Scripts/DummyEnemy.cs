using UnityEngine;

[RequireComponent(typeof(LootSpawner))]
public class DummyEnemy : MonoBehaviour, IDamageable, ITargetable
{
    [Header("Характеристики")]
    [SerializeField] private float maxHealth = 50f;

    [Header("Прицеливание")]
    [SerializeField] private Transform aimPoint; // Пустой объект на уровне груди/центра модели

    private LootSpawner _lootSpawner;

    public float CurrentHealth { get; private set; }

    // Реализация интерфейса ITargetable
    public Transform TargetTransform => transform;
    public Vector3 AimPosition => aimPoint != null ? aimPoint.position : transform.position + Vector3.up * 1.2f;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        _lootSpawner = GetComponent<LootSpawner>();
    }

    // Реализация интерфейса IDamageable
    public void TakeDamage(float amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Спавним монетки
        if (_lootSpawner != null)
        {
            _lootSpawner.SpawnLoot();
        }

        // В будущем здесь будет возврат объекта в Object Pool
        Destroy(gameObject);
    }
}