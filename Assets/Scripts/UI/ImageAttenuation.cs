using UnityEngine;
using UnityEngine.UI;

public class ImageAttenuation : MonoBehaviour
{
    [SerializeField] private Image _attenuationImage;
    [SerializeField] private float _attenuationSpeed = 2f;
    private float _timer;
    private Color _imageColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timer = _attenuationSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        _imageColor = _attenuationImage.color;
        _imageColor.a = _timer / _attenuationSpeed;

        _attenuationImage.color = _imageColor;

    }
}
