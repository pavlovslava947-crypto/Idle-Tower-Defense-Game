using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FillImageController : MonoBehaviour
{
    private Image _fillImage;
    [SerializeField] private float _durationSpeed = 0.5f;
    [SerializeField] private bool _playFadeOut = true;

    private void Awake()
    {
        if (!TryGetComponent<Image>(out _fillImage))
        {
            Debug.LogError("Скрипт должен быть на одном объекте с компонентом Image");
        }

        if( _playFadeOut)
        {
            FadeOut();
        }
    }

    public void Gain()
    {
        if (_fillImage == null) return;

        _fillImage.enabled = true;
        _fillImage.DOFade(1f, _durationSpeed);
    }

    public void FadeOut()
    {
        if (_fillImage == null) return;

        _fillImage.enabled = true;
        Color alphaColor = _fillImage.color;
        alphaColor.a = 1;
        _fillImage.color = alphaColor;

        StartCoroutine(FadeOutCorutine(_durationSpeed));
    }

    private IEnumerator FadeOutCorutine(float durationSpeed)
    {
        _fillImage.DOFade(0f, _durationSpeed);

        yield return new WaitForSeconds(durationSpeed);

        _fillImage.enabled = false;
    }
}
