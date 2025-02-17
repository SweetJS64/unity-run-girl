using UnityEngine;
using Random = UnityEngine.Random;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField] private float MaxSpawnInterval = 4f;
    [SerializeField] private float MinSpawnInterval = 2f;
    
    [SerializeField] private GameObject[] ObstaclePrefabs;
    
    private float _spawnInterval;
    private float _spawnTimer;
    
    private int _nextObstacle;
    private int _lastObstacle;
    private int _penultimateObstacle;
    
    private bool _stopSpawn;
    
    private GameObject[] _obstaclesObjects;

    public float SpeedBoost { get; private set; } = 1f;
    private void Awake()
    {
        InstantiatePrefabs();
        Init();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (_stopSpawn) return;
        Timer();
        Spawn();
    }

    private void Init()
    {
        _spawnInterval = MinSpawnInterval;
        _nextObstacle = Random.Range(0, _obstaclesObjects.Length);
        _lastObstacle = _penultimateObstacle = _nextObstacle;
    }
    
    private void InstantiatePrefabs()
    {
        _obstaclesObjects = new GameObject[ObstaclePrefabs.Length];
        for (int i = 0; i < ObstaclePrefabs.Length; i++)
        {
            _obstaclesObjects[i] = Instantiate(
                    ObstaclePrefabs[i], 
                    Vector3.zero, 
                    Quaternion.identity, 
                    transform);
            _obstaclesObjects[i].SetActive(false);
        }
    }
    
    private void Timer()
    {
        _spawnTimer += Time.deltaTime;
    }

    private void Spawn()
    {
        if (_spawnTimer < _spawnInterval) return;
        if (_obstaclesObjects[_nextObstacle].activeInHierarchy)
        {
            NextObstacle();
            return;
        }
        _obstaclesObjects[_nextObstacle].SetActive(true);
        
        _spawnTimer = 0;
        _spawnInterval = Random.Range(MinSpawnInterval, MaxSpawnInterval);
        
        _penultimateObstacle = _lastObstacle;
        _lastObstacle = _nextObstacle;
        
        NextObstacle();
    }

    private void NextObstacle()
    {
        _nextObstacle = Random.Range(0, _obstaclesObjects.Length);
        if (_obstaclesObjects.Length <= 3) return;
        if (_nextObstacle == _lastObstacle || _nextObstacle == _penultimateObstacle) NextObstacle();
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopSpawn;
        DistanceTracker.GameSpeedUp += SpeedUp;
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
