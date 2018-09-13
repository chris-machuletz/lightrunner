using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modifications : MonoBehaviour {

    public float movSpeed = 5f; //Geschwindigkeit mit der sich das Objekt bewegt
    public delegate void mov(); //erschafft delegate, der es erlaubt funktionen wie variablen zu nutzen
    public List<mov> moves = new List<mov>(); //liste von funktionen

    public int tposX = 5; //Grenze für Verschiebung nach rechts
    public int tposXneg = -5; //Grenze für Verschiebung nach links
    public int tposY = 5; //Grenze für Verschiebung nach oben
    public int tposYneg = -5; //Grenze für Verschiebung nach unten

    Vector3 tarVecXpos; //Vektor für Zielposition bei X (positiv)
    Vector3 tarVecXneg; //Vektor für Zielposition bei X (negativ)
    Vector3 tarVecYpos; //Vektor für Zielposition bei Y (positiv)
    Vector3 tarVecYneg; //Vektor für Zielposition bei X (negativ)

    bool xrb = false; //Boolean für die Auslösung von XmoveRight
    bool xlb = false; //Boolean für die Auslösung von XmoveLeft
    bool xbb = false; //Boolean für die Auslösung von XmoveBoth
    bool yub = false; //Boolean für die Auslösung von YmoveUp
    bool ydb = false; //Boolean für die Auslösung von YmoveDown
    bool ybb = false; //Boolean für die Auslösung von YmoveBoth
    bool n1 = false;
    bool n2 = false;
    bool n3 = false;
    bool n4 = false;

    bool both = false; //boolean für die coroutine beide Achsen
    float waitT = 2.5f; //wartezeit zwischen dem umschalten der coroutine für beide Achsen

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

        
        //int randi = Random.Range(0, moves.Count + 2); //erstellt eine Zufallszahl welche sich im Bereich der Größe der Funktionsliste befindet (plus 2 da die beiden Fkt.s für hin und her dazukommen
                                                      //wird genutzt, um eine Funktion zufällig aus der Liste zu wählen

        int randi = Random.Range(0, 9);
        

        if (randi == 0)
        {
            //Debug.Log("x rechts randi = " + randi);
            
            xrb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("XmoveRight");
        }

        if (randi == 1)
        {
            //Debug.Log("x links randi = " + randi);
            
            xlb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("XmoveLeft");
        }

        if (randi == 2)
        {
            //Debug.Log("x beide randi = " + randi);
            
            xbb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("XmoveBoth");
        }

        if (randi == 3)
        {
            //Debug.Log("y hoch randi = " + randi);
            
            yub = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("YmoveUp");
        }

        if (randi == 4)
        {
            //Debug.Log("y runter randi = " + randi);
            
            ydb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("YmoveDown");
        }

        if (randi == 5)
        {
            //Debug.Log("y beide randi = " + randi);
            
            ybb = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird
                        //Debug.Log("YmoveBoth");
        }

        if (randi == 6)
        {
            //Debug.Log("nichts randi = " + randi);

            n1 = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird         
        }

        if (randi == 7)
        {
            //Debug.Log("nichts randi = " + randi);

            n2 = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird         
        }

        if (randi == 8)
        {
            //Debug.Log("nichts randi = " + randi);

            n3 = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird         
        }

        if (randi == 9)
        {
            //Debug.Log("nichts randi = " + randi);

            n4 = true; //sorgt dafür dass die Funktion die in der Update wartet, ausgeführt wird         
        }

    }


    void Nothing1()
    {
        //ist ein Puffer sodass nicht jedes Hindernis sich bewegt
    }

    void Nothing2()
    {
        //ist ein Puffer sodass nicht jedes Hindernis sich bewegt
    }

    void Nothing3()
    {
        //ist ein Puffer sodass nicht jedes Hindernis sich bewegt
    }

    void Nothing4()
    {
        //ist ein Puffer sodass nicht jedes Hindernis sich bewegt
    }

    //NOTIZ: ES GIBT SICHERLICH BESSERE LÖSUNG FÜR DIE NOTHING MEHTODEN, ZUM BEISPIEL DASS DIE MODIFIKATIONEN ZUFÄLLIG AUF DIE HINDERNISSE ANGEWANDT WERDEN


    public void XmoveRight() //sorgt für Bewegung des Objekts in Richtung der Zielposition
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecXpos, movSpeed * Time.deltaTime); 
    }

    public void XmoveLeft() //sorgt für Bewegung des Objekts in Richtung der Zielposition
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecXneg, movSpeed * Time.deltaTime); 
    }

    public void YmoveUp() //sorgt für Bewegung des Objekts in Richtung der Zielposition
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecYpos, movSpeed * Time.deltaTime); 
    }

    public void YmoveDown() //sorgt für Bewegung des Objekts in Richtung der Zielposition
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, tarVecYneg, movSpeed * Time.deltaTime); 
    }

    
    IEnumerator XmRight() //Coroutine für Rechtsbewegung
    {
        yield return new WaitForSeconds(5); //wartet eine zufällige Zeit zwischen 0 und 5 Sekunden bevor es die Funktion ausführt

        if (obj.transform.position != tarVecXpos) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
        {
            XmoveRight();
        }
    }

    IEnumerator XmLeft() //Coroutine für Linksbewegung
    {
        yield return new WaitForSeconds(5); //wartet eine zufällige Zeit zwischen 0 und 5 Sekunden bevor es die Funktion ausführt
        
        if (obj.transform.position != tarVecXneg) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
        {
            XmoveLeft();
        }
    }

    IEnumerator XYBoth() //coroutine für beide Achsen (egal ob x oder y)
    {
        yield return new WaitForSeconds(waitT - 2); //wartet am Anfang eine Zeit lang, damit die Bewegung nicht sofort umspringt

        both = true; //setzt den bool auf true und löst damit die andere Bewegung aus

        yield return new WaitForSeconds(waitT); //wartet nochmal eine Zeit lang 

        both = false; //setzt den bool auf false und löst damit wieder die Bewegung vom Anfang aus
    }

    IEnumerator YmUp() //Coroutine für Bewegung nach oben
    {
        yield return new WaitForSeconds(5); //wartet eine zufällige Zeit zwischen 0 und 5 Sekunden bevor es die Funktion ausführt
        
        if (obj.transform.position != tarVecYpos) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
        {
            YmoveUp();
        }
    }

    IEnumerator YmDown() //Coroutine für Bewegung nach unten
    {
        yield return new WaitForSeconds(5); //wartet eine zufällige Zeit zwischen 0 und 5 Sekunden bevor es die Funktion ausführt
        
        if (obj.transform.position != tarVecYneg) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
        {
            YmoveDown();
        }
    }


    // Update is called once per frame
    void Update () {

        if (xrb == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {

           StartCoroutine("XmRight"); //führt Coroutine aus



            //ALTER WEG OHNE WARTEZEIT
            //if (obj.transform.position != tarVecXpos) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            //{
            //    XmoveRight();
            //}
        }


        if (xlb == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {
            //StartCoroutine("XmLeft");

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
            //StartCoroutine("YmUp");

            if (obj.transform.position != tarVecYpos) //Funktion wird solange ausgeführt, bis es die Zielposition erreicht hat
            {
                YmoveUp();
            }
        }


        if (ydb == true) //löst die entsprechende Funktion nur aus, wenn sie auch ausgewählt wurde
        {
            //StartCoroutine("YmDown");

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


        if (n1 == true)
        {
            Nothing1();
        }

        if (n2 == true)
        {
            Nothing2();
        }

        if (n3 == true)
        {
            Nothing3();
        }

        if (n4 == true)
        {
            Nothing4();
        }


    }


}
