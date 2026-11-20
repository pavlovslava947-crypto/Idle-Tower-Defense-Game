using UnityEngine;
using DG.Tweening;
using System;

public class PanelAnim : MonoBehaviour
{
    private Tween _panelAnim;
    [SerializeField] private GameObject _panel;

    [Header("Настройки")]

    [SerializeField] private float _scale = 1f;
    [SerializeField] private float _time = 0.5f;
    
    void Start()
    {
        _panel.transform.DOKill();
        _panelAnim = _panel.transform.DOScale(_scale, _time);
    }

    public void SetScale()
    {
        if (_panel == null) return;
        _panel.transform.localScale = new Vector2(_scale, _scale);
    }
}
