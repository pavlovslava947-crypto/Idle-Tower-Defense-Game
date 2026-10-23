using System.Threading;
using TMPro;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    public float _maxTime = 30f;
    public float _timer;
    private PowerUpSpawner _spawner;
    
    
    void Start()
    {
        _spawner = GetComponent<PowerUpSpawner>();
        _timer = _maxTime;
    }

    
    void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        
    }


    public void OnPowerUpButton()
    {
        if(_timer > 0)
        {
            return;
        }

        PowerUp();
    }

    void PowerUp()
    {

        _timer = _maxTime;
        _spawner.Spawn();
        Debug.Log("УЛЬТААААААААААААААААААААААА");
    }
}
