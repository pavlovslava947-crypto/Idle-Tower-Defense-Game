using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string mainScene = "GamePlay";
    [SerializeField] private string EnviromentScene = "Enviroment";
    private float _timer;
    [SerializeField] private float _maxTime = 2f;
    private bool _isStartGmae = false;

    private void Start()
    {
        _timer = _maxTime;
    }

    private void Update()
    {
        if (_timer > 0 && _isStartGmae)
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
        _isStartGmae = true;
       
    }

    void LoadScene()
    {
        SceneManager.LoadScene(mainScene);
        SceneManager.LoadScene(EnviromentScene);
    }
}
