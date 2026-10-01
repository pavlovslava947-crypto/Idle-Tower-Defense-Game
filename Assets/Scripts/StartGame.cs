using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string mainScene = "GamePlay";
    [SerializeField] private string EnviromentScene = "Enviroment";

    public void Scene()
    {
        SceneManager.LoadScene(mainScene);
        SceneManager.LoadScene(EnviromentScene);
    }
}
