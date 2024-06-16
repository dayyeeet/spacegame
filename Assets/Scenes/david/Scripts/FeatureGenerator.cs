using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;
using UnityRandom = UnityEngine.Random;

public class FeatureGenerator
{
    private FeatureSettings _settings;
    private int _tries;

    public void UpdateSettings(FeatureSettings settings)
    {
        _settings = settings;
    }

    public void Populate(Planet planet)
    {
        var radius = planet.shapeSettings.planetRadius;
        _tries = (int)planet.shapeSettings.planetRadius * 3 / 2;
        var populated = new Dictionary<Vector3, GeneratableFeature>();
        var generationDict = ToGenerationOrderDict(_settings);
        foreach (var key in generationDict.Keys)
        {
            if(key.Length == 0) continue;
            var localTries = _tries * generationDict[key];
            for (var i = 0; i < localTries; i++)
            {
                var direction = UnityRandom.onUnitSphere * radius * 2;
                var transform = planet.gravityObject.transform;
                Physics.Raycast(transform.position + direction, -direction, out var hit);
                if(!IsAvailable(hit, populated)) continue;
                var feature = key[new Random().Next(key.Length)];
                var obj = GameObject.Instantiate(feature.prefab);
                obj.transform.position = hit.point;
                obj.transform.up = hit.normal;
                populated.Add(hit.point, feature);
            }
        }
    }

    private static bool IsAvailable(RaycastHit hit, Dictionary<Vector3, GeneratableFeature> populated)
    {
        return !populated.Any(entry => !entry.Value.isGroundCover && Vector3.Distance(entry.Key, hit.point) < entry.Value.size);
    }

    private Dictionary<GeneratableFeature[], float> ToGenerationOrderDict(FeatureSettings settings)
    {
        var dict = new Dictionary<GeneratableFeature[], float>
        {
            { settings.rocks, settings.rockPercent },
            { settings.groundCover, settings.groundCoverPercent },
            { settings.trees, settings.treePercent },
            { settings.flowers, settings.flowerPercent }
        };
        return dict;
    }
}