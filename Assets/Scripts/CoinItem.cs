using UnityEngine;

public class CoinItem : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    private bool _isCollected;

    public void Collect()
    {
        if (_isCollected) return;
        _isCollected = true;

        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddCoins(coinValue);
        }

        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(4);
        }

        Destroy(gameObject);
    }
}
