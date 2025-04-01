using UnityEngine;

public abstract class AbstractRareObstacle : MonoBehaviour
{
    private int _minSpawnCount = 5; //TODO: minMax in SO
    private  int _maxSpawnCount;
    private int _currentSpawnCount;
    
    public void SetMinSpawnCount(int value)
    {
        _minSpawnCount = value;
        _maxSpawnCount = (int)(_minSpawnCount * 1.5f);
        _currentSpawnCount = Random.Range(_minSpawnCount, _maxSpawnCount);
    }
    
    public bool ShouldSpawn()
    {
        if (_currentSpawnCount <= 0)
        {
            _currentSpawnCount = Random.Range(_minSpawnCount, _maxSpawnCount);
            return true;
        }
        _currentSpawnCount--;
        return false;
    }
}