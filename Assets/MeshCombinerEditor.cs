#if UNITY_EDITOR
using UnityEngine;
using Assets.Mega.Scripts;
using UnityEditor;


[CustomEditor(typeof(MeshCombiner))]
public class MeshCombinerEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        if(GUILayout.Button("Combine")) {
            MeshCombiner ms = target as MeshCombiner;
                ms.CombineSubMeshes();
        }
        if(GUILayout.Button("Clear")) {
            MeshCombiner ms = target as MeshCombiner;
            ms.Clear();
        }
    }
}
#endif