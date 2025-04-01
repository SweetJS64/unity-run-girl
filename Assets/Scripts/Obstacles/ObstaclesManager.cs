using System.Collections;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private float MinSpawnInterval = 2f;
    [SerializeField] private float MaxSpawnInterval = 4f;
    [SerializeField] private int RotateMinSpawnIndex = 3;
    [SerializeField] private int LaserMinSpawnIndex = 2;
    //scriptable object

    private ObstaclesSpawner _spawner;
    private AbstractRareObstacle _rotateObstacle;
    private AbstractRareObstacle _laserObstacle;

    private GameObject[] _regularObstacles;
    private int _idRegularObstacle;

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
        LaserGeneratorMover.DisableLaserObstacle += EnableNextObstacle;

        var waitToSpawn = Random.Range(MinSpawnInterval, MaxSpawnInterval);
        StartCoroutine(WaitNextObstacle(waitToSpawn));
    }

    private void OnDisable()
    {
        ObstacleTrigger.OnPlayerHit -= StopSpawn;
        DistanceTracker.GameSpeedUp -= SpeedUp;
        LaserGeneratorMover.DisableLaserObstacle -= EnableNextObstacle;
    }

    private void Init()
    {
        _spawner = GetComponent<ObstaclesSpawner>();
        _regularObstacles = _spawner.GetRegularObstacles();
        _rotateObstacle = _spawner.GetRotateObstacle().GetComponent<AbstractRareObstacle>();
        _rotateObstacle.SetMinSpawnCount(RotateMinSpawnIndex);
        _laserObstacle = _spawner.GetLaserObstacle().GetComponent<AbstractRareObstacle>();
        _laserObstacle.SetMinSpawnCount(LaserMinSpawnIndex);
    }

    private void EnableNextObstacle()
    {
        if (_stopSpawn) return;

        var waitTime = Random.Range(MinSpawnInterval, MaxSpawnInterval);

        if (_rotateObstacle.ShouldSpawn())
        {
            EnableAbstractObstacle(_rotateObstacle, waitTime);
            return;
        }

        if (_laserObstacle.ShouldSpawn())
        {
            EnableAbstractObstacle(_laserObstacle, waitTime);
            StopAllCoroutines();
            return;
        }

        EnableRegularObstacle(waitTime);
    }

    private void EnableRegularObstacle(float waitTime)
    {

        _regularObstacles[_idRegularObstacle].SetActive(true);

        var arrayPassed = _idRegularObstacle == _regularObstacles.Length - 1;
        _idRegularObstacle = arrayPassed ? 0 : ++_idRegularObstacle;
        StartCoroutine(WaitNextObstacle(waitTime));
    }

    private void EnableAbstractObstacle (AbstractRareObstacle obstacle, float waitTime)
    {
        obstacle.gameObject.SetActive(true);
        StartCoroutine(WaitNextObstacle(waitTime));
    }
    
    private void SpeedUp(float boost)
    {
        if(_stopSpawn) return;
        SpeedBoost += boost;
    }

    private void StopSpawn()
    {
        StopAllCoroutines();
        _stopSpawn = true;
    }
    
    private IEnumerator WaitNextObstacle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        EnableNextObstacle();
    }
}