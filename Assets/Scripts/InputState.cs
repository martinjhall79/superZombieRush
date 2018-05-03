/// <summary>
/// Keeps track of the current state of the player
/// Current physical state of player referenced by any script that updates player movement
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MonoBehaviour
{

    // When the player is standing on something solid

    // When the player is standing
    public bool standing;
    public float standingThreshold = 1;
    // When any input is taking place
    public bool actionButton;
    // When the player is in motion
    public float absVelX = 0f;
    public float absVelY = 0f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Has any key been pressed
        actionButton = Input.anyKeyDown;
    }

    // Is the player standing
    private void FixedUpdate()
    {
        // Calculate absolute velocity, is there any movement in any direction
        absVelX = System.Math.Abs(rb.velocity.x);
        absVelY = System.Math.Abs(rb.velocity.y);

        // Is the player standing on something solid
        // If not moving on the Y axis, we assume player must be standing on something
        // TODO Use colliders to detect collisions with the floor, objects etc...
        standing = absVelY <= standingThreshold;
    }
}
