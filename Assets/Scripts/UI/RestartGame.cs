using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        var scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
