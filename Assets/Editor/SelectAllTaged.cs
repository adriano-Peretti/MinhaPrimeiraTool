using System.Linq;
using UnityEditor;
using UnityEngine;

public class SelectAllTaged : EditorWindow
{
    GameObject[] gameObjects;

    public string searchTag = "Your tag here";

    string tempString;

    string[] tags;

    Vector2 scrollPosition = Vector2.zero;

    public string log = "status da tag";

    [MenuItem("Tools/Select All Of Tag...")]

    public static void SelectAllTagedWindow()
    {
        var win = GetWindow<SelectAllTaged>();
        GUIContent content = new GUIContent("Select All Tag..");
        win.titleContent = content;
        win.Show();
    }

    private void OnGUI()
    {
        searchTag = EditorGUILayout.TextField("Search Tag", searchTag);
        tags = UnityEditorInternal.InternalEditorUtility.tags;

        if (GUILayout.Button("Select Objects with Tag."))
        {
            Selection.objects = gameObjects;
        }
        ReArrengeFields();
        Repaint();

        try
        {
            char[] tempChars = searchTag.ToString().ToCharArray();
            for (int k = 0; k < tags.Length; k++)
            {
                tempString = tags[k];

                for (int i = 0; i < tempString.Length; i++)
                {
                    for (int j = 0; j < tempChars.Length; j++)      //Go over every character in our selection
                    {
                        if (tempString[i + j].CompareTo(tempChars[j]) != 0)  //if next character is not the character in our selection, go back to the 1st For Loop
                        {
                            break;
                        }

                        if (j == tempChars.Length - 1)       //if every character was correct, We found our selection!!
                        {
                            Debug.Log("Found the word!");
                            tempString = searchTag.ToString();
                            if (tags.Contains<string>(tempString))
                            {
                                SearchTag(tempString);
                            }
                            return;
                        }
                    }
                }
            }
        }
        catch (System.IndexOutOfRangeException ex)
        {
            Debug.Log("Array out bound: " + ex);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Generico: " + ex);
        }
    }

    void SearchTag(string searchTag)
    {
        gameObjects = GameObject.FindGameObjectsWithTag(searchTag);
        Repaint();
    }

    void ReArrengeFields()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        EditorGUILayout.LabelField(log);

        if (gameObjects.Length != 0)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(gameObjects[i].name);
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            log = "Não encontrado nenhum objeto com essa Tag";
            //EditorGUILayout.LabelField("Não encontrado nenhum objeto com essa Tag");
        }

        GUILayout.EndScrollView();
    }
}
