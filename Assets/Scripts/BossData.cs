using UnityEngine;

[CreateAssetMenu(fileName = "NewBossData", menuName = "TowerDefense/Boss Data")]
public class BossData : ScriptableObject
{
    [Header("Вражина")]
    [SerializeField] private string _bossName = "Zombie Wagon";
    [SerializeField] private GameObject _bossPrefab;
    [Header("Характеристики")]
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _baseTreatment = 2.5f;
}
