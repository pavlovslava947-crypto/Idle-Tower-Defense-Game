using UnityEngine;
using DG.Tweening;
using System;

public class PanelAnim : MonoBehaviour
{
    private Tween _panelAnim;

    [Header("Настройки")]

    [SerializeField] private float _scale = 1f;
    [SerializeField] private float _time = 0.5f;
    
    void Start()
    {
        transform.DOKill();
        _panelAnim = transform.DOScale(_scale, _time);
    }
}
