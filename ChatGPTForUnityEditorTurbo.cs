using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(gptTurbo))]
public class ChatGPTForUnityEditorTurbo : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        gptTurbo script = (gptTurbo)target;

        GUILayout.Space(15);

        if(GUILayout.Button("Ask"))
        {
            script.SendRequest();
        }
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Save Script"))
        {
            script.SaveScript();
        }


        if(GUILayout.Button("Clear"))
        {
            script.Clear();
        }

         GUILayout.EndHorizontal();
       
    }
}
