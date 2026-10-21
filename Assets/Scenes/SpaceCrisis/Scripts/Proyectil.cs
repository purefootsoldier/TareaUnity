using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Proyectil : MonoBehaviour
{
    [SerializeField] private float velocidad = 35f;
    [SerializeField] private float danio = 10f;
    [SerializeField] private float tiempoVida = 3f;

    [Header("Impacto")]
    [SerializeField] private GameObject impactoPrefab;
    [SerializeField] private float duracionImpacto = 2f;

    private Rigidbody rb;
    private VidaNave propietario;

    private bool terminado;
    private VidaNave objetivoImpactado;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        SphereCollider colliderBala = GetComponent<SphereCollider>();
        colliderBala.isTrigger = true;

        rb.useGravity = false;
        rb.linearDamping = 0f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Destroy(gameObject, tiempoVida);
    }

    public void Inicializar(VidaNave nave)
    {
        propietario = nave;
        rb.linearVelocity = transform.forward * velocidad;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (terminado)
            return;

        VidaNave objetivo = other.GetComponentInParent<VidaNave>();

        if (objetivo == propietario)
            return;

        objetivoImpactado = objetivo;

        Terminar();
    }

    private void Terminar()
    {
        if (terminado)
            return;

        terminado = true;
        Destroy(gameObject); // Esto disparará automáticamente OnDestroy()
    }

    private void OnDestroy()
    {
        // Si el objeto se está destruyendo (por tiempo o por impacto), ejecutamos los efectos finales.
        // Verificamos que la aplicación esté jugando para evitar errores al cerrar el editor.
        if (!gameObject.scene.isLoaded) return;

        // Instanciar efecto de impacto si existe
        if (impactoPrefab != null)
        {
            GameObject efecto = Instantiate(
                impactoPrefab,
                transform.position,
                Quaternion.LookRotation(-transform.forward)
            );

            Destroy(efecto, duracionImpacto);
        }

        // Aplicar daño si chocó contra una nave válida
        if (objetivoImpactado != null)
        {
            objetivoImpactado.RecibirDanio(danio);
        }
    }
}