using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ObstacleRegularPrefab;
    private GameObject[] _regularObstacles;
    
    [SerializeField] private GameObject ObstacleRotatePrefab;
    private GameObject _rotateObstacle;
    
    [SerializeField] private GameObject ObstacleLaserPrefab;
    private GameObject _laserObstacle;

    private void Awake()
    {
        InstantiateObstacles();
    }

    private void InstantiateObstacles()
    {
        _regularObstacles = new[]
        { 
            Instantiate(ObstacleRegularPrefab, transform), 
            Instantiate(ObstacleRegularPrefab, transform) 
        };
        foreach (var regularObstacle in _regularObstacles) 
            regularObstacle.SetActive(false);
        
        _rotateObstacle = Instantiate(ObstacleRotatePrefab, transform);
        _rotateObstacle.SetActive(false);
        
        _laserObstacle = Instantiate(ObstacleLaserPrefab, transform);
        _laserObstacle.SetActive(false);
    }
    
    //GetSet //return objects
    public GameObject[] GetRegularObstacles() => _regularObstacles;
    public GameObject GetRotateObstacle() => _rotateObstacle;
    public GameObject GetLaserObstacle() => _laserObstacle;
}
