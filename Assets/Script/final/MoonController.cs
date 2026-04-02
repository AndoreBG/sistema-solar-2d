using UnityEngine;

public class MoonController : MonoBehaviour
{
    [SerializeField] private EarthController earth;

    [Header("Orbita Eliptica")]
    [SerializeField] private float semiMajorAxis = 2f;    // Semi-eixo maior (a)
    [SerializeField] private float semiMinorAxis = 1.5f;  // Semi-eixo menor (b)
    [SerializeField] private float orbitSpeed = 60f;

    [Header("Escala")]
    [SerializeField] private float scale = 0.25f;

    private float orbitAngle = 0f;

    void Update()
    {
        if (earth == null) return;

        orbitAngle += orbitSpeed * (Time.deltaTime);    // Incrementa a velocidade de orbita com base na velocidade do tempo

        // Posicao na elipse: x = a*cos(θ), y = b*sin(θ)
        float rad = orbitAngle * Mathf.Deg2Rad;
        float ellipseX = semiMajorAxis * Mathf.Cos(rad);
        float ellipseY = semiMinorAxis * Mathf.Sin(rad);

        // Rotacao sincronizada: mesma face sempre voltada para a Terra
        // A rotacao própria = ângulo orbital
        float syncRotation = orbitAngle;

        Matrix4x4[] matrices = {
            MatrixTransformationSystem.Scale(scale, scale),
            MatrixTransformationSystem.Rotation(syncRotation),
            MatrixTransformationSystem.Translation(ellipseX, ellipseY),
            MatrixTransformationSystem.Translation(earth.Position.x, earth.Position.y)
        };

        Matrix4x4 composed = MatrixTransformationSystem.ComposeMatrices(matrices);
        MatrixTransformationSystem.Apply(transform, composed);
    }
}