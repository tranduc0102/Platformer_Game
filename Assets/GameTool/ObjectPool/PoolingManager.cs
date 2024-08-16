using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : SingleTon<PoolingManager>
{
    public GameObject parent;
    private string poolName = "PoolObject";
    public List<Transform> listObject;

    protected override void Awake()
    {
        base.Awake();
        if (GameObject.Find(poolName) == null)
        {
            parent = new GameObject(poolName);
            DontDestroyOnLoad(parent);
        }
        // Initialize the listObject
        listObject = new List<Transform>();
    }

    public GameObject Spawn(GameObject gameObject, Vector3 position, Quaternion quaternion)
    {
        GameObject newObject = GetFormPool(gameObject);
        newObject.transform.SetPositionAndRotation(position, quaternion);
        newObject.transform.parent = this.parent.transform;
        return newObject;
    }

    public GameObject GetFormPool(GameObject obj)
    {
        GameObject newObject;
        foreach (var pool in listObject)
        {
            if (pool.name == obj.name)
            {
                listObject.Remove(pool);
                newObject = pool.gameObject;
                newObject.SetActive(true);
                return pool.gameObject;
            }
        }

        newObject = Instantiate(obj);
        newObject.name = obj.name;
        newObject.transform.parent = this.parent.transform;
        return newObject;
    }

    public void Despawn(GameObject gameObject)
    {
        gameObject.SetActive(false);
        listObject.Add(gameObject.transform);
    }
    
}