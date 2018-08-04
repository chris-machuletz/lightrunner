using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode {NoiseMap, ColourMap, Mesh};
    public DrawMode drawMode;

    const int mapChunkSize = 241; //241, da später weiter entfernte Chnuks mit weniger verticies dargestellt werden sollen. und die Formel ist width -1/i +1. und 240 ist gut teilbar. Gibt quasi länge und breite des Chunks an
    [Range(0,6)]
    public int levelOfDetail; // umso entfernter der Hintertgrund umso weniger Verticies werden verwendet. ggf unnötig in unserem Projekt wegen der Geschwindigkeit
    public float noiseScale;

    public int octaves;
    [Range(0,1)] // persistance wird zum Slider welcher nuir zwischen null und 1 sein kann.
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMuliplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    //generiert eine Neue Noise Map mit dne in Unity eingegebenen Werten.
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapChunkSize* mapChunkSize];
      // loopt durch alle Pixel der Noise Map und setzt den Wert auf die Farbe der Region je nachdem in welcher er sich aufgrund seiner höhe befindet
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }
        //stellt die verschiedenen Maps dar, je nachdem welcher Drawmode selectet ist.
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }
        else if(drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMuliplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }

    }
    //wird beim verändern von werten ausgeführt und sorgt dafür, das Werte nicht außerhalb ihrer gewünschten Range liegen. 
    private void OnValidate()
    {
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }

    }
}
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour; 
}