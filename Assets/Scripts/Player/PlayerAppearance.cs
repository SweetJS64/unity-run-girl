using UI;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    [SerializeField] private float AppearanceSpeed = 5f;
    private Camera _cameraMain;
    private Vector3 _spawnPos;
    private Vector3 _stopPos;
    private bool _canMove;
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (_canMove) MovePlayer();
    }
    
    private void OnEnable()
    {
        MainMenuController.StartGamePlay += StartMove;
    }

    private void OnDisable()
    {
        MainMenuController.StartGamePlay -= StartMove;
    }

    private void Init()
    {
        _stopPos = new Vector3(-4f, -3.5f, 0);
        _spawnPos = new Vector3(BordersData.Instance.MinX - 1f, _stopPos.y, 0);
        transform.position = _spawnPos;
    }
    
    private void MovePlayer()
    {
        var newX = Mathf.MoveTowards(transform.position.x, _stopPos.x, AppearanceSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, 0f);
        if (transform.position.x >= _stopPos.x) _canMove = false;
    }
    private void StartMove()
    {
        _canMove = true;
    }
}
