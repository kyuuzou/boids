using UnityEditor;
using UnityEngine;
 
public class TriangleCreator {

    [MenuItem("GameObject/2D Object/Triangle")]
    public static void Create() {
        TriangleCreator.CreateGameObject();
    }

    private static GameObject CreateGameObject() {
        GameObject triangle = new GameObject("Triangle");
        MeshFilter filter = triangle.AddComponent<MeshFilter>();
        MeshCollider collider = triangle.AddComponent<MeshCollider>();
        triangle.AddComponent<MeshRenderer>();
 
        Mesh mesh = CreateMesh();
        filter.sharedMesh = mesh;
        collider.sharedMesh = mesh;

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Triangle.mesh");
        
        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();        
        AssetDatabase.Refresh();
 
        return triangle;
    }
    
    private static Mesh CreateMesh() {
        Vector3[] vertices = { new (-0.5f, -0.5f), new (0.0f, 0.5f), new (0.5f, -0.5f) };
        Vector2[] uv = { new (0.0f, 0.0f), new (0.5f, 1.0f), new (1.0f, 0.0f) };
        int[] triangles = { 0, 1, 2 };
        
        Mesh mesh = new Mesh { name = "Triangle", vertices = vertices, uv = uv, triangles = triangles };
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        
        return mesh;
    }
}