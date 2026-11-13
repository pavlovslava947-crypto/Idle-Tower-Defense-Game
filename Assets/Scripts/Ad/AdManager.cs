
using UnityEngine;
using YG;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void AdCall()
    {
        YG2.InterstitialAdvShow();
    }
}
