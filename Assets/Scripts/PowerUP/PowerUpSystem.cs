
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

        if (Keyboard.current.qKey.isPressed)
        {
            OnPowerUpButton();
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
