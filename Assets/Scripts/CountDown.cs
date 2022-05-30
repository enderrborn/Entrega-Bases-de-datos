using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{   
    //En este script se ejecuta el contador de tiempo
    //Las variables necesarias para que el temporizador funcione
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    public GameObject CanvasFinJuego;
    public Text puntuacion;
    public Button boton;
    public GameObject ScriptDisparo;

    private void Start()
    {
        timerIsRunning = true;
    }
     void Update()
    {
        //el contador de tiempo que se actualiza cada segundo
        if(timerIsRunning){
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else{ //Lo que ocurre cuando se acaba el tiempo
            timeRemaining = 0;
            timerIsRunning = false;
            CanvasFinJuego.SetActive(true);
            puntuacion.text = PuntuacionTracker.puntuacion.ToString() + " puntos";
            boton.Select();
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Destroy(ScriptDisparo);

        }
        
        }
    }
    //La funcion que permite correr el tiempo
    void DisplayTime(float timeToDisplay){
        //Se suma uno al tiempo para que el juego acabe cuando llega a 0, no un segundo después 
        //debido a cómo funciona el cálculo y display del tiempo en Unity
        timeToDisplay += 1;
        //Se calculan los minutos
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        //Se calculan los segundos
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //Se convierte en un string que primero coloca el número de segundos en el marcador y despúes el número de minutos
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
