using UnityEngine;
using UnityEngine.InputSystem; // Importante: namespace del nuevo Input System

public class GeneradorCanicas : MonoBehaviour
{
    [Header("Prefab y Punto de Aparición")]
    [Tooltip("Arrastra aquí el Prefab de la canica (Marble)")]
    public GameObject marblePrefab;

    [Tooltip("Lugar donde aparecerá la canica. Si queda vacío, se usará la posición de este GameObject")]
    public Transform puntoDeAparicion;

    [Header("Configuración de Tiempo")]
    [Tooltip("Tiempo de espera en segundos entre cada disparo")]
    public float tiempoRecarga = 1.5f;

    private float ultimoTiempoDisparo = -999f;

    void Update()
    {
        // Uso de la API directa del nuevo Input System
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            IntentarGenerarCanica();
        }
    }

    void IntentarGenerarCanica()
    {
        // Verificar si ha transcurrido el tiempo de recarga
        if (Time.time >= ultimoTiempoDisparo + tiempoRecarga)
        {
            GenerarCanica();
            ultimoTiempoDisparo = Time.time;
        }
    }

    void GenerarCanica()
    {
        if (marblePrefab == null)
        {
            Debug.LogWarning("Falta asignar el prefab 'Marble' en el Inspector.");
            return;
        }

        Vector3 posicion = (puntoDeAparicion != null) ? puntoDeAparicion.position : transform.position;
        Quaternion rotacion = (puntoDeAparicion != null) ? puntoDeAparicion.rotation : Quaternion.identity;

        Instantiate(marblePrefab, posicion, rotacion);
    }
}