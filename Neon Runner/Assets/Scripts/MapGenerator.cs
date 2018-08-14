using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


public class MapGenerator : MonoBehaviour {

    public enum DrawMode {NoiseMap, ColourMap, Mesh};
    public DrawMode drawMode;

    public const int mapChunkSize = 241; //241, da später weiter entfernte Chnuks mit weniger verticies dargestellt werden sollen. und die Formel ist width -1/i +1. und 240 ist gut teilbar. Gibt quasi länge und breite des Chunks an
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
    //erstellt eine neue Queue vom Typ ThreadInfo welcher vom typ MapData ist
    Queue<MapThreadInfo<MapData>> mapDataThradInfoQueue = new Queue<MapThreadInfo<MapData>>();
    Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();

    public void DrawMapInEditor()
    {
        //stellt die verschiedenen Maps dar, je nachdem welcher Drawmode selectet ist.
       MapData mapData = GenerateMapData();
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMuliplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize));
        }
    }


    // ---------------> THREADING MapData <------------------
    public void RequestMapData(Action<MapData> callback)
    {
        //repräsentiert den MapDataThread mit dem Callback  Parameter
        ThreadStart threadStart = delegate
        {
            MapDataThread(callback);
        };

        new Thread(threadStart).Start();
           
    }
    //Diese Methode läuft nun auf einem anderen Thread als dem HauptThread von Unity
    void MapDataThread(Action<MapData> callback)
    {
        MapData mapData = GenerateMapData(); // durch das Aufrufen der Methode innehalb eines anderen threads wird diese in dem selben ausgeführt. 
        lock (mapDataThradInfoQueue)//damit die Queue nicht von mehreren Stellen gleichzeitig genutzt wird, verwendet man das lock Keyword
        {
            //fügt die Mapdata mit dem Callback einer Queue hinzu um diese an der richtigen Stelle auszuführen. hierfür existiert das Struct was beide variablen enthält.
            mapDataThradInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }
    }

    //----------> Threading MeshData <------------
    public void RequestMeshData(MapData mapData,  Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate {
            MeshDataThread(mapData, callback);
        };
        new Thread(threadStart).Start();
    }

    void MeshDataThread(MapData mapData, Action<MeshData> callback)
    {
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMuliplier, meshHeightCurve, levelOfDetail); //erstelt eine neues Mesh mithilfe der GenerateTerrainMesh Methode und specihert dieses in der meshData variable vom Typ meshData(stuct)
        lock (meshDataThreadInfoQueue)
        {
            meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback, meshData));
        }
    }






    private void Update()
    {
        if (mapDataThradInfoQueue.Count > 0) // wenn sich ein Element in der Warteschlange befindet
        {
            for (int i = 0; i < mapDataThradInfoQueue.Count; i++)
            {
                MapThreadInfo<MapData> threadInfo = mapDataThradInfoQueue.Dequeue(); //entfernt das erste element aus der Queue und schreibt dieses in die threadInfo Variable.
                threadInfo.callback(threadInfo.parameter);
            }
        }

        if(meshDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }



    //
    //generiert eine Neue Noise Map mit den in Unity eingegebenen Werten.
    MapData GenerateMapData()
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
        return new MapData(noiseMap, colourMap);
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


    //Das struct ins gerneric, damit sowohl MeshData als auch MapData genutzt werden können
    struct MapThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;
 
        public MapThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
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


public struct MapData
{
    public readonly float[,] heightMap;
    public readonly Color[] colourMap;

    //beonhaltet die informationen um die einzelnen Maps im Editor zu erstellen.
    public MapData(float[,] heightMap, Color[] colourMap)
    {
        this.heightMap = heightMap;
        this.colourMap = colourMap;
    }
}