using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public enum Tool
    {
        none = 0,
        hoe = 1,
        watercan = 2,
        seed = 3,
    }

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private PlantSO plantToSow = null;

    private Tool currentTool = Tool.hoe;

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

    private void OnGUI()
    {
        GUILayout.Label("Current tool: " + currentTool.ToString());
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

    public void OnToolUsage(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            switch (currentTool)
            {
                case Tool.hoe://tilling
                    FarmController.Instance.TillSoil(transform.position);
                    break;
                case Tool.watercan://watering

                    break;
                case Tool.seed://sowing
                    FarmController.Instance.SowSoil(transform.position, plantToSow);
                    break;
                case Tool.none:
                default:
                    Debug.Log("No tool or unimplemented tool");
                    break;
            }
        }
    }

    public void OnChangeTool(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            currentTool = (Tool)ctx.ReadValue<float>();
        }
    }
}
