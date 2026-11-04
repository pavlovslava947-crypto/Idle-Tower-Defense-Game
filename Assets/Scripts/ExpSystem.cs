using TMPro;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private CurrentWave _wave;
    [SerializeField] private GameObject _expStarPrefab;
    [SerializeField] private Transform _canvas;
    [SerializeField] private Transform _exp;
    [SerializeField] private float _speed = 60f;
    [SerializeField] private static int _score = 25;

    void Start()
    {    
        StarSpawn();
    }
   



    public void UpdateUI()
    {
        _score += _wave._wave;
        _scoreText.text = _score.ToString();
    }

    void StarSpawn()
    {


        for (int i = 0; i < _score; i++)
        {
            GameObject star = Instantiate(_expStarPrefab, _canvas);
        }

    }
}
