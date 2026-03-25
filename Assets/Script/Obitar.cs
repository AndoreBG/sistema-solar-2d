using UnityEngine;

public class Orbitar : MonoBehaviour
{
    [Header("Configurações de Órbita")]
    public float velocidadeOrbita = 30f;

    void Update()
    {
        transform.Rotate(0f, 0f, velocidadeOrbita * Time.deltaTime);
    }
}