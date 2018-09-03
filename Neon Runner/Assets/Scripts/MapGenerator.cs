using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

//######Inhalt Skript#######
// Variablen die zum Generieren der NosieMap wichtig sind.
//DrawInEditor Methode die je nach ausgewählter MEthode die NoiseMap, Die ColourMap oder das Mesh darstellt.
// Threading von MapData und Meshdata in je eigenen Threads mit callback Funktionen auf die folgende Methode
//Update Methode die sofern Vorhanden MeshData und MapData aus der Queueu in eine Variable eschriebt und die beim Aufruf übergebene Action ausführt. 
// GenerateMapData Methode die Methden aus Noise.cs benutzt um eine Noisemap und Colourmap zurückgibt (Mapdata struct)
// struct MapThreadInfo welches für Mesh und Mapdata verwendet wird um parameter und callback funktion zu übergeben
// DrawMap Methode. weiter Infos unten. 



public class MapGenerator : MonoBehaviour {

    
    public enum DrawMode {NoiseMap, ColourMap, Mesh};
    public DrawMode drawMode;

    public const int mapChunkSize = 241; //241, da später weiter entfernte Chnuks mit weniger verticies dargestellt werden sollen. und die Formel ist width -1/i +1. und 240 ist gut teilbar. Gibt quasi länge und breite des Chunks an
    [Range(0,6)]
    public int edit_LOD; // umso entfernter der Hintertgrund umso weniger Verticies werden verwendet. ggf unnötig in unserem Projekt wegen der Geschwindigkeit
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

    public void DrawInEditor()
    {
        //stellt die verschiedenen Maps dar, je nachdem welcher Drawmode selectet ist.
       MapData mapData = GenerateMapData(Vector2.zero); // da jetzt ein Vector2 benötigt wird übergeben wir einfach den 0 Vector2
        DrawMap display = FindObjectOfType<DrawMap>();
       
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
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMuliplier, meshHeightCurve, edit_LOD), TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize));
        }
    }


    // ---------------> THREADING MapData <------------------
    public void RequestMapData(Vector2 centre, Action<MapData> callback)// Vector2 centre sorgt dafür, dass nicht dauernd die selbe Map generiert wird sondern andere. 
    {
        //repräsentiert den MapDataThread mit dem Callback  Parameter
        ThreadStart threadStart = delegate
        {
            MapDataThread(centre, callback);
        };

        new Thread(threadStart).Start();
           
    }
    //Diese Methode läuft nun auf einem anderen Thread als dem HauptThread von Unity
    void MapDataThread(Vector2 centre, Action<MapData> callback)// Vector2 centre sorgt dafür, dass nicht dauernd die selbe Map generiert wird sondern andere. 
    {
        MapData mapData = GenerateMapData(centre); // durch das Aufrufen der Methode innehalb eines anderen threads wird diese in dem selben ausgeführt. 
        lock (mapDataThradInfoQueue)//damit die Queue nicht von mehreren Stellen gleichzeitig genutzt wird, verwendet man das lock Keyword
        {
            //fügt die Mapdata mit dem Callback einer Queue hinzu um diese an der richtigen Stelle auszuführen. hierfür existiert das Struct was beide variablen enthält.
            mapDataThradInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }
    }

    //----------> Threading MeshData <------------
    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate {
            MeshDataThread(mapData, lod, callback);
        };
        new Thread(threadStart).Start();
    }

    void MeshDataThread(MapData mapData,int lod, Action<MeshData> callback)
    {
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMuliplier, meshHeightCurve, lod); //erstelt eine neues Mesh mithilfe der GenerateTerrainMesh Methode und specihert dieses in der meshData variable vom Typ meshData(stuct)
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
    //generiert eine Neue Map mit den in Unity eingegebenen Werten(der daraus generierten noisemap(heightmap)).
    MapData GenerateMapData(Vector2 centre)// Vector2 centre sorgt dafür, dass nicht dauernd die selbe Map generiert wird sondern andere. 
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, centre + offset);

        Color[] colourMap = new Color[mapChunkSize* mapChunkSize];
      // loopt durch alle Pixel der Noise Map und setzt den Wert auf die Farbe der Region je nachdem in welcher er sich aufgrund seiner höhe befindet
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight >= regions[i].range)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                    }
                    else
                    {
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

//Wird oben als Aray deklariert um die Regionen wie gewünscht einzustellen, welche für das aussehen der ColourMap verantwortlich sind. 
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float range;
    public Color colour; 
}

// enthält sowohl height als auch ColourMap. Wird in vorallem im BackgroundTerrain Skript verwendet. 
public struct MapData
{
    public readonly float[,] heightMap;
    public readonly Color[] colourMap;

    //beinhaltet die informationen um die einzelnen Maps im Editor zu erstellen.
    public MapData(float[,] heightMap, Color[] colourMap)
    {
        this.heightMap = heightMap;
        this.colourMap = colourMap;
    }
}

// die im texture Generator erzeugten Texturen werden mithilfe der DrawTexture Methode auf die Plane gebracht.
// Das im MeshGenerator erzeugte Mesh wird mithilfe der Draw Mesh Methode erteugt. (Im Editor)
// sharedMaterial sorgt dafür, dass auch im Editor Modus die NoiseMap zu sehen ist

// Außerhalb des Editor werden die Funktionen nicht benötigt. 

 public class DrawMap : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawMesh(MeshData data, Texture2D texture)
    {
        meshFilter.sharedMesh = data.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}