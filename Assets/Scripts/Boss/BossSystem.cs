using UnityEngine;

public class BossSystem : MonoBehaviour
{
    private int _enemyCount = 0;
    private Collider[] _enemys;
    [SerializeField] private LayerMask _enemyLayer;
    private WaveManager _waveManager;
    private BossSpawner _bossSpawner;

    private void Awake()
    {
        _waveManager = FindAnyObjectByType<WaveManager>();
        _bossSpawner = FindAnyObjectByType<BossSpawner>();
    }

    private void Start()
    {
        _enemys = Physics.OverlapSphere(transform.position, 100f, _enemyLayer);

        foreach (Collider enemy in _enemys)
        {
            if(enemy.TryGetComponent<DummyEnemy>(out var newEnemy))
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
            _waveManager.StartNextWave();
            _waveManager.UpdateWaveUI();

            foreach (Collider enemy in _enemys)
            {
                if (enemy.TryGetComponent<DummyEnemy>(out var newEnemy))
                {
                    newEnemy.OnEnemyDied -= OnEnemyDie;
                }
            }
            _bossSpawner._isSpawned = false;

        }
    }


}
