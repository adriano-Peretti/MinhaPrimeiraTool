using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


public class FullSceneController : EditorWindow
{
    public static List<String> scenePaths = new List<String>();
    Vector2 scrollPosition = Vector2.zero;
    [MenuItem("Tools/Scene Manager")]
    public static void OpenWindow()
    {
        FullSceneController win = GetWindow<FullSceneController>();
        win.titleContent = new GUIContent("Scene Manager");
        GetScenes();
        win.Show();
    }

    private static void GetScenes()
    {
        scenePaths.Clear();
        string[] guids = AssetDatabase.FindAssets("t:scene", null);
        foreach (string guid in guids)
        {
            scenePaths.Add(AssetDatabase.GUIDToAssetPath(guid));
        }
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        foreach (string scenePath in scenePaths)
        {
            string[] splitedString = scenePath.Split('/');
            if (GUILayout.Button(splitedString[splitedString.Length - 1]))
            {
                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
            }
        }
        GUILayout.EndScrollView();
    }
}
