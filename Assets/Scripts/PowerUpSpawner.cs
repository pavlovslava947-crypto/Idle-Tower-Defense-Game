using Unity.VisualScripting;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Префаб пулимета")]

    [SerializeField] private PowerUpTurret _powerUp;

    [Header("Координаты спавна пулимета")]

    [SerializeField] private Vector3 _turrentPos = new Vector3(-0.16f, 1.46f, 7.33f);

    [Header("Скорость удаления турели (в секундах)")]

    [SerializeField] private float _deleateSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    

    public void Spawn()
    {
        PowerUpTurret turret = Instantiate(_powerUp);
        turret.transform.position = _turrentPos;
        

        Destroy(turret.gameObject , _deleateSpeed);
    }
}
