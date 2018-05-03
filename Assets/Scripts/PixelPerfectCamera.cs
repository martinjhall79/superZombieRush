/// <summary>
/// Automatically resize camera based on height of screen, to maintain look and feel across different screen resolutions
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {

    // Pixels to unit ratio of artwork
    public static float pixelsToUnits = 1f;
    // Assume pixels to unit scale is always 1 when starting
    public static float scale = 1f;

    // Native resolution of game, based on a device suitable for art style
    // Gameboy advanced resolution for this game as it's a pixel art game
    // Calculates difference in scale of device resolution and native resolution and recalculates scale to maintain intended look
    // Changes size of camera to zoom in more or less to get a consistent look
    public Vector2 nativeResolution = new Vector2(240, 160);

    private void Awake()
    {
        // Camera calculates scale before anything else starts
        var camera = GetComponent<Camera>();

        // Make sure camera is orthagraphic
        if (camera.orthographic) {
            // Take current screen height and divide by native resolution to get scale
            scale = Screen.height / nativeResolution.y;
            // Modify a reference to pixels to units, to match scale
            pixelsToUnits *= scale;
            // Change size of camera
            // 0 coords in Unity is the centre of screen
            camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;
        }
    }
}
