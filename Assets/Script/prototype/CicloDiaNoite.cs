using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CicloDiaNoite : MonoBehaviour
{
    [Header("Referências")]
    public Light2D luzGlobal;

    [Header("Configurações")]
    public float duracaoDia = 10f;

    private float tempo = 0f;

    void Update()
    {
        tempo += Time.deltaTime / duracaoDia;

        float intensidade = Mathf.Lerp(0.2f, 1.0f,
            (Mathf.Sin(tempo * 2 * Mathf.PI) + 1) / 2);

        luzGlobal.intensity = intensidade;
    }
}