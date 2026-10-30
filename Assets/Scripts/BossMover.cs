using System;
using UnityEngine;

public class BossMover : MonoBehaviour
{
    private Transform _tower;
    [SerializeField] private BossData _bossScrObj;
    [SerializeField] private float _radius = 2;
    private bool _isMoving = true;
    private Animator _wheelAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _wheelAnim = GetComponentInChildren<Animator>();
        var tower = FindAnyObjectByType<TowerHealth>();
        _tower = tower.transform;
        RotateToTower();
    }

    // Update is called once per frame
    void Update()
    {
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
