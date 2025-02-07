using System;
using UnityEngine;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public static event Action StartGamePlay;
        
        [SerializeField] private float SpeedMove = 0.2f;
        
        private Camera _cameraMain;
        private Vector3 _startPos = Vector3.zero;
        private Vector3 _closePos;
        private bool _menuDisable;
        
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
                _closePos.x, Mathf.Lerp(transform.position.y, _closePos.y, SpeedMove * Time.deltaTime), 0f);
            
            StartGamePlay?.Invoke();
        }
    }
}
