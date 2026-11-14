
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
   
    public static SoundManager Instance { get; private set; }

    [Header("ЗВУКИ")]

    [SerializeField] private AudioClip[] _sounds = new AudioClip[0];
    private AudioSource _audioSource;

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
}
