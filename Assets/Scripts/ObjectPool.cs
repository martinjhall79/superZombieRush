/// <summary>
/// Object pool - class manages instances that it creates
/// When new oject is created, looks through its list and sees if any of those objects has previously been created and is not in use
/// If no object already created and innactive, create a new object
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // The prefab this script manages
    public RecycleGameObject prefab;

    // List of GameObjects in the pool
    // You can dynamically change the size of a list, whereas an array has a fixed size once created
    private List<RecycleGameObject> poolInstances = new List<RecycleGameObject>();

    // Create instance
    private RecycleGameObject CreateInstance(Vector3 pos)
    {
        // Use GameObjects built in Instantiate method to avoid potential infinite loop with GameObjectUtil creating objects
        var clone = GameObject.Instantiate(prefab);
        clone.transform.position = pos;
        clone.transform.parent = transform;

        poolInstances.Add(clone);

        // Return instance
        return clone;
    }

    public RecycleGameObject NextObject(Vector3 pos)
    {
        RecycleGameObject instance = null;

        // Go through list of deactivated GameObjects and return one of those if there is one
        foreach (var go in poolInstances) {
            if (go.gameObject.activeSelf != true) { // Found deactivated GameObject in pool
                instance = go;
                instance.transform.position = pos;
            }

        }

        // Not found deactivated GameObject to reuse in pool, so create a new GameObject
        if (instance == null) {
            instance = CreateInstance(pos);

        }

        instance.Restart();
        return instance;
    }
}
