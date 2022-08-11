using UnityEngine;

public class Paint : MonoBehaviour
{
    public Color objectColor;
    void Start()
    {
        ColorObject();
    }

    // Update is called once per frame
    public void ColorObject()
    {
        GetComponent<Renderer>().sharedMaterial.color = objectColor;
    }
}
