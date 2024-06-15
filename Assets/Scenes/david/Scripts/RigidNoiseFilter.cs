using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    private readonly NoiseSettings _settings;
    private readonly Noise _noise = new Noise();

    public RigidNoiseFilter(NoiseSettings settings)
    {
        _settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        var noiseValue = 0f;
        var frequency = _settings.baseRoughness;
        var amplitude = 1f;
        for (int i = 0; i < _settings.numLayers; i++)
        {
            float v = 1 - Mathf.Abs(_noise.Evaluate(point * frequency + _settings.centre));
            v *= v;
            noiseValue += (v + 1) * .5f * amplitude;
            frequency += _settings.roughness;
            amplitude += _settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - _settings.minValue);
        return noiseValue * _settings.strength;
    }
}