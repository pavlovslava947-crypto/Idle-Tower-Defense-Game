using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PanelAnim : MonoBehaviour
{
    private Tween _panelAnim;
    [SerializeField] private GameObject _panel;

    [Header("Настройки")]

    [SerializeField] private float _scale = 1f;
    [SerializeField] private float _time = 0.5f;

    [Header("Настройки сжатия")]

    [SerializeField] private float _backScale = 0.2f;
    [SerializeField] private float _disableTime = 1f;

    private Coroutine _corutine;

    public void PanelDeparture()
    {
        _panel.transform.DOKill();
        _panelAnim = _panel.transform.DOScale(_scale, _time);
    }

    public void SetBackScale()
    {
        _panel.transform.DOKill();
        _panelAnim = _panel.transform.DOScale(_backScale, _time);

        if (_corutine != null)
        {
            StopCoroutine(_corutine);
        }

        _corutine = StartCoroutine(PanelDisabling());
    }

    private IEnumerator PanelDisabling()
    {
        yield return new WaitForSeconds(_disableTime);

        _panel.SetActive(false);

        _corutine = null;
    }
}
