using UnityEditor;
using UnityEditor.SceneManagement;

public class SimpleSceneControler : Editor
{
    [MenuItem("Tools/Scenes/Open Scene 1")]
    static void LoadScene1()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MinhaPrimeiraTool.unity", OpenSceneMode.Single);
    }

    [MenuItem("Tools/Scenes/Open Scene 2")]
    static void LoadScene2()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MinhaSegundaScene.unity", OpenSceneMode.Single);
    }
}
