using UnityEngine;

// Clase 04, reto final "Player reactivo": WASD con Input.GetKey (Legacy Input Manager).
// Se llama MovimientoPlayer porque Movimiento.cs ya lo usa la nave de Reto_Final.
public class MovimientoPlayer : MonoBehaviour
{
    public float velocidad = 5f;

    private CambioColor cambioColor;

    void Start()
    {
        cambioColor = GetComponent<CambioColor>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
            cambioColor.CambiarColor(Color.green);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
            cambioColor.CambiarColor(Color.yellow);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
            cambioColor.CambiarColor(Color.red);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
            cambioColor.CambiarColor(Color.blue);
        }
    }
}
