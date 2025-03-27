using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProcGenManager))]
public class ProcGenManagerEditor : Editor
{
    void Start()
    {
        ProcGenManager targetManager = serializedObject.targetObject as ProcGenManager;
        targetManager.RegenerateTextures();
       // ProcGenManager targetManager = serializedObject.targetObject as ProcGenManager;
        targetManager.RegenerateWorld();
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate Textures"))
        {
            ProcGenManager targetManager = serializedObject.targetObject as ProcGenManager;
            targetManager.RegenerateTextures();
        }

        if (GUILayout.Button("Regenerate World"))
        {
            ProcGenManager targetManager = serializedObject.targetObject as ProcGenManager;
            targetManager.RegenerateWorld();
        }
    }
}
