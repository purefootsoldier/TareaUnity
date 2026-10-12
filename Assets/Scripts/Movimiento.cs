using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    InputAction moveAction;
    public float fuerza = 10f;

    [SerializeField]
    private Rigidbody2D rigidbody2;

    [SerializeField]
    private TrailRenderer trailRenderer;

    private void Start()
    {
        if (rigidbody2 == null)
            rigidbody2 = GetComponent<Rigidbody2D>();

        if (trailRenderer == null)
            trailRenderer = GetComponent<TrailRenderer>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        MovePlayer();
        GestionarTrail();
    }

    private void MovePlayer()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector2 moveDirection = new Vector2(moveValue.x, moveValue.y).normalized;
        rigidbody2.AddForce(moveDirection * fuerza , ForceMode2D.Force);
    }

    private void GestionarTrail()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        if (trailRenderer != null)
        {
            trailRenderer.emitting = moveValue.sqrMagnitude > 0.01f;
        }
    }
}