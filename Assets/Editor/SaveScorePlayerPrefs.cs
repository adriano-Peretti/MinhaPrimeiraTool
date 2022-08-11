using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SaveScorePlayerPrefs : EditorWindow
{
    List<string> players = new List<string>();
    string playerName = "";
    int scoreValue;
    Vector2 scrollPosition = Vector2.zero;


    [MenuItem("Tools/Save Score")]
    public static void SaveScoreWindow()
    {
        var win = GetWindow<SaveScorePlayerPrefs>();
        GUIContent content = new GUIContent("Save Score");
        win.titleContent = content;
        win.Show();
    }

    private void OnGUI()
    {
        playerName = EditorGUILayout.TextField("Player Name", playerName);
        scoreValue = EditorGUILayout.IntField("Score: ", scoreValue);

        if (GUILayout.Button("Save Score."))
        {
            SaveScore(playerName, scoreValue);
        }

        if (GUILayout.Button("Reset Scores."))
        {
            ResetScore();
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        Repaint();
        EditorGUILayout.LabelField("Saved Scores:");

        int i = 0;
        foreach (var player in players)
        {
            int score = GetScore(player);
            i++;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Player " + i, player);
            EditorGUILayout.LabelField("Score ", score.ToString());
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }

    void SaveScore(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
        players.Add(KeyName);
    }

    public int GetScore(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

    void ResetScore()
    {
        players.Clear();
        PlayerPrefs.DeleteAll();
    }

}
