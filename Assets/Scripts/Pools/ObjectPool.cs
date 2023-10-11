using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    public GameObject ObjectsToPool;
    public int startSize;

    [SerializeField] private List<PooledObject> objectPool = new List<PooledObject>();
    [SerializeField] private List<PooledObject> usedPool = new List<PooledObject>();

    private PooledObject tempObject;

    private void Start()
    {
        Initialize();
    }


    private void Update()
    {
        
    }

    private void Initialize()
    {
        for(int i = 0; i < startSize; i++)
        {
            AddNewObject();
        }
    }


    void AddNewObject()
    {
        tempObject = Instantiate(ObjectsToPool, transform).GetComponent<PooledObject>();
        tempObject.gameObject.SetActive(false);
        tempObject.SetObjectPool(this);
        objectPool.Add(tempObject);
    }

    public PooledObject GetPooledObject()
    {
        PooledObject tempObject;
        if(objectPool.Count > 0)
        {
            tempObject = objectPool[0];
            usedPool.Add(tempObject);
            objectPool.RemoveAt(0);
        }
        else
        {
            AddNewObject();
            tempObject = GetPooledObject();
        }

        tempObject.gameObject.SetActive(true);
        tempObject.ResetObject();

        return tempObject;
    }

    public void DestroyPooledObject(PooledObject obj, float time = 0)
    {
        if(time == 0)
        {
            obj.Destroy();
        }
        else
        {
            obj.Destroy(time);        
        }
    }

    public void RestoreObject(PooledObject obj)
    {
        Debug.Log("Restored!");
        obj.gameObject.SetActive(false);
        usedPool.Remove(obj);
        objectPool.Add(obj);
    }
}
