using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO; //Usar StreamWriter y StreamReader
using System.Security.Cryptography; //Libería para encriptación y desencriptación de información

public class GuardadoJsonEncript : MonoBehaviour
{

    public Text textoEXP;
    public Text textoNivel;
    public GameObject Logro1;
    public GameObject Logro2;
    public GameObject Logro3;
    //Variables para el archivo de guardado
     public int nivel;
     public float exper;
     public bool logro1;
     public bool logro2;
     public bool logro3;
    

    //Variable que seguirá siendo pública y accesible pero no se guardará en nuestro archivo, al especificarle que no será serializable
    //[System.NonSerialized] public int damage = 10;



    public class SaveData
    {
        //Variables para serializar
        public int nivel;
        public float exper;
        public bool logro1;
        public bool logro2;
        public bool logro3;
        

        //Constructor de la clase
        public SaveData(int _nivel, float _EXP, bool _Logro1, bool _Logro2, bool _Logro3)
        {
            //Rellenamos las variables con las que le pasamos por parámetro
            nivel = _nivel;
            exper = _EXP;
            logro1 = _Logro1;
            logro2 = _Logro2;
            logro3 = _Logro3;
            


        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(exper >= 10){
            nivel++;
            textoNivel.text = nivel.ToString();
            exper = 0;
        }
        if(nivel >= 10){
            logro1 = true;
            if(logro1 == true){
            Logro1.SetActive(true);
            }
        }
        if(nivel >= 50){
            logro2 = true;
            if(logro2 == true){
            Logro2.SetActive(true);
            Logro1.SetActive(false);
            }
        }
        if(nivel >= 100){
            logro3 = true;
            if(logro3 == true){
            Logro3.SetActive(true);
            Logro2.SetActive(false);
            }
        }
        //Write to File
        //Si pulsamos el botón S guardamos en el archivo de guardado
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Instanciamos la clase anidada pasándole por parámetro las variables que queremos guardar
            SaveData sd = new SaveData(nivel, exper, logro1, logro2, logro3);

            //Guardamos en un string el contenido del script osea la instancia de este
            string jsonString = JsonUtility.ToJson(sd);

            //Ruta donde queremos guardar la información
            string saveFilePath = Application.persistentDataPath + "/jsonGuardado.sav";

            //Creamos un StreamWriter para guardar la información en la ruta dada
            StreamWriter sw = new StreamWriter(saveFilePath);

            //Muestra la ruta del archivo por consola
            Debug.Log("Saving to: " + saveFilePath);

            //Escribimos la información que queremos en el archivo de guardado
            sw.WriteLine(jsonString);

            //Al acabar cerramos el StreamWriter
            sw.Close();


            //ENCRIPTAMOS LA INFORMACION DE NUESTRAS VARIABLES
            //Creamos un array de bytes para guardar el array que nos devuelve el método Encrypt para que pueda ser usado
            byte[] encryptSavegame = Encrypt(jsonString.ToString());
            //Escribimos esta información en el archivo de guardado, ya encriptada la información en su ruta 
            File.WriteAllBytes(saveFilePath, encryptSavegame);
            //Muestra la ruta del archivo por consola
            Debug.Log("Saving to: " + saveFilePath);
        }

        //Si pulsamos el botón L cargamos el archivo de guardado
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Ruta de donde queremos leer la información
            string saveFilePath = Application.persistentDataPath + "/jsonGuardado.sav";

            //Muestra la ruta del archivo por consola
            Debug.Log("Loading from: " + saveFilePath);


          


            //CARGAMOS LA INFORMACION ENCRIPTADA, DESENCIPTANDOLA 
            //Creamos un array con la información encriptada recibida
            byte[] decryptedSavegame = File.ReadAllBytes(saveFilePath);
            //Creamos un array donde guardar la información desencriptada recibida
            string jsonString = Decrypt(decryptedSavegame);

            //Instanciamos la clase anidada para cargar las variables de esta
            //La información recibida del archivo de guardado sobreescribirá los campos oportunos del jsonString
            SaveData sd = JsonUtility.FromJson<SaveData>(jsonString);

