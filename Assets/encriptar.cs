using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using UnityEngine.UI;
 
[System.Serializable] 
public struct PlayerInfo
{
    public int playerLevel;
    public float playerEXP;
}
 
public class encriptar : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] bool serialize;
    [SerializeField] bool encrypt;
 
    void Start()
    {
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.playerLevel = 0;
        playerInfo.playerEXP = 0f;
 
        text.text = playerInfo.ToString();
        
 
        //////////////////////////////////////////////////////////////
        // Let's first serialize and encrypt....
        //////////////////////////////////////////////////////////////
 
        if ( serialize )
        {
            
            text.text = JsonUtility.ToJson( playerInfo );
            string serialized = text.text;
            
        }
 
        if ( encrypt )
        {
            text.text = Utils.EncryptAES( text.text );
 
            string encrypted = text.text;
            Debug.Log( "Encrypted: " + encrypted );
        }
 
        //////////////////////////////////////////////////////////////
        // Now let's de-serialize and de-encrypt....
        //////////////////////////////////////////////////////////////
 
        string stringData = text.text;
        if ( encrypt )
        {
            stringData = Utils.DecryptAES( stringData );
            Debug.Log( "Decrypted: " + stringData );
        }
 
        PlayerInfo derivedWeaponInfo = new PlayerInfo();
        if ( serialize )
        {
            
            
                derivedWeaponInfo = JsonUtility.FromJson<PlayerInfo>( stringData );
 
            Debug.Log( "Deserialized: " + derivedWeaponInfo.playerLevel );
        }
    }
}
 
public static class Utils
{
    
 
    static byte [] ivBytes = new byte [ 16 ]; // Generate the iv randomly and send it along with the data, to later parse out
    static byte [] keyBytes = new byte [ 16 ]; // Generate the key using a deterministic algorithm rather than storing here as a variable
 
    static void GenerateIVBytes()
    {
        System.Random rnd = new System.Random();
        rnd.NextBytes( ivBytes );
    }
 
    const string nameOfGame = "The Game of Life";
    static void GenerateKeyBytes()
    {
        int sum = 0;
        foreach ( char curChar in nameOfGame )
            sum += curChar;
   
        System.Random rnd = new System.Random( sum );
        rnd.NextBytes( keyBytes );
    }
 
    public static string EncryptAES( string data )
    {
        GenerateIVBytes();
        GenerateKeyBytes();
 
        SymmetricAlgorithm algorithm = Aes.Create();
        ICryptoTransform transform = algorithm.CreateEncryptor( keyBytes, ivBytes );
        byte [] inputBuffer = Encoding.Unicode.GetBytes( data );
        byte [] outputBuffer = transform.TransformFinalBlock( inputBuffer, 0, inputBuffer.Length );
 
        string ivString = Encoding.Unicode.GetString( ivBytes );
        string encryptedString = Convert.ToBase64String( outputBuffer );
 
        return ivString + encryptedString;
    }
 
    public static string DecryptAES( this string text )
    {
        GenerateIVBytes();
        GenerateKeyBytes();
 
        int endOfIVBytes = ivBytes.Length / 2;  // Half length because unicode characters are 64-bit width
 
        string ivString = text.Substring( 0, endOfIVBytes );
        byte [] extractedivBytes = Encoding.Unicode.GetBytes( ivString );
 
        string encryptedString = text.Substring( endOfIVBytes );
 
        SymmetricAlgorithm algorithm = Aes.Create();
        ICryptoTransform transform = algorithm.CreateDecryptor( keyBytes, extractedivBytes );
        byte [] inputBuffer = Convert.FromBase64String( encryptedString );
        byte [] outputBuffer = transform.TransformFinalBlock( inputBuffer, 0, inputBuffer.Length );
 
        string decryptedString = Encoding.Unicode.GetString( outputBuffer );
 
        return decryptedString;
    }
}
