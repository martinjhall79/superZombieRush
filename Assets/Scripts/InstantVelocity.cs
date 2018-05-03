/// <summary>
/// Moves object that this script is attached to
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantVelocity : MonoBehaviour
{
    public Vector2 velocity = Vector2.zero;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }
}
