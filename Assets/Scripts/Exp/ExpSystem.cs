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
    private int _uiNumbers;


    void Start()
    {
        _uiNumbers = ProgressSaver.Instance.CurrentExp;
        _scoreText.text = _uiNumbers.ToString();

        _score = _wave._wave;
        ProgressSaver.Instance.ExpSave(_score); // сейв менеджер

        StartCoroutine(StarSpawn(_score));
    }




    public void UpdateUI()
    {
        _uiNumbers++;
        _scoreText.text = _uiNumbers.ToString();
    }

    private IEnumerator StarSpawn(int starsForSpawn)
    {
        for (int i = 0; i < starsForSpawn; i++)
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
