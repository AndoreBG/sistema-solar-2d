using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float scale = 2f;

    private float rotation = 0f;

    void Update()
    {
        rotation += rotationSpeed * (Time.deltaTime); // Incrementa a rotacao com base na velocidade do tempo atual

        Matrix4x4[] matrices = {
            MatrixTransformationSystem.Scale(scale, scale),
            MatrixTransformationSystem.Rotation(rotation)
        };

        Matrix4x4 composed = MatrixTransformationSystem.ComposeMatrices(matrices);
        MatrixTransformationSystem.Apply(transform, composed);
    }
}