using UnityEngine;

[CreateAssetMenu()]
public class GeneratableFeature : ScriptableObject
{
    public float size;
    public GameObject prefab;
    public bool isGroundCover;
    public bool isCollidable;
}