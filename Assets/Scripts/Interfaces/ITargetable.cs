using UnityEngine;

public interface ITargetable
{
    Transform TargetTransform { get; }
    Vector3 AimPosition { get; }
}