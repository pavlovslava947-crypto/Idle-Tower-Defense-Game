using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Header("Настройки Лута")]
    [SerializeField] private GameObject coinPrefab;
    [Range(0f, 100f)]
    [SerializeField] private float dropChance = 100f;
    [SerializeField] private Vector3 spawnOffset = new Vector3(0, 1.5f, 0);

    public void SpawnLoot()
    {
        // Подстраховка: если поле coinPrefab пустое, загружаем монетку из папки Assets/Resources/Coin
        if (coinPrefab == null)
        {
            coinPrefab = Resources.Load<GameObject>("Coin");

            if (coinPrefab == null)
            {
                Debug.LogError("[LootSpawner] Ошибка: Coin Prefab не назначен и не найден в папке 'Assets/Resources/Coin'!");
                return;
            }
        }

        float randomRoll = Random.Range(0f, 100f);
        if (randomRoll <= dropChance)
        {
            Vector3 dropPosition = transform.position + spawnOffset;
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
            Debug.Log($"[LootSpawner] Монетка успешно заспавнена в {dropPosition}");
        }
    }
}