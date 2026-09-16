using UnityEngine;

public class Projetil : MonoBehaviour
{
    [SerializeField] private int dano = 1;
    [SerializeField] private float tempoVida = 5f;

    private void Start()
    {
        Destroy(gameObject, tempoVida);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<VidaJogador>().TomarDano(dano);
            Destroy(gameObject);
        }
    }
}