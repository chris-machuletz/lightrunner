using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#####Inhalt Skript######
//Normalen werden berechnet. 
//Methode zum Mesh erzeugen.
// verticies und die daraus gebildeten triangles werden dem Mesh hinzugefügt. 
// die MeshData Klasse befindet sich hier. (enthält triangles, verticies und uvs)
// Die "Fertige" Meshdata wird zurückgegeben.





public static class MeshGenerator {

public static MeshData GenerateTerrainMesh(float [,] heightMap, float heightMultiplier, AnimationCurve oneheightCurve, int levelOfDetail)
    {
        AnimationCurve heightCurve = new AnimationCurve(oneheightCurve.keys);// jeder Thread hat seine eigen heightCurve. ansonsten gibt es bugs und Das Mesh hat extreme spitzen.
        int meshSize = heightMap.GetLength(0);

        //sorgt dafür das das Mesh am Ende in der Mitte des Bildschirms ist.
        float topLeftX = (meshSize - 1) / -2f;
        float topLeftZ = (meshSize - 1) / 2f;

        int meshSimplificationIncrement =(levelOfDetail == 0)?1:levelOfDetail * 2;
        int verticiesPerLine = (meshSize - 1) / meshSimplificationIncrement + 1; //Berechnet die verticiesperLine um die größe der MeshData festlegen zu können 

        //ruft die MeshData Funktion auf und übergibt width und height der generierten Map
        MeshData meshData = new MeshData(verticiesPerLine, verticiesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < meshSize; y+= meshSimplificationIncrement)
        {
            for (int x = 0; x < meshSize; x+= meshSimplificationIncrement)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x,y])* heightMultiplier, topLeftZ - y); // fügt die verticies an der aktuellen stelle meshData hinzu.  heightmao[x,y] ist der y Wert und y in dem Fall der z wert.
                meshData.uvs[vertexIndex] = new Vector2(x / (float)meshSize, y / (float)meshSize);
                //Speichert die Information wo sich der Vertex innerhalb der Map befindet ?
                //ignoriert die letzte Reihe und Spalte der Verticies, da dort keine neuen Dreieecke mehr erzeugt werden müssen
                if(x < meshSize -1 && y < meshSize - 1)
                {
                    meshData.AddTriangles(vertexIndex, vertexIndex + verticiesPerLine + 1, vertexIndex + verticiesPerLine);
                    meshData.AddTriangles(vertexIndex + verticiesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }
        meshData.ThreadNormals(); // berechnet die normalen im selben Thread wo das Mesh generiert wird. 

        return meshData; // man kann innerhalb des threads keine neuen meshes erzeugen. deswegen returnen wir die MeshData innerhalb und erzeugen außerhalb des Threads ein neues Mesh.
    }
}
//Speichert die Daten des Meshs
public class MeshData
{
    //Array für verticies und triangles wie bei snake
    public Vector3[] vertices;
    public int[] triangles;
    //erlaubt später texturen auf dem Mesh
    public Vector2[] uvs;

    Vector3[] threadnormals;//wird benötigt damit wir die normlas nicht im Hauptthread berechnen und sorgt für weniger Spikes.

    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }
    //fügt ein Dreieck dem mesh hinzu
    public void AddTriangles(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;
        triangleIndex += 3;

    }            
    //Berechnen der normalen. Wird hier selbst geschriebenb, damit der Übergang zwischen Chunks die richtigen noralen hat, und kein sichtbarer Übergang mehr zu sehen ist 
    Vector3[] CalculateNormals()
    {
        Vector3[] vertexnormals = new Vector3[vertices.Length];// erstellt ein neues vector3 array welches die normalen der verticies enthält und dementsprechend gleich groß wie das Array der verticies ist
        int triangleCount = triangles.Length / 3; //berechnet wie viele Dreiecke wir erzeugt haben 
        for (int i = 0; i < triangleCount; i++)
        {
            int normalTriangleIndex = i * 3; //Berechnet den Index des Dreiecks. mal 3 da jedes Dreieck 3 verticies hat
            int vertIndexA = triangles[normalTriangleIndex]; //schreibt die einzelnen Indexe der Vertex des Dreiecks in die Variablen
            int vertIndexB = triangles[normalTriangleIndex + 1];
            int vertIndexC = triangles[normalTriangleIndex + 2];

            Vector3 triangleNormal = SurfaceNormals(vertIndexA, vertIndexB, vertIndexC); //Jeder Vertex bekommt die zugehörige Normale zugewiesen.
            vertexnormals[vertIndexA] += triangleNormal;
            vertexnormals[vertIndexB] += triangleNormal;
            vertexnormals[vertIndexC] += triangleNormal;
        }

        for (int i = 0; i < vertexnormals.Length; i++)//Normalisiert jeden Vektor des vertexnormals Array
        {
            vertexnormals[i].Normalize();
        }

        return vertexnormals;
    }                

    Vector3 SurfaceNormals (int a, int b, int c)//Um die normalen zu berechnen, berechen wir das Kreuzprodukt aus den Vektoren der 3 vertex die ein Dreieck bilden.
    {
        Vector3 pointA = vertices[a];
        Vector3 pointB = vertices[b];
        Vector3 pointC = vertices[c];

        Vector3 sideAB = pointB - pointA;
        Vector3 sideAC = pointC - pointA;

        return Vector3.Cross(sideAB, sideAC).normalized;
    }
    //Extra MEthode die die Funktion aufruft da diese nicht im Main Thread ausgeführt wird. 
    public void ThreadNormals()
    {
        threadnormals = CalculateNormals();
    }


    //Ertsellt ein Mesh und berechnet die normalen (threadnormals)
    public Mesh CreateMesh ()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.normals = threadnormals;
        return mesh;
    }

}