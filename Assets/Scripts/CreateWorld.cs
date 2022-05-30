using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWorld : MonoBehaviour
{   
    //eje X
    public int width;
    //eje z
    public int depth;
    //Prefab de lo que usemos para hacer el terreno
    public GameObject cube;

    
    void Start()
    {
        //calcula una zona a partir de los pará metros que le hemos dado arriba

        //cantidad de unidades que calcula del eje X
        for (int x = 0; x < width; x++)
        {
            //Cantidad de unidades que calcula del eje Z
            for (int z = 0; z < depth; z++)
            {
                //utilizamos el perlin noise para que no estén todos los cubos a la misma altura
                Vector3 pos = new Vector3 (x, Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * 3, z);
                //Instancia la cantidad de cubos que ha calculado en la posición de altura que ha generado
                GameObject go = Instantiate(cube, pos, Quaternion.identity);
            }
        }
    }
}
