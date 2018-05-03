/// <summary>
/// Instruct the object pool how to shutdown and restart GameObjects
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecycle
{
    void Restart();
    void Shutdown();
}

public class RecycleGameObject : MonoBehaviour
{
    private List<IRecycle> recycleComponents;

    // Look for other scripts implementing IRecycle interface
    private void Awake()
    {
        // Get all script components attached to this GameObject
        var components = GetComponents<MonoBehaviour>();
        recycleComponents = new List<IRecycle>();
        // Find components that implement IRecycle and add to list
        foreach (var component in components) {
            if (component is IRecycle) {
                recycleComponents.Add(component as IRecycle);
            }
        }
    }

    // Restart GameObject
    public void Restart()
    {
        gameObject.SetActive(true);

        // Tell all recycle components to restart
        foreach (var component in recycleComponents) {
            component.Restart();
        }
    }

    // Shutdown GameObject for later use
    public void Shutdown()
    {
        gameObject.SetActive(false);

        // Tell all recycle components to shutdown
        foreach (var component in recycleComponents) {
            component.Shutdown();
        }
    }
}
