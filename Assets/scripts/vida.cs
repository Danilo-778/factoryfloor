using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public int vidas = 3;

    [Header("Feedback Visual de Dano (Flash)")]
    public SpriteRenderer spriteRenderer;
    public Color corDano = Color.red;
    public float duracaoFlash = 0.15f;

    private Color corOriginal = Color.white;
    private float timerFlash;
    private bool piscando;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            corOriginal = spriteRenderer.color;
        }
    }

    private void Update()
    {
        if (piscando)
        {
            timerFlash += Time.deltaTime;

            if (timerFlash >= duracaoFlash)
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = corOriginal;
                }
                piscando = false;
            }
        }
    }

    public void TomarDano(int dano)
    {
        if (vidas <= 0 || dano <= 0) return;
        if (piscando) return; // evita tomar dano de novo durante o flash (pequena invulnerabilidade)

        vidas -= dano;

        Debug.Log("Tomou dano! Vidas restantes: " + vidas);

        if (vidas > 0)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = corDano;
                timerFlash = 0f;
                piscando = true;
            }
        }
        else
        {
            vidas = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}