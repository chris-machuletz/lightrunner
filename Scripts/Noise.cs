using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#####Inhalt Skript: #####
// Es werden Funktionen bereitgestellt, die das erzeugen einer NoiseMap erlauben. 
// Es werden PerlinNoise Werte in einem 2D float Array gespeichert und somit verschiedene height Werte zu jedem Punkt zugeordnet. 
// Die Werte der NosieMap werden Normalisiert. Jedes Mesh verwendet die selbe Maximnale Höhe, da ansonsten unsaubere übergänge zwischen den Chunks entstehen.

//#####Erklärung Variablen:#####
//persistance ist ein multiplier welcher angibt wie schnell sich die amplitude verkleinert also den Einfluss welche jede einzelne Oktave auf das Endergebnis hat. 
//amplitude Range in der sich das Ergbenis der Perlin noise Funktion befinden kann
//lacunarity ist ein multiplier welcher angibt wie schnell die frequency erhöht wird bei jeder erfolgreichen oktave
//frequency gibt an wie Häufig die Daten in einem Zeitraum abgefragt werden
//octave multiple Aufrufe der Perlin Noise Funktion für jeden Punkt im Array. Das Terrain wirkt dadurch natürlicher.
//seed sorgt dafür, dass wenn wir die selbe Map wollen, auch die selbe erhalten.


public static class Noise
{
    public static float[,] GenerateNoiseMap(int width, int height, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) 
    {
        float[,] noiseMap = new float[width, height];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves]; // jede Oktave beginnt an einem anderen Punkt
        float maxPossibleHeight = 0;
        float amplitude = 1; //initialisieren von amplitude
        float frequency = 1;//initialisieren von frequency


        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x; // durch + offset.x können wir später in Unity durch die NoiseMap "scrollen"
            float offsetY = prng.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude; //speichert die maximale Height um für jeden Chunk die selbe zu verwendne und flüssige übergänge zu schaffen.
            amplitude *= persistance;
        }

        if(scale <= 0)
        {
            scale = 0.0001f;
        }
      
        //mitte der Map finden, um beim Zoomen (Scaling) in die Mitte statt an den oberen Rand zu zoomen.
        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                amplitude = 1; //amplitude wird resetted 
                frequency = 1; //frequency wird resetted
                float noiseHeight = 0;

                //loop durch jede oktave für jeden x und y wert der Noise Map und generiert einen 2d perlin Noise Wert zurück welcher Später die "Höhe" an diser Position sein wird.
                //Jede oktave wird am ende "verbunden" und es entsteht ein Natürlicher werdendes "terrain". umso mehr Oktaven desto weniger Einfluss  hat jede einzelne. Mehr Oktaven bdeutet mehr code execution time.
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (float)(x- halfWidth + octaveOffsets[i].x) / (scale * frequency);
                    float sampleY = (float)(y - halfHeight + octaveOffsets[i].y)  / (scale * frequency);

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2 -1; // erlaubt auch negative Werte
                    noiseHeight += perlinValue * amplitude;
                
                    amplitude *= persistance; // wird kleiner da persistance kleiner 0 
                    frequency *= lacunarity; // wird größer da lacunarity größer 0 
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        //Normalize the noiseMap. 
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                if (x <= (height / 2) + 25 && x >= (height / 2) - 25)
                {
                    noiseMap[x, y] = 0;
                }
                else
                {
                    float normalizedHeight = (noiseMap[x, y] + 1) / (2f * maxPossibleHeight / 1.5f);
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue);
                }
                    
            }
        }
                return noiseMap;
    }
}
