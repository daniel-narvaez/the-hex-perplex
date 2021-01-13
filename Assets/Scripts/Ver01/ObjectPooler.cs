using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//"[System.Serializable] allows you to make instances of this class editable from within the Inspector."
[System.Serializable]
public class ObjectPoolItem {
    public int amountToPool;
    public GameObject objectToPool;
}
public class ObjectPooler : Manager
{
    //CREDITS FOR THIS CODE AND IT'S INFO: https://www.raywenderlich.com/847-object-pooling-in-unity

    //"Several scripts will need to access the object pool during gameplay, and public static instance 
    //allows other scripts to access it without getting a Component from a GameObject."
    public static ObjectPooler SharedInstance;

    //"By using the Inspector in Unity, you’ll be able to specify a GameObject to pool and a 
    //number to pre-instantiate."
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;

    void Awake()
    {
        if (FindObjectsOfType<ObjectPooler>().Length > 1)
            Destroy(gameObject);

        if(SharedInstance == null)
            SharedInstance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //"The for loop will instantiate the objectToPool GameObject the specified number of 
    //times in numberToPool. Then the GameObjects are set to an inactive state before 
    //adding them to the pooledObjects list."
    public override void Reset()
    {
        pooledObjects.Clear();
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    //"The first thing to note is that this method has a GameObject return type as opposed to void. This 
    //means a script can ask for a pooled object from GetPooledObject and it'll be able to return a 
    //GameObject in response."
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
