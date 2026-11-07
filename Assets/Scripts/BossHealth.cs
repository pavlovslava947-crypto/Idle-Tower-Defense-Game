using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 250f;
    [SerializeField] BossData _bossData;
    public float CurrentHealth => _health;

    public void TakeDamage(float amount)
    {
        _health = _bossData.MaxHealth;
        _health -= amount;
    }
}
