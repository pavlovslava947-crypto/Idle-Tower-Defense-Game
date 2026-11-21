using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DevPlayerSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private string _initParameter = "SFX";
    private float _currentVolume;

    private void Start()
    {
        float savedValue = ProgressSaver.Instance.CurrentVolume;
        _currentVolume = savedValue;
        _volumeSlider.value = savedValue;
        ApplyVolumeToMixer(savedValue);
    }

    private void OnEnable()
    {
        _volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnDisable()
    {
        _volumeSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnSliderChanged(float sliderValue)
    {
        _currentVolume = Mathf.Clamp(sliderValue, 0.0001f, 1f);
        ApplyVolumeToMixer(_currentVolume);
    }

    public void SaveCurrentVolume()
    {
        ProgressSaver.Instance.VolumeSaver(_currentVolume);
    }

    private void ApplyVolumeToMixer(float value)
    {
        float newVolume = Mathf.Log10(value) * 20f;
        _audioMixer.SetFloat(_initParameter, newVolume);
    }
}
