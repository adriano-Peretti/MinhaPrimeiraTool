using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelCreator : EditorWindow
{
    string levelObjectName = "";
    Object levelImage = null;
    int quantityOfObjects;
    List<Object> objects = new List<Object>();
    List<Color> colors = new List<Color>();
    Vector2 scrollPosition;

    [MenuItem("Tools/Level Creator")]
    public static void Launch()
    {
        GetWindow<LevelCreator>().titleContent = new GUIContent("Level Creator", "This tool can create levels based on a .jpeg");
        GetWindow<LevelCreator>().Show();
    }

    private void OnGUI()
    {
        levelObjectName = EditorGUILayout.TextField("Level name: ", levelObjectName);
        levelImage = EditorGUILayout.ObjectField("Image file: ", levelImage, typeof(Texture2D), false);
        quantityOfObjects = EditorGUILayout.IntField("Quantity of objects: ", quantityOfObjects);


        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < quantityOfObjects; i++)
        {
            if (colors.Count > 1)
            {
                objects[i] = EditorGUILayout.ObjectField("Object " + (i + 1) + " :", objects[i], typeof(GameObject), false);
                colors[i] = EditorGUILayout.ColorField("Color " + (i + 1) + " :", colors[i]);
            }
            else
            {
                objects.Add(null);
                colors.Add(Color.white);
            }
        }
        GUILayout.EndScrollView();

        if (GUILayout.Button("Generate Level."))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        Texture2D image = (Texture2D)levelImage;
        GameObject nullParent = new GameObject(levelObjectName);

        for (int x = 0; x < image.width; x++)
        {
            for (int y = 0; y < image.height; y++)
            {
                Color pixelColor = image.GetPixel(x, y);
                Debug.Log(pixelColor);
                for (int i = 0; i < quantityOfObjects; i++)
                {
                    if (pixelColor == colors[i])
                    {
                        Instantiate(objects[i], new Vector3(x, y, 0), Quaternion.identity, nullParent.transform);
                    }
                }
            }
        }

    }


}
