using System;
using UnityEngine;

public class RuinedTowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ruinedTower;
    [SerializeField] private Transform _tower;

   public void Spawn()
    {
        GameObject ruinedTower = Instantiate(_ruinedTower);
        ruinedTower.transform.position = _tower.transform.position;
    }
}
