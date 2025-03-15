using System;
using UnityEngine;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public static event Action StartGamePlay;
        
        [SerializeField] private float SpeedMove = 0.2f;
        
        private Camera _cameraMain;
        private Vector3 _closePos;
        private bool _menuDisable;
        private bool _gameStarted;
        
        private void Start()
        {
            Init();
        }
        
        private void Update()
        {
            if (!_menuDisable) return;
            CloseMainMenu();
        }

        private void Init()
        {
            _cameraMain = Camera.main;
            if (_cameraMain == null)
            {
                Debug.LogError("_cameraMain == null");
                return;
            }
            _closePos = _cameraMain.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0)) * 2;
        }
        
        public void StartGame()
        {
            _menuDisable = true;
        }

        private void CloseMainMenu()
        {
            transform.position = new Vector3(
                _closePos.x, 
                Mathf.Lerp(transform.position.y, _closePos.y, SpeedMove * Time.deltaTime), 
                0f);
            
            if (Mathf.Abs(_closePos.y - transform.position.y) < 0.1f) gameObject.SetActive(false);
            
            if (_gameStarted) return;
            StartGamePlay?.Invoke();
            _gameStarted = true;
        }
    }
}
