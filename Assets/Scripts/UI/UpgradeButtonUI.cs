using TMPro; // Подключаем TextMeshPro
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeButtonUI : MonoBehaviour
{
    [Header("Системы")]
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private CurrencyManager _currencyManager;

    [Header("Настройки прокачки")]
    [SerializeField] private StatType statToUpgrade; // Выбор стата в Инспекторе
    [SerializeField] private int currentCost = 20;
    public int CurrentCost => currentCost;
    [SerializeField] private float upgradeAmount = 5f;
    [SerializeField] private int costIncreasePerLevel = 15; // Прогрессия цены

    [Header("UI Элементы")]
    [SerializeField] private TextMeshProUGUI costText;


    private Button upgradeButton;
    private Color _baseButtonColor;
    private Image _buttonImage;

    private void Awake()
    {
        upgradeButton = GetComponent<Button>();
        UpdateUIText();
    }

    private void Start()
    {
        _currencyManager.OnCoinsChanged += ChangeButtonColor;
        _buttonImage = upgradeButton.GetComponent<Image>();
        _baseButtonColor = _buttonImage.color;
    }

    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(OnButtonClicked);
        _currencyManager.OnCoinsChanged -= ChangeButtonColor;
    }

    private void OnButtonClicked()
    {
        // Пытаемся купить апгрейд
        bool success = upgradeManager.ProcessUpgrade(statToUpgrade, currentCost, upgradeAmount);

        if (success)
        {
            // Если купили успешно, увеличиваем цену для следующей покупки
            currentCost += costIncreasePerLevel;
            UpdateUIText();
        }
    }

    private void UpdateUIText()
    {
        if (costText != null)
        {
            costText.text = currentCost.ToString();
        }
    }

    private void ChangeButtonColor(int coins)
    {
        if (coins >= currentCost) _buttonImage.color = Color.green;

        else _buttonImage.color = _baseButtonColor;
    }
}