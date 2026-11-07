using UnityEngine;
using UnityEngine.InputSystem;

public class CoinClicker : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        
        if (Mouse.current.leftButton.isPressed)
        {
            
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                
                if (hit.collider.TryGetComponent<CoinItem>(out CoinItem coin))
                {
                    coin.Collect();
                }
            }
        }
    }
}
