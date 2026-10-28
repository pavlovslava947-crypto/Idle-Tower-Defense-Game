using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _healthImage;
    [SerializeField] private float _animDuration = 0.25f;
    [SerializeField] private TowerHealth _towerHealth;
    private Tweener _healthTweener;
    private float _health;
    private float _maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        _health = 
        _maxHealth = _health;
        _towerHealth.OnHealthChanged += HandleHealthChanged;
       
    }

    private void Start()
    {
        UpdateUI(_towerHealth.CurrentHealth, _towerHealth.maxHealth);
    }

    private void OnDestroy()
    {
        _towerHealth.OnHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(float currentHealth, float maxHealth)
    {
        UpdateUI(currentHealth, maxHealth);
    }



    public void UpdateUI(float currentHealth , float maxHealth)
    {
        if (maxHealth <= 0 || float.IsNaN(currentHealth) || float.IsNaN(maxHealth))
        {
            _healthImage.fillAmount = 0f;
            _healthText.text = "0";
            return;
        }

        float _targetFill = currentHealth / maxHealth;
        _healthTweener = _healthImage.DOFillAmount(_targetFill, _animDuration).SetEase(Ease.OutQuad);
        _healthText.text = currentHealth.ToString("F0");
    }
}
