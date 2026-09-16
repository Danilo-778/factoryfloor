using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TipoArmadilha { Espetos, Torreta }

public class ArmadilhasConfig : MonoBehaviour
{
    [Header("Configuração Geral")]
    [SerializeField] private TipoArmadilha tipo = TipoArmadilha.Espetos;
    public int valorDano = 1;

    [Header("⚙️ Configurações do torreta")]
    [SerializeField] private float TempoDisparo = 2f;
    [SerializeField] private float velocidade = 3f;
    [SerializeField] private Transform pontoDisparo = null;
    [SerializeField] private GameObject prefabProjetil = null;

    [Header("⚡ Configurações do espinhos")]
    [SerializeField] private float tempoAtivo = 2f;     // Duração do espinho ligado
    [SerializeField] private float tempoInativo = 1.5f; // Duração do espinho desligado
    [SerializeField] private float atrasoInicial = 0f;  // ⏱️ Tempo de espera ANTES de iniciar o ritmo
    [SerializeField] private Collider2D colisorDano;

    private Animator animator;
    private float cronometro;
    private int estado;

    private void Start()
    {
        animator = GetComponent<Animator>();

        estado = 1;
        cronometro = 0f;
    }
    private void Update()
    {
        switch (tipo)
        {
            case TipoArmadilha.Espetos:
                AtualizarEspetos();
                break;
            case TipoArmadilha.Torreta:
                AtualizarTorreta();
                break;
        }

    }
    private void AtualizarEspetos()
    {
        cronometro += Time.deltaTime;
        if (cronometro < tempoAtivo)

        {
            animator.SetInteger("Estado", 1);

            colisorDano.enabled = true;

        }
        else if (cronometro < tempoAtivo + tempoInativo)
        {
            animator.SetInteger("Estado", 3);
            colisorDano.enabled = false;

            ;
        }
        else
        {
            cronometro = 0f;
        }

    }

    private void AtualizarTorreta()
    {
        cronometro += Time.deltaTime;
       
        if (cronometro >= TempoDisparo)
        {

            GameObject projetil = Instantiate(prefabProjetil, pontoDisparo.position, pontoDisparo.rotation);

            Rigidbody2D rbProjetil = projetil.GetComponent<Rigidbody2D>();
            if (rbProjetil != null)
            {
                rbProjetil.linearVelocity = Vector2.left * velocidade;
            }

            cronometro = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("Trigger detectado com: " + other.name + " | Tag: " + other.tag);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<VidaJogador>().TomarDano(valorDano);
        }
    }
}
  
