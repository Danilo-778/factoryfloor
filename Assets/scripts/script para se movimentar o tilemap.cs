using UnityEngine;

public class MoverTilemap : MonoBehaviour
{
    public float velocidade = 2f;

    void Update()
    {
        transform.Translate(Vector3.left * velocidade * Time.deltaTime);
    }
}