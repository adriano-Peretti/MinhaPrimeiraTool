using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Slider))]
public class SliderVariable : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Slider slider = attribute as Slider;
        property.floatValue = EditorGUI.Slider(position, property.floatValue, slider.min, slider.max);
    }
}
