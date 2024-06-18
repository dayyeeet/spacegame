using UnityEngine;

[CreateAssetMenu()]
public class FeatureSettings : ScriptableObject
{
    [Range(0f, 1f)] public float rockPercent;
    public GeneratableFeature[] rocks;
    [Range(0f, 1f)] public float flowerPercent;
    public GeneratableFeature[] flowers;
    [Range(0f, 1f)] public float treePercent;
    public GeneratableFeature[] trees;
    [Range(0f, 1f)] public float groundCoverPercent;
    public GeneratableFeature[] groundCover;
}