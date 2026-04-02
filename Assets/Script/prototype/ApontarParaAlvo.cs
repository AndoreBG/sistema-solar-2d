using UnityEngine;

public class ApontarParaAlvo : MonoBehaviour
{
    [SerializeField]
    private Transform alvo;

    void Update()
    {
        if (alvo == null) return;

        Vector2 direcao = alvo.position - transform.position;

        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angulo);
    }
}