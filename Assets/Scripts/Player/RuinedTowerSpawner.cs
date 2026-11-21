using UnityEngine;
using System.Collections;

public class RuinedTowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ruinedTower;
    [SerializeField] private Transform _tower;
    [SerializeField] GameObject _defeatPanel;
    [SerializeField] private GameObject _turret;
    [SerializeField] private TowerHealth _towerHealth;
    [SerializeField] private PanelAnim _panelAnim;

    private ButtonAnim[] UIelements;
    private bool _isSpawned = false;
    public bool IsSpawned => _isSpawned;

    private void Start()
    {
        _towerHealth.OnTowerDestroyed += Spawn;
        UIelements = FindObjectsByType<ButtonAnim>();
    }
    private void Spawn()
    {
        GameObject ruinedTower = Instantiate(_ruinedTower);
        ruinedTower.transform.position = _tower.transform.position;
        _isSpawned = true;
        UpdateUI();
    }

    void UpdateUI()
    {
        _defeatPanel.SetActive(true);
        _panelAnim.PanelDeparture();
        _turret.SetActive(false);

        foreach(ButtonAnim ui in UIelements)
        {
            ui.gameObject.SetActive(false);
        }

        _towerHealth.OnTowerDestroyed -= Spawn;
    }
}
