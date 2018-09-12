using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//######Inhalt Skript#####
// Variablen für Chunksize, viewdst, und dst die sich ein Spieler bis zum nächsten Update bewegen muss. 
//detaillevles (ggf. noch entfernen da vlt unnötig)
//Start Methode die verschiedene variablen initialisiert und erste Chunks generiert. (mithilfe UpdateVisibleChunks)
// Update Methode, die wenn sich der Spieler genug bewegt hat die Chuks aktualisiert (mithilfe UpdateVisibleChunks). 
// UpdateVisibleChunks Methode. setzt Chunks die im letzten Update visible waren auf false, überprüft ob alle umliegenden Chunks die sichtabr sein sollten, existieren und setzt diese auf visible (UpdateTerrainChunks wird aufgerufen). sollten sie noch nicht existieren wird an diesem Punkt ein neuier chunk generiert. 
// Klasse TerrainChunk. Erzeugt einen neuen Chunk mit MeshFilter und MeshRenderer. Wenn die MapDatarecieved Methode aufgerufen wird, wird mithilfe des TextureGenerator.cs Skript einen neue textur erzeugt und dem MeshRenderer hinzugefügt. Anschließend wird die UpdateTerrainChunk Methode ausgeführt.
// Beim Updaten des TerrainChunks, wird zuerst überprüft ob sich der Chunk im sichtbaren Bereich des Spielers befindet. Tut er das wird überprüft wie weit er von der aktuellen Spielerpposition entfernt ist und welches Mesh mit welchen Detaillevel aktuell angezeigt werden muss. ISt noch keines vorhanden wird ein passendes Mesh erzeugt und anschließend der liste aktiver Chunks hinzugefügt.
// Klasse LODMesh welche weiter informationen üner die einzelnen MEshes wier LOD enthält und Methoden zum überprüfen sowie die OnMeshDataReceived und RequestMeshData Methoden. 
// Diese benutzen die RequestMeshData Methode vom MapGenerator Skript und rufen anschließend die beim aufruf übergebene Callback funktion auf (UpdateTerrainChunk). 


public class BackgroundTerrain : MonoBehaviour {

    const float scale = 5f;

    const float dstViewerHasToMove = 25f; // Distanz die sich der Spieler bewegen muss damit sich die Chunks Updaten
    const float sqrDstViewerHasToMove = dstViewerHasToMove * dstViewerHasToMove; 

  
    public Transform viewer;
    public Material mapMaterial;
    
    public LODInfo[] detailLevels;
    public static float maxViewDst; // gibt an wie viele Chunks um den Spieler generiert werden. ist ein höheres LOD im MapGenerator eingetragen wird dieser Wert verwendet.

    int chunkSize;
    int chunksVisibleinViewDst;

    public static Vector2 viewerPosition;
    Vector2 viewerPositionOld;
    static MapGenerator mapGenerator;
   
    //Liste mit allen Koordinaten und Chunks um unnötige Dopplungen zu vermeiden. 
    Dictionary<Vector2, TerrainChunk> terrainChunkDictonary = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> terrainChunksVisibleLastUpdate; // Liste an Chunks die vorher sichtbar waren. ohne diese werden die Meshs die außerhalb der ViewDistance sind nachdem sich das Schiff bewegt hat nicht ausgeblendet.

    void Start()
    {
        Reset();
        chunkSize = MapGenerator.mapChunkSize - 1; // chunkSize ist also 240 x 240 
        chunksVisibleinViewDst = Mathf.RoundToInt(maxViewDst / chunkSize); // gibt an wie viele Chunks um den Spieler herum zu sehen sind.
        UpdateVisibleChunks();
    }

