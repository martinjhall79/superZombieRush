/// <summary>
/// Scrolling texture / background effect created by modifying texture shader offset values
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour
{

    // Scrolling speed
    public Vector2 speed = Vector2.zero;
    // Keep track of default offsets, before starting scroll
    private Vector2 offset = Vector2.zero;
    private Material material;

    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;
        // Starting offset
        offset = material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        // Scroll effect
        // TODO Smooth scroll effect with interpolation
        offset += speed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", offset);
    }
}
