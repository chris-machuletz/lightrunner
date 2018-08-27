using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_manager : MonoBehaviour {

    GameObject ship1, ship2, ship3, ship4, ship5;
    Vector3 camRayVec = new Vector3(0.5f, 0.5f, 0);
    bool s1 = false;
    bool s2 = false;
    bool s3 = false;
    bool s4 = false;
    bool s5 = false;
    float waitT = 0.5f; //wartezeit zwischen dem umschalten


    // Use this for initialization
    void Start () {

        Instanciation();

	}
	

    void Instanciation()
    {
        //findet das entsprechende GameObject und weist ihm eine neue Position zu

        ship1 = GameObject.Find("ship01");
        ship1.transform.position = new Vector3(0,0,-22);

        ship2 = GameObject.Find("ship01_neonframe");
        ship2.transform.position = new Vector3(0, 0, -10);

        ship3 = GameObject.Find("ship02");
        ship3.transform.position = new Vector3(0, 0, 2);

        ship4 = GameObject.Find("ship03");
        ship4.transform.position = new Vector3(0, 0, 14);

        ship5 = GameObject.Find("ship03_neonframe");
        ship5.transform.position = new Vector3(0, 0, 26);

    }



    void OnClick()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            RaycastHit hit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); //nimmt Ray von der aktuellen Mausposition auf, um EIngabe über Mausklick zu ermöglichen
            Ray camRay = Camera.main.ViewportPointToRay(camRayVec); //nimmt Ray von der Kameraposition auf, um Eingabe über Enter-Taste zu ermöglichen

            if (Physics.Raycast(mouseRay, out hit) || Physics.Raycast(camRay, out hit)) //sucht sich entsprechende Eingabe aus und gibt den Schiffsnamen zurück
            {
                if (hit.transform.name == "ship01") { 
                    Debug.Log("ship01 selected");
                    s1 = true; //Coroutinen-Aktivierung

                    //ship1.transform.Rotate(new Vector3(0, 0, 20 * Time.deltaTime));
                }

                if (hit.transform.name == "ship01_neonframe") {
                    Debug.Log("ship01_neonframe selected");
                    s2 = true; //Coroutinen-Aktivierung
                }

                if (hit.transform.name == "ship02")
                {
                    Debug.Log("ship02 selected");
                    s3 = true; //Coroutinen-Aktivierung
                }

                if (hit.transform.name == "ship03")
                {
                    Debug.Log("ship03 selected");
                    s4 = true; //Coroutinen-Aktivierung
                }

                if (hit.transform.name == "ship03_neonframe")
                {
                    Debug.Log("ship03_neonframe selected");
                    s5 = true; //Coroutinen-Aktivierung
                }


            }
        }
    }



    IEnumerator turnS1() //coroutine für Drehung von Schiff 1
    {
        ship1.transform.Rotate(0, 0, 500 * Time.deltaTime); //lässt sich das Schiff einmal schnell Drehen (für Auswahlanimation)

        yield return new WaitForSeconds(waitT); //wartet 

        s1 = false; //setzt den bool auf false und löst damit wieder die Drehung vom Anfang aus
    }

    IEnumerator turnS2() //coroutine für Drehung von Schiff 2
    {
        ship2.transform.Rotate(0, 0, 500 * Time.deltaTime); //lässt sich das Schiff einmal schnell Drehen (für Auswahlanimation)

        yield return new WaitForSeconds(waitT); //wartet 

        s2 = false; //setzt den bool auf false und löst damit wieder die Drehung vom Anfang aus
    }

    IEnumerator turnS3() //coroutine für Drehung von Schiff 3
    {
        ship3.transform.Rotate(0, 0, 500 * Time.deltaTime); //lässt sich das Schiff einmal schnell Drehen (für Auswahlanimation)

        yield return new WaitForSeconds(waitT); //wartet 

        s3 = false; //setzt den bool auf false und löst damit wieder die Drehung vom Anfang aus
    }

    IEnumerator turnS4() //coroutine für Drehung von Schiff 4
    {
        ship4.transform.Rotate(0, 0, 500 * Time.deltaTime); //lässt sich das Schiff einmal schnell Drehen (für Auswahlanimation)

        yield return new WaitForSeconds(waitT); //wartet 

        s4 = false; //setzt den bool auf false und löst damit wieder die Drehung vom Anfang aus
    }

    IEnumerator turnS5() //coroutine für Drehung von Schiff 5
    {
        ship5.transform.Rotate(0, 0, 500 * Time.deltaTime); //lässt sich das Schiff einmal schnell Drehen (für Auswahlanimation)

        yield return new WaitForSeconds(waitT); //wartet 

        s5 = false; //setzt den bool auf false und löst damit wieder die Drehung vom Anfang aus
    }



    // Update is called once per frame
    void Update () {

        //ruft die Raycast Funktion auf

        OnClick();

        //lässt die Raumschiffe sich um 1 pro Sekunde um die y Achse drehen (wenn nichts ausgewählt wird

        if (s1 == false) { ship1.transform.Rotate(0, 0, 5 * Time.deltaTime); }
        if (s1 == true) { StartCoroutine("turnS1"); }

        if (s2 == false) { ship2.transform.Rotate(0, 0, 5 * Time.deltaTime); }
        if (s2 == true) { StartCoroutine("turnS2"); }

        if (s3 == false) { ship3.transform.Rotate(0, 0, 5 * Time.deltaTime); }
        if (s3 == true) { StartCoroutine("turnS3"); }

        if (s4 == false) { ship4.transform.Rotate(0, 0, 5 * Time.deltaTime); }
        if (s4 == true) { StartCoroutine("turnS4"); }

        if (s5 == false) { ship5.transform.Rotate(0, 0, 5 * Time.deltaTime); }
        if (s5 == true) { StartCoroutine("turnS5"); }


    }
}
