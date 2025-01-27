using UnityEngine;
using System.Collections;
public class MeshCreation : MonoBehaviour
{
    // Set up width and height variables
    // These are required to define our vertices
    public float meshWidth = 10f;
    public float meshHeight = 10f;
    public float meshDepth = 10f;
    // Use this for initialisation
    void Start()
    {
        // Create mesh filter using GetComponent<meshfilter>
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        // Vertices
        Vector3[] vertices = new Vector3[8] {
        // Front face
            new Vector3(0, 0, 0),                           // Bottom-left-front 
            new Vector3(meshWidth, 0, 0),                  // Bottom-right-front 
            new Vector3(0, meshHeight, 0),                 // Top-left-front 
            new Vector3(meshWidth, meshHeight, 0),         // Top-right-front 

            // Back face
            new Vector3(0, 0, -meshDepth),                 // Bottom-left-back 
            new Vector3(meshWidth, 0, -meshDepth),         // Bottom-right-back 
            new Vector3(0, meshHeight, -meshDepth),        // Top-left-back 
            new Vector3(meshWidth, meshHeight, -meshDepth) // Top-right-back 
        };

        // Triangles
        int[] triangles = new int[36];
        // Front Face
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;

        // Back Face
        triangles[6] = 5;
        triangles[7] = 7;
        triangles[8] = 4;
        triangles[9] = 7;
        triangles[10] = 6;
        triangles[11] = 4;

        // Left Face
        triangles[12] = 0;
        triangles[13] = 4;
        triangles[14] = 2;
        triangles[15] = 4;
        triangles[16] = 6;
        triangles[17] = 2;

        // Right Face
        triangles[18] = 1;
        triangles[19] = 3;
        triangles[20] = 5;
        triangles[21] = 3;
        triangles[22] = 7;
        triangles[23] = 5;

        // Top Face
        triangles[24] = 2;
        triangles[25] = 6;
        triangles[26] = 3;
        triangles[27] = 6;
        triangles[28] = 7;
        triangles[29] = 3;

        // Bottom Face
        triangles[30] = 0;
        triangles[31] = 1;
        triangles[32] = 4;
        triangles[33] = 1;
        triangles[34] = 5;
        triangles[35] = 4;

        // Update mesh with vertices, triangles and normals
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }


}