    void Update()
    {

        Position();

        if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrDstViewerHasToMove) // damit die Chunks nicht bei jedem Frame geupdated wird wird überprüft ob der Spieler sich eine gewisse Distanz bewegt hat.
        {
            viewerPositionOld = viewerPosition;
            UpdateVisibleChunks();
        }
    }


    public void Reset()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        maxViewDst = detailLevels[detailLevels.Length - 1].distanceToNextLOD;
        terrainChunksVisibleLastUpdate = new List<TerrainChunk>();
        Position();
    }

    public void Position()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z) / scale; // sezt die Aktuelle Position des Viewers(Transform object) auf die viewerPosition. Da sich die Kamera quasi bewegt wird sie hier die Position übergeben
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
                    
                }
                else
                {
                    terrainChunkDictonary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, detailLevels, transform, mapMaterial));
                }
               
            }
        }
    }
    //Erstellt einen neuen TerrainChunk. Also ein neues Mesh anhand einer NoiseMap und den in den anderen Skripten übergebenen Methoden 
    public class TerrainChunk
    {
        public GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        LODMesh[] lodMeshes;
        LODInfo[] detailLevels;

        MapData mapData;
        bool mapDataRecieved;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        int prevLOD = -1; //specihert den LOD vor dem Update damit dieser bei überinstimmung nicht neu generiert wird. 



        public TerrainChunk(Vector2 coord,  int size, LODInfo[] detailLevels, Transform parent, Material material)
        {
            this.detailLevels = detailLevels;

            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshRenderer.material = material;
            meshFilter = meshObject.AddComponent<MeshFilter>();

            meshObject.transform.position = positionV3 * scale; //setzt die Position auf die Stelle an der das mesh generiert werden soll
            meshObject.transform.parent = parent; //Sorgt dafür das alle erstellten Meshs unter dem parent Element angefügt werden 
            meshObject.transform.localScale = Vector3.one * scale;//scalet die einzelnen Meshes auf die gewünschte größe
            SetVisible(false);//der TerrainChunk wird beim erstellen erstmals nicht angezeigt. 


            // setzt den LOD des lvlOfDetailMeshes auf die im lvlOfDetails übergebenen Werte.
            lodMeshes = new LODMesh[detailLevels.Length];

            for (int i = 0; i < detailLevels.Length; i++)
            {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateTerrainChunk); //Update TerrainChunk wird als callback übergeben
            }

            mapGenerator.RequestMapData(position, OnMapDataRecieved);
        }

        // Wenn die Berechnungen des extra Threads fertig sind soll aus der MapData die MeshData generiert werden.
        void OnMapDataRecieved(MapData mapData)
        {
            this.mapData = mapData;
            mapDataRecieved = true;

            Texture2D texture = TextureGenerator.TextureFromColourMap(mapData.colourMap, MapGenerator.mapChunkSize, MapGenerator.mapChunkSize); ///////// unnötigg
            meshRenderer.material.mainTexture = texture;

            UpdateTerrainChunk();//da nicht bei jedem Frame geupdatet wird, muss die methode manuell aufgerufen werden.
        }

   

        //überprüft ob der Punkt innehralb des aktuellen Chunks der am nähesten zum Spieler liegt innerhalb der maximalen View Distance liegt. Tut er das wird der Chunk angezeigt. Tut er das nicht ausgeblendet.
        public void UpdateTerrainChunk()
        {
            if (mapDataRecieved)
            { //nur wenn die MapData bereits vorhanden ist macht es Sinn diesen teil auszuführen. vorher können keine Meshes generiert werden.
                float viewerDstFromNearestEdge = bounds.SqrDistance(viewerPosition); 
                bool visible = viewerDstFromNearestEdge <= maxViewDst * maxViewDst;

                //setzt den LOD der Meshes auf basis der Distanz zum Spieler und die eingestellten LOD im Editor;
                if (visible)
                {
                    int lodindex = 0;//gibt an welcher LOD aufgrund der aktuellen Distanz zum Spieler dargestellt werden soll.
                    for (int i = 0; i < detailLevels.Length -1 ; i++)
                    {
                        if(viewerDstFromNearestEdge > detailLevels[i].distanceToNextLOD * detailLevels[i].distanceToNextLOD)
                        {
                            lodindex = i + 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if(lodindex != prevLOD)
                    {
                        LODMesh lodMesh = lodMeshes[lodindex]; // wenn die beiden Werte nicht überinstimmen ist das neue Mesh das lvlOfDetailMesh mit dem aktuell benötigten LOD;
                        if (lodMesh.hasMesh)
                        {
                            prevLOD = lodindex;
                            meshFilter.mesh = lodMesh.mesh; //das "alte" Mesh wird mit dem neuen Mesh mit richtigen LOD ersetzt.
                            
                        }
                        else if (!lodMesh.hasRequestedMesh)//wenn noch kein Mesh requested also generiert wurde wird heir ein neues erzeugt.
                        {
                            lodMesh.RequestMesh(mapData);
                        }
                    }
                    terrainChunksVisibleLastUpdate.Add(this); //fügt sich selbst der liste hinzu um später ausgeblendet zu werden wenn der viewer nicht mehr in Reichweite ist.
                }

                SetVisible(visible);
            }
        }
        //setzt das Mesh Aktiv je nachdem ob es sich in der Max View Distance befindet
        public void SetVisible(bool visible)
        {
<<<<<<< HEAD
           // meshObject.SetActive(visible);        //  JOSTI ÄNDERUNG; AUSGEKLAMMERT WEIL DER HINTERGRUND SONST IMMER NUR 1X PRO SCENE SICHTBAR WAR
        } 
=======
           
           meshObject.SetActive(visible);
        }
>>>>>>> master

        public void DetroyMeshes()
        {
            for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
            {
                Destroy(terrainChunksVisibleLastUpdate[i].meshObject);
            }

        }

        //public bool IsVisible()
        //{
        //    return meshObject.activeSelf;
        //}

    }

    class LODMesh
    {
        public Mesh mesh;
        public bool hasRequestedMesh;
        public bool hasMesh;
        int lod; // soll der RequestMesh Methode mitgegeben werden um festzulegen welcher LOD gerade benötigt wird. 
        System.Action updateCallback;

        public LODMesh(int lod, System.Action updateCallback)
        {
            this.lod = lod;
            this.updateCallback = updateCallback;
        }

        //callback Funktion der RequestMesh Meshode 
        void OnMeshDataReceived(MeshData meshData)
        {
            mesh = meshData.CreateMesh();
            hasMesh = true;

            updateCallback();
        }

        public void RequestMesh(MapData mapData)
        {
            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, OnMeshDataReceived);
        }

    
    }

    [System.Serializable]
    public struct LODInfo
    {
        public int lod;
        public float distanceToNextLOD;
    }


}
