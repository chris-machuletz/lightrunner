using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumenCubeSpawnManager : MonoBehaviour {

    public float width; // X-Range, in der Cubes gespawnt werden
    public float depth; // Z-Range, in der Cubes gespawnt werden
    public int cubes; // Anzahl der Cubes, die in dem Bereich generiert werden sollen
    private Transform playerTransform;

    private float spawnZ = -15.0f; // -15, damit beim Start keine Lücke hinterm Spieler zu sehen ist
    private float safeZone = 90.0f; // Löscht Streckensegmente erst, nachdem Spieler sie passiert hat
    private int amountofRenderedTracks = 5;
    private float trackSectionLength = 60.0f;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Ship"))
        {
            //If Player has passed the safeZone: new cubes get spawned and the first stash of cubes gets deleted
            if (playerTransform.position.z > (spawnZ + safeZone - amountofRenderedTracks * trackSectionLength))
            {
                SpawnLumenCubes();
                //DeleteLumenCubes();
            }
        }
    }
    void SpawnLumenCubes()
    {

    }
}
