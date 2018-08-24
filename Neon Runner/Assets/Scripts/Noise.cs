using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
        //persistance ist ein multiplier welcher angibt wie schnell sich die amplitude verkleinert also den Einfluss welche jede einzelne Oktave auf das Endergebnis hat. 
        //lacunarity ist ein multiplier welcher angibt wie schnell die frequency erhöht wird bei jeder erfolgreichen oktave
        // seed sorgt dafür, dass wenn wir die selbe Map wollen, auch die selbe erhalten.
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves]; // jede Oktave beginnt an einem anderen Punkt
        float maxPossibleHeight = 0;
        float amplitude = 1;//gibt den maximum absolute value an welcher ausgegeben werden kann.
        float frequency = 1;//gibt an wie Häufig die Daten in einem Zeitraum abgefragt werden


        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x; // durch + offset.x können wir später in Unity durch die NoiseMap "scrollen"
            float offsetY = prng.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude; //specihert die maximale Height um für jeden Chunk die selbe zu verwendne und flüssige übergänge zu schaffen.
            amplitude *= persistance;
        }

        if(scale <= 0)
        {
            scale = 0.0001f;
        }
        //max und min Value initialisieren welche später per for loop auf die max und min ergebnisse der NoiseMap gesetzt werden.
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeighht = float.MaxValue;
        //mitte der Map finden, um beim Zoomen (Scaling) in die Mitte statt an den oberen Rand zu zoomen.
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                amplitude = 1;//gibt den maximum absolute value an welcher ausgegeben werden kann.
                frequency = 1;//gibt an wie Häufig die Daten in einem Zeitraum abgefragt werden
                float noiseHeight = 0;
                // loop durch jede oktave für jeden x und y wert der Noise Map und generiert einen 2d perlin Noise Wert zurück welcher Später die "Höhe" an diser Position sein wird.
                //Jede oktave wird am ende "verbunden" und es entsteht ein Natürlicher werdendes "terrain". umso mehr Oktaven desto weniger hat jede einzelne. Mehr Oktaven bdeutet mehr code execution time.
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (float)(x- halfWidth + octaveOffsets[i].x) / (scale * frequency);
                    float sampleY = (float)(y - halfHeight + octaveOffsets[i].y)  / (scale * frequency);

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2 -1; // erlaubt auch negative Werte
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance; // wird kleiner da persistance kleiner 0 
                    frequency *= lacunarity; // wird größer da lacunarity größer 0 
                }
                //setzten der Range der noiseHeight welche später in die Range zwischen 0 und 1 umgewandelt wird.
                //if (noiseHeight > maxNoiseHeight)
                //{
                //    maxNoiseHeight = noiseHeight;
                //}
                //else if (noiseHeight < minNoiseHeighht)
                //{
                //    minNoiseHeighht = noiseHeight;
                //}
                noiseMap[x, y] = noiseHeight;
            }
        }
        //Normalize the Noise Map. InverseLerp retuns Number between 0 and 1.
        // die Parameter geben die min die max Zahl und den aktuellen wert in der noise map an. Die Klasse gibt mit dieser Information den passenden wert zwischen null und 1 aus 
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                //noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeighht, maxNoiseHeight, noiseMap[x, y]);
                float normalizedHeight = (noiseMap[x, y] + 1) / (2f * maxPossibleHeight / 1.5f); //reverse Zeile 58 
                noiseMap[x, y] = Mathf.Clamp(normalizedHeight,0, int.MaxValue);
            }
        }
                return noiseMap;



    }
   

}
