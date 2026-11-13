using UnityEngine;

public class BeatReactive : MonoBehaviour
{
    [Header("Настройки звука")]
    public AudioSource audioSource;
    [Range(0, 63)] [SerializeField] private int bassBand = 2; 
    [SerializeField] private float beatThreshold = 0.15f;     

    [Header("Настройки эффекта")]
    [SerializeField] private Vector3 beatScale = new Vector3(1.5f, 1.5f, 1.5f); 
    [SerializeField] private float smoothSpeed = 10f;                          

    private Vector3 initialScale;
    private float[] spectrumData = new float[64];

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = FindAnyObjectByType<AudioSource>();
        }
    }

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        
        float bassValue = spectrumData[bassBand];

        
        if (bassValue > beatThreshold)
        {
            transform.localScale = beatScale;
        }
        else
        {
            
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * smoothSpeed);
        }
    }
}
