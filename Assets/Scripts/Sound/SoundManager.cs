
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
   
    public static SoundManager Instance { get; private set; }

    [Header("ЗВУКИ")]

    [SerializeField] private AudioClip[] _sounds = new AudioClip[0];
    private AudioSource _audioSource;
    private float _lastPlayTime;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        if (this == null || gameObject == null) return;

        if (_audioSource == null) return;

        if (index < 0 || index >= _sounds.Length)
        {
            Debug.LogError("Такого звука нет. Думай.");

            return;
        }

        if (_sounds[index]  == null)
        {
            Debug.LogError($"ячейка {index} не назначена в инспекторе, пж прикрепи");

            return;
        }

        _audioSource.PlayOneShot(_sounds[index]);
    }

    public void RandomPlay(int firstRange, int secondRange)
    {
        if (this == null || gameObject == null) return;

        if (_audioSource == null) return;

        if (firstRange < 0 || firstRange >= _sounds.Length || secondRange < 0 || secondRange >= _sounds.Length)
        {
            Debug.LogError("Такого звука нет. Думай.");

            return;
        }

        int index = Random.Range(firstRange, secondRange);

        if (_sounds[index] == null)
        {
            Debug.LogError($"ячейка {index} не назначена в инспекторе, пж прикрепи");

            return;
        }

        

        _audioSource.PlayOneShot(_sounds[index]);
    }

    public void PlaySound(int index, float volume, float cooldown)
    {
        if (this == null || gameObject == null) return;

        if (_audioSource == null) return;

        if (cooldown > 0f && Time.time - _lastPlayTime < cooldown)
        {
            return;
        }

        if (index < 0 || index >= _sounds.Length)
        {
            Debug.LogError("Такого звука нет. Думай.");

            return;
        }

        if (_sounds[index] == null)
        {
            Debug.LogError($"ячейка {index} не назначена в инспекторе, пж прикрепи");

            return;
        }

        if (cooldown > 0f)
        {
            _lastPlayTime = Time.time;
        }

        _audioSource.PlayOneShot(_sounds[index], volume);
    }

    public void RandomPlay(int firstRange, int secondRange, float volume, float cooldown)
    {
        if (this == null || gameObject == null) return;

        if (_audioSource == null) return;

        if (cooldown > 0f && Time.time - _lastPlayTime < cooldown)
        {
            return;
        }

        if (firstRange < 0 || firstRange >= _sounds.Length || secondRange < 0 || secondRange >= _sounds.Length)
        {
            Debug.LogError("Такого звука нет. Думай.");

            return;
        }

        int index = Random.Range(firstRange, secondRange);

        if (_sounds[index] == null)
        {
            Debug.LogError($"ячейка {index} не назначена в инспекторе, пж прикрепи");

            return;
        }

        if(cooldown > 0f)
        {
            _lastPlayTime = Time.time;
        }

        _audioSource.PlayOneShot(_sounds[index], volume);
    }
}
