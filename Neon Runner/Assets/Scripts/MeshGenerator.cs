using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {

public static MeshData GenerateTerrainMesh(float [,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int levelOfDetail)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        //sorgt dafür das das Mesh am Ende in der Mitte des Bildschirms ist.
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshSimplificationIncrement =(levelOfDetail == 0)?1:levelOfDetail * 2;
        int verticiesPerLine = (width - 1) / meshSimplificationIncrement + 1;

        //ruft die MeshData Funktion auf und übergibt width und height der generierten Map
        MeshData meshData = new MeshData(verticiesPerLine, verticiesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < height; y+= meshSimplificationIncrement)
        {
            for (int x = 0; x < width; x+= meshSimplificationIncrement)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x,y])* heightMultiplier, topLeftZ - y); // fügt die verticies an der aktuellen stelle meshData hinzu.  heightmao[x,y] ist der y Wert und y in dem Fall der z wert.
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                //Speichert die Information wo sich der Vertex innerhalb der Map befindet ?
                //ignoriert die letzte Reihe und Spalte der Verticies, da dort keine neuen Dreieecke mehr erzeugt werden müssen
                if(x < width -1 && y < height - 1)
                {
                    meshData.AddTriangles(vertexIndex, vertexIndex + verticiesPerLine + 1, vertexIndex + verticiesPerLine);
                    meshData.AddTriangles(vertexIndex + verticiesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }
        return meshData; // Daten werden zurückgegeben statt dem fertigen Mesh für später vlt zu implementierendes Threading. man kann innerhalb des threads keine neuen meshes erzeugen. deswegen returnen wir die MeshData innerhalb und erzeugen außerhalb des Threads ein neues Mesh.
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
    //Ertsellt ein Mesh und berechnet die normalen neu wegen Beleuchtung etc
    public Mesh CreateMesh ()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

}