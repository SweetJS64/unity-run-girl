using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Pause");
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Resume");
    }
}