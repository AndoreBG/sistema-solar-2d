using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRenderer : MonoBehaviour
{
    [Header("Configuracao da Orbita")]
    [SerializeField] private Transform centerObject;     // Centro da orbita
    [SerializeField] private float semiMajorAxis = 5f;   // Semi-eixo maior (a)
    [SerializeField] private float semiMinorAxis = 5f;   // Semi-eixo menor (b) - igual a 'a' para circulo
    [SerializeField] private int segments = 64;          // Quantidade de segmentos

    [Header("Visual")]
    [SerializeField] private float lineWidth = 0.05f;
    [SerializeField] private Color orbitColor = Color.white;

    private LineRenderer lineRenderer;

    void Start()
    {
        SetupLineRenderer();
        DrawOrbit();
    }

    void Update()
    {
        // Atualiza posicao se o centro se move (para a Lua seguir a Terra)
        if (centerObject != null)
        {
            transform.position = centerObject.position;
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

        // Material simples
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = orbitColor;
        lineRenderer.endColor = orbitColor;
    }

    void DrawOrbit()
    {
        for (int i = 0; i <= segments; i++)
        {
            float angle = (float)i / segments * 360f * Mathf.Deg2Rad;
            float x = semiMajorAxis * Mathf.Cos(angle);
            float y = semiMinorAxis * Mathf.Sin(angle);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}