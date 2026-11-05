using UnityEngine;
using System.Collections;

public class RuinedTowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ruinedTower;
    [SerializeField] private Transform _tower;
    [SerializeField] GameObject _defeatPanel;
    [SerializeField] private GameObject _turret;
    [SerializeField] private ButtonAnim[] UIelements;
    
    private bool _isSpawned = false;
    public bool IsSpawned => _isSpawned;

    private void Start()
    {
        UIelements = FindObjectsByType<ButtonAnim>();
    }
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
        _turret.SetActive(false);

        foreach(ButtonAnim ui in UIelements)
        {
            ui.gameObject.SetActive(false);
        }
    }
}
