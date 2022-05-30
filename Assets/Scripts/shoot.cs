using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class shoot : MonoBehaviour
{

    

    
    //asignamos el prefab de la bala
    public GameObject bala;
    
    

    
    
    
    void Update()
    {   //El boton para disparar
        if( Input.GetButtonDown("Fire1")){

            
        GameObject bala = ObjectPool.SharedInstance.GetPooledObject();
        // Si el resultado del object pool es diferente a null, coloca las balas en el lugar del jugador con
        // con la misma rotación y la misma posicion, cambiando su estado a activo
        if (bala != null){
            bala.transform.position = this.gameObject.transform.position;
            bala.transform.rotation = this.gameObject.transform.rotation;
            bala.SetActive(true);
        }
        }

        
    }
    }

    

    


