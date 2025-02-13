using UnityEngine;
using UnityEngine.UI;

public class BestDistanceText : MonoBehaviour
{
    private Text _text;
    
    void Start()
    {
        _text = GetComponent<Text>();
        var distance = GameDataManager.GetBestDistance();
        _text.text = $"BEST SCORE: {distance} M.";
        
    }

    void Update()
    {
        
    }
}
