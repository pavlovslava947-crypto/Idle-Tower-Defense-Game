using UnityEngine;

public class CashEjector : MonoBehaviour
{
    [SerializeField] private float _pushSpeed = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cash"))
        {
            Vector3 direction = other.transform.position - transform.position;

            direction.y = 0;

            Vector3 normizedVector = direction.normalized;

            other.transform.position += normizedVector * _pushSpeed * Time.deltaTime;

        }
    }
}
