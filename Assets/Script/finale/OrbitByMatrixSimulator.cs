using UnityEngine;

public class OrbitByMatrixSimulator : MonoBehaviour
{
    public enum OrbitType
    {
        Circular,
        Elliptical
    }

    [Header("Tipo de Órbita")]
    [SerializeField] private OrbitType orbitType = OrbitType.Circular;

    [Header("Objeto Central")]
    [SerializeField] private Transform centerObject;

    [Header("Configuração da Órbita")]
    [SerializeField] private float initialAngle = 0f;           // Posição inicial (graus)
    [SerializeField] private float orbitRadius = 5f;            // Raio (circular) ou semi-eixo maior (elipse)
    [SerializeField] private float semiMinorAxis = 3f;          // Semi-eixo menor (apenas para elipse)
    [SerializeField] private float orbitSpeed = 30f;            // Velocidade orbital (graus/segundo)

    [Header("Rotação Própria")]
    [SerializeField] private float rotationSpeed = 100f;        // Velocidade de rotação própria
    [SerializeField] private bool synchronizedRotation = false; // Rotação sincronizada (como a Lua)

    [Header("Escala")]
    [SerializeField] private float scale = 1f;

    // Variáveis internas
    private float currentOrbitAngle;
    private float currentSelfRotation;

    // Propriedades públicas (para o TrailRenderer acessar)
    public OrbitType CurrentOrbitType => orbitType;
    public float OrbitRadius => orbitRadius;
    public float SemiMajorAxis => orbitRadius;
    public float SemiMinorAxis => orbitType == OrbitType.Elliptical ? semiMinorAxis : orbitRadius;
    public Transform CenterObject => centerObject;

    void Start()
    {
        currentOrbitAngle = initialAngle;
        currentSelfRotation = 0f;
    }

    void Update()
    {
        UpdateAngles();
        ApplyTransformation();
    }

    void UpdateAngles()
    {
        currentOrbitAngle += orbitSpeed * Time.deltaTime;

        if (synchronizedRotation)
        {
            currentSelfRotation = currentOrbitAngle;
        }
        else
        {
            currentSelfRotation += rotationSpeed * Time.deltaTime;
        }
    }

    void ApplyTransformation()
    {
        // Calcula posição na órbita
        Vector2 orbitPosition = CalculateOrbitPosition();

        // Posição do centro (ou origem se não houver objeto central)
        Vector2 centerPosition = centerObject != null
            ? (Vector2)centerObject.position
            : Vector2.zero;

        // Monta as matrizes de transformação
        Matrix4x4[] matrices = {
            MatrixTransformationSystem.Scale(scale, scale),
            MatrixTransformationSystem.Rotation(currentSelfRotation),
            MatrixTransformationSystem.Translation(orbitPosition.x, orbitPosition.y),
            MatrixTransformationSystem.Translation(centerPosition.x, centerPosition.y)
        };

        // Compõe e aplica
        Matrix4x4 composed = MatrixTransformationSystem.ComposeMatrices(matrices);
        MatrixTransformationSystem.Apply(transform, composed);
    }

    Vector2 CalculateOrbitPosition()
    {
        float rad = currentOrbitAngle * Mathf.Deg2Rad;

        switch (orbitType)
        {
            case OrbitType.Circular:
                return new Vector2(
                    orbitRadius * Mathf.Cos(rad),
                    orbitRadius * Mathf.Sin(rad)
                );

            case OrbitType.Elliptical:
                return new Vector2(
                    orbitRadius * Mathf.Cos(rad),      // Semi-eixo maior
                    semiMinorAxis * Mathf.Sin(rad)     // Semi-eixo menor
                );

            default:
                return Vector2.zero;
        }
    }

    // Retorna a posição atual do astro
    public Vector2 GetCurrentPosition()
    {
        return transform.position;
    }

    // Retorna o ângulo atual da órbita
    public float GetCurrentOrbitAngle()
    {
        return currentOrbitAngle;
    }
}