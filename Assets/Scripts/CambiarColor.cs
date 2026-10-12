using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(SpriteRenderer))]
public class CambiarColor : MonoBehaviour
{
    public Color colorArriba = Color.yellow;
    public Color colorLados = Color.green;
    public Color colorDefault = Color.white;

    private InputAction moveAction;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveAction = InputSystem.actions.FindAction("Move");

    }

    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        if (Mathf.Abs(moveInput.y) > 0.1f)
        {
            spriteRenderer.color = colorArriba;
        }
        else if (Mathf.Abs(moveInput.x) > 0.1f)
        {
            spriteRenderer.color = colorLados;
        }
        else
        {
            spriteRenderer.color = colorDefault;
        }
    }
}