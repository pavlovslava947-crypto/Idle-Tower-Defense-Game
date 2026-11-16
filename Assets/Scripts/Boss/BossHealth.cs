using System;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 250f;
    [SerializeField] BossData _bossData;
    public event Action<float, float> OnBossHealthChanged;
    public float CurrentHealth => _health;
    private float _maxHealth;

    void Start()
    {
        _health = _bossData.MaxHealth;
        _maxHealth = _health;
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        OnBossHealthChanged?.Invoke(_health, _maxHealth);
    }
}
