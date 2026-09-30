using UnityEngine;

[RequireComponent(typeof(LootSpawner))]
public class DummyEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 50f;

    public float CurrentHealth { get; private set; }
    private LootSpawner _lootSpawner;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        _lootSpawner = GetComponent<LootSpawner>();
    }

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
        // 1. Выбиваем лут
        _lootSpawner.SpawnLoot();

        // 2. Уничтожаем или скрываем врага (в дальнейшем заблокируем для Object Pool)
        Destroy(gameObject);
    }
}