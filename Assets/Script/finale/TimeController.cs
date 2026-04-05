using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [Header("Referencia do Slider")]
    [SerializeField] private Slider timeSlider;

    [Header("Referencia do CheckButton")]
    [SerializeField] private Toggle checkButton;

    [Header("Referencia do Script")]
    [SerializeField] private AstronomicalPhenomenaDetector detector;

    [Header("Escala de Tempo")]
    [SerializeField] private float minTimeScale = 0.001f;
    [SerializeField] private float maxTimeScale = 100f;

    private bool isPaused = false;
    private bool pausedByAlignment = false;  // Nova flag
    private float currentTimeScale;

    void Start()
    {
        UpdateTimeScale();
        timeSlider.onValueChanged.AddListener(delegate { OnSliderChanged(); });
    }

    void Update()
    {
        HandleManualPause();
        HandleAlignmentPause();
    }

    void HandleManualPause()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    void HandleAlignmentPause()
    {
        // Só verifica se o toggle está ativo
        if (!checkButton.isOn)
        {
            // Se estava pausado pelo alinhamento, despausa
            if (pausedByAlignment)
            {
                pausedByAlignment = false;
                SetPaused(false);
            }
            return;
        }

        // Toggle ativo: verifica alinhamento
        if (detector.IsAligned)
        {
            // Pausa se ainda não pausou pelo alinhamento
            if (!pausedByAlignment)
            {
                pausedByAlignment = true;
                SetPaused(true);
            }
        }
        else
        {
            // Despausa quando alinhamento termina
            if (pausedByAlignment)
            {
                pausedByAlignment = false;
                SetPaused(false);
            }
        }
    }

    void TogglePause()
    {
        SetPaused(!isPaused);

        // Se despausou manualmente, reseta flag de alinhamento
        if (!isPaused)
        {
            pausedByAlignment = false;
        }
    }

    void SetPaused(bool paused)
    {
        isPaused = paused;
        Time.timeScale = isPaused ? 0f : currentTimeScale;
    }

    void OnSliderChanged()
    {
        UpdateTimeScale();

        if (!isPaused)
        {
            Time.timeScale = currentTimeScale;
        }
    }

    void UpdateTimeScale()
    {
        float t = (timeSlider.value - 1f) / 99f;
        currentTimeScale = Mathf.Lerp(minTimeScale, maxTimeScale, t);
    }
}