using UnityEngine;
using UnityEngine.UI;
using TMPro; // Подключаем TextMeshPro

[RequireComponent(typeof(Button))]
public class UpgradeButtonUI : MonoBehaviour
{
    [Header("Системы")]
    [SerializeField] private UpgradeManager upgradeManager;

    [Header("Настройки прокачки")]
    [SerializeField] private StatType statToUpgrade; // Выбор стата в Инспекторе
    [SerializeField] private int currentCost = 20;
    [SerializeField] private float upgradeAmount = 5f;
    [SerializeField] private int costIncreasePerLevel = 15; // Прогрессия цены

    [Header("UI Элементы")]
    [SerializeField] private TextMeshProUGUI costText;
    

    private Button upgradeButton;

    private void Awake()
    {
        upgradeButton = GetComponent<Button>();
        UpdateUIText();
    }

    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(OnButtonClicked);
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
}