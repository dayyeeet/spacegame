using UnityEngine;

public class NoiseFilter
{
    private readonly NoiseSettings _settings;
    private readonly Noise _noise = new Noise();

    public NoiseFilter(NoiseSettings settings)
    {
        _settings = settings;
    }
    
    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = _settings.baseRoughness;
        float amplitude = 1;
        for (int i = 0; i < _settings.numLayers; i++)
        {
            float v = _noise.Evaluate(point * frequency + _settings.centre);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= _settings.roughness;
            amplitude *= _settings.persistence;
        }
        return noiseValue * _settings.strength;
    }
}
