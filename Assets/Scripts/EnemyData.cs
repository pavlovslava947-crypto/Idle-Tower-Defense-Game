using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "TowerDefense/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Визуальное отображение и префаб")]
    public string enemyName = "Basic Enemy";
    public GameObject enemyPrefab;

    [Header("Базовые характеристики (1-я волна)")]
    public float baseHealth = 20f;
    public float baseDamage = 5f;
    public float baseSpeed = 3f;

    [Header("Вес/Шанс спавна")]
    [Tooltip("Относительный вес шанса выпадения спавна этого типа")]
    [Range(1, 100)]
    public int spawnWeight = 10;
}