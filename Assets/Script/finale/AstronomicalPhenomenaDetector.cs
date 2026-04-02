using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AstronomicalPhenomenaDetector : MonoBehaviour
{
    [Header("Corpos Celestes Principais")]
    [SerializeField] private Transform sun;
    [SerializeField] private Transform earth;
    [SerializeField] private Transform moon;

    [Header("Lista de Astros para Alinhamento")]
    [SerializeField] private List<Transform> celestialBodies = new List<Transform>();

    [Header("Configurações de Deteccao")]
    [SerializeField] private float eclipseThreshold = 0.5f;
    [SerializeField] private float alignmentThreshold = 0.3f;

    [Header("UI - TextMeshPro")]
    [SerializeField] private TMP_Text solarEclipseText;
    [SerializeField] private TMP_Text lunarEclipseText;
    [SerializeField] private TMP_Text alignmentText;

    [Header("Visualizacao da Linha")]
    [SerializeField] private bool showAlignmentLine = true;
    [SerializeField] private Color normalLineColor = Color.yellow;
    [SerializeField] private Color eclipseLineColor = Color.red;
    [SerializeField] private Color alignedLineColor = Color.green;

    // Estados
    private bool isSolarEclipse = false;
    private bool isLunarEclipse = false;
    private bool isAligned = false;
    private int alignedCount = 0;

    private LineRenderer lineRenderer;

    // Propriedades publicas
    public bool IsSolarEclipse => isSolarEclipse;
    public bool IsLunarEclipse => isLunarEclipse;
    public bool IsAligned => isAligned;

    void Start()
    {
        SetupLineRenderer();

        // Desativa todos os textos no inicio
        SetTextActive(solarEclipseText, false);
        SetTextActive(lunarEclipseText, false);
        SetTextActive(alignmentText, false);
    }

    void Update()
    {
        CheckSolarEclipse();
        CheckLunarEclipse();
        CheckAlignment();
        UpdateLine();
    }

    void SetupLineRenderer()
    {
        if (!showAlignmentLine) return;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.sortingOrder = -1;
    }

    #region Eclipse Detection

    void CheckSolarEclipse()
    {
        if (sun == null || earth == null || moon == null) return;

        bool eclipseNow = IsObjectBetween(moon, sun, earth);

        if (eclipseNow != isSolarEclipse)
        {
            isSolarEclipse = eclipseNow;
            SetTextActive(solarEclipseText, isSolarEclipse);
        }
    }

    void CheckLunarEclipse()
    {
        if (sun == null || earth == null || moon == null) return;

        bool eclipseNow = IsObjectBetween(earth, sun, moon);

        if (eclipseNow != isLunarEclipse)
        {
            isLunarEclipse = eclipseNow;
            SetTextActive(lunarEclipseText, isLunarEclipse);
        }
    }

    bool IsObjectBetween(Transform middle, Transform source, Transform target)
    {
        if (middle == null || source == null || target == null) return false;

        Vector2 sourcePos = source.position;
        Vector2 middlePos = middle.position;
        Vector2 targetPos = target.position;

        float distanceToLine = DistancePointToLine(middlePos, sourcePos, targetPos);

        float distSourceMiddle = Vector2.Distance(sourcePos, middlePos);
        float distSourceTarget = Vector2.Distance(sourcePos, targetPos);
        bool isBetween = distSourceMiddle < distSourceTarget;

        Vector2 toMiddle = (middlePos - sourcePos).normalized;
        Vector2 toTarget = (targetPos - sourcePos).normalized;
        float alignment = Vector2.Dot(toMiddle, toTarget);

        return distanceToLine < eclipseThreshold && isBetween && alignment > 0.9f;
    }

    #endregion

    #region Alignment Detection

    void CheckAlignment()
    {
        if (sun == null || celestialBodies.Count < 2)
        {
            if (isAligned)
            {
                isAligned = false;
                SetTextActive(alignmentText, false);
            }
            return;
        }

        Transform lastBody = celestialBodies[celestialBodies.Count - 1];
        if (lastBody == null) return;

        Vector2 lineStart = sun.position;
        Vector2 lineEnd = lastBody.position;

        int currentAlignedCount = 0;

        foreach (Transform body in celestialBodies)
        {
            if (body == null) continue;

            if (body == lastBody)
            {
                currentAlignedCount++;
                continue;
            }

            float distance = DistancePointToLine(body.position, lineStart, lineEnd);

            if (distance <= alignmentThreshold)
            {
                float distToSun = Vector2.Distance(lineStart, body.position);
                float distTotal = Vector2.Distance(lineStart, lineEnd);

                if (distToSun <= distTotal)
                {
                    currentAlignedCount++;
                }
            }
        }

        bool allAligned = currentAlignedCount == celestialBodies.Count;

        if (allAligned != isAligned)
        {
            isAligned = allAligned;
            alignedCount = isAligned ? currentAlignedCount : 0;
            SetTextActive(alignmentText, isAligned);
        }
    }

    #endregion

    #region Utilities

    float DistancePointToLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
    {
        Vector2 line = lineEnd - lineStart;
        float lineLength = line.magnitude;

        if (lineLength == 0) return Vector2.Distance(point, lineStart);

        Vector2 lineDir = line / lineLength;
        Vector2 pointToStart = point - lineStart;

        float projection = Mathf.Clamp(Vector2.Dot(pointToStart, lineDir), 0, lineLength);
        Vector2 closestPoint = lineStart + lineDir * projection;

        return Vector2.Distance(point, closestPoint);
    }

    void SetTextActive(TMP_Text text, bool active)
    {
        if (text != null)
        {
            text.gameObject.SetActive(active);
        }
    }

    #endregion

    #region Line Visualization

    void UpdateLine()
    {
        if (!showAlignmentLine || lineRenderer == null) return;

        if (sun == null || celestialBodies.Count == 0)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        Transform lastBody = celestialBodies[celestialBodies.Count - 1];
        if (lastBody == null)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        // Define cor
        Color lineColor = normalLineColor;

        if (isSolarEclipse || isLunarEclipse)
            lineColor = eclipseLineColor;
        else if (isAligned)
            lineColor = alignedLineColor;

        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        // Desenha linha
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, sun.position);
        lineRenderer.SetPosition(1, lastBody.position);
    }

    #endregion
}