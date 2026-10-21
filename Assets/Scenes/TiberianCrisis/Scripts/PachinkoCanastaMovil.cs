using UnityEngine;

public class PachinkoCanastaMovil : MonoBehaviour
{
    [Header("Nodos Limite")]
    public Transform nodoA;
    public Transform nodoB;

    [Header("Configuración de Movimiento")]
    [Tooltip("Tiempo en segundos para ir de un nodo a otro")]
    public float duracionViaje = 2.0f;

    [Tooltip("Usa suavizado al inicio y al final de cada trayecto")]
    public bool usarMovimientoSuave = true;

    private float tiempoActual;

    void Update()
    {
        if (nodoA == null || nodoB == null || duracionViaje <= 0) return;

        // Avanzar el contador de tiempo de forma cíclica
        tiempoActual += Time.deltaTime;
        float factorNormalizado = Mathf.PingPong(tiempoActual / duracionViaje, 1.0f);

        // Aplicar curva de suavizado si está activa
        if (usarMovimientoSuave)
        {
            factorNormalizado = Mathf.SmoothStep(0f, 1f, factorNormalizado);
        }

        // Interpolar posición entre Nodo A y Nodo B
        transform.position = Vector3.Lerp(nodoA.position, nodoB.position, factorNormalizado);
    }

    // Visualizar la ruta en la pestaña Scene (solo en el Editor)
    private void OnDrawGizmos()
    {
        if (nodoA != null && nodoB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(nodoA.position, 0.3f);
            Gizmos.DrawWireSphere(nodoB.position, 0.3f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(nodoA.position, nodoB.position);
        }
    }
}