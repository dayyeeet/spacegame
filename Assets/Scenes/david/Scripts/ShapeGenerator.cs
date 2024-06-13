using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings _settings;

    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        _settings = shapeSettings;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        return pointOnUnitSphere * _settings.planetRadius;
    }
}
