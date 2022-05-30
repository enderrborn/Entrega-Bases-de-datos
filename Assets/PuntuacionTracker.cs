using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionTracker : MonoBehaviour
{

    //asigna el texto que será el contador de puntuacion
    public Text textopuntuacion;
    // pone la puntuacion inicial
    static public int puntuacion = 0;

    //pone la puntuación a 0 siempre que empieza el juego
    private void Start()
    {
        textopuntuacion.text = "Puntuacion: " + puntuacion;
    }


    //actualiza el contador de la puntuación
    private void Update()
    {
        textopuntuacion.text = "Puntuacion: " + puntuacion;
    }
}
