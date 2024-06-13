using UnityEngine;

public class ShapeGenerator
{
    private readonly ShapeSettings _settings;
    private NoiseFilter _noiseFilter;

    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        _settings = shapeSettings;
        _noiseFilter = new NoiseFilter(_settings.noiseSettings);
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = _noiseFilter.Evaluate(pointOnUnitSphere);
        return pointOnUnitSphere * _settings.planetRadius * (1 + elevation);
    }
}
