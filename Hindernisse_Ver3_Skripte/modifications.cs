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
        moves.Add(XmoveBoth);
        moves.Add(YmoveUp);
        moves.Add(YmoveDown);
        moves.Add(YmoveBoth);
        moves.Add(Nothing1);
        moves.Add(Nothing2);
        moves.Add(Nothing3);
        moves.Add(Nothing4);

        Initialize(); //Aufrufen der Initialisierungsfunktion

    }

    public void Initialize()
    {
        obj = gameObject; //setzt das Objekt hier mit dem eigentlichen Obstacle gleich

        tarVecXpos = new Vector3(obj.transform.position.x + tposX, 0, 0); //Ort der Verschiebung (aktuelle Position + tposX)
        tarVecXneg = new Vector3(obj.transform.position.x + tposXneg, 0, 0); //Ort der Verschiebung (aktuelle Position + tposXneg)
        tarVecYpos = new Vector3(0, obj.transform.position.y + tposY, 0); //Ort der Verschiebung (aktuelle Position + tposY)
        tarVecYneg = new Vector3(0, obj.transform.position.x + tposYneg, 0); //Ort der Verschiebung (aktuelle Position + tposYneg)

        //PROBLEM BEI DEN ZIELPOSITIONEN: SIE SCHEINEN MIT GAMEOBJECT DAS EMPTY ZU MEINEN, NICHT DIE ERZEUGTEN HINDERNISSE AUS DER LISTE...
        //DADURCH VERSCHIEBEN SIE SICH OFT AUF DIE GLEICHE POSITION...
        //ANSATZ: DIE OBJEKTE SO ANSPRECHEN WIE BEI DER SELBSTKOLLISION

        int randi = Random.Range(0, moves.Count - 1); //erstellt eine Zufallszahl welche sich im Bereich der Größe der Funktionsliste befindet

        //Debug.Log("randi: " + randi);

        moves[randi](); //führt die Funktion an der Stelle "randi" aus EIGENTLICH UNNÖTIG?!

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

    public void XmoveBoth()
    {
        XmoveRight();
        XmoveLeft();
        //ruft beide Richtungen auf, bewegt sich hin und her (x mal)
    }

    public void YmoveUp()
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecYpos, movSpeed * Time.deltaTime); //sorgt für Bewegung des Objekts in Richtung der Zielposition
    }

    public void YmoveDown()
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecYneg, movSpeed * Time.deltaTime); //sorgt für Bewegung des Objekts in Richtung der Zielposition
    }

        public void YmoveBoth()
    {
        YmoveUp();
        YmoveDown();
        //ruft beide Richtungen auf, bewegt sich von Oben nach Unten (x mal)
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
            if (obj.transform.position != tarVecXpos) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                XmoveRight();
            }

            if (obj.transform.position != tarVecXneg) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                XmoveLeft();
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
            if (obj.transform.position != tarVecYpos) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                YmoveUp();
            }

            if (obj.transform.position != tarVecYneg) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                YmoveDown();
            }
        }



    }
}
