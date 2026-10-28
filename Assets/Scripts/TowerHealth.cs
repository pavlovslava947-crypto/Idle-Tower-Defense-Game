using System;
using UnityEngine;

public class TowerHealth : MonoBehaviour, IDamageable
{
    public event Action<float, float> OnHealthChanged; // Current, Max
    public event Action OnTowerDestroyed;

    [Header("Ссылки на систему прокачки")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private RuinedTowerSpawner _spawner;

    public float maxHealth;
    public float CurrentHealth { get; private set; }

    private void Start()
    {
        if (playerStats != null)
        {
            // Берем стартовое значение здоровья из PlayerStats
            maxHealth = playerStats.GetStat(StatType.Health);

            // Подписываемся на событие изменения статов (если игрок прокачает здоровье)
            playerStats.OnStatChanged += HandleStatChanged;
        }
        else
        {
            Debug.LogError("[TowerHealth] Не назначена ссылка на PlayerStats!");
            maxHealth = 100f; // Резервное значение
        }

        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    private void OnDestroy()
    {
        // Отписываемся от событий во избежание утечек памяти
        if (playerStats != null)
        {
            playerStats.OnStatChanged -= HandleStatChanged;
        }
    }

    private void HandleStatChanged(StatType statType, float newValue)
    {
        if (statType == StatType.Health)
        {
            float oldMax = maxHealth;
            maxHealth = newValue;

            // Разница между новым и старым максимумом
            float increase = maxHealth - oldMax;

            // Увеличиваем текущее здоровье на величину прироста макс. здоровья
            // (или можно полностью восстанавливать CurrentHealth = maxHealth при покупке)
            CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + increase);

            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
            Debug.Log($"[TowerHealth] Здоровье улучшено! Максимум: {maxHealth}, Текущее: {CurrentHealth}");
        }
    }

    public void TakeDamage(float amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        Debug.Log($"[Tower] Получен урон: {amount}. Осталось HP: {CurrentHealth}");
        

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("[Tower] Башня уничтожена! Game Over.");
        OnTowerDestroyed?.Invoke();
        _spawner.Spawn();
        Destroy(gameObject);
    }
}