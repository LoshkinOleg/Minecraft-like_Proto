using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*[CustomEditor(typeof(OldChunkGenerator))]
public class OldChunkGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        OldChunkGenerator oldChunkGen = (OldChunkGenerator)target;

        if (DrawDefaultInspector())
        {
            if (oldChunkGen.autoUpdate)
            {
                oldChunkGen.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            oldChunkGen.GenerateMap();
        }
    }
}
*/