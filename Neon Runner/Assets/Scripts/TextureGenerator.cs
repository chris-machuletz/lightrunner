using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator{

    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point; //sorgt dafür das die ColorMap icht verschwommen aussieht
        texture.wrapMode = TextureWrapMode.Clamp;//sorgt dafür das die Ränder nicht überblenden
        texture.SetPixels(colourMap);
        texture.Apply();

        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
     
        //ColorMap wird auf Basis der noisemap erzeugt
        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                //es werden werte Zwischen schwarz und weis auf Basis der noisMap zu den einzelnen pixeln? gesetzt
            }
        }
        return TextureFromColourMap(colourMap, width, height);
    }

}
