using UnityEngine;

using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
    public int vidas = 3;

    public void TomarDano()
    {
        vidas--;

        Debug.Log("Tomou dano! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
