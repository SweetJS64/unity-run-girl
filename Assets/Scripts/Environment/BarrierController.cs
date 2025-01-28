using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private float TransformMove = 1.8f;
    
    private Transform[] _barriersTransformArray;
    private Vector3 _position;
    
    void Start()
    {
        Init();
    }
    
    void Update()
    {
        MoveBarrier();
    }

    private void Init()
    {
        _barriersTransformArray = GetComponentsInChildren<Transform>();
        _position = new Vector3(-TransformMove, 0, 0);
    }

    private void MoveBarrier()
    {
        for (int i = 0; i < _barriersTransformArray.Length; i++)
        {
            _barriersTransformArray[i].position += _position * Time.deltaTime;
        }
    }
}
