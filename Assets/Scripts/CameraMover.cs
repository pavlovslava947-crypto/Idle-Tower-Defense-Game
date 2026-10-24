using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
   
    [SerializeField] Transform _pointOne;
    [SerializeField] Transform _pointTwo;
    private Transform _currentPoint;
    [SerializeField] private float _speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentPoint = _pointOne;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, _speed * Time.deltaTime);

        if(transform.position == _currentPoint.position)
        {
            if (_currentPoint == _pointOne)
            {
                _currentPoint = _pointTwo;
            }
            else
            {
                _currentPoint = _pointOne;
            }
        }
    }
}
