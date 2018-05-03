/// <summary>
/// Creates new obstacles on timer wherever the spawner object is placed in scene
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // Obstacles to spawn
    public GameObject[] prefabs;
    // Seconds to wait between creating next obstacle
    public float delay = 2.0f;
    public bool active = true;
    // Add randomness to the spawning
    public Vector2 delayRange = new Vector2(1, 2);

    // Use this for initialization
    void Start()
    {
        // Set random delay between obstacle spawning
        ResetDelay();
        StartCoroutine(EnemyGenerator());
    }

    // Enemy generator
    IEnumerator EnemyGenerator()
    {
        yield return new WaitForSeconds(delay);

        // Create a new random obstacle at the spawner location
        if (active) {
            var newTransform = transform;

            GameObjectUtil.Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position);

            // Set random delay between obstacle spawning
            ResetDelay();
        }

        // Restart coroutine so it continues to run
        StartCoroutine(EnemyGenerator());
    }

    private void ResetDelay()
    {
        delay = Random.Range(delayRange.x, delayRange.y);
    }
}
