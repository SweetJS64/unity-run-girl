using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float MaxSpawnInterval;
    [SerializeField] private float MinSpawnInterval = 4f;
    [SerializeField] private Vector3 SpawnPos = new Vector3(10f, 0f, 0f);
    [SerializeField] private GameObject[] ObstaclePrefabs;

    private float _spawnInterval;
    private float _spawnTimer;
    private int _nextObstacle;
    private int _lastObstacle;
    private int _penultimateObstacle;
    private bool _stopSpawn;
    
    private void Start()
    {
        Init();
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
        _nextObstacle = Random.Range(0, ObstaclePrefabs.Length - 1);
    }
    
    private void Timer()
    {
        _spawnTimer += Time.deltaTime;
    }

    private void Spawn()
    {
        if (_spawnTimer < _spawnInterval) return;
        Instantiate(
            ObstaclePrefabs[_nextObstacle], 
            SpawnPos, 
            Quaternion.identity);
        
        _spawnTimer = 0;
        
        _penultimateObstacle = _lastObstacle;
        _lastObstacle = _nextObstacle;
        
        NextObstacle();
    }

    private void NextObstacle()
    {
        if (ObstaclePrefabs.Length < 3) _nextObstacle++;
        _nextObstacle = Random.Range(0, ObstaclePrefabs.Length - 1);
        if (_nextObstacle == _lastObstacle || _nextObstacle == _penultimateObstacle) NextObstacle();
    }
    
    private void OnEnable()
    {
        ObstacleTrigger.OnPlayerHit += StopSpawn;
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopSpawn;
    }

    private void StopSpawn()
    {
        _stopSpawn = true;
    }
}
