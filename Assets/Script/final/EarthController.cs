using UnityEngine;

public class EarthController : MonoBehaviour
{
    [SerializeField] private float orbitRadius = 5f;
    [SerializeField] private float orbitSpeed = 30f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float scale = 0.5f;

    private float orbitAngle = 0f;
    private float selfRotation = 0f;

    public Vector2 Position => transform.position;

    void Update()
    {
        orbitAngle   += orbitSpeed    * (Time.deltaTime);   // Incrementa a velocidade de orbita com base na velocidade do tempo
        selfRotation += rotationSpeed * (Time.deltaTime);   // Incrementa a velocidade da rotacao com base na velocidade do tempo

        Matrix4x4[] matrices = {
            MatrixTransformationSystem.Scale(scale, scale),
            MatrixTransformationSystem.Rotation(selfRotation),
            MatrixTransformationSystem.Translation(orbitRadius, 0),
            MatrixTransformationSystem.Rotation(orbitAngle)
        };

        Matrix4x4 composed = MatrixTransformationSystem.ComposeMatrices(matrices);
        MatrixTransformationSystem.Apply(transform, composed);
    }
}