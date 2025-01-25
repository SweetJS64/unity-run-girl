using UnityEngine;

public class TargetFps : MonoBehaviour
{
    [SerializeField] private int Target = 60;

    void Awake()
    {
        Application.targetFrameRate = Target;
    }

    void Update()
    {
        if (Application.targetFrameRate != Target)
            Application.targetFrameRate = Target;
    }
}