using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    private WaveManager _waveManager;

    public void Init(WaveManager waveManager, float health, float damage, float speed)
    {
        _waveManager = waveManager;

        // Если есть DummyEnemy — применяем параметры
        if (TryGetComponent(out DummyEnemy enemy))
        {
            enemy.Initialize(health, damage, speed);
        }
    }

    private void OnDestroy()
    {
        // При удалении объекта (смерти) сообщаем менеджеру
        if (_waveManager != null)
        {
            _waveManager.OnEnemyKilled();
        }
    }
}