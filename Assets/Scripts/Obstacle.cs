/// <summary>
/// Manages obstacles
/// Changes random sprites, every time an obstacle is restarted from object pool
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IRecycle
{

    // Change sprite randomly every time the object is restarted in object pool
    public Sprite[] sprites;

    // Keep track of collider offset
    public Vector2 colliderOffset = Vector2.zero;

    public void Restart()
    {
        // Pick random sprite and display
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // Dynamically resize collider to sprite size, when the obstacle sprite is changed
        var collider = GetComponent<BoxCollider2D>();
        var size = renderer.bounds.size;
        size.y += colliderOffset.y;
        // Adjust collider offset to match sprite position offset
        collider.size = size;
        collider.offset = new Vector2(-colliderOffset.x, collider.size.y / 2 - colliderOffset.y);
    }

    public void Shutdown()
    {

    }
}
