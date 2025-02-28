using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField] private float MaxGlobalSpawnInterval = 4f; //THIS Time
    [SerializeField] private float MinGlobalSpawnInterval = 2f;
    
    [SerializeField] private GameObject ObstacleRegularPrefab;
    private GameObject[] _obstaclesRegularObjects;
    private int _obstacleRegularLenghtArray = 3;
    private int _idRegularObstacle;
    
    [SerializeField] private GameObject ObstacleLaserPrefab;
    [SerializeField] private int MinLaserSpawnInterval = 15; // This spawn counter
    private int _maxLaserSpawnCount;
    private int _callNumForLaser;
    private GameObject[] _obstaclesLaserObjects;
    private int _obstacleLaserLenghtArray = 2;
    
    [SerializeField] private GameObject ObstacleRotatePrefab;
    [SerializeField] private int MinRotateSpawnInterval = 3;
    private int _maxRotateSpawnCount;
    private int _callNumForRotate;
    private GameObject _obstacleRotateObject;

    private int _counterForLaser;
    private int _counterForRotate;
    
    //private float _spawnTimer;
    //private int _nextObstacle;
    //private int _lastObstacle;
    //private int _penultimateObstacle;
    
    private bool _stopSpawn;

    public float SpeedBoost { get; private set; } = 1f;
    
    private void Awake()
    {
        InstantiateObstacles();
        Init();
    }

    private void Update()
    {
        if (_stopSpawn) return;
    }

    private void InstantiateObstacles()
    {
        _obstaclesRegularObjects = InstantiatePrefabsArray(ObstacleRegularPrefab, _obstacleRegularLenghtArray);
        _obstaclesLaserObjects = InstantiatePrefabsArray(ObstacleLaserPrefab, _obstacleLaserLenghtArray);
        _obstacleRotateObject = Instantiate(
            ObstacleRotatePrefab, 
            Vector3.zero, 
            Quaternion.identity, 
            transform);
        _obstacleRotateObject.SetActive(false);
    }

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
    
    private void Init()
    {
        _maxLaserSpawnCount = (int)(MinLaserSpawnInterval * 1.5f);
        _maxRotateSpawnCount = (int)(MinRotateSpawnInterval * 1.5f);
        _callNumForLaser = Random.Range(MinLaserSpawnInterval, _maxLaserSpawnCount);
        _callNumForRotate = Random.Range(MinRotateSpawnInterval, _maxRotateSpawnCount);
    }

    IEnumerator WaitEndSound()
    {
        yield return new WaitForSeconds(Random.Range(MinGlobalSpawnInterval, MaxGlobalSpawnInterval));
        EnableNextObstacle();
    }

    private void EnableNextObstacle()
    {
        if (_stopSpawn)
        {
            StartCoroutine(WaitEndSound());
            return;
        }
        
        if (_counterForLaser >= _callNumForLaser)
        {
            Debug.Log("LaserEnable");
            _callNumForLaser = Random.Range(MinLaserSpawnInterval, _maxLaserSpawnCount);
            _counterForLaser = 0;
            return;
        }

        if (_counterForRotate >= _callNumForRotate)
        {
            _obstacleRotateObject.SetActive(true);
            _callNumForRotate = Random.Range(MinRotateSpawnInterval, _maxRotateSpawnCount);
            _counterForRotate = 0;
            StartCoroutine(WaitEndSound());
            return;
        }
        
        _obstaclesRegularObjects[_idRegularObstacle].SetActive(true);
        _counterForLaser++;
        _counterForRotate++;
        StartCoroutine(WaitEndSound());
    }

    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopSpawn;
        DistanceTracker.GameSpeedUp += SpeedUp;
        
        StartCoroutine(WaitEndSound());
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopSpawn;
        DistanceTracker.GameSpeedUp -= SpeedUp;
    }
    
    private void SpeedUp(float boost)
    {
        SpeedBoost += boost;
    }

    private void StopSpawn()
    {
        _stopSpawn = true; 
    }
}
