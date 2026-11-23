using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DevPlayerSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private string _initParameter = "SFX";
    private float _currentVolume;


    private void OnEnable()
    {
        volumeSlider.onValueChanged.AddListener(OnSliderChanged);

        float rawSaved = (_initParameter == "Music") ? ProgressSaver.Instance.CurrentMusicVolume : ProgressSaver.Instance.CurrentSFXVolume;

        float savedValue = Mathf.Clamp(rawSaved, 0.0001f, 1f);
        _currentVolume = savedValue;
        volumeSlider.value = _currentVolume;
        ApplyVolumeToMixer(savedValue);
    }

    private void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnSliderChanged(float sliderValue)
    {
        _currentVolume = Mathf.Clamp(sliderValue, 0.0001f, 1f);
        ApplyVolumeToMixer(_currentVolume);
    }

    public void SaveCurrentVolume()
    {
        if (_initParameter == "Music")
        {
            ProgressSaver.Instance.SaveMusicVolume(_currentVolume);
        }

        else
        {
            ProgressSaver.Instance.SaveSFXVolume(_currentVolume);
        }
    }

    private void ApplyVolumeToMixer(float value)
    {
        float newVolume = Mathf.Log10(value) * 20f;
        _audioMixer.SetFloat(_initParameter, newVolume);
    }
}
