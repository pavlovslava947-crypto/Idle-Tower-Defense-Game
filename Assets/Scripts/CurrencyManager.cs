using System;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    // Синглтон/Статический доступ для быстрого обращения (в дальнейшем можно заинжектить через DI/ServiceLocator)
    public static CurrencyManager Instance { get; private set; }

    public event Action<int> OnCoinsChanged;

    [SerializeField] private TextMeshProUGUI coinsText;

    public int Coins { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        Coins += amount;
        UpdateUI();
        OnCoinsChanged?.Invoke(Coins);
    }

    private void UpdateUI()
    {
        if (coinsText != null)
        {
            coinsText.text = $"Валюта: {Coins}";
        }
    }
}