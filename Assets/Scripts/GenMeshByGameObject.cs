using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GenMeshByGameObject : MonoBehaviour
{
    public Material mat;
    private int genCount = 3;
    private void Awake()
    {
        FullFill(genCount);
    }
    private void OnEnable()
    {
        GetComponent<MeshRenderer>().sharedMaterial = mat;
    }
    public void FullFill()
    {
        FullFill(genCount);
    }
    public void FullFill(int totalCount)
    {
        int count = totalCount - transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject gen = new GameObject();
            gen.name = "mesh" + (transform.childCount + 1);
            gen.transform.SetParent(transform, false);
        }
    }

    void OnDrawGizmos()
    {
        MeshFilter filter = this.GetComponent<MeshFilter>();
        MeshRenderer render = this.GetComponent<MeshRenderer>();
        Mesh mesh = filter.sharedMesh;
        int ts_len = transform.childCount;
        Vector3[] vers = new Vector3[ts_len];

        for (int i = 0; i < ts_len; i++)
        {
            vers[i] = transform.GetChild(i).localPosition;
        }

        var tris = new int[] { 0, 1, 2, 2, 0, 1 };
        // mesh.Clear();
        mesh.vertices = vers;
        mesh.triangles = tris;
        // Recalculate
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        filter.sharedMesh = mesh;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GenMeshByGameObject))]
public class GenMeshByGameObjectInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Gen"))
        {
            GenMeshByGameObject _t = target as GenMeshByGameObject;
            _t.FullFill();

        }
    }
}
#endif