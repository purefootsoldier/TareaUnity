using UnityEngine;
using UnityEditor;

public class DuplicarConFlechas : EditorWindow
{
    private static float distanciaMovimiento = 1.5f; // Modifica este valor según tus unidades de cuadrícula

    [MenuItem("Edit/Duplicar y Mover/Izquierda &LEFT")]
    private static void DuplicarIzquierda() => DuplicarYDesplazar(Vector3.left);

    [MenuItem("Edit/Duplicar y Mover/Derecha &RIGHT")]
    private static void DuplicarDerecha() => DuplicarYDesplazar(Vector3.right);

    [MenuItem("Edit/Duplicar y Mover/Arriba &UP")]
    private static void DuplicarArriba() => DuplicarYDesplazar(Vector3.up);

    [MenuItem("Edit/Duplicar y Mover/Abajo &DOWN")]
    private static void DuplicarAbajo() => DuplicarYDesplazar(Vector3.down);

    private static void DuplicarYDesplazar(Vector3 direccion)
    {
        // Verificar que haya un objeto seleccionado
        if (Selection.activeGameObject == null) return;

        GameObject original = Selection.activeGameObject;
        
        // Duplicar el objeto registrándolo en el sistema de Undo (Deshacer)
        GameObject duplicado = Instantiate(original, original.transform.parent);
        duplicado.name = original.name;
        
        // Moverlo en la dirección deseada
        duplicado.transform.position = original.transform.position + (direccion * distanciaMovimiento);
        
        // Registrar la creación para poder usar Ctrl+Z y seleccionar el nuevo objeto
        Undo.RegisterCreatedObjectUndo(duplicado, "Duplicar con Flechas");
        Selection.activeGameObject = duplicado;
    }
}
