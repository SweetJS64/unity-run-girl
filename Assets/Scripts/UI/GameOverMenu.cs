using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += EnableMenu;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= EnableMenu;
    }

    private void EnableMenu()
    {
        gameObject.SetActive(true);
        Debug.Log("VAR");
    }
}
