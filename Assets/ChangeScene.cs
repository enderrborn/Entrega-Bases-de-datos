using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    public void cambiarEscena(int numeroEscena){
        SceneManager.LoadScene(numeroEscena);
    }
}
