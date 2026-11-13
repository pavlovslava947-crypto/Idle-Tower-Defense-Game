using System;
using UnityEngine;

public class BossMover : MonoBehaviour
{
    private Transform _tower;
    [SerializeField] private BossData _bossScrObj;
    [SerializeField] private float _radius = 2;
    [SerializeField] private float _maxTime = 2.497f;
    private float _soundTimer;
    private bool _isMoving = true;
    private Animator _wheelAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _soundTimer = _maxTime;
        _wheelAnim = GetComponentInChildren<Animator>();
        var tower = FindAnyObjectByType<TowerHealth>();
        _tower = tower.transform;
        SoundManager.Instance.PlaySound(6);
        RotateToTower();
    }

    // Update is called once per frame
    void Update()
    {
        if(_soundTimer > 0)
        {
            _soundTimer -= Time.deltaTime;
        }

        if(_soundTimer <= 0)
        {
            SoundManager.Instance.PlaySound(7);

            _soundTimer = _maxTime;
        }


        if (!_isMoving) return;
        transform.position = Vector3.MoveTowards(transform.position, _tower.position, _bossScrObj.Speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _tower.position) <= _radius)
        {
            _wheelAnim.enabled = false;
            _isMoving = false;
        }
    }

    void RotateToTower()
    {
        Vector3 direction = _tower.position - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }
}
