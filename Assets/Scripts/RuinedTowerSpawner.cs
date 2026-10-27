using System;
using UnityEngine;

public class RuinedTowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ruinedTower;
    [SerializeField] private Transform _tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Spawn()
    {
        GameObject ruinedTower = Instantiate(_ruinedTower);
        ruinedTower.transform.position = _tower.transform.position;
    }
}
