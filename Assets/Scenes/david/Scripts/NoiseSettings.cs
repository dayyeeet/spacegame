using System;
using UnityEngine;

[Serializable]
public class NoiseSettings
{
    public float strength = 1;
    [Range(1, 8)]
    public int numLayers = 1;

    public float baseRoughness = 1;
    public float persistence = .5f;
    public float roughness = 2;
    public Vector3 centre;
}
