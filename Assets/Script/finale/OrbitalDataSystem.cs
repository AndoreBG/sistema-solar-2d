using UnityEngine;
using TMPro;

public class OrbitalDateSystem : MonoBehaviour
{
    [Header("Referência")]
    [SerializeField] private OrbitByMatrixSimulator earthSimulator;

    [Header("Data Inicial")]
    [SerializeField] private int startDay = 1;
    [SerializeField] private int startMonth = 1;
    [SerializeField] private int startYear = 2026;

    [Header("UI")]
    [SerializeField] private TMP_Text dateText;

    [Header("Configuração")]
    [SerializeField] private string dateFormat = "dd/MM/yyyy";

    // Variáveis internas
    private float previousAngle = 0f;
    private float totalAngle = 0f;
    private System.DateTime startDate;
    private System.DateTime currentDate;

    // Propriedades públicas
    public System.DateTime CurrentDate => currentDate;
    public int CurrentDay => currentDate.Day;
    public int CurrentMonth => currentDate.Month;
    public int CurrentYear => currentDate.Year;

    void Start()
    {
        startDate = new System.DateTime(startYear, startMonth, startDay);
        currentDate = startDate;
        previousAngle = earthSimulator.GetCurrentOrbitAngle();

        UpdateUI();
    }

    void Update()
    {
        CalculateDate();
        UpdateUI();
    }

    void CalculateDate()
    {
        if (earthSimulator == null) return;

        float currentAngle = earthSimulator.GetCurrentOrbitAngle();

        // Calcula diferença de ângulo (considera volta completa)
        float angleDelta = currentAngle - previousAngle;

        // Acumula ângulo total
        totalAngle += angleDelta;
        previousAngle = currentAngle;

        // 360° = 1 ano = 365 dias
        // 1° = 365/360 dias ≈ 1.014 dias
        float totalDays = (totalAngle / 360f) * 365f;

        // Calcula nova data
        currentDate = startDate.AddDays(totalDays);
    }

    void UpdateUI()
    {
        if (dateText != null)
        {
            dateText.text = currentDate.ToString(dateFormat);
        }
    }
}