using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    
    //eliges dos prefabs
    public GameObject Enemigo1;
    public GameObject Enemigo2;

     void 
     Update()
    {
        //genera un número aleatorio de entre 0 y 100, y si es mayor que 10, instancia el primer enemigo
        if(Random.Range(0,100)<10){
            //en la posición y rotación del gameobject que lleve este script
            Instantiate(Enemigo1, this.transform.position, Quaternion.identity);
            //si el resultado del número anterior es menor o igual que 10, genera otro número
            //y si ese nuevo número es mayor que 10, instancia el segundo enemigo
        }else if(Random.Range(0,100)<10){
            Instantiate(Enemigo2, this.transform.position, Quaternion.identity);
        }

        

        
}}
