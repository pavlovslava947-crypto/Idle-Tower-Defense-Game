using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class ButtonAnim : MonoBehaviour
{
    [Header("До какого скейла сожметься кнопка")]
    [SerializeField] private float _buttonScale = 0.9f;
    [Header("За сколько секунд сожмется кнопка")]
    [SerializeField] private float _buttonScaleSpeed = 0.5f;
    public void AnimationClick()
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOScale(_buttonScale, _buttonScaleSpeed).SetLoops(2 , LoopType.Yoyo);
        SoundManager.Instance.PlaySound(1);
    }
}
