using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SetCursor))]
public class SetCursorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SetCursor script = (SetCursor)target;
        if (GUILayout.Button("Cambiar cursor"))
        {
            script.Set();
        }
    }
}
