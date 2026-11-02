using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private Transform towerTransform;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private List<EnemyData> enemyTypes = new List<EnemyData>();

    [Header("Настройки спавна вокруг башни")]
    [SerializeField] private float spawnRadius = 20f; // Дистанция от башни
    [SerializeField] private float spawnDelay = 0.5f;   // Пауза между спавном отдельных врагов

    [Header("Базовый Баланс (Волна 1)")]
    [SerializeField] private int baseEnemyCount = 5;
    [SerializeField] private float pauseBetweenWaves = 3f;

    [Header("Экспоненциальный рост сложности")]
    [Tooltip("Множитель роста количества врагов: Count = Base * (Growth ^ (Wave - 1))")]
    [SerializeField] private float countGrowthFactor = 1.25f;

    [Tooltip("Множитель роста здоровья: Health = BaseHealth * (Growth ^ (Wave - 1))")]
    [SerializeField] private float healthGrowthFactor = 1.18f;

    [Tooltip("Множитель роста урона: Damage = BaseDamage * (Growth ^ (Wave - 1))")]
    [SerializeField] private float damageGrowthFactor = 1.12f;

    [Tooltip("Ограничение максимальной скорости врага")]
    [SerializeField] private float maxEnemySpeed = 8f;
    [SerializeField] private float speedGrowthFactor = 1.03f;
    [SerializeField] private BossSpawner _bossSpawner;

    // Состояние
    private int _currentWave = 0;
    public int CurrentWave => _currentWave;
    private int _activeEnemiesCount = 0;
    private bool _isWaveSpawning = false;

    private void Start()
    {
        if (towerTransform == null)
        {
            GameObject tower = GameObject.FindGameObjectWithTag("Tower");
            if (tower != null) towerTransform = tower.transform;
            else Debug.LogError("[WaveManager] Башня не найдена!");
        }

        StartNextWave();
    }

    public void StartNextWave()
    {
        _currentWave++;
        UpdateWaveUI();

        if (_bossSpawner != null)
        {
            _bossSpawner.UpdateStats();
        }

        UpdateWaveUI();
        StartCoroutine(SpawnWaveRoutine());
    }

    private IEnumerator SpawnWaveRoutine()
    {
        _isWaveSpawning = true;

        // Пауза перед началом новой волны
        yield return new WaitForSeconds(pauseBetweenWaves);

        // 1. Рассчитываем количество врагов в текущей волне по экспоненте
        int enemiesToSpawn = Mathf.RoundToInt(baseEnemyCount * Mathf.Pow(countGrowthFactor, _currentWave - 1));
        _activeEnemiesCount = enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

        _isWaveSpawning = false;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyTypes == null || enemyTypes.Count == 0)
        {
            Debug.LogError("[WaveManager] Список типы врагов (Enemy Types) пуст!");
            return;
        }

        // Выбираем тип врага по весу спавна (Weighted Random)
        EnemyData selectedData = GetRandomEnemyType();
        if (selectedData == null || selectedData.enemyPrefab == null) return;

        // Расчет позиции спавна на окружности (плоскость XZ)
        Vector3 spawnPosition = GetRandomPointOnCircle(towerTransform.position, spawnRadius);

        // Спавним объект
        GameObject enemyObj = Instantiate(selectedData.enemyPrefab, spawnPosition, Quaternion.identity);

        // 2. Рассчитываем экспоненциальные характеристики врага
        float scaledHealth = selectedData.baseHealth * Mathf.Pow(healthGrowthFactor, _currentWave - 1);
        float scaledDamage = selectedData.baseDamage * Mathf.Pow(damageGrowthFactor, _currentWave - 1);
        float scaledSpeed = Mathf.Min(selectedData.baseSpeed * Mathf.Pow(speedGrowthFactor, _currentWave - 1), maxEnemySpeed);

        // Передаем рассчитанные параметры врагу
        if (enemyObj.TryGetComponent(out DummyEnemy enemyComponent))
        {
            enemyComponent.Initialize(scaledHealth, scaledDamage, scaledSpeed);
            // Подписываемся на смерть врага для отслеживания счетчика
            enemyComponent.OnEnemyDied += OnEnemyKilled;
        }
        else
        {
            // Резервное отслеживание через триггер уничтожения
            EnemyDeathNotifier notifier = enemyObj.AddComponent<EnemyDeathNotifier>();
            notifier.Init(this, scaledHealth, scaledDamage, scaledSpeed);
        }
    }

    private EnemyData GetRandomEnemyType()
    {
        int totalWeight = 0;
        foreach (var data in enemyTypes)
        {
            totalWeight += data.spawnWeight;
        }

        int randomValue = Random.Range(0, totalWeight);
        int currentWeightSum = 0;

        foreach (var data in enemyTypes)
        {
            currentWeightSum += data.spawnWeight;
            if (randomValue < currentWeightSum)
            {
                return data;
            }
        }

        return enemyTypes[0];
    }

    private Vector3 GetRandomPointOnCircle(Vector3 center, float radius)
    {
        // Случайный угол в радианах
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Точка на плоскости XZ (сохраняем высоту башни)
        float x = center.x + radius * Mathf.Cos(angle);
        float z = center.z + radius * Mathf.Sin(angle);

        return new Vector3(x, center.y, z);
    }

    public void OnEnemyKilled()
    {
        _activeEnemiesCount--;

        // Если все враги спавнились и все убиты — начинаем следующую волну
        if (_activeEnemiesCount <= 0 && !_isWaveSpawning && !_bossSpawner.IsSpawned)
        {
            StartNextWave();
        }
    }

    private void UpdateWaveUI()
    {
        if (waveText != null)
        {
            if (_bossSpawner.IsSpawned)
            {
                waveText.color = Color.red;
                waveText.text = "БОСС!";
            }

            else
            {
                waveText.text = $"Волна: {_currentWave}";
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса спавна в редакторе Unity
        if (towerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(towerTransform.position, spawnRadius);
        }
    }
}