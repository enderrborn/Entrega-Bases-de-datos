using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vueloMoneda : MonoBehaviour
{
    //pillo el rigidbody del objeto
    Rigidbody Rigidbody;
    //Pillo la fuerza para el disparo
    public float fuerza = 20f;
    
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
            //Le añado esa fuerza hacia delante para que el objeto salga disparado
            Rigidbody.AddForce(transform.forward * fuerza);
            
        
    }
}
