using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestDistanceText : MonoBehaviour
{
    
    private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        var distance = GameDataManager.GetBestDistance();
        _text.text = $"BEST SCORE: {distance} M.";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
