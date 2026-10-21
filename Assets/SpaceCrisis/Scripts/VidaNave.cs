using UnityEngine;

public class VidaNave : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private float umbralCritico = 25f;

    [Header("Material")]
    [SerializeField] private Renderer rendererObjetivo;
    [SerializeField] private Color colorSano = Color.white;
    [SerializeField] private Color colorDanado = Color.red;

    [Header("Parpadeo")]
    [SerializeField] private float velocidadParpadeo = 5f;

    public float VidaActual { get; private set; }

    private Material material;

    private void Awake()
    {
        VidaActual = vidaMaxima;

        if (rendererObjetivo == null)
            rendererObjetivo = GetComponentInChildren<Renderer>();

        material = rendererObjetivo.material;
    }

    private void Update()
    {
        ActualizarColor();
    }

    public void RecibirDanio(float cantidad)
    {
        VidaActual -= cantidad;
        VidaActual = Mathf.Clamp(VidaActual, 0f, vidaMaxima);

        if (VidaActual <= 0f)
        {
            Debug.Log($"{name} fue destruida.");
            gameObject.SetActive(false);
        }
    }

    private void ActualizarColor()
    {
        if (VidaActual <= umbralCritico)
        {
            // Parpadea entre rojo y negro
            float parpadeo = Mathf.PingPong(
                Time.time * velocidadParpadeo,
                1f
            );

            material.color = Color.Lerp(
                Color.black,
                Color.red,
                parpadeo
            );
        }
        else
        {
            // Se vuelve más rojo mientras pierde vida
            float porcentajeDanio =
                1f - (VidaActual / vidaMaxima);

            material.color = Color.Lerp(
                colorSano,
                colorDanado,
                porcentajeDanio
            );
        }
    }
}