using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [Header("Характеристики башни")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        // При старте игры здоровье полное
        currentHealth = maxHealth;
    }

    // Публичный метод, который смогут вызывать враги
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Башня получила {amount} урона! Осталось HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Башня уничтожена! Game Over.");
        // Позже здесь вы добавите логику проигрыша (вызов экрана Game Over, остановку спавна и т.д.)
    }
}