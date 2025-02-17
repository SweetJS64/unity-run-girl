using UI;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private GameObject DistanceTracker;
    [SerializeField] private GameObject ObstaclesController;
    
    private void OnEnable()
    {
        MainMenuController.StartGamePlay += EnableTracker;
    }

    private void OnDisable()
    {
        MainMenuController.StartGamePlay -= EnableTracker;
    }

    private void EnableTracker()
    {
        DistanceTracker.SetActive(true);
        ObstaclesController.SetActive(true);
    }
}
