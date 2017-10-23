#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AddArrow))]
public class ObjectBuilderEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        AddArrow myScript = (AddArrow) target;
        if(GUILayout.Button("Build Object")) {
            myScript.AddArrow3();
        }

    }
}

#endif
