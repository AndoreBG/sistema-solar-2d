using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitTrailRenderer : MonoBehaviour
{
    [Header("Fonte dos Dados")]
    [SerializeField] private OrbitByMatrixSimulator orbitSimulator;

    [Header("Visual")]
    [SerializeField] private int segments = 64;
    [SerializeField] private float lineWidth = 0.05f;
    [SerializeField] private Color lineColor = Color.white;

    private LineRenderer lineRenderer;

    void Start()
    {
        if (orbitSimulator == null)
        {
            Debug.LogError("OrbitTrailRenderer: Nenhum OrbitByMatrixSimulator definido!");
            return;
        }

        SetupLineRenderer();
        DrawOrbit();
    }

    void Update()
    {
        // Atualiza posição para seguir o objeto central
        if (orbitSimulator != null && orbitSimulator.CenterObject != null)
        {
            transform.position = orbitSimulator.CenterObject.position;
        }
    }

    void SetupLineRenderer()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;

        // Material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    void DrawOrbit()
    {
        float semiMajorAxis = orbitSimulator.SemiMajorAxis;
        float semiMinorAxis = orbitSimulator.SemiMinorAxis;

        for (int i = 0; i <= segments; i++)
        {
            float angle = (float)i / segments * 360f * Mathf.Deg2Rad;

            float x = semiMajorAxis * Mathf.Cos(angle);
            float y = semiMinorAxis * Mathf.Sin(angle);

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    // Atualiza visual em tempo de execução
    public void UpdateVisual()
    {
        if (lineRenderer == null) return;

        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        DrawOrbit();
    }

    // Para atualizar via Inspector
    void OnValidate()
    {
        if (Application.isPlaying && lineRenderer != null)
        {
            UpdateVisual();
        }
    }
}