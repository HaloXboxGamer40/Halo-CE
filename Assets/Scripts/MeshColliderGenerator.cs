using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class MeshColliderGenerator : MonoBehaviour
{
    [ContextMenu("Generate Colliders")]
    public void GenerateColliders()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshCollider collider = meshFilter.gameObject.GetComponent<MeshCollider>();
            if (collider == null)
            {
                collider = meshFilter.gameObject.AddComponent<MeshCollider>();
            }
            collider.sharedMesh = meshFilter.sharedMesh;
        }
        Debug.Log("Mesh Colliders generated successfully.");
    }
}

[CustomEditor(typeof(MeshColliderGenerator))]
public class MeshColliderGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshColliderGenerator script = (MeshColliderGenerator)target;
        if (GUILayout.Button("Generate Colliders"))
        {
            script.GenerateColliders();
        }
    }
}
