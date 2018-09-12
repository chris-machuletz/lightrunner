using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawn : MonoBehaviour {

    public float min = 0.1f; //mindestzeit die vergeht bis zum nächsten Spawn
    public float max = 0.5f; //höchstzeit die zum nächsten Spawn vergeht
    public GameObject obstacl; //ein nutzbares Gameobject
    public float ranx, rany, ranz; //achsen-werte die zufällig sein sollen
    public float borderx = 100f; //obere Grenze für zufälligen x-wert
    public float bordery = 0f; //obere Grenze für y-wert
    public float borderz = 400f; //obere Grenze für zufälligen z-wert
    public Vector3 posVec; //Vector für neue Position
    public List<GameObject> obstacls = new List<GameObject>(); //macht die erzeugten gameObjects zugreifbar/ speichert sie
    public int obstcount; //Listenzähler --> nicht benutzt


    public float RandomizeX() //setzt den zu verwendenden x-wert zufällig für spawn
    {
        ranx = Random.Range(-borderx, borderx);
        return ranx;
    }

    public float RandomizeY() //setzt den zu verwendenden x-wert zufällig
    {
        //rany = Random.Range(0, bordery);
        rany = -0.5f;
        return rany;
    }

    public float RandomizeZ() //setzt den zu verwendenden z-wert zufällig
    {
        ranz = Random.Range(0, borderz);
        return ranz;
    }

    public int NumberPlusOne = 100;

    // Use this for initialization
    public void Test()
    { 
        //Invoke("Instanciate", (Random.Range(min, max))); //ruft die fkt zu zufälligen Zeiten auf, was dazu führt, 
                                                           //dass zufällig viele Instanzen der Hindernisse zu zufälligen Zeiten gespawnt werden

        //ruft nur einmal zu Beginn die Hindernissfkt. auf und erzeugt i+1 Instanzen von ihnen
        for(int i = 0; i<= NumberPlusOne; i++)
        {
            Instanciate();
        }

    }

    public void Instanciate()
    {
        //float rant = Random.Range(min, max); //random time im bereich von min - max --> nicht benötigt, wenn die Hindernisse alle auf einmal spawnen sollen

        //hier dazwischen den code für das spawnobject einfügen

        //obstacl.AddComponent<cub>();
        obstacl = GetComponent<cub>().Create(); //ruft create funktion von cub auf und damit dessen gameObject
        obstacl.AddComponent<modifications>(); //fügt Skript mit Modifikationen hinzu
        obstacl.GetComponent<modifications>().Start();

        //obstacl.AddComponent<col_self>(); // fügt selbstkollisions-script hinzu EDIT: RAUSGENOMMEN WEIL HINDERLICH/ VERWIRREND BEI BEWEGL. HINDERNISSEN
        obstacls.Add(obstacl);  //fügt grade erschaffenes obstacle der Liste hinzu

        if(obstacls.Count >= 350 )
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(obstacls[0]);
                obstacls.RemoveAt(0);
            }
        }

        //obstcount = obstacls.Count-1; //funktioniert nur für das zum Zeitpunkt des Löschens aktuellste erschaffene Obstacle... nicht nacheinander in der Reihenfolge
        //obstcount++;

        //Code für zufällige Position aufrufen
        RandomizeX();
        RandomizeY();
        RandomizeZ();

        obstacl.transform.position = GameObject.Find("TrackSpawnManager").GetComponent<TrackSpawnManager>().activeTracks[GameObject.Find("TrackSpawnManager").GetComponent<TrackSpawnManager>().activeTracks.Count-1].transform.position;

        
        //tsm.GetComponent<TrackSpawnManager>();
        //obstacl = tsm.activeTracks[tsm.activeTracks.Count-1];


        posVec = new Vector3(ranx, rany, ranz);//speichern von zufälligen Werten in Vector
        obstacl.transform.position += posVec;//zufällige Position zuweisen
        obstacl.transform.localScale += new Vector3(3, Random.Range(0, 10), 3); //skaliert die obstacles zufällig


        //Invoke("Instanciate", rant); //ruft sich selber nochmal auf --> nicht benötigt, wenn die Hindernisse alle auf einmal spawnen sollen
    }

}



