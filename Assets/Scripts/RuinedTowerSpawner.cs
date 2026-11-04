using UnityEngine;

public class RuinedTowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ruinedTower;
    [SerializeField] private Transform _tower;
    [SerializeField] GameObject _defeatPanel;
    private bool _isSpawned = false;
    public bool IsSpawned => _isSpawned;

    public void Spawn()
    {
        GameObject ruinedTower = Instantiate(_ruinedTower);
        ruinedTower.transform.position = _tower.transform.position;
        _isSpawned = true;
        UpdateUI();
    }

    void UpdateUI()
    {
        _defeatPanel.SetActive(true);
    }
}
