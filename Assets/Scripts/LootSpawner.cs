using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Header("Настройки дропа")]
    [SerializeField] private GameObject coinPrefab;
    [Range(0f, 100f)][SerializeField] private float dropChance = 100f; // Шанс выпадения в %
    [SerializeField] private Vector3 spawnOffset = new Vector3(0, 0.5f, 0); // Небольшой сдвиг по высоте

    public void SpawnLoot()
    {
        if (coinPrefab == null) return;

        // Проверяем шанс выпадения
        float randomRoll = Random.Range(0f, 100f);
        if (randomRoll <= dropChance)
        {
            Vector3 dropPosition = transform.position + spawnOffset;
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }
}