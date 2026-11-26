using UnityEngine;

public class BossSystem : MonoBehaviour
{
    private int _enemyCount = 0;
    private Collider[] _enemys;
    [SerializeField] private LayerMask _enemyLayer;
    private WaveManager _waveManager;
    private BossSpawner _bossSpawner;
    private BossMover _bossMover;

    private void Awake()
    {
        _waveManager = FindAnyObjectByType<WaveManager>();
        _bossSpawner = FindAnyObjectByType<BossSpawner>();
        _bossMover = GetComponent<BossMover>();
    }

    private void Start()
    {
        _enemys = Physics.OverlapSphere(transform.position, 100f, _enemyLayer);

        foreach (Collider enemy in _enemys)
        {
            if (enemy.TryGetComponent<DummyEnemy>(out var newEnemy))
            {
                newEnemy.OnEnemyDied += OnEnemyDie;
                _enemyCount++;
            }
        }
    }

    void OnEnemyDie()
    {
        _enemyCount--;

        if (_enemyCount <= 0)
        {
            if (_waveManager != null)
            {
                _waveManager.UpdateWaveUI();
            }

            if (_enemys != null)
            {
                foreach (Collider enemy in _enemys)
                {
                    if (enemy != null && enemy.TryGetComponent<DummyEnemy>(out var newEnemy))
                    {
                        newEnemy.OnEnemyDied -= OnEnemyDie;
                    }
                }
            }

            if (_bossSpawner != null)
            {
                _bossSpawner._isSpawned = false;
            }

            // Запускаем отъезд грузовика после зачистки волн/врагов
            if (_bossMover != null)
            {
                _bossMover.StartLeaving();
            }
        }
    }
}