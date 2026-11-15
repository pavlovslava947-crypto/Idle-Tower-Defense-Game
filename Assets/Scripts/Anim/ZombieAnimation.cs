using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }


    public void SetSpeed(float normalizedSpeed)
    {
        _animator.SetFloat("Speed", normalizedSpeed, 0.1f, Time.deltaTime);
    }

    public void SetPunch()
    {
        _animator.SetTrigger("Punch");
    }

    public void SetDeath()
    {
        _animator.SetTrigger("Death");
    }
}
