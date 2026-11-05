using UnityEngine;

public class StarsMover : MonoBehaviour
{
    private Transform _exp;
    [SerializeField] private float _speed = 80f;
    private ExpSystem _expSystem;
    


    private void Start()
    {
        var targetStar = GameObject.FindWithTag("Star");
        _exp = targetStar.transform;
        _expSystem = FindAnyObjectByType<ExpSystem>();
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _exp.position, _speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, _exp.position) < 0.001)
        {
            _expSystem.UpdateUI();
            Destroy(gameObject);
        }
    }
}
