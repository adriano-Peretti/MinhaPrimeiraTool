using System;
using UnityEditor;
using UnityEngine;

public class RenamerToolWindow : EditorWindow
{
    UnityEngine.Object[] selectedObjects;
    string wantedPrefix = "";
    string wantedName = "";
    string wantedSuffix = "";
    string originalWord = "";
    string newWord = "";
    bool addNumbering;
    string finalName = String.Empty;
    Vector2 scrollPosition = Vector2.zero;
    GUIStyle yellowText = new GUIStyle();
    [MenuItem("Tools/Rename Assets")]
    public static void LaunchRenamer()
    {
        var win = GetWindow<RenamerToolWindow>();
        GUIContent content = new GUIContent("Rename Objects");
        win.titleContent = content;
        win.Show();
    }

    private void OnEnable()
    {
        yellowText.normal.textColor = Color.yellow;
    }

    private void OnGUI()
    {
        selectedObjects = Selection.objects;
        EditorGUILayout.LabelField("Selected: " + selectedObjects.Length.ToString("000"));
        wantedPrefix = EditorGUILayout.TextField("Prefix: ", wantedPrefix);
        wantedName = EditorGUILayout.TextField("Name: ", wantedName);
        wantedSuffix = EditorGUILayout.TextField("Suffix: ", wantedSuffix);

        EditorGUILayout.BeginHorizontal();
        originalWord = EditorGUILayout.TextField("Replace ", originalWord);
        EditorGUILayout.LabelField(" with ", GUILayout.Width(30));
        newWord = EditorGUILayout.TextField(newWord);
        EditorGUILayout.EndHorizontal();

        addNumbering = EditorGUILayout.Toggle("Add Numbering? ", addNumbering);
        if (GUILayout.Button("Rename selected objects."))
        {
            SaveRenames();
        }
        EditorGUILayout.HelpBox("Remember to test after renaming, since some objects in game have their names hardcoded.", MessageType.Warning);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        Repaint();
        EditorGUILayout.LabelField("Preview:");
        PreviewRename();
        GUILayout.EndScrollView();
    }

    void PreviewRename()
    {
        Array.Sort(selectedObjects, delegate (UnityEngine.Object objectA, UnityEngine.Object objectB)
        {
            return objectA.name.CompareTo(objectB.name);
        });

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            string initialName = selectedObjects[i].name;
            finalName = String.Empty;
            if (wantedPrefix != "")
            {
                finalName += wantedPrefix;
            }
            if (wantedName != "")
            {
                finalName = wantedName;
            }
            else
            {
                finalName += selectedObjects[i].name;
                if (originalWord != "" && newWord != "" && selectedObjects[i].name.Contains(originalWord))
                {
                    finalName = finalName.Replace(originalWord, newWord);
                }
            }
            if (wantedSuffix != "")
            {
                finalName = wantedSuffix;
            }
            if (addNumbering == true)
            {
                finalName += i.ToString("_00");
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(initialName, GUILayout.Width(300));
            EditorGUILayout.LabelField(" ==> ", yellowText, GUILayout.Width(40));
            EditorGUILayout.LabelField(finalName);
            EditorGUILayout.EndHorizontal();
        }
    }

    void SaveRenames()
    {
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            string initialName = selectedObjects[i].name;
            finalName = String.Empty;
            if (wantedPrefix != "")
            {
                finalName += wantedPrefix;
            }
            if (wantedName != "")
            {
                finalName = wantedName;
            }
            else
            {
                finalName += selectedObjects[i].name;
                if (originalWord != "" && newWord != "" && selectedObjects[i].name.Contains(originalWord))
                {
                    finalName = finalName.Replace(originalWord, newWord);
                }
            }
            if (wantedSuffix != "")
            {
                finalName = wantedSuffix;
            }
            if (addNumbering == true)
            {
                finalName += i.ToString("_00");
            }
            if (selectedObjects[i].name.ToUpper() == finalName.ToUpper())
            {
                Debug.Log("Error on Item " + selectedObjects[i].name + " you can't only change the capitalization in the name of an Item.");
            }
            else
            {
                selectedObjects[i].name = finalName;
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(selectedObjects[i]), finalName);
            }
        }
    }
}
