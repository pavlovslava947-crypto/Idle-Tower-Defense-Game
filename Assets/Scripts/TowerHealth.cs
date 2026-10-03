using System;
using UnityEngine;

public class TowerHealth : MonoBehaviour, IDamageable
{
    public event Action<float, float> OnHealthChanged; // Current, Max
    public event Action OnTowerDestroyed;

    [Header("Характеристики башни")]
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
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
    }
}