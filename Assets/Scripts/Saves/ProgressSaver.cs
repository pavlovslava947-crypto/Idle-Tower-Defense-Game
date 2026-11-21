using UnityEngine;
using YG;
public class ProgressSaver : MonoBehaviour
{
    public static ProgressSaver Instance { get; private set; }

    public int CurrentExp => YG2.saves._expCount;
    public float CurrentVolume => YG2.saves._playerVolume;

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

    public void ExpSave(int starAmount)
    {
        YG2.saves._expCount += starAmount;
        YG2.SaveProgress();
    }

    public void VolumeSaver(float saveValue)
    {
        YG2.saves._playerVolume = saveValue;
        YG2.SaveProgress();
    }
}
