using UnityEngine;

// Movimiento básico: mueve el objeto con los ejes Horizontal y Vertical del Input Manager.
public class MovimientoBasico : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
        }
    }
}
