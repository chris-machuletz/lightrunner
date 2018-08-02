using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawnManager : MonoBehaviour {

    public GameObject[] trackSections;
    // public Material trackMaterial; // Material für Streckenteile
    private Transform playerTransform;
    private float spawnZ = -15.0f; // -15, damit beim Start keine Lücke hinterm Spieler zu sehen ist
    private float trackSectionLength = 60.0f;
    private int amountofRenderedTracks = 5;
    private float safeZone = 90.0f; // Löscht Streckensegmente erst, nachdem Spieler sie passiert hat
    private int lastSpawnedTrack = 0;

    private List<GameObject> activeTracks;

    // Use this for initialization
    void Start()
    {
        //Spawns the amount of Rendered Tracks at the beginning
        activeTracks = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for(int i = 0; i < amountofRenderedTracks; i++)
        {
            if (i < 2)
                SpawnTrack(0);
            else
                SpawnTrack();
            
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
            }
        }
        
    }
    //Spanws a new Track
    private void SpawnTrack(int trackIndex = -1)
    {
        GameObject gameobj;
        if(trackIndex== -1)
            gameobj = Instantiate(trackSections[RandomTrackGenerator()]) as GameObject;
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



}
