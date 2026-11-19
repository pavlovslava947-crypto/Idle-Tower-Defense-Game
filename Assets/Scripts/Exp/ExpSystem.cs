using System.Collections;
using TMPro;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private CurrentWave _wave;
    [SerializeField] private GameObject _expStarPrefab;
    [SerializeField] private Transform _canvas;
    [SerializeField] private int _score = 0;
    private int _newScore;
    private int _uiNumbers = 0;


    void Start()
    {
        _score = _wave._wave;
       
        _newScore = ProgressSaver.Instance.ExpSave(_score); // сейв менеджер

        StartCoroutine(StarSpawn());
    }




    public void UpdateUI()
    {
        _uiNumbers++;
        _scoreText.text = _uiNumbers.ToString();
    }

    private IEnumerator StarSpawn()
    {
        for (int i = 0; i < _newScore; i++)
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
