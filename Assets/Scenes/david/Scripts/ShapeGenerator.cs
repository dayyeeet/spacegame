using System.Linq;
using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings _settings;
    private INoiseFilter[] _noiseFilters;
    public MinMax elevationMinMax;

    public void UpdateSettings(ShapeSettings shapeSettings)
    {
        _settings = shapeSettings;
        _noiseFilters = _settings.noiseLayers.ToList()
            .Select(NoiseFilterFactory.CreateNoiseFilter).ToArray();
        elevationMinMax = new MinMax();
    }

    public float CalculateUnscaledElevation(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        if (_noiseFilters.Length > 0)
        {
            firstLayerValue = _noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (_settings.noiseLayers[0].enabled)
            {
                elevation = firstLayerValue;
            }
        }

        for (var i = 1; i < _noiseFilters.Length; i++)
        {
            if (_settings.noiseLayers[i].enabled)
            {
                float mask = _settings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1;
                elevation += _noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        return elevation;
    }

    public float GetScaledElevation(float unscaledElevation)
    {
        float elevation = Mathf.Max(0, unscaledElevation);
        elevation = _settings.planetRadius * (1 + elevation);
        elevationMinMax.AddValue(elevation);
        return elevation;
    }
}