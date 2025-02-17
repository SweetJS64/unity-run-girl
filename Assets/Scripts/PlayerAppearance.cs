using UI;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    private Camera _cameraMain;
    private Vector3 _spawnPos;
    private Vector3 _stopPos;
    private bool _startMove;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _cameraMain = Camera.main;
        if (_cameraMain == null)
        {
            Debug.LogError("_cameraMain == null");
            return;
        }
        _stopPos = new Vector3(-4f, -3.5f, 0);
        var cameraSize = _cameraMain.ViewportToWorldPoint(new Vector3(0, 0.5f, 0.5f));
        _spawnPos = new Vector3(cameraSize.x - 1f, _stopPos.y, _stopPos.z);
        
        transform.position = _spawnPos;
    } 

    private void Update()
    {
        if (_startMove) MovePlayer();
    }
    
    private void OnEnable()
    {
        MainMenuController.StartGamePlay += StartMove;
    }

    private void OnDisable()
    {
        MainMenuController.StartGamePlay -= StartMove;
    }

    private void MovePlayer()
    {
        var offset = new Vector3(
            Mathf.Lerp(transform.position.x, _stopPos.x, 0.01f), 
            transform.position.y, 
            0f);
        transform.position = offset;
        if ((int)transform.position.x >= _stopPos.x) _startMove = false;
    }
    private void StartMove()
    {
        _startMove = true;
    }
}
