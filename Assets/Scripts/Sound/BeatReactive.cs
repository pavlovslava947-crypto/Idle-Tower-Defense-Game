using UnityEngine;

public class BeatReactive : MonoBehaviour
{
    [Header("Настройки звука")]
    public AudioSource audioSource;
    [SerializeField] private float beatThreshold = 0.25f; // Порог срабатывания (подберите под трек)
    [SerializeField] private float sensitivity = 2.0f;    // Усиление сигнала

    [Header("Настройки эффекта")]
    [SerializeField] private Vector3 beatScale = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private float smoothSpeed = 10f;

    private Vector3 initialScale;
    private float[] sampleData = new float[256];

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
        // Защита от пустого источника или паузы при перезагрузке сцены
        if (audioSource == null || !audioSource.isPlaying || audioSource.clip == null)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * smoothSpeed);
            return;
        }

        float currentLoudness = GetCurrentLoudness() * sensitivity;

        if (currentLoudness > beatThreshold)
        {
            transform.localScale = beatScale;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * smoothSpeed);
        }
    }

    private float GetCurrentLoudness()
    {
        int currentSample = audioSource.timeSamples;
        int totalSamples = audioSource.clip.samples;

        // Защита от выхода за границы массива в самом конце трека
        if (currentSample + sampleData.Length >= totalSamples)
            return 0f;

        // Читаем кусок звуковой волны напрямую из клипа (работает в WebGL)
        audioSource.clip.GetData(sampleData, currentSample);

        float sum = 0f;
        for (int i = 0; i < sampleData.Length; i++)
        {
            // Берём квадрат амплитуды (RMS - среднеквадратичная громкость)
            sum += sampleData[i] * sampleData[i];
        }

        return Mathf.Sqrt(sum / sampleData.Length);
    }
}
