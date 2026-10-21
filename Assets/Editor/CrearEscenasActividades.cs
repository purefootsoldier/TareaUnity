using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

// Arma las escenas de las clases 04 y 06 que faltaban, con los objetos y valores que piden las guías.
// Menú: Actividades → Crear escenas faltantes. Nunca sobrescribe una escena que ya exista.
public static class CrearEscenasActividades
{
    const string CarpetaEscenas = "Assets/Scenes";
    const string RutaControles = "Assets/Settings/PlayerControls.inputactions";
    const string RutaSprites = "Packages/com.unity.2d.sprite/Editor/ObjectMenuCreation/DefaultAssets/Textures/v2/";

    [MenuItem("Actividades/Crear escenas faltantes")]
    static void CrearEscenas()
    {
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            return;

        // Clase 02: la estructura base pide Scripts, Prefabs y Sprites.
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            AssetDatabase.CreateFolder("Assets", "Prefabs");

        var creadas = new List<string>();
        Crear("Clase04_PlayerReactivo", ArmarPlayerReactivo, creadas);
        Crear("Escena01_InputMovement", ArmarInputMovement, creadas);
        Crear("Escena02_InputPhysics", ArmarInputPhysics, creadas);
        Crear("Reto_Input_Equilibrio", ArmarRetoInput, creadas);

        AgregarEscenasABuild();

        Debug.Log(creadas.Count > 0
            ? "Escenas creadas: " + string.Join(", ", creadas)
            : "No faltaba ninguna escena.");
    }

    static void Crear(string nombre, System.Action armar, List<string> creadas)
    {
        string ruta = $"{CarpetaEscenas}/{nombre}.unity";
        if (AssetDatabase.LoadAssetAtPath<SceneAsset>(ruta) != null)
            return;

        Scene escena = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        CrearCamaraYLuz();
        armar();
        EditorSceneManager.MarkSceneDirty(escena);
        EditorSceneManager.SaveScene(escena, ruta);
        creadas.Add(nombre);
    }

    // Clase 04, reto final: un solo objeto con dos scripts que se comunican.
    static void ArmarPlayerReactivo()
    {
        GameObject player = CrearSprite("Player", "Square", Vector3.zero, Vector3.one, Color.white);
        player.AddComponent<CambioColor>();
        player.AddComponent<MovimientoPlayer>();
    }

    // Clase 06, ejercicio 2: movimiento directo con Transform, sin Rigidbody2D.
    static void ArmarInputMovement()
    {
        GameObject jugador = CrearSprite("Jugador", "Square", Vector3.zero, Vector3.one, Color.cyan);
        jugador.AddComponent<MovimientoInput>();
        AgregarPlayerInput(jugador);
    }

    // Clase 06, ejercicio 3: fuerzas, impulso y cambio de color con gravedad 0.
    static void ArmarInputPhysics()
    {
        GameObject jugador = CrearSprite("JugadorFisico", "Square", Vector3.zero, Vector3.one, Color.white);
        jugador.AddComponent<BoxCollider2D>();
        Rigidbody2D rb = jugador.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        jugador.AddComponent<MovimientoFisicoInput>();
        jugador.AddComponent<CambioColorInput>();
        AgregarPlayerInput(jugador);
    }

    // Clase 06, reto final: la nave de equilibrio de la clase 05 con el New Input System.
    static void ArmarRetoInput()
    {
        GameObject nave = CrearSprite("Nave", "Square", new Vector3(0f, -1f, 0f), new Vector3(3f, 0.5f, 1f), Color.white);
        nave.AddComponent<BoxCollider2D>();
        Rigidbody2D rbNave = nave.AddComponent<Rigidbody2D>();
        rbNave.bodyType = RigidbodyType2D.Dynamic;
        rbNave.gravityScale = 0f;
        nave.AddComponent<NaveInput>();
        nave.AddComponent<CambioColorInput>();
        AgregarPlayerInput(nave);

        GameObject pelota = CrearSprite("Pelota", "Circle", Vector3.zero, new Vector3(0.5f, 0.5f, 1f), Color.yellow);
        pelota.AddComponent<CircleCollider2D>();
        Rigidbody2D rbPelota = pelota.AddComponent<Rigidbody2D>();
        rbPelota.bodyType = RigidbodyType2D.Dynamic;
        rbPelota.gravityScale = 1f;
        pelota.AddComponent<PelotaInput>();
        AgregarPlayerInput(pelota);

        GameObject suelo = CrearSprite("Suelo", "Square", new Vector3(0f, -4f, 0f), new Vector3(14f, 1f, 1f), Color.gray);
        suelo.AddComponent<BoxCollider2D>();
    }

    static void CrearCamaraYLuz()
    {
        var camaraGO = new GameObject("Main Camera");
        camaraGO.tag = "MainCamera";
        camaraGO.transform.position = new Vector3(0f, 0f, -10f);
        Camera camara = camaraGO.AddComponent<Camera>();
        camara.orthographic = true;
        camara.orthographicSize = 5f;
        camara.clearFlags = CameraClearFlags.SolidColor;
        camara.backgroundColor = new Color(0.19f, 0.30f, 0.47f);
        camaraGO.AddComponent<AudioListener>();
        camaraGO.AddComponent<UniversalAdditionalCameraData>();

        // Sin una luz 2D global los sprites con material Lit se ven negros.
        Light2D luz = new GameObject("Global Light 2D").AddComponent<Light2D>();
        luz.lightType = Light2D.LightType.Global;
    }

    static GameObject CrearSprite(string nombre, string forma, Vector3 posicion, Vector3 escala, Color color)
    {
        var go = new GameObject(nombre);
        go.transform.position = posicion;
        go.transform.localScale = escala;
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(RutaSprites + forma + ".png");
        sr.color = color;
        return go;
    }

    // PlayerInput con PlayerControls, mapa Player y Send Messages, como en la guía de la clase 06.
    static void AgregarPlayerInput(GameObject go)
    {
        PlayerInput input = go.AddComponent<PlayerInput>();
        input.actions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(RutaControles);
        input.defaultActionMap = "Player";
        input.notificationBehavior = PlayerNotifications.SendMessages;
    }

    static void AgregarEscenasABuild()
    {
        List<EditorBuildSettingsScene> escenas = EditorBuildSettings.scenes.ToList();
        string[] carpetas = { "Assets/Scenes", "Assets/SpaceCrisis/Scenes", "Assets/TiberianCrisis/Scenes" };

        foreach (string guid in AssetDatabase.FindAssets("t:Scene", carpetas))
        {
            string ruta = AssetDatabase.GUIDToAssetPath(guid);
            if (escenas.All(e => e.path != ruta))
                escenas.Add(new EditorBuildSettingsScene(ruta, true));
        }

        EditorBuildSettings.scenes = escenas.ToArray();
    }
}
