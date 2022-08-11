using UnityEngine;

public class teste : MonoBehaviour
{
    [Locked] public int lockedVariable;
    [Slider(0, 5)] public float sliderVariable;
}
