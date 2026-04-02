using UnityEngine;
using TMPro;

public class RainbowTMP : MonoBehaviour
{
    public TextMeshProUGUI texto;

    public float velocidade = 1f;

    void Update()
    {
        float hue = Mathf.PingPong(Time.time * velocidade, 1);
        Color cor = Color.HSVToRGB(hue, 1, 1);
        texto.color = cor;
    }
}