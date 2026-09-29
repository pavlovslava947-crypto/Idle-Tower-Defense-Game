using UnityEngine;

public class DummyEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 50f;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        Debug.Log($"[Enemy] Получен урон: {amount}. Осталось HP: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Debug.Log("[Enemy] Уничтожен!");
            gameObject.SetActive(false); // В будущем заменим на спавн частиц и возврат в Object Pool
        }
    }
}