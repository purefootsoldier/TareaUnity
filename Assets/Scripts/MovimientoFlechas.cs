using UnityEngine;

// Clase 04, Actividad 1: movimiento con las flechas usando Input.GetKey.
public class MovimientoFlechas : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
    }
}
