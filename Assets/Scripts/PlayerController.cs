using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;

    private Rigidbody2D rb2d;

    //input variables
    private Vector2 moveDirection = Vector2.zero;

    // ---------- Unity messages

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveDirection * moveSpeed;
    }

    // ---------- Input methods

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            moveDirection = ctx.ReadValue<Vector2>();
        }
        if (ctx.canceled)
        {
            moveDirection = Vector2.zero;
        }
    }

    public void OnTill(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            FarmController.Instance.TillSoil(transform.position);
        }
    }
}
