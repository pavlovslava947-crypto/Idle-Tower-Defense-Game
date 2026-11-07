 using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Damage,
    FireRate
}

public class PlayerStats : MonoBehaviour
{
    // Инициализация базовых значений
    private Dictionary<StatType, float> stats = new Dictionary<StatType, float>()
    {
        { StatType.Health, 100f },
        { StatType.Damage, 10f },
        { StatType.FireRate, 1f }
    };

    [SerializeField] private ShootingAnim _shootingAnim;

    // Событие для оповещения других систем (например, полоски здоровья) об изменениях
    public event Action<StatType, float> OnStatChanged;

    public float GetStat(StatType statType)
    {
        return stats.TryGetValue(statType, out float val) ? val : 0f;
    }

    public void IncreaseStat(StatType statType, float amount)
    {
        if (stats.ContainsKey(statType))
        {
            stats[statType] += amount;
            OnStatChanged?.Invoke(statType, stats[statType]);
            Debug.Log($"[PlayerStats] {statType} увеличен. Текущее значение: {stats[statType]}");
            _shootingAnim._speed += 0.2f;
        }
    }
}