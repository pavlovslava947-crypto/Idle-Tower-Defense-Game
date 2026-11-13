using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuLoader : MonoBehaviour
{
    private bool _isLoad = false;
    private string _menu = "MainMenuScene";
    private string _gamePlay = "GamePlay";
 
    private void Start()
    {
        _isLoad = false; // на всякий случай
    }

    public void OnMainMenuLoad()
    {
        if(_isLoad) return;
        StartCoroutine(MainMenuLoad());
    }

    public void OnRepeatSassion()
    {
        if (_isLoad) return;
        StartCoroutine(RepeatGameSassion());
    }

    private IEnumerator MainMenuLoad()
    {
        _isLoad = true;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_menu);
      
        yield break;
    }

    private IEnumerator RepeatGameSassion()
    {
        _isLoad = true;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_gamePlay);

        yield break;
    }
}
