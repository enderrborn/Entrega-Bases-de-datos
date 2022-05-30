using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class destroyBala : MonoBehaviour
{


    
    //contador para destruirlo cuando está activo
    public int contadordestroy;
    //Rigidbody para saber la velocidad y la velocidad angular
     Rigidbody m_Rigidbody;
    
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        
    }
    
    
    

    private void Update()
    {
        //Si este objeto está activo en la jerarquía
        if (this.gameObject.activeInHierarchy){
            //Y el contador de tiempo es igual a 4 segundos (210 frames)
        if (contadordestroy >= 210){
            //desactiva este objeto
            this.gameObject.SetActive(false);
            //resetea el contador a 0
            contadordestroy = 0;
            //Pon la velocidad y la velocidad de giro a 0
             m_Rigidbody.velocity = Vector3.zero;
             m_Rigidbody.angularVelocity = Vector3.zero;

            
        } 
        //si no, suma 1 al contador de tiempo
        else{
            contadordestroy++;
        }}
        
    }

    //Si detecta colision con:
    private void OnCollisionEnter(Collision colision){
        //el suelo, se desactiva y la velocidad y la velocidad de giro se ponen a 0
        if (colision.gameObject.tag == "Suelo" ){
            this.gameObject.SetActive(false);
            contadordestroy = 0;
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
        }
        //el enemigo, suma 1 a la puntuacion, se desactiva, resetea el contador de destruccion a 0,
        //y la velocidad y la velocidad de giro se ponen a 0
        if (colision.gameObject.tag =="Enemigo"){
            PuntuacionTracker.puntuacion++;
            this.gameObject.SetActive(false);
            contadordestroy=0;
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;

        }
        
    }
}
