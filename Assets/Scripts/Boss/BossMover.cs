using UnityEngine;

public class BossMover : MonoBehaviour
{
    private Transform _tower;
    [SerializeField] private BossData _bossScrObj;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private float _leaveDistance = 40f; // На какое расстояние от башни уехать перед удалением
    [SerializeField] private float _maxTime = 2.497f;

    private float _soundTimer;
    [SerializeField] private bool _isMoving = true;
    private bool _isLeaving = false;
    private Vector3 _leaveDirection;
    private Animator _wheelAnim;

    void Start()
    {
        _soundTimer = _maxTime;
        _wheelAnim = GetComponentInChildren<Animator>();
        var tower = FindAnyObjectByType<TowerHealth>();
        if (tower != null)
        {
            _tower = tower.transform;
        }

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(5, 1, 2);
        }

        RotateTo(_tower.position);
    }

    void Update()
    {
        if (_soundTimer > 0f)
        {
            _soundTimer -= Time.deltaTime;
        }

        if (_soundTimer <= 0f)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySound(6);
            }
            _soundTimer = _maxTime;
        }

        if (!_isMoving || _tower == null) return;

        if (!_isLeaving)
        {
            Vector3 targetPos = new Vector3(_tower.position.x, transform.position.y, _tower.position.z);
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                _bossScrObj.Speed * Time.deltaTime
            
            );
        
            if (Vector3.Distance(transform.position, _tower.position) <= _radius)
            {
                if (_wheelAnim != null) _wheelAnim.enabled = false;
                _isMoving = false;
            }
        }
        else
        {
            // Едем прочь от башни и удаляемся за границей камеры
            transform.position += _leaveDirection * (_bossScrObj.Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _tower.position) >= _leaveDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Запускает отъезд грузовика от башни с последующим уничтожением объекта.
    /// </summary>
    public void StartLeaving()
    {
        if (_isLeaving || _tower == null) return;

        Debug.Log($"До разворота Y: {transform.position.y}");

        Vector3 awayDir = transform.position - _tower.position;
        awayDir.y = 0f;

        if (awayDir.sqrMagnitude < 0.001f)
        {
            awayDir = -transform.forward;
        }

        _leaveDirection = awayDir.normalized;
        RotateTowardsDirection(_leaveDirection);

        // Если аниматор двигает позицию — проверь клип!
        if (_wheelAnim != null) _wheelAnim.enabled = true;

        _isLeaving = true;
        _isMoving = true;
    }

    void RotateTo(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001f)
        {
            RotateTowardsDirection(direction.normalized);
        }
    }

    void RotateTowardsDirection(Vector3 direction)
    {
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}