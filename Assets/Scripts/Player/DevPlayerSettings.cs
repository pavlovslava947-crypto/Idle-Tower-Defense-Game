using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DevPlayerSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private string _initParameter = "SFX";

    private void Start()
    {
        float savedValue = ProgressSaver.Instance.VolumeSaver(0.75f);
        _volumeSlider.value = savedValue;
        SetVolume(savedValue);
    }

    private void OnEnable()
    {
        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        float saveValue = Mathf.Clamp(sliderValue, 0.0001f, 1f);
        float newVolume = Mathf.Log10(saveValue) * 20f;
        _audioMixer.SetFloat(_initParameter, newVolume);
        ProgressSaver.Instance.VolumeSaver(saveValue);
    }

    private void OnDisable()
    {
        _volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}
