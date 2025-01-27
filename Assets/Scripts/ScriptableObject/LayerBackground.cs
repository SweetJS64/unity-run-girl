using UnityEngine;

[CreateAssetMenu(fileName = "LayerDataConfig", menuName = "Configurations/ParallaxConfiguration")]
public class LayerBackground : ScriptableObject
{
    public Sprite SpriteTexture;
    public float Speed = 1;
}