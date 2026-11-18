using UnityEngine;

public class TargetPoint : MonoBehaviour, ITargetable
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Vector3 _aimPoint;
    public Transform TargetTransform => _targetTransform;
    public Vector3 AimPosition => _aimPoint;

    


}
