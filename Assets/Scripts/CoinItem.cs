using UnityEngine;
using UnityEngine.InputSystem; // Добавляем пространство имен нового Input System

[RequireComponent(typeof(Collider))]
public class CoinItem : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private bool _isCollected;

    private void OnMouseDown()
    {
        Collect();
    }

    private void OnMouseEnter()
    {
        // Проверяем нажатие левой кнопки мыши через новый Input System
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (_isCollected) return;
        _isCollected = true;

        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddCoins(coinValue);
        }

        Destroy(gameObject);
    }
}