using System; //Librería para conectar con Files
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;//Librería añadida para trabajar con la DB
using System.IO;//Librería para poder abrir archivos
using Mono.Data.Sqlite;//Librería para trabajar con SQLite
using UnityEngine.UI; //Para usar el Canvas de Unity

public class SQliteConnect : MonoBehaviour
{
    //Variable donde guardar la dirección de la Base de Datos
    string rutaDB;
    string strConexion;
    //Nombre de la base de datos con la que vamos a trabajar
    string DBFileName = "Database.db";
    //Variable texto UI
    public Text myText;

    //Referencia que necesitamos para poder crear una conexión 
    IDbConnection dbConnection;
    //Referencia que necesitamos para poder ejecutar comandos
    IDbCommand dbCommand;
    //Referencia que necesitamos para leer datos
    IDataReader reader;

    // Start is called before the first frame update
    void Start()
    {
        //Llamamos al método que abre las conexiones
        AbrirDB();
    }

    //Método que nos servirá para abrir la conexión con la Base de Datos
    void AbrirDB()
    {
        
            //Ruta dentro del pc para buscar la base de datos
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBFileName;
        
    
        

        //Para leer esta misma base de datos desde un dispositivo móvil
        strConexion = "URI=file:" + rutaDB;
        //Creamos una nueva conexión usando esa ruta
        dbConnection = new SqliteConnection(strConexion);
        //Abrimos esa conexión
        dbConnection.Open();

        //Crear la consulta
        //Generamos un comando para la conexión que hemos abierto
        dbCommand = dbConnection.CreateCommand();
        //El query
        string sqlQuery = "SELECT * FROM Datos";
        //Le pasamos el query al comando que vamos a ejecutar
        dbCommand.CommandText = sqlQuery;

        //Leer la base de datos
        //Ejecutamos el comando que hemos creado en formato lectura de datos
        reader = dbCommand.ExecuteReader();
        //Haremos que lea datos hasta que no queden más por leer de ese query
        while (reader.Read())
        {
            //id -> recojo el dato de la primera columna
            int posicion = reader.GetInt32(0);
            //marca -> recojo el dato de la segunda columna
            string nombre = reader.GetString(1);
            //color -> recojo el dato de la tercera columna
            int Puntuacion = reader.GetInt32(2);
            //cantidad -> recojo el dato de la cuarta columna
            int tiempo = reader.GetInt32(3);
            //Mostramos en consola los datos obtenidos de cada fila
            Debug.Log(posicion + " - " + nombre + " - " + Puntuacion + " - " + tiempo);
            myText.text = posicion.ToString() + " - " + nombre + " - " + Puntuacion + " - " + tiempo.ToString();
        }

        //Cerrar las conexiones
        //Cerramos el lector de datos
        reader.Close();
        //Vaciamos por si acaso el lector de datos
        reader = null;
        //Dejamos de disponer del comando que habíamos creado
        dbCommand.Dispose();
        //Vaciamos por si acaso el comando que habíamos creado
        dbCommand = null;
        //Cerramos la conexión que hemos abierto
        dbConnection.Close();
        //Vaciamos por si acaso la conexión que hemos usado
        dbConnection = null;
    }

}
