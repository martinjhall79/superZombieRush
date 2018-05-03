/// <summary>
/// Player jump behaviour
/// In a seperate class so it's reusable and can make player jump without having to know too much about other functions of player
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    public float jumpSpeed = 240f;
    public float forwardSpeed = 20;

    private Rigidbody2D rb;
    private InputState inputState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is standing on something solid listen for button press
        // On button press, make player jump straight up
        if (inputState.standing) {
            if (inputState.actionButton) {
                // Player only jumps forward if they are past the centre of screen
                rb.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
            }
        }
    }
}
