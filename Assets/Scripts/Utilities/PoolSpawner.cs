using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolSpawner<T> where T : Component
{
    private List<T> pool;
    private T prefab;

    public PoolSpawner(List<T> pool, T prefab)
    {
        this.pool = pool;
        this.prefab = prefab;
    }

    public T Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        var firstInactive = pool.FirstOrDefault(x => !x.gameObject.activeSelf);

        if (firstInactive == null)
        {
            firstInactive = Object.Instantiate(prefab, position, rotation, parent);
            pool.Add(firstInactive);
        }

        else
        {
            firstInactive.transform.localPosition = position;
            firstInactive.transform.localRotation = rotation;
            firstInactive.transform.parent = parent;
        }
        
        firstInactive.gameObject.SetActive(true);

        return firstInactive;
    }
}
