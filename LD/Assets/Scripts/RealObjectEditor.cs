using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RealObject))]
public class RealObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RealObject obj = target as RealObject;
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Ghost Object"))
        {
            obj.CreateGhost();
        }
    }
}
