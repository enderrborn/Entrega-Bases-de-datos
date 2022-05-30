using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;

    //el objeto que se va a instanciar
    public GameObject objectToPool;

    //la cantidad de objetos que se van a instanciar
    public int amountToPool;

    void Awake(){
        SharedInstance = this;
    }

    private void Start()
    {
        //instancia la cantidad de objetos que le hemos dicho
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i<amountToPool; i++){
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    //Crea la lista de objetos que se van instanciar
    public GameObject GetPooledObject(){
        for(int i = 0; i < amountToPool; i++){
            if(!pooledObjects[i].activeInHierarchy){
                return pooledObjects[i];
            }
        }
        return null;
    }
}
