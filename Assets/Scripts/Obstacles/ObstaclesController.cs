using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField] private float MaxGlobalSpawnInterval = 4f;
    [SerializeField] private float MinGlobalSpawnInterval = 2f;
    
    [Header("Params for RegularObstacles")]
    [SerializeField] private GameObject ObstacleRegularPrefab;
    private GameObject[] _obstaclesRegularObjects;
    private int _idRegularObstacle;
    
    [Header("Params for RotateObstacles")]
    [SerializeField] private GameObject ObstacleRotatePrefab;
    [SerializeField] private int MinRotateSpawnCount = 3;
    private int _maxRotateSpawnCount;
    private int _callNumForRotate;
    private GameObject _obstacleRotateObject;
    
    [Header("Params for LaserObstacles")]
    [SerializeField] private GameObject ObstacleLaserPrefab;
    [SerializeField] private int MinLaserSpawnCount = 15; 
    private int _maxLaserSpawnCount;
    private int _callNumForLaser;
    private GameObject _obstacleLaserObject;
    
    private int _counterForLaser;
    private int _counterForRotate;
    
    private bool _stopSpawn;

    public float SpeedBoost { get; private set; } = 1f;
    
    private void Awake()
    {
        InstantiateObstacles();
        Init();
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopSpawn;
        DistanceTracker.GameSpeedUp += SpeedUp;
        
        var regularWaitTime = Random.Range(MinGlobalSpawnInterval, MaxGlobalSpawnInterval);
        StartCoroutine(WaitNextRegularObstacle(regularWaitTime));
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopSpawn;
        DistanceTracker.GameSpeedUp -= SpeedUp;
    }
    
    private void Init()
    {
        _maxLaserSpawnCount = (int)(MinLaserSpawnCount * 1.5f);
        _maxRotateSpawnCount = (int)(MinRotateSpawnCount * 1.5f);
        _callNumForLaser = Random.Range(MinLaserSpawnCount, _maxLaserSpawnCount);
        _callNumForRotate = Random.Range(MinRotateSpawnCount, _maxRotateSpawnCount);
    }

    private void InstantiateObstacles()
    {
        _obstaclesRegularObjects = InstantiatePrefabsArray(ObstacleRegularPrefab, 2);
        _obstacleLaserObject = Instantiate(ObstacleLaserPrefab, transform);
        _obstacleRotateObject = Instantiate(ObstacleRotatePrefab, transform);
        
        _obstacleLaserObject.SetActive(false);
        _obstacleRotateObject.SetActive(false);
    }
    
    //TODO: Delete this block?
    private GameObject[] InstantiatePrefabsArray(GameObject prefab, int count)
    {
        var prefabsArray = new GameObject[count];
        for (int i = 0; i < prefabsArray.Length; i++)
        {
            prefabsArray[i] = Instantiate(
                prefab, 
                Vector3.zero, 
                Quaternion.identity, 
                transform);
            prefabsArray[i].SetActive(false);
        }
        return prefabsArray;
    }
    
    private void EnableNextObstacle()
    {
        if (_stopSpawn) return;
        
        if (_counterForLaser >= _callNumForLaser)
        {
            _obstacleLaserObject.SetActive(true);
            _callNumForLaser = Random.Range(MinLaserSpawnCount, _maxLaserSpawnCount);
            _counterForLaser = 0;
            var laserWaitTime = Random.Range(MinGlobalSpawnInterval, MaxGlobalSpawnInterval) + 4f;
            StartCoroutine(WaitNextRegularObstacle(laserWaitTime));
            return;
        }

        if (_counterForRotate >= _callNumForRotate)
        {
            _obstacleRotateObject.SetActive(true);
            _callNumForRotate = Random.Range(MinRotateSpawnCount, _maxRotateSpawnCount);
            _counterForRotate = 0;
            
            var rotateWaitTime = Random.Range(MinGlobalSpawnInterval, MaxGlobalSpawnInterval);
            StartCoroutine(WaitNextRegularObstacle(rotateWaitTime));
            return;
        }
        
        _obstaclesRegularObjects[_idRegularObstacle].SetActive(true);
        
        var arrayPassed = _idRegularObstacle == _obstaclesRegularObjects.Length - 1;
        _idRegularObstacle = arrayPassed ? 0 : ++_idRegularObstacle;
        
        _counterForLaser++;
        _counterForRotate++;
        
        var regularWaitTime = Random.Range(MinGlobalSpawnInterval, MaxGlobalSpawnInterval);
        StartCoroutine(WaitNextRegularObstacle(regularWaitTime));
    }
    
    private void SpeedUp(float boost)
    {
        SpeedBoost += boost;
    }

    private void StopSpawn()
    {
        _stopSpawn = true; 
    }
    
    private IEnumerator WaitNextRegularObstacle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (_obstacleLaserObject.activeSelf) _obstacleLaserObject.SetActive(false);
        EnableNextObstacle();
    }
}