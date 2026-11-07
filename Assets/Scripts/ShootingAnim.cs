using UnityEngine;

public class ShootingAnim : MonoBehaviour
{
    [SerializeField] Transform _backAnimPoint;
    [SerializeField] Transform _forwardAnimPoint;
    public float _speed = 80f;
    private bool _isMovingBack = true;
    private bool _isStartShoot = false;


    void FixedUpdate()
    {
        Transform targetPoint = _isMovingBack ? _backAnimPoint : _forwardAnimPoint;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            if (_isMovingBack)
            {
                _isMovingBack = false;
            }

            else
            {
                _isStartShoot = false;
            }
        }
    }

    public void StartShootAnimation(Transform nearestEnemy)
    {
        if (nearestEnemy != null && !_isStartShoot)
        {
            _isStartShoot = true;
            _isMovingBack = true;
        }


    }
}
