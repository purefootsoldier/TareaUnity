using UnityEngine;

// Clase 04: se encarga solo del color; MovimientoPlayer le pide el color de cada dirección.
public class CambioColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CambiarColor(Color nuevoColor)
    {
        spriteRenderer.color = nuevoColor;
    }
}
