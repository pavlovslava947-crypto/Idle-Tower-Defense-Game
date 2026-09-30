using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotator = new Vector3 (0, 1, 0);
    [SerializeField] private float rotationSpeed = 1.0f;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(_rotator * rotationSpeed * Time.deltaTime);
    }
}
