using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Paint))]
public class PaintCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Paint paint = (Paint)target;

        if (GUILayout.Button("Generate Random Color"))
        {
            Undo.RecordObject(paint, "Random Color");
            paint.objectColor = Random.ColorHSV();
        }
        paint.ColorObject();
    }
}
