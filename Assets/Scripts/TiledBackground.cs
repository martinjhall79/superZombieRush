/// <summary>
/// Fill screen with repeating texture tile
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour
{
    // Native tile size, only works in powers of 2
    public int textureSize = 32;
    // Can we scale this texture horizontally and vertically
    public bool scaleHorizontally = true;
    public bool scaleVertically = true;

    // Use this for initialization
    void Start()
    {
        // How many tiles fill the screen height and width
        // Round up to avoid gaps in the tiles
        var newWidth = !scaleHorizontally ? 1 : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
        var newHeight = !scaleVertically ? 1: Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));

        // Change scale of tile based on calculated width and height
        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1); // Horizontal scale

        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
    }
}
