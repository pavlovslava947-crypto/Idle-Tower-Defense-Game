using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    // Ссылка на менеджер экономики больше не нужна, используем ваш Синглтон

    public bool ProcessUpgrade(StatType statType, int cost, float upgradeValue)
    {
        // Вызываем ваш CurrencyManager через статическое свойство Instance
        if (CurrencyManager.Instance.TrySpend(cost))
        {
            playerStats.IncreaseStat(statType, upgradeValue);
            return true; // Покупка успешна
        }

        Debug.LogWarning("Не хватает валюты для покупки!");
        return false; // Покупка отклонена
    }
}