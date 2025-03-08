using UI;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private GameObject DistanceTracker;
    [SerializeField] private GameObject ObstaclesController;
    [SerializeField] private GameObject GameOverWindow;
    
    private void OnEnable()
    {
        MainMenuController.StartGamePlay += EnableTracker;
        ObstacleTrigger.OnPlayerHit += GameOver;
    }

    private void OnDisable()
    {
        MainMenuController.StartGamePlay -= EnableTracker;
        ObstacleTrigger.OnPlayerHit -= GameOver;
    }

    private void EnableTracker()
    {
        DistanceTracker.SetActive(true);
        ObstaclesController.SetActive(true);
    }

    private void GameOver()
    {
        GameOverWindow.SetActive(true);
        DistanceTracker.SetActive(false);
    }
}
