using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour {

    public const float maxViewDst = 300;
    public Transform viewer;
    public Material mapMaterial;

    public static Vector2 viewerPosition;
    static MapGenerator mapGenerator;
    int chunkSize;
    int chunksVisibleinViewDst;
    //Liste mit allen Koordinaten und Chunks um unnötige Dopplungen zu vermeiden. 
    Dictionary<Vector2, TerrainChunk> terrainChunkDictonary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>(); // Liste an Chunks die vorher sichtbar waren. ohne diese werden die Meshs die außerhalb der ViewDistance sind nachdem sich das Schiff bewegt hat nicht ausgeblendet.

    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>(); 
        chunkSize = MapGenerator.mapChunkSize - 1; // chunkSize ist also 240 x 240 
        chunksVisibleinViewDst = Mathf.RoundToInt(maxViewDst / chunkSize); // gibt an wie viele Chunks um den Spieler herum zu sehen sind.
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z); // sezt die Aktuelle Position des Viewers(Transform object) auf die viewerPosition. Da sich die Kamera quasi bewegt wird sie hier die Position übergeben
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks()
    {
        //Blendet alle Chunks aus die im Update vorher visible waren
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
        {
            terrainChunksVisibleLastUpdate[i].SetVisible(false);
        }
        terrainChunksVisibleLastUpdate.Clear();//cleart die Liste an Chunks die visible waren

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize); //berechnet die Aktuellen Koordinaten des Chunks() also rund um den Spieler quasi 1,0 oder 0,1 oder 1,1 usw. wird zum überprüfen benötigt welche Chunks aktuell eingeblendet sein müssen und welche nicht. 
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffset = -chunksVisibleinViewDst; yOffset <= chunksVisibleinViewDst; yOffset++) // loopt durch alle umliegenden Chunks welche sichtbar für den "Spieler sind"
        {
            for (int xOffset = -chunksVisibleinViewDst; xOffset <= chunksVisibleinViewDst; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                //erstellt nur neue Chunks wenn an disen koordinaten noch keiner erstellt wurde (überprüft ob das Dictionary die aktuellen koordinaten enthält)
                if (terrainChunkDictonary.ContainsKey(viewedChunkCoord))
                {
                    terrainChunkDictonary[viewedChunkCoord].UpdateTerrainChunk();//wenn ein Chunk vorhanden ist überprüft er ob er sich in der View Distance befindet und setzt diesen ggf aktiv.
                    if (terrainChunkDictonary[viewedChunkCoord].isVisible()) //wenn der Chunk aktiv ist wird er der Liste an terrainChunksVisibleLastupdate hinzugefügt.
                    {
                        terrainChunksVisibleLastUpdate.Add(terrainChunkDictonary[viewedChunkCoord]);
                    }
                }
                else
                {
                    terrainChunkDictonary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform, mapMaterial));
                }
               
            }
        }
    }
    //Erstellt einen neuen TerrainChunk. Also ein neues Mesh anhand einer NoiseMap und den in den anderen Skripten übergebenen Methoden 
    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        MapData mapData;
        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        public TerrainChunk(Vector2 coord, int size, Transform parent, Material material)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshRenderer.material = material;
            meshFilter = meshObject.AddComponent<MeshFilter>();

            meshObject.transform.position = positionV3; //setzt die Position auf die Stelle an der das mesh generiert werden soll
            meshObject.transform.parent = parent; //Sorgt dafür das alle erstellten Meshs unter dem parent Element angefügt werden 
            SetVisible(false);//der TerrainChunk wird beim erstellen erstmals nicht angezeigt. 

            mapGenerator.RequestMapData(OnMapDataRecieved);
        }

        // Wenn die Berechnungen des extra Threads fertig sind soll aus der MapData die MeshData generiert werden.
        void OnMapDataRecieved(MapData mapData)
        {
            mapGenerator.RequestMeshData(mapData, OnMeshDataReceived);
        }

        //Wenn die MeshData erhalten wurde wird die funktion aufgerufen
        void OnMeshDataReceived(MeshData meshData)
        {
            meshFilter.mesh = meshData.CreateMesh();
        }

        //überprüft ob der Punkt innehralb des aktuellen Chunks der am nähesten zum Spieler liegt innerhalb der maximalen View Distance liegt. Tut er das wird der Chunk angezeigt. Tut er das nicht ausgeblendet.
        public void UpdateTerrainChunk()
        {
            float viewerDstFromNearestEdge = bounds.SqrDistance(viewerPosition); 
            bool visible = viewerDstFromNearestEdge <= maxViewDst * maxViewDst;
            SetVisible(visible);
        }
        //setzt das Mesh Aktiv je nachdem ob es sich in der Max View Distance befindet
        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        } 

        public bool isVisible()
        {
            return meshObject.activeSelf;
        }

    }
}
