using UI;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    [SerializeField]private float Speed = 4f;
    
    private Camera _cameraMain;
    private Vector3 _spawnPos;
    private Vector3 _stopPos;
    private bool _startAnim;
    
    void Start()
    {
        _cameraMain = Camera.main;
        if (_cameraMain == null)
        {
            Debug.LogError("_cameraMain == null");
            return;
        }
        _stopPos = new Vector3(-3.5f, -3.5f, 0);
        var cameraSize = _cameraMain.ViewportToWorldPoint(new Vector3(0, 0.5f, 0.5f));
        _spawnPos = new Vector3(cameraSize.x - 3f, _stopPos.y, _stopPos.z);
        
        transform.position = _spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (_startAnim) MovePlayer();
    }
    
    private void OnEnable()
    {
        MainMenuController.StartGamePlay += StartPlayer;
    }

    private void OnDisable()
    {
        MainMenuController.StartGamePlay -= StartPlayer;
    }

    private void MovePlayer()
    {
        if (transform.position.x >= _stopPos.x) return;
        var offset = new Vector3(Speed * Time.deltaTime, 0, 0);
        transform.position += offset;
    }
    private void StartPlayer()
    {
        _startAnim = true;
    }
}
