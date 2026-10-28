using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string mainScene = "GamePlay";
    private float _timer;
    [SerializeField] private float _maxTime = 2f;
    private bool _isStartGame = false;

    private void Start()
    {
        _timer = _maxTime;
    }

    private void Update()
    {
        if (_timer > 0 && _isStartGame)
        {
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0)
        {
            LoadScene();
        }
    }




    public void Scene()
    {
        _isStartGame = true;
       
    }

    void LoadScene()
    {
        SceneManager.LoadScene(mainScene);
    }
}
