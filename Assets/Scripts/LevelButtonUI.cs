using TMPro;
using UnityEngine;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthLevelText;
    [SerializeField] private TextMeshProUGUI _damageLevelText;
    [SerializeField] private TextMeshProUGUI _fireRateLevelText;
    private int _healthLevelCount = 1;
    private int _damageLevelCount = 1;
    private int _fireRateLevelCount = 1;

    private void Start()
    {
        UpdateUI();
    }

    public void HealthLevelCalc()
    {
        _healthLevelCount++;
        UpdateUI();
        Debug.Log("хп обновлено");
    }

    public void DamageLevelCalc()
    {
        _damageLevelCount++;
        UpdateUI();
        Debug.Log("урон обновлен");
    }

    public void FireRateLevelCalc()
    {
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
