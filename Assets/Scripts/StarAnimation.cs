using DG.Tweening;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    [SerializeField] private float _scale = 0.9f;
    [SerializeField] private float _speed = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CurrentStar"))
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
            transform.DOScale(_scale, _speed).SetLoops(2, LoopType.Yoyo);
        }
    }
}