            //Realmente cargamos la información del archivo de guardado en las variables de Unity
            nivel = sd.nivel;
            exper = sd.exper;
            logro1 = sd.logro1;
            logro2 = sd.logro2;
            logro3 = sd.logro3;
            
            textoNivel.text = nivel.ToString();
            textoEXP.text = exper.ToString();

            

        }
    }


    /*PARA ENCRIPTAR Y DESENCRIPTAR LA INFORMACIÓN DEL ARCHIVO DE GUARDADO
    */

    //Clave generada para la encriptación en formato bytes, 16 posiciones
    byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    //Vector de inicialización para la clave
    byte[] _initializationVector = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

    //Encriptamos los datos del archivo de guardado que le pasaremos en un string
    byte[] Encrypt(string message)
    {
        //Usamos esta librería que nos permitirá a través de una referencia crear un encriptador de la información
        AesManaged aes = new AesManaged();
        //Para usar este encriptador le pasamos tanto la clave como el vector de inicialización que hemos creado nosotros arriba
        ICryptoTransform encryptor = aes.CreateEncryptor(_key, _initializationVector);
        //Lugar en memoria donde guardamos la información encriptada
        MemoryStream memoryStream = new MemoryStream();
        //Con esta referencia podremos escribir en el MemoryStream de arriba la información ya encriptada usando el encriptador con sus claves que ya habíamos creado
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        //Con el StreamWriter podemos escribir en el archivo la información encriptada, que se habrá guardado en el MemoryStream
        StreamWriter streamWriter = new StreamWriter(cryptoStream);

        //Usando todo lo anterior, guardamos en el archivo de guardado el json que le pasamos por parámetro, haciendo el siguiente proceso: recibimos el string, lo encriptamos, queda guardado en la memoria reservada para la encriptación
        streamWriter.WriteLine(message);

        //Una vez hemos usado estas referencias las cerramos para evitar problemas de guardado o corrupción del archivo o de la propia encriptación
        streamWriter.Close();
        cryptoStream.Close();
        memoryStream.Close();

        //Por último el método devolverá esta información que reside en el hueco de memoria con la información encriptada, convertida esta información en array de bytes
        return memoryStream.ToArray();
    }

    //Generamos un método que nos devuelva la información del archivo de guardado desencriptada
    string Decrypt(byte[] message)
    {
        //Usamos esta librería que nos permitirá a través de una referencia crear un desencriptador de la información
        AesManaged aes = new AesManaged();
        //Para usar este desencriptador le pasamos tanto la clave como el vector de inicialización que hemos creado nosotros arriba
        ICryptoTransform decrypter = aes.CreateDecryptor(_key, _initializationVector);
        //Lugar en memoria donde guardamos la información desencriptada
        MemoryStream memoryStream = new MemoryStream(message);
        //Con esta referencia podremos escribir en el MemoryStream de arriba la información ya desencriptada usando el desencriptador con sus claves que ya habíamos creado
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read);
        //Con el StreamReader podemos leer del archivo la información desencriptada, que se habrá guardado en el MemoryStream
        StreamReader streamReader = new StreamReader(cryptoStream);

        //Usando todo lo anterior, cargamos del archivo de guardado el json que le pasamos por parámetro, haciendo el siguiente proceso: recibimos el string, lo desencriptamos, queda guardado en la memoria reservada para la desencriptación
        string decryptedMessage = streamReader.ReadToEnd();

        //Una vez hemos usado estas referencias las cerramos para evitar problemas de guardado o corrupción del archivo o de la propia encriptación
        streamReader.Close();
        cryptoStream.Close();
        memoryStream.Close();

        //Por último el método devolverá esta información que reside en el hueco de memoria con la información desencriptada, convertida esta en un string
        return decryptedMessage;
    }

    public void SubirExp(){
        exper++;
        textoEXP.text = exper.ToString();
    }

    /*Ruta del archivo de guardado
        C:/Users/User/AppData/LocalLow/DefaultCompany/DataPersistenceProject/jsonUtilityDemo.sav
    */

}

