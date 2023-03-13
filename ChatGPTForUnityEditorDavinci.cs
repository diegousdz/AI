using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(davinci003))]
public class ChatGPTForUnityEditorDavinci : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        davinci003 script = (davinci003)target;

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
