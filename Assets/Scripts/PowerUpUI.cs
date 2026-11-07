using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    private PowerUpSystem _powerUpSystem;
    [SerializeField] private TextMeshProUGUI _powerUpTimerText;
    [SerializeField] private Image _powerUpImage;
    private Color _alphaColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _powerUpSystem = GetComponent<PowerUpSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_powerUpSystem._timer > 0)
        {
            UpdateUI();
        }

        if(_powerUpSystem._timer <= 0)
        {
            Resett();
        }
    }

    void UpdateUI()
    {
        _powerUpImage.fillAmount = _powerUpSystem._timer / _powerUpSystem._maxTime;

        _alphaColor = _powerUpImage.color;
        _alphaColor.a = 0.5f;
        _powerUpImage.color = _alphaColor;
        _powerUpTimerText.text = _powerUpSystem._timer.ToString("F0");
    }

    void Resett()
    {
        _powerUpImage.fillAmount = 1;
        _powerUpTimerText.text = " ";
        _alphaColor.a = 1;
        _powerUpImage.color = _alphaColor;
    }
}
