using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(Locked))]
public class LockedDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, true);
    }
}
