using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private BossHealth _bossHealth;
    [SerializeField] private float _animDuration = 0.25f;
    private Image _bossHealthBar;
    private TextMeshProUGUI _bossHealthText;
    private Tweener _healthTween;

    private void Awake()
    {
        _bossHealthBar = GetComponent<Image>();
        _bossHealthText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _bossHealth.OnBossHealthChanged += UpdateIU;
    }

   
    void Update()
    {
        
    }

    private void UpdateIU(float currentHealth, float maxHealth)
    {
        if (gameObject == null) return;

        if (maxHealth <= 0 || float.IsNaN(currentHealth) || float.IsNaN(maxHealth))
        {
            _bossHealthBar.fillAmount = 0f;
            _bossHealthText.text = "0";
            return;
        }

        if (currentHealth <= 0)
        {
            _bossHealthBar.DOKill();
            Destroy(gameObject);
        }

        float targetFill = currentHealth / maxHealth;
        _bossHealthBar.DOFillAmount(targetFill, _animDuration).SetEase(Ease.OutQuad);
        _bossHealthText.text = currentHealth + " / " + maxHealth;
    }

    private void OnDestroy()
    {
        _bossHealth.OnBossHealthChanged -= UpdateIU;
    }
}
