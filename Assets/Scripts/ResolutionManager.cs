using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private float BaseCameraWidth = 17.78f;
    
    private float _currentWidth;
    private float _currentHeight;
    
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;  
        _currentHeight = 2f * _camera.orthographicSize;
        _currentWidth = _currentHeight * _camera.aspect;
        
        AdaptToScreen();
    }

    private void Update()
    {
        if (Mathf.Abs(BaseCameraWidth - _currentWidth) > 0.01f)
        {
            AdaptToScreen();
        }
    }

    private void AdaptToScreen()
    {
        var ratio = _currentWidth / BaseCameraWidth;
        transform.localScale = new Vector3(
            transform.localScale.x * ratio,
            transform.localScale.y , 
            1f);
        BaseCameraWidth = _currentWidth;
    }
}