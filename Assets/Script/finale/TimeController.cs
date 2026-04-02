using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [Header("Referencia do Slider")]
    [SerializeField] private Slider timeSlider;

    [Header("Escala de Tempo")]
    [SerializeField] private float minTimeScale = 0.001f;
    [SerializeField] private float maxTimeScale = 10f;

    private bool isPaused = false;
    private float currentTimeScale;

    void Start()
    {
        UpdateTimeScale();
        timeSlider.onValueChanged.AddListener(delegate { OnSliderChanged(); });
    }

    void Update()
    {
        // Pressionar espaco pausa/despausa
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = currentTimeScale;
            }
        }
    }

    void OnSliderChanged()
    {
        UpdateTimeScale();

        // So atualiza se nao estiver pausado
        if (!isPaused)
        {
            Time.timeScale = currentTimeScale;
        }
    }

    void UpdateTimeScale()
    {
        // Normaliza o valor do slider (1–100 → 0–1)
        float t = (timeSlider.value - 1f) / 99f;

        // Interpola entre 0.001f e 10f
        currentTimeScale = Mathf.Lerp(minTimeScale, maxTimeScale, t);
    }
}