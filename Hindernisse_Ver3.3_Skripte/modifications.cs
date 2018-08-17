using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modifications : MonoBehaviour {

    public float movSpeed = 5f; //Geschwindigkeit mit der sich das Objekt bewegt
    public delegate void mov(); // erschafft delegate, der es erlaubt funktionen wie variablen zu nutzen
    public List<mov> moves = new List<mov>(); // liste von funktionen

    public int tposX = 5; // Grenze für Verschiebung nach rechts
    public int tposXneg = -5; // Grenze für Verschiebung nach links
    public int tposY = 5; // Grenze für Verschiebung nach oben
    public int tposYneg = -5; // Grenze für Verschiebung nach unten

    Vector3 tarVecXpos; //Vektor für Zielposition bei X (positiv)
    Vector3 tarVecXneg; //Vektor für Zielposition bei X (negativ)
    Vector3 tarVecYpos; //Vektor für Zielposition bei Y (positiv)
    Vector3 tarVecYneg; //Vektor für Zielposition bei X (negativ)

    bool xrb = false; //Boolean für die Auslösung von XmoveRight
    bool xlb = false; //Boolean für die Auslösung von XmoveLeft
    bool xbb = false;
    bool yub = false; //Boolean für die Auslösung von YmoveUp
    bool ydb = false; //Boolean für die Auslösung von YmoveDown
    bool ybb = false;

    public GameObject obj; //Platzhalterobjekt



    // Use this for initialization
    public void Start () {
        
        //Hinzufügen der jeweiligen Funktionen in die Liste
        moves.Add(XmoveRight);
        moves.Add(XmoveLeft);
        moves.Add(YmoveUp);
        moves.Add(YmoveDown);
        moves.Add(Nothing1);
        moves.Add(Nothing2);
        moves.Add(Nothing3);
        moves.Add(Nothing4);

        Initialize(); //Aufrufen der Initialisierungsfunktion

    }

    
    public void Initialize()
    {
        obj = gameObject; //setzt das Objekt hier mit dem eigentlichen Obstacle gleich


        tarVecXpos = new Vector3(obj.transform.position.x + tposX, obj.transform.position.y, obj.transform.position.z); //Ort der Verschiebung (aktuelle Position + tposX)
        tarVecXneg = new Vector3(obj.transform.position.x + tposXneg, obj.transform.position.y, obj.transform.position.z); //Ort der Verschiebung (aktuelle Position + tposXneg)
        tarVecYpos = new Vector3(obj.transform.position.x, obj.transform.position.y + tposY, obj.transform.position.z); //Ort der Verschiebung (aktuelle Position + tposY)
        tarVecYneg = new Vector3(obj.transform.position.x, obj.transform.position.y + tposYneg, obj.transform.position.z); //Ort der Verschiebung (aktuelle Position + tposYneg)
        

        int randi = Random.Range(0, moves.Count - 1); //erstellt eine Zufallszahl welche sich im Bereich der Größe der Funktionsliste befindet


        if (randi == 0)
        {
            xrb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("XmoveRight");
        }

        if (randi == 1)
        {
            xlb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("XmoveLeft");
        }

        if (randi == 2)
        {
            xbb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("XmoveBoth");
        }

        if (randi == 3)
        {
            yub = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("YmoveUp");
        }

        if (randi == 4)
        {
            ydb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("YmoveDown");
        }

        if (randi == 5)
        {
            ybb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("YmoveBoth");
        }
        
    }


    public void Nothing1()
    {
        Debug.Log("You shall not move!"); //ist ein Puffer sodass nicht jedes Hindernis sich bewegt
    }

    public void Nothing2()
    {
        Debug.Log("You shall not move!"); //ist ein Puffer sodass nicht jedes Hindernis sich bewegt
    }

    public void Nothing3()
    {
        Debug.Log("You shall not move!"); //ist ein Puffer sodass nicht jedes Hindernis sich bewegt
    }

    public void Nothing4()
    {
        Debug.Log("You shall not move!"); //ist ein Puffer sodass nicht jedes Hindernis sich bewegt
    }

    //NOTIZ: ES GIBT SICHERLICH BESSERE LÖSUNG FÜR DIE NOTHING MEHTODEN, ZUM BEISPIEL DASS DIE MODIFIKATIONEN ZUFÄLLIG AUF DIE HINDERNISSE ANGEWANDT WERDEN


    public void XmoveRight()
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecXpos, movSpeed * Time.deltaTime); //sorgt für Bewegung des Objekts in Richtung der Zielposition
    }

    public void XmoveLeft()
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecXneg, movSpeed * Time.deltaTime); //sorgt für Bewegung des Objekts in Richtung der Zielposition
    }

    public void YmoveUp()
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecYpos, movSpeed * Time.deltaTime); //sorgt für Bewegung des Objekts in Richtung der Zielposition
    }

    public void YmoveDown()
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecYneg, movSpeed * Time.deltaTime); //sorgt für Bewegung des Objekts in Richtung der Zielposition
    }


    bool both = false;
    float waitT = 2.5f; //wartezeit zwischen dem umschalten

    IEnumerator XYBoth() //coroutine für beide Achsen
    {
        yield return new WaitForSeconds(waitT-2); //wartet am Anfang eine Zeit lang, damit die Bewegung nicht sofort umspringt

        both = true; //setzt den bool auf true und löst damit die andere Bewegung aus

        yield return new WaitForSeconds(waitT); //wartet nochmal eine Zeit lang 
    
        both = false; //setzt den bool auf false und löst damit wieder die Bewegung vom Anfang aus
    }



    // Update is called once per frame
    void Update () {

        if (xrb == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {
            
            if (obj.transform.position != tarVecXpos) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                XmoveRight();
            }
        }


        if (xlb == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {
            if (obj.transform.position != tarVecXneg) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                XmoveLeft();
            }
        }


        if (xbb == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {

            if (!both){
                XmoveRight(); //bewegt sich für einen Ablauf nach Rechts (während der bool true ist)

                if(obj.transform.position == tarVecXpos) //sobald das Objekt seinen bestimmungsort erreicht hat...
                {
                  StartCoroutine("XYBoth"); //...wird die coroutine ausgelöst, die den bool umschaltet
                }
            }

            if (both) //sobald der bool umgeschaltet ist...
            {
                XmoveLeft(); //...wandert das objekt in die entgegengesetzte richtung
            }
                
        }


        if (yub == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {
            if (obj.transform.position != tarVecYpos) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                YmoveUp();
            }
        }


        if (ydb == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {
            if (obj.transform.position != tarVecYneg) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                YmoveDown();
            }
        }


        if (ybb == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {
            if (!both)
            {
                YmoveUp(); //bewegt sich für einen Ablauf nach Rechts (während der bool true ist)

                if (obj.transform.position == tarVecYpos) //sobald das Objekt seinen bestimmungsort erreicht hat...
                {
                    StartCoroutine("XYBoth"); //...wird die coroutine ausgelöst, die den bool umschaltet
                }
            }

            if (both) //sobald der bool umgeschaltet ist...
            {
                YmoveDown(); //...wandert das objekt in die entgegengesetzte richtung
            }
        }
        
    }
}
