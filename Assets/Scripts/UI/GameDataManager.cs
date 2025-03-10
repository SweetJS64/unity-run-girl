using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private const string BestDistanceKey = "BestDistance";
    
    public static float GetBestDistance()
    {
        return PlayerPrefs.GetInt(BestDistanceKey, 0);
    }

    private void OnEnable()
    {
        DistanceTracker.SetScore += SaveBestDistance;
    }

    private void OnDisable()
    {
        DistanceTracker.SetScore -= SaveBestDistance;
    }
    private static void SaveBestDistance(int distance)
    {
        var currentBest = GetBestDistance();
        if (distance > currentBest)
        {
            PlayerPrefs.SetInt(BestDistanceKey, distance);
            PlayerPrefs.Save();
        }
    }
    
    
}
