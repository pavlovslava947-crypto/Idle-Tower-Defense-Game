using TMPro;
using UnityEngine;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthLevelText;
    [SerializeField] private TextMeshProUGUI _damageLevelText;
    [SerializeField] private TextMeshProUGUI _fireRateLevelText;
    [SerializeField] private UpgradeButtonUI _upgradeButton;
    [SerializeField] private CurrencyManager _currencyManager;
    private int _healthLevelCount = 1;
    private int _damageLevelCount = 1;
    private int _fireRateLevelCount = 1;

    private void Start()
    {
        UpdateUI();
    }

    public void HealthLevelCalc()
    {
        if (_currencyManager.Coins < _upgradeButton.CurrentCost) return;
        _healthLevelCount++;
        UpdateUI();
        Debug.Log("хп обновлено");
    }

    public void DamageLevelCalc()
    {
        if (_currencyManager.Coins < _upgradeButton.CurrentCost) return;
        _damageLevelCount++;
        UpdateUI();
        Debug.Log("урон обновлен");
    }

    public void FireRateLevelCalc()
    {
        if (_currencyManager.Coins < _upgradeButton.CurrentCost) return;
        _fireRateLevelCount++;
        UpdateUI();
        Debug.Log("скорострельность обновлена");
    }

    void UpdateUI()
    {
        _healthLevelText.text = _healthLevelCount.ToString();
        _damageLevelText.text = _damageLevelCount.ToString();
        _fireRateLevelText.text = _fireRateLevelCount.ToString();
        Debug.Log("UI обновлен");
    }
   
}
