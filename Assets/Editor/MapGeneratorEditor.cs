using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MRMapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MRMapGenerator mrMapGen = (MRMapGenerator) target;

        if (DrawDefaultInspector())
        {
            if (mrMapGen.autoUpdate)
            {
                mrMapGen.DrawMapInEditor();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mrMapGen.DrawMapInEditor();
        }
    }
}
