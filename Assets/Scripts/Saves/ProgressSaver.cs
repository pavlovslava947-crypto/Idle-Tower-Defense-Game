using UnityEngine;
using YG;
public class ProgressSaver : MonoBehaviour
{
    public static ProgressSaver Instance { get; private set; }

    public int CurrentExp => YG2.saves._expCount;
    public float CurrentSFXVolume => YG2.saves._sfxVolume;
    public float CurrentMusicVolume => YG2.saves._musicVolume;

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
    public void SaveSFXVolume(float saveValue)
    {
        YG2.saves._sfxVolume = saveValue;
        YG2.SaveProgress();
    }

    public void SaveMusicVolume(float saveValue)
    {
        YG2.saves._musicVolume = saveValue;
        YG2.SaveProgress();
    }
}
