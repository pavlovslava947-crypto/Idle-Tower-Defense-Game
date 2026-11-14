using UnityEngine;

public class ZombiesSounds : MonoBehaviour
{
    [SerializeField] private float _firstRange;
    [SerializeField] private float _secondRange;
     private AudioSource _source;
    private float maxTime;
    private float Timer;

    private void Awake()
    {
        _source = FindAnyObjectByType<AudioSource>();
    }
    void Start()
    {
        maxTime = Random.Range(_firstRange, _secondRange);
        Timer = maxTime;
    }

    void Update()
    {
        if (SoundManager.Instance == null) return;

        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            PlaySound();
        }

    }
    void PlaySound()
    {
        if (_source != null && Timer <= 0 && !_source.isPlaying)
        {
            SoundManager.Instance.PlaySound(3);
            maxTime = Random.Range(_firstRange, _secondRange);
            Timer = maxTime;
        }
    }
}
