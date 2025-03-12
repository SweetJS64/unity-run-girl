using System.Collections;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private float MaxGlobalSpawnInterval = 4f;
    [SerializeField] private float MinGlobalSpawnInterval = 2f;
    [SerializeField] private int MinRotateSpawnCount = 3;
    [SerializeField] private int MinLaserSpawnCount = 10;

    private ObstaclesSpawner _spawner;
    
    private GameObject[] _regularObstacles;
    private int _idRegularObstacle;
    
    private GameObject _rotateObstacle;
    private int _maxRotateSpawnCount;
    private int _callNumForRotate;
    
    private GameObject _laserObstacle;
    private int _maxLaserSpawnCount;
    private int _callNumForLaser;
    
    private int _counterForLaser;
    private int _counterForRotate;
    
    private bool _stopSpawn;

    public float SpeedBoost { get; private set; } = 1f;
    
    private void Awake()
    {
        Init();
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopSpawn;
        DistanceTracker.GameSpeedUp += SpeedUp;
        
        var waitToSpawn = Random.Range(MinGlobalSpawnInterval, MaxGlobalSpawnInterval);
        StartCoroutine(WaitNextRegularObstacle(waitToSpawn));
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopSpawn;
        DistanceTracker.GameSpeedUp -= SpeedUp;
    }
    
    private void Init()
    {
        _spawner = GetComponent<ObstaclesSpawner>();
        _regularObstacles = _spawner.GetRegularObstacles();
        _rotateObstacle = _spawner.GetRotateObstacle();
        _laserObstacle = _spawner.GetLaserObstacle();
        
        _maxLaserSpawnCount = (int)(MinLaserSpawnCount * 1.5f);
        _maxRotateSpawnCount = (int)(MinRotateSpawnCount * 1.5f);
        _callNumForLaser = Random.Range(MinLaserSpawnCount, _maxLaserSpawnCount);
        _callNumForRotate = Random.Range(MinRotateSpawnCount, _maxRotateSpawnCount);
    }
    
    private void EnableNextObstacle()
    {
        if (_stopSpawn) return;
        
        if (_counterForLaser >= _callNumForLaser)
        {
            _laserObstacle.SetActive(true);
            _callNumForLaser = Random.Range(MinLaserSpawnCount, _maxLaserSpawnCount);
            _counterForLaser = 0;
            var laserWaitTime = Random.Range(MinGlobalSpawnInterval, MaxGlobalSpawnInterval) + 4f;
            StartCoroutine(WaitNextRegularObstacle(laserWaitTime));
            return;
        }

        if (_counterForRotate >= _callNumForRotate)
        {
            _rotateObstacle.SetActive(true);
            _callNumForRotate = Random.Range(MinRotateSpawnCount, _maxRotateSpawnCount);
            _counterForRotate = 0;
            
            var rotateWaitTime = Random.Range(MinGlobalSpawnInterval, MaxGlobalSpawnInterval);
            StartCoroutine(WaitNextRegularObstacle(rotateWaitTime));
            return;
        }
        
        _regularObstacles[_idRegularObstacle].SetActive(true);
        
        var arrayPassed = _idRegularObstacle == _regularObstacles.Length - 1;
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
        StopAllCoroutines();
    }
    
    private IEnumerator WaitNextRegularObstacle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (_laserObstacle.activeSelf) _laserObstacle.SetActive(false);
        EnableNextObstacle();
    }
}
