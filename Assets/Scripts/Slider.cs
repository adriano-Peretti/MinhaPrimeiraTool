using UnityEngine;

public class Slider : PropertyAttribute
{
    public float min;
    public float max;
    public Slider(float min_, float max_)
    {
        min = min_;
        max = max_;
    }
}
