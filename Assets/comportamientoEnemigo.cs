using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comportamientoEnemigo : MonoBehaviour
{
    //numero que será el tiempo que esta activo el objeto
    public float lifetime;
 
     private void Update()
     {
         //destruye el gameobject pasado un tiempo, que ese tiempo sera el numero que hemos asignado a lifetime
         Destroy(gameObject, lifetime);
     }
         
         //si colisiona con
     private void OnCollisionEnter(Collision other){
         //un objeto con el tag de bala, se destruye
        if (other.gameObject.tag == "Bala"){
            this.gameObject.SetActive(false);            
        }
         //un objeto con el tag de player, se destruye y resta 5 a la puntuacion del jugador
        if (other.gameObject.tag == "Player"){
            Destroy(this.gameObject);
            PuntuacionTracker.puntuacion = PuntuacionTracker.puntuacion-5;
        }
 
         
         
     }
}
 

