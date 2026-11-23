using System.Collections;
using TMPro;
using UnityEngine;

public class SoundtrackFadeOut : MonoBehaviour
{
    [SerializeField] private AudioClip[] _soundtracks = new AudioClip[0];
    [SerializeField] private float _durationSpeed = 3f;

    [Header("БАШНЯ")]
    [SerializeField] private TowerHealth _towerHealth;

    [Header("Панель настроек для инициализции слайдеров")]

    [SerializeField] private GameObject _settingsPanel;
    private AudioSource _audioSource;
    private bool _isFading = false;
    private int _index;
    private float _defaultVolume;
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if( _audioSource != null)
        {
            _defaultVolume = _audioSource.volume;
        }
    }

    private void Start()
    {
        _towerHealth.OnTowerDestroyed += OnPlayerDie;

        PlayNextSound();
        StartCoroutine(InitPanelsSound());
    }

    void Update()
    {
        if(_isFading || _audioSource == null || _soundtracks == null || !_audioSource.isPlaying)
        {
            return;
        }

        float remainingTime = _audioSource.clip.length - _audioSource.time;

        if(remainingTime <= _durationSpeed)
        {
            StartCoroutine(SoundtrackFade());
        }
    }

    private IEnumerator SoundtrackFade()
    {
        _isFading = true;
        float startVolume = _audioSource.volume;
      
        for(float i = 0; i < _durationSpeed; i += Time.deltaTime)
        {
            _audioSource.volume = Mathf.Lerp(startVolume, 0f, i / _durationSpeed);

            yield return null;
        }

        _audioSource.volume = 0f;

        _audioSource.Stop();

        PlayNextSound();

        _isFading = false;
    }

    private void PlayNextSound()
    {
        _index = Random.Range(0, _soundtracks.Length);

        _audioSource.clip = _soundtracks[_index];
        _audioSource.volume = _defaultVolume;
        _audioSource.Play();
    }

    private void OnPlayerDie()
    {
        _audioSource.Stop();
        SoundManager.Instance.PlaySound(13, 0.5f, 0);

        _towerHealth.OnTowerDestroyed -= OnPlayerDie;

        this.enabled = false;
        return;
    }

    private IEnumerator InitPanelsSound()
    {
        _settingsPanel.SetActive(true);

        yield return null;

        _settingsPanel.SetActive(false);
    }
}
