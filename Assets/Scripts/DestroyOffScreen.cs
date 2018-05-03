/// <summary>
/// Destroy object once it's off-screen
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    // How far off-screen the object needs to be before it's destroyed
    public float offset = 16f;

    // Player dies when pushed off screen by obstacle
    public delegate void OnDestroy();
    // Delegate used to connect one script to another
    // Essentially, the property calls the OnDestroy() method
    public event OnDestroy DestroyCallback;

    // Flag that the object is offscreen and needs to be destroyed
    private bool offscreen;
    private float offscreenX = 0;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        // Find where offscreen x is
        // Screen centre in unity is 0,0
        // Takes into account camera scaling for different resolutions and offset ensures obstacle is far enough off screen before destroying
        offscreenX = (Screen.width / PixelPerfectCamera.pixelsToUnits) / 2 + offset;
    }

    // Update is called once per frame
    void Update()
    {
        // Find when the object is offscreen
        var posX = transform.position.x;
        // Which direction is object facing
        var dirX = rb.velocity.x;

        if (Mathf.Abs(posX) > offscreenX) {
            // Is it going off left or right side off screen
            if (dirX < 0 && posX < -offscreenX) {
                offscreen = true;
            } else if (dirX > 0 && posX > offscreenX) {
                offscreen = true;
            }
        } else { // Not offscreen
            offscreen = false;
        }

        if (offscreen) {
            OnOutOfBounds();
        }
    }

    public void OnOutOfBounds()
    {
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);

        // If a method has been connected to the property, call the method
        if (DestroyCallback != null) {
            DestroyCallback();
        }
    }
}
