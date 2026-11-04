using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuLoader : MonoBehaviour
{
    private bool _isLoad = false;
    private string _menu = "MainMenuScene";
    private WaitForSeconds _timer = new WaitForSeconds(0.5f);

    public void OnMainMenuLoad()
    {
        if(_isLoad) return;
        StartCoroutine(MainMenuLoad());
    }

    private IEnumerator MainMenuLoad()
    {
        _isLoad = true;

        yield return _timer;

        SceneManager.LoadScene(_menu);
      
        yield break;
    }
}
