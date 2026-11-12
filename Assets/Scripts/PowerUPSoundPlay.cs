using UnityEngine;

public class PowerUPSoundPlay : MonoBehaviour
{
    [SerializeField] private float maxTime = 0.5f;
    private float Timer;
    void Start()
    {
        Timer = maxTime;
    }

    void Update()
    {
        if(Timer > 0)
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
        if(Timer <= 0)
        {
            SoundManager.Instance.PlaySound(2);
            Timer = maxTime;
        }
    }
}
