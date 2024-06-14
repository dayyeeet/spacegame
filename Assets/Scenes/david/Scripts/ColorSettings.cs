using UnityEngine;

[CreateAssetMenu()]
public class ColorSettings : ScriptableObject
{
    //Preparation for more advanced terrain coloring
    public Gradient terrainColor;
    public Color foliageColor;
    public Color rockColor;
}