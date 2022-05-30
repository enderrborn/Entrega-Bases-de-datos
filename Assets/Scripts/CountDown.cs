using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{
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
        
        if(timerIsRunning){
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else{
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
    void DisplayTime(float timeToDisplay){
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
