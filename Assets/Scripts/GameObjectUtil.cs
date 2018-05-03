using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectUtil
{
    // Manage object pools
    private static Dictionary<RecycleGameObject, ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool>();
    // Static method called with class name and method name
    public static GameObject Instantiate(GameObject prefab, Vector3 pos)
    {
        GameObject instance = null;

        // Make sure prefab passed in is a recyclable GameObject
        var recycledScript = prefab.GetComponent<RecycleGameObject>();
        if (recycledScript != null) {
            var pool = GetObjectPool(recycledScript);
            instance = pool.NextObject(pos).gameObject;
        } else { // Not recyclable, instantiate new 
            // Uses GameObjects static Instantiate()
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = pos;
        }

        // TODO Decide whether we need to instantiate a new gameobject, or reuse from object pool
        return instance;
    }

    public static void Destroy(GameObject gameObject)
    {
        // Can this GameObject be recycled in the object pool
        var recycleGameObject = gameObject.GetComponent<RecycleGameObject>();

        // Can be recycled, deactivate for later use
        if (recycleGameObject != null) {
            recycleGameObject.Shutdown();
        } else { // Can't be recycled, destroy
            GameObject.Destroy(gameObject);
        }
    }

    // Return the relevant pool, based on the requested GameObject
    private static ObjectPool GetObjectPool(RecycleGameObject reference)
    {
        ObjectPool pool = null;

        // Check dictionary key exists, if not the dictionary will throw error
        if (pools.ContainsKey(reference)) {
            // Get dict key
            pool = pools[reference];
        } else { // Pool doesn't exist yet, create it
            // Create a container GameObject to hold the pool
            var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
            pool = poolContainer.AddComponent<ObjectPool>();
            pool.prefab = reference;
            pools.Add(reference, pool);
        }

        return pool;
    }
}
