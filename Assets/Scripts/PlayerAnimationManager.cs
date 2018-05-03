/// <summary>
/// Manages switching animations
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private InputState inputState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputState = GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update()
    {
        // Always assume the player is running
        var running = true;

        // When player is being dragged off screen and not in the air
        if (inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold) {
            running = false;
        }

        animator.SetBool("Running", running);
    }
}
