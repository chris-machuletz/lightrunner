using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpawnManager : MonoBehaviour {

    public GameObject[] trackSections;
    private Transform playerTransform;
    private float spawnZ = -15.0f; // -15, damit beim Start keine Lücke hinterm Spieler zu sehen ist
    private float trackSectionLength = 60.0f;
    private int amountofRenderedTracks = 4;
    private float safeZone = 90.0f; // Löscht Streckensegmente erst, nachdem Spieler sie passiert hat

    private List<GameObject> activeTracks;

    // Use this for initialization
    void Start()
    {
        activeTracks = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for(int i = 0; i < amountofRenderedTracks; i++)
        {
            SpawnTrack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z > (spawnZ + safeZone - amountofRenderedTracks * trackSectionLength))
        {
            SpawnTrack();
            DeleteTrack();
        }
    }

    void SpawnTrack(int trackIndex = -1)
    {
        GameObject gameobj;
        gameobj = Instantiate(trackSections[0]) as GameObject;
        gameobj.transform.SetParent(transform);
        gameobj.transform.position = Vector3.forward * spawnZ;
        spawnZ += trackSectionLength;
        activeTracks.Add(gameobj);
    }

    void DeleteTrack()
    {
        Destroy(activeTracks[0]);
        activeTracks.RemoveAt(0);
    }
}
