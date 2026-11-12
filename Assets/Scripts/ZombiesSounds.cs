using UnityEngine;

public class ZombiesSounds : MonoBehaviour
{
    [SerializeField] private float _firstRange;
    [SerializeField] private float _secondRange;

    private float maxTime;
    private float Timer;
    void Start()
    {
        maxTime = Random.Range(_firstRange, _secondRange);
        Timer = maxTime;
    }

    void Update()
    {
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
        if (Timer <= 0)
        {
            SoundManager.Instance.PlaySound(3);
            maxTime = Random.Range(_firstRange, _secondRange);
            Timer = maxTime;
        }
    }
}
