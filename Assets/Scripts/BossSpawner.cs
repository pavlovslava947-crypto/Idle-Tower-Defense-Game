using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Сошка босса")]
    [SerializeField] private BossData _bossScrObj;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] private WaveManager _waveManager;
    private bool _isSpawned = false;
   

    public void UpdateStats()
    {
        if (_bossScrObj == null) return;

        if (_bossScrObj.WaveCount == _waveManager.CurrentWave && !_isSpawned)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject boss = Instantiate(_bossScrObj.BossPrefab);
        boss.transform.position = _spawnPoint.position;
        _isSpawned = true;
    }
}
