using UnityEngine;

public class TowerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private float _nextFireTime;

    private void Update()
    {
        // Вместо фиксированного cooldown используем FireRate из PlayerStats
        // FireRate может означать «выстрелов в секунду», тогда интервал = 1 / FireRate
        float fireRate = playerStats != null ? playerStats.GetStat(StatType.FireRate) : 1f;
        float fireInterval = 1f / Mathf.Max(0.1f, fireRate);

        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + fireInterval;
        }
    }

    private void Shoot()
    {
        // Получаем актуальный урон из системы прокачки при каждом выстреле
        float currentDamage = playerStats != null ? playerStats.GetStat(StatType.Damage) : 10f;

        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Если у пули есть скрипт урона, передаем ей currentDamage:

        Debug.Log($"[Weapon] Выстрел с уроном: {currentDamage}");
    }
}