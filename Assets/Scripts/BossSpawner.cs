using UnityEngine;
using System.Collections.Generic;
using System;

public class BossSpawner : MonoBehaviour
{
    [Header("Сошка босса")]
    [SerializeField] List<BossData> _bossData = new List<BossData>();
    [SerializeField] Transform _spawnPoint;
    [SerializeField] private WaveManager _waveManager;
    private bool _isSpawned = false;
    public bool IsSpawned => _isSpawned;
   


    public void UpdateStats()
    {
        foreach (BossData boss in _bossData)
        {
            if (_bossData == null) return;

            if (boss.WaveCount == _waveManager.CurrentWave && !_isSpawned)
            {
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        foreach (BossData boss in _bossData)
        {
            GameObject newBoss = Instantiate(boss.BossPrefab);
            newBoss.transform.position = _spawnPoint.position;
            _isSpawned = true;

        }

    }

   

}