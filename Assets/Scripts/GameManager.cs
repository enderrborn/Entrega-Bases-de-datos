using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public Text puntosTXT;
    public Text nombreTXT;
    public GameObject panelGO;
    public GameObject rankingGO;
    

    

    public void ActivarPanel()
    {
        panelGO.SetActive(true);
    }

    public void GuardarPuntosDB()
    {
        rankingGO.GetComponent<RankingManager>().InsertarPuntos(nombreTXT.text, PuntuacionTracker.puntuacion);
    }
}
