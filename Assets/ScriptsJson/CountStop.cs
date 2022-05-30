using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountStop : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    
    public GameObject CanvasFinJuego;
    public GameObject ScriptDisparo;
   

    private void Start()
    {
        timerIsRunning = true;
    }
     void Update()
    {
        
        if(timerIsRunning){
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            
           
        }
        else{
            timeRemaining = 0;
            timerIsRunning = false;
            CanvasFinJuego.SetActive(true);
            Destroy(ScriptDisparo);
           
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        
        }
    }
    
}
