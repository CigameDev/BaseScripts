using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 20;

    [SerializeField] private GameObject bulletPrefab;

    void Awake()
    {
        if(instance == null)
        {
            instance = this as ObjectPool;
        }    
    }
    void Start()
    {
        for(int i=0;i< amountToPool; i++)
        {
            GameObject obj =Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }    
    }

    public GameObject GetPoolObject()
    {
        for(int i=0;i<pooledObjects.Count;i++)
        {
            if (!pooledObjects[i].activeInHierarchy)return pooledObjects[i];
            //ta co the active true o day luon cung dc
        }

        GameObject newobj = Instantiate(bulletPrefab);
        pooledObjects.Add(newobj);
        return newobj;
    }    
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }    
    
}
