/// <summary>
/// Basic time manager
/// Effectively pause game when player dies by setting timescale to 0
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Pause game when player dies
    public void ManipulateTime(float newTime, float duration)
    {
        // Speed up time slightly to ensure all objects 
        if (Time.timeScale == 0) {
            // Increase time slightly to allow everything else to start executing as we go back to normal timescale
            Time.timeScale = 0.1f;
        }

        StartCoroutine(FadeTo(newTime, duration));
    }

    // Smooth transition to different timescale
    IEnumerator FadeTo(float value, float time)
    {
        // Smooth timescale transition
        for (float t = 0; t < 1; t += Time.deltaTime / time) {
            // What should the timescale be on this iteration of the loop
            Time.timeScale = Mathf.Lerp(Time.timeScale, value, t);
            // Avoid scenerio where time is so close to zero that it's never reached or takes too long to scale down
            // If time is close enough to zero, just set to zero
            if (Mathf.Abs(value - Time.timeScale) < 0.1f) {
                Time.timeScale = value;
                yield break;
            }
            // Execute over multiple frames
            yield return null;
        }
    }
}
