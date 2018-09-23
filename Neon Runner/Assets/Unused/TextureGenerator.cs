using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wird NICHT während der Laufzeit des Spiels genutzt.
//#####Inhalt Skript:#####
//erzeugen der NoiseMap auf Basis der Werte die im Noise.cs Skripot generiert werden. 
//erzeugen der Textur mit einer Color Map welche in der GenerateMapData Funktion des MapGenerator Skript erezugt wird. 
// es ist sinnvoller erst eine ColorMap zu erzeugen als die Pixel nacheinander zu aktualisieren.


// Für Game in der Laufzeit nicht benötigt. 


//public static class TextureGenerator
//{

//    public static int trackWidth = 50;

//    //gibt eine Textur zurück welche im MapGenerator Skript auf die Plane "gezeichnet" wird.
//    public static Texture2D TextureFromColourMap(Color[] color, int width, int height)
//    {
//        Texture2D texture = new Texture2D(width, height);
//        texture.filterMode = FilterMode.Point; //sorgt dafür das die ColorMap icht verschwommen aussieht
//        texture.wrapMode = TextureWrapMode.Clamp;//sorgt dafür das die Ränder nicht überblenden
//        texture.SetPixels(color);
//        texture.Apply();
//        return texture;
//    }
    
//    //generiert die typische NoiseMap mit Werten zwischen schwarz und weis und gibt diese Textur zurück..
//    public static Texture2D TextureFromHeightMap(float[,] heightMap)
//    {
//        int size = heightMap.GetLength(0);
//        Debug.Log(size);

//        Color[] color = new Color[size * size];
//        for (int y = 0; y < size; y++)
//        {
//            for (int x = 0; x < size; x++)
//            {
//                if (x <= (size/2) + 25 && x >= (size/2) -25)
//                {
//                    color[y * size + x] = Color.white;
//                }
//                else
//                {
//                    color[y * size + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
//                }
                
//                //es werden werte Zwischen schwarz und weis auf Basis der noisMap zu den einzelnen pixeln? gesetzt
//            }
//        }
//        return TextureFromColourMap(color, size, size);
//    }

//}
