using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeatureGenerator
{
    private FeatureSettings _settings;
    private int _seed;
    public void UpdateSeed(int seed)
    {
        _seed = seed;
    }

    public void UpdateSettings(FeatureSettings settings)
    {
        _settings = settings;
    }

    public void Populate(Planet planet, TerrainFace face)
    {
        Random.InitState(_seed);
        var populationObj = planet.GetObjectByName("features");
        if (populationObj == null)
        {
            populationObj = new GameObject("features");
            populationObj.transform.parent = planet.transform;
            populationObj.transform.position = planet.transform.position;
        }

        var populated = new Dictionary<Vector3, GeneratableFeature>();
        var randomGenerationDict = ToRandomGenerationOrderDict(_settings);
        GenerateFeatureRandomPopulated(randomGenerationDict, face.mesh.vertices, populated, planet, populationObj);

        var noiseGenerationDict = ToNoiseGenerationOrderDict(_settings);
        GenerateFeatureNoisePopulated(noiseGenerationDict, face.mesh.vertices, populated, planet, populationObj);
    }

    private static void GenerateFeatureRandomPopulated(Dictionary<GeneratableFeature[], float> keys, Vector3[] vertices,
        Dictionary<Vector3, GeneratableFeature> populated, Planet planet, GameObject parent)
    {
        var defaultChance = 0.3f;
        foreach (var vertice in vertices)
        {
            var generated = false;
            foreach (var key in keys)
            {
                if(generated) continue;
                var chance = defaultChance * key.Value;
                if (Random.value > chance) continue;
                if(!IsAvailable(vertice, populated)) continue;
                var randomFeature = key.Key[Random.Range(0, key.Key.Length)];
                GenerateFeature(randomFeature, vertice, populated, planet, parent);
                generated = true;
            }
        }
    }

    private static void GenerateFeatureNoisePopulated(Dictionary<GeneratableFeature[], NoiseSettings> keys,
        Vector3[] vertices,
        Dictionary<Vector3, GeneratableFeature> populated, Planet planet, GameObject parent)
    {
        foreach (var vertice in vertices)
        {
            var generated = false;
            foreach (var key in keys)
            {
                if(generated) continue;
                var noise = NoiseFilterFactory.CreateNoiseFilter(key.Value);
                if (noise.Evaluate(vertice) <= key.Value.minValue) continue;
                if (!IsAvailable(vertice, populated)) continue;
                var randomFeature = key.Key[Random.Range(0, key.Key.Length)];
                GenerateFeature(randomFeature, vertice, populated, planet, parent);
                generated = true;
            }
        }
    }

    private static void GenerateFeature(GeneratableFeature feature, Vector3 point,
        Dictionary<Vector3, GeneratableFeature> populated, Planet planet, GameObject parent)
    {
        var obj = GameObject.Instantiate(feature.prefab);
        obj.transform.position = point + planet.transform.position;
        var center = obj.transform.position - planet.transform.position;
        obj.transform.up = center;
        obj.transform.parent = parent.transform;
        if (feature.isCollidable)
        {
            var meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                var newCollider = obj.AddComponent<MeshCollider>();
                newCollider.sharedMesh = meshFilter.sharedMesh;
                obj.layer = 6;
            }
        }
        populated.Add(point, feature);
    }

    private static bool IsAvailable(Vector3 hit, Dictionary<Vector3, GeneratableFeature> populated)
    {
        return !populated.Any(
            entry => (!entry.Value.isGroundCover || entry.Key == hit) && Vector3.Distance(entry.Key, hit) < entry.Value.size);
    }

    private Dictionary<GeneratableFeature[], float> ToRandomGenerationOrderDict(FeatureSettings settings)
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
    
    private Dictionary<GeneratableFeature[], NoiseSettings> ToNoiseGenerationOrderDict(FeatureSettings settings)
    {
        var dict = new Dictionary<GeneratableFeature[], NoiseSettings>();
        if (settings.groundNoise is { enabled: true })
        {
            dict.Add(settings.groundCover, settings.groundNoise);
        }

        if (settings.treeNoise is { enabled: true })
        {
            dict.Add(settings.trees, settings.treeNoise);
        }
        return dict;
    }
}