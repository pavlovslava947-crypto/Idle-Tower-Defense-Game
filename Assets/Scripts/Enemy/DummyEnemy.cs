using System;
using UnityEngine;

public class DummyEnemy : MonoBehaviour, IDamageable
{
    [Header("Здоровье")]
    [SerializeField] private float maxHealth = 20f;
    private float _currentHealth;

    // Реализация свойства из интерфейса IDamageable
    public float CurrentHealth => _currentHealth;

    [Header("Компоненты")]
    private LootSpawner _lootSpawner;

    // C# событие для уведомления WaveManager о смерти врага
    public event Action OnEnemyDied;

    private void Awake()
    {
        _lootSpawner = GetComponent<LootSpawner>();
        if (_lootSpawner == null)
        {
            _lootSpawner = GetComponentInChildren<LootSpawner>();
        }
    }

    private void Start()
    {
        // Если здоровье не было инициализировано извне (WaveManager), ставим базовое
        if (_currentHealth <= 0)
        {
            _currentHealth = maxHealth;
        }
    }

    // Вызывается из WaveManager при спавне для масштабирования
    public void Initialize(float scaledHealth, float scaledDamage, float scaledSpeed)
    {
        maxHealth = scaledHealth;
        _currentHealth = scaledHealth;

        if (TryGetComponent(out EnemyAI ai))
        {
            ai.SetStats(scaledDamage, scaledSpeed);
        }
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Сообщаем спавнеру, что этот враг погиб
        OnEnemyDied?.Invoke();

        // Вызываем спавн лута
        if (_lootSpawner != null)
        {
            _lootSpawner.SpawnLoot();
        }

        Destroy(gameObject);
    }
}