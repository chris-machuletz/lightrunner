using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawnManager : MonoBehaviour {

    public GameObject[] trackSections;
    public GameObject lumenCube;
    public GameObject lifeCube;
    public GameObject indestructableCube;

    private int anzLumenCubes = 50;
    private int anzLifeCubes = 20;
    private int anzIndestructableCubes = 20;

    // public Material trackMaterial; // Material für Streckenteile
    private Transform playerTransform;
    public float spawnZ = -15.0f; // -15, damit beim Start keine Lücke hinterm Spieler zu sehen ist
    private float trackSectionLength = 400.0f;
    private int amountofRenderedTracks = 5;
    private float safeZone = 500.0f; // Löscht Streckensegmente erst, nachdem Spieler sie passiert hat
    private int lastSpawnedTrack = 0;

    public List<GameObject> activeTracks;
    private List<GameObject> activeLumenCubes;
    private List<GameObject> activeLifeCubes;
    private List<GameObject> activeIndestructableCubes;

    // Use this for initialization
    void Start()
    {
        //Spawns the amount of Rendered Tracks at the beginning
        activeTracks = new List<GameObject>();
        activeLumenCubes = new List<GameObject>();
        activeLifeCubes = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i = 0; i < amountofRenderedTracks; i++)
        {
            if (i < 1) // als erste 2 Tracksections werden tracksection[0] gespawnt, auf dem sich noch keine Hindernisse befinden
            {
                SpawnTrack(0);

            }
            else
            {
                SpawnTrack();

                SpawnLumenCubes();
                DeleteLumenCubes();

                SpawnLifeCubes();
                DeleteLifeCubes();

                //SpawnIndestructableCubes();
                //DeleteIndestructableCubes();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Ship")){
            //If Player has passed the safeZone: a new track gets spawned and the first gets deleted
            if (playerTransform.position.z > (spawnZ + safeZone - amountofRenderedTracks * trackSectionLength))
            {
                SpawnTrack();
                DeleteTrack();

                SpawnLumenCubes();
                DeleteLumenCubes();

                SpawnLifeCubes();
                DeleteLifeCubes();

                SpawnIndestructableCubes();
                DeleteIndestructableCubes();
            }
        }
    }



    //Spanws a new Track
    private void SpawnTrack(int trackIndex = -1)
    {
        GameObject gameobj;

        if (trackIndex == -1)
        {
            gameobj = Instantiate(trackSections[RandomTrackGenerator()]) as GameObject;

        }
        else
            gameobj = Instantiate(trackSections[0]) as GameObject;

        //Renderer rend = gameobj.GetComponent<Renderer>(); //Verweist auf den Renderer des Gameobjects Track
        //if(rend != null) //wenn der Renderer vorhanden ist, weise das Material zu
        //{
        //    rend.material = trackMaterial;
        //    Debug.Log("Material geladen");
        //}

        gameobj.transform.SetParent(transform);
        gameobj.transform.position = Vector3.forward * spawnZ;


        spawnZ += trackSectionLength;
        activeTracks.Add(gameobj);
        gameObject.AddComponent<randomSpawn>();
        gameObject.GetComponent<randomSpawn>().Test();
    }
    //Deletes the Track the Player already passed
    private void DeleteTrack()
    {
        Destroy(activeTracks[0]);
        activeTracks.RemoveAt(0);
    }

    //generates a Random Track (Baukastensystem)
    private int RandomTrackGenerator()
    {
        if(trackSections.Length <= 1)
        return 0;

        int rndIndex = lastSpawnedTrack;
        while(rndIndex == lastSpawnedTrack)
        {
            rndIndex = Random.Range(0, trackSections.Length);
        }
        lastSpawnedTrack = rndIndex;
        return rndIndex;
    }

    private void SpawnLumenCubes()
    {

        for(int i = 0; i < anzLumenCubes; i++)
        {
            Vector3 cubePos = new Vector3(Random.Range(-100.0f, 100.0f), 0.6f, Random.Range(spawnZ - trackSectionLength, spawnZ));

            GameObject lcube;
            lcube = Instantiate(lumenCube, cubePos, Quaternion.identity) as GameObject;

            activeLumenCubes.Add(lcube);
        }
    }
    private void DeleteLumenCubes()
    {
        while (activeLumenCubes.Count > (anzLumenCubes * 5)) // so werden Cubes hinterm spieler korrekt entfernt, unabhängig von der anzahl der Cubes die auf dem Tracksegment gespawnt werden sollen
        {
            Destroy(activeLumenCubes[0]);
            activeLumenCubes.RemoveAt(0);
        }
    }

    private void SpawnLifeCubes()
    {
        for (int i = 0; i < anzLifeCubes; i++)
        {
            Vector3 cubePos = new Vector3(Random.Range(-100.0f, 100.0f), 0.6f, Random.Range(spawnZ - trackSectionLength, spawnZ));

            GameObject licube;
            licube = Instantiate(lifeCube, cubePos, Quaternion.identity) as GameObject;

            activeLifeCubes.Add(licube);
        }
    }

    private void DeleteLifeCubes()
    {
        while (activeLifeCubes.Count > (anzLifeCubes * 5)) // so werden Cubes hinterm spieler korrekt entfernt, unabhängig von der anzahl der Cubes die auf dem Tracksegment gespawnt werden sollen
        {
            Destroy(activeLifeCubes[0]);
            activeLifeCubes.RemoveAt(0);
        }
    }

    private void SpawnIndestructableCubes()
    {

        for (int i = 0; i < anzIndestructableCubes; i++)
        {
            Vector3 cubePos = new Vector3(Random.Range(-100.0f, 100.0f), 0.6f, Random.Range(spawnZ - trackSectionLength, spawnZ));

            GameObject indcube;
            indcube = Instantiate(indestructableCube, cubePos, Quaternion.identity) as GameObject;

            activeIndestructableCubes.Add(indcube);
        }
    }
    private void DeleteIndestructableCubes()
    {
        while (activeIndestructableCubes.Count > (anzLumenCubes * 5)) // so werden Cubes hinterm spieler korrekt entfernt, unabhängig von der anzahl der Cubes die auf dem Tracksegment gespawnt werden sollen
        {
            Destroy(activeIndestructableCubes[0]);
            activeIndestructableCubes.RemoveAt(0);
        }
    }
}
