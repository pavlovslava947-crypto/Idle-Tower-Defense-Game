using UnityEngine;
using YG;
public class ProgressSaver : MonoBehaviour
{
    public static ProgressSaver Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int ExpSave(int starAmount)
    {
        YG2.saves._expCount = starAmount;
        YG2.SaveProgress();

        return YG2.saves._expCount;
    }

    public float VolumeSaver(float saveValue)
    {
        YG2.saves._playerVolume = saveValue;
        YG2.SaveProgress();

        return YG2.saves._playerVolume;
    }
}
