using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(VidaNave))]
public class NaveController : MonoBehaviour
{
    [Header("Controles")]
    [SerializeField] private string teclaAvanzar = "<Keyboard>/w";
    [SerializeField] private string teclaIzquierda = "<Keyboard>/a";
    [SerializeField] private string teclaDerecha = "<Keyboard>/d";
    [SerializeField] private string teclaDisparo = "<Keyboard>/space";

    [Header("Movimiento")]
    [SerializeField] private float fuerza = 20f;
    [SerializeField] private float velocidadGiro = 150f;

    [Header("Disparo")]
    [SerializeField] private Proyectil proyectilPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float intervaloDisparo = 0.25f;

    private Rigidbody rb;
    private VidaNave vida;

    private InputAction avanzar;
    private InputAction girar;
    private InputAction disparar;

    private float siguienteDisparo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        vida = GetComponent<VidaNave>();
        ConfigurarControles();
    }

    private void ConfigurarControles()
    {
        avanzar = new InputAction(
            "Avanzar",
            InputActionType.Button,
            teclaAvanzar
        );

        girar = new InputAction(
            "Girar",
            InputActionType.Value
        );

        girar.AddCompositeBinding("1DAxis")
            .With("Negative", teclaIzquierda)
            .With("Positive", teclaDerecha);

        disparar = new InputAction(
            "Disparar",
            InputActionType.Button,
            teclaDisparo
        );
    }

    private void OnEnable()
    {
        avanzar.Enable();
        girar.Enable();
        disparar.Enable();
    }

    private void Update()
    {
        if (vida.VidaActual <= 0)
            return;

        if (disparar.IsPressed() &&
            Time.time >= siguienteDisparo)
        {
            Disparar();

            siguienteDisparo =
                Time.time + intervaloDisparo;
        }
    }

    private void FixedUpdate()
    {
        if (vida.VidaActual <= 0)
            return;

        Girar();
        Mover();
    }

    private void Girar()
    {
        float direccionGiro =
            girar.ReadValue<float>();

        Quaternion rotacion = rb.rotation *
            Quaternion.Euler(
                0f,
                direccionGiro *
                velocidadGiro *
                Time.fixedDeltaTime,
                0f
            );

        rb.MoveRotation(rotacion);
    }

    private void Mover()
    {
        if (avanzar.IsPressed())
        {
            rb.linearVelocity = transform.forward * fuerza * Time.fixedDeltaTime;
        }
    }

    private void Disparar()
    {
        Proyectil proyectil = Instantiate(
            proyectilPrefab,
            puntoDisparo.position,
            puntoDisparo.rotation
        );
        proyectil.Inicializar(vida);
    }


    private void OnDisable()
    {
        avanzar?.Disable();
        girar?.Disable();
        disparar?.Disable();
    }

    private void OnDestroy()
    {
        avanzar?.Dispose();
        girar?.Dispose();
        disparar?.Dispose();
    }
}