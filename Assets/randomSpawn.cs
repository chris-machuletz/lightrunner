using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawn : MonoBehaviour {

    public float min = 0.1f; // mindestzeit die vergeht bis zum nächsten Spawn
    public float max = 0.5f; // höchstzeit die zum nächsten Spawn vergeht
    public GameObject obstacl; // ein nutzbares Gameobject
    public float ranx, rany, ranz; // achsen-werte die zufällig sein sollen
    public float borderx = 3f; // obere Grenze für zufälligen x-wert
    public float bordery = 0f; // obere Grenze für y-wert
    public float borderz = 5f; //obere Grenze für zufälligen z-wert
    public Vector3 posVec; // Vector für neue Position
    public List<GameObject> obstacls = new List<GameObject>(); // macht die erzeugten gameObjects zugreifbar/ speichert sie
    public int obstcount; // Listenzähler --> nicht benutzt
    

    public float RandomizeX() // setzt den zu verwendenden x-wert zufällig
    {
        ranx = Random.Range(-borderx, borderx);
        return ranx;
    }

    public float RandomizeY() // setzt den zu verwendenden x-wert zufällig
    {
        rany = Random.Range(0, bordery);
        return rany;
    }

    public float RandomizeZ() // setzt den zu verwendenden z-wert zufällig
    {
        ranz = Random.Range(-borderz, borderz);
        return ranz;
    }


    // Use this for initialization
    void Start()
    { 
        //Invoke("Instanciate", (Random.Range(min, max))); // ruft die fkt zu zufälligen Zeiten auf, was dazu führt, 
                                                           //dass zufällig viele Instanzen der Hindernisse zu zufälligen Zeiten gespawnt werden


        //ruft nur einmal zu Beginn die Hindernissfkt. auf und erzeugt i+1 Instanzen von ihnen
        for(int i = 0; i<=5; i++)
        {
            Instanciate();
        }

    }

    public void Instanciate()
    {
        float rant = Random.Range(min, max); // random time im bereich von min - max

        // hier dazwischen den code für das spawnobject einfügen
        obstacl = GetComponent<cub>().Create(); // ruft create funktion von cub auf und damit dessen gameObject
        obstacl.AddComponent<col_self>(); // fügt selbstkollisions-script hinzu
        obstacls.Add(obstacl);  // fügt grade erschaffenes obstacle der Liste hinzu

        //obstcount = obstacls.Count-1; // funktioniert nur für das zum Zeitpunkt des Löschens aktuellste erschaffene Obstacle... nicht nacheinander in der Reihenfolge
        //obstcount++;

        // code für zufällige Position aufrufen (wird hier aufgerufen, da man ja neue Positionen pro spawn will und nicht nach gewisser Zeit oder pro Frame
        RandomizeX();
        RandomizeY();
        RandomizeZ();
       
        posVec = new Vector3(ranx, rany, ranz);// speichern vpn zufälligen Werten in Vector
        obstacl.transform.position += posVec;// zufällige Position zuweisen
        obstacl.transform.localScale += new Vector3(0, Random.Range(0, 4), 0); // skaliert die obstacles



        
        //Invoke("Instanciate", rant); // ruft sich selber nochmal auf
   }
}



