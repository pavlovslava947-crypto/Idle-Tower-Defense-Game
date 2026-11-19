using System.Collections;
using UnityEngine;

public class PassiveTreatment : MonoBehaviour
{
    [SerializeField] private float _treatment = 5f;
    [SerializeField] private float _cooldown = 2f;
    private Coroutine _healCorutine;
    private TowerHealth _towerHealth;

    private void Awake()
    {
        _towerHealth = GetComponent<TowerHealth>();

        if (_towerHealth == null) return;
    }
    void Start()
    {
        _towerHealth.OnHealthChanged += Treatment;
    }

    private void Treatment(float currentHealth, float maxHealth)
    {
        if(_towerHealth != null && currentHealth > 0 && currentHealth < maxHealth && _healCorutine == null)
        {
            _healCorutine = StartCoroutine(HealthAdder());
        }
    }

    private IEnumerator HealthAdder()
    {
        while(_towerHealth != null && _towerHealth.CurrentHealth > 0 && _towerHealth.CurrentHealth < _towerHealth.maxHealth)
        {
            yield return new WaitForSeconds(_cooldown);

            _towerHealth.Heal(_treatment);
        }

        _healCorutine = null;
    }


    private void OnDestroy()
    {
        _towerHealth.OnHealthChanged -= Treatment;
    }
}
