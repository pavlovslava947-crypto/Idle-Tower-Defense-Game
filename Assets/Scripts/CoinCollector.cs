using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int _coins = 0;
    public TextMeshProUGUI mainText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainText.text = "Валюты " + _coins;
    }

    // Update is called once per frame
    void Update()
    {
        _coins++;
        Destroy(gameObject);
    }
}
