using System.Collections;
using TMPro;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private CurrentWave _wave;
    [SerializeField] private GameObject _expStarPrefab;
    [SerializeField] private Transform _canvas;
    [SerializeField] private Transform _exp;
    [SerializeField] private StarsMover _starsMover;
    [SerializeField] private float _speed = 60f;
    [SerializeField] private static int _score = 0;


    void Start()
    {
        _score += _wave._wave;
        PlayerPrefs.SetInt("_score", _score);
        PlayerPrefs.Save();
        StartCoroutine(StarSpawn());
    }




    public void UpdateUI()
    {
        _scoreText.text = _score.ToString();
    }

    private IEnumerator StarSpawn()
    {
        for (int i = 0; i < _score; i++)
        {
            GameObject star = Instantiate(_expStarPrefab, _canvas);
            RectTransform rect = star.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition = new Vector2(Random.Range(-500, 500), Random.Range(-500, 500));
            }
            yield return new WaitForSeconds(0.05f);
        }

    }
}
